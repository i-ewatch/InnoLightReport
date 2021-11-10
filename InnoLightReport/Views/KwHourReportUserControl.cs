using Dapper;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using Serilog;
using SunnineReport.Configuration;
using SunnineReport.Methods;
using SunnineReport.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace InnoLightReport.Views
{
    public partial class KwHourReportUserControl : Field4UserControl
    {
        #region 基本資訊
        /// <summary>
        /// 跳出迴圈旗標
        /// </summary>
        private bool BreakFlag { get; set; }
        /// <summary>
        /// 目前查詢設備編號
        /// </summary>
        private int _Index { get; set; } = 0;
        /// <summary>
        /// 目前查詢設備編號
        /// </summary>
        private int Index
        {
            get { return _Index; }
            set
            {
                if (value != _Index)
                {
                    _Index = value;
                    BreakFlag = true;
                }
            }
        }
        /// <summary>
        /// 電表數量
        /// </summary>
        private int ElectricIndex { get; set; }
        /// <summary>
        /// 位置數量
        /// </summary>
        private int DiskBoxe { get; set; }
        /// <summary>
        /// 感測器名稱
        /// </summary>
        private List<string> ElectricStr { get; set; } = new List<string>();
        /// <summary>
        /// 位置名稱
        /// </summary>
        private List<string> DiskBoxeStr { get; set; } = new List<string>();
        /// <summary>
        /// 區域名稱
        /// </summary>
        private List<string> AreaStr { get; set; } = new List<string>();
        #endregion
        public KwHourReportUserControl(MssqlMethod method, DeviceSetting device)
        {
            InitializeComponent();
            DeviceSetting = device;
            MssqlMethod = method;
            #region 設備下拉選單
            if (DeviceSetting != null)
            {
                foreach (var Systemitem in DeviceSetting.KwNames)
                {
                    DevicecheckedComboBoxEdit.Properties.Items.Add(Systemitem.Name, false);
                }
            }
            #endregion
            #region 查詢按鈕
            SearchsimpleButton.Click += (s, e) =>
            {
                if (Convert.ToDateTime(StartdateEdit.EditValue) <= Convert.ToDateTime(EnddateEdit.EditValue))
                {
                    handle = SplashScreenManager.ShowOverlayForm(FindForm());
                    AreaStr = new List<string>();
                    DiskBoxeStr = new List<string>();
                    ElectricStr = new List<string>();
                    Index = 0;
                    DateTime StartTime = Convert.ToDateTime(Convert.ToDateTime(StartdateEdit.EditValue).ToString("yyyy/MM/dd 00:00:00"));
                    DateTime EndTime = Convert.ToDateTime(Convert.ToDateTime(EnddateEdit.EditValue).ToString("yyyy/MM/dd 23:59:59"));
                    string[] Device = DevicecheckedComboBoxEdit.Text.Split(',');
                    DataTable dataTable = new DataTable();
                    string sql = $"DECLARE @regTIme DATETIME, @startTime DATETIME, @endTime DATETIME " +
                                 $"SET @startTime ='{StartTime.ToString("yyyy/MM/dd HH:mm:ss")}' " +
                                 $"SET @endTime = '{EndTime.ToString("yyyy/MM/dd HH:mm:ss")}' " +
                                 $"SET @regTIme = @startTime DECLARE @mainTemp AS TABLE (ttime CHAR(14),ttimen DATETIME) " +
                                 $"WHILE @regTIme <= @endTime " +
                                 $"BEGIN " +
                                 $"INSERT INTO @mainTemp(ttime, ttimen) VALUES (FORMAT(@regTIme, N'yyyyMMddHHmmss'), @regTIme) " +
                                 $"SET @regTIme = DATEADD(hour,1,@regTIme) " +
                                 $"END ";
                    for (int i = 0; i < Device.Length; i++)
                    {
                        foreach (var Areaitem in DeviceSetting.KwNames)
                        {
                            if (Areaitem.Name == Device[i].Trim())
                            {
                                foreach (var DiskBoxeitem in Areaitem.DiskBoxes)
                                {
                                    foreach (var item in DiskBoxeitem.DeviceName)
                                    {
                                        sql = Select_Current(Index, sql, item, StartTime, EndTime);
                                        Index++;
                                        ElectricStr.Add(item.Name);
                                        var DiskBoxeData = DiskBoxeStr.SingleOrDefault(g => g == DiskBoxeitem.Name);
                                        if (DiskBoxeData == null)
                                        {
                                            DiskBoxeStr.Add(DiskBoxeitem.Name);
                                        }
                                        var AreaData = AreaStr.SingleOrDefault(g => g == Areaitem.Name);
                                        if (AreaData == null)
                                        {
                                            AreaStr.Add(Areaitem.Name);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    sql = SelectFunction(Index, sql);
                    try
                    {
                        using (var con = new SqlConnection(MssqlMethod.scsb.ConnectionString))
                        {
                            var data = con.ExecuteReader(sql);
                            dataTable.Load(data);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "查詢小時累積量報表錯誤");
                        CloseProgressPanel(handle);
                    }
                    BandedGridView view = new BandedGridView(CurrentgridControl);
                    view.MinBandPanelRowCount = 1; // Band欄位高度
                                                   //view.OptionsPrint.AutoWidth = false;//報表匯出不自動縮放長度
                                                   //view.OptionsPrint.PrintHeader = false;//不顯示Columns標頭
                                                   //view.Appearance.Row.FontSizeDelta = 9; //數值大小
                    view.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;//Colum 自動調整高度
                    view.OptionsBehavior.AutoPopulateColumns = false;//不建立沒有Colum相同名稱
                    view.OptionsView.ShowGroupPanel = false;//不顯示Drag
                    view.OptionsView.ColumnAutoWidth = false;//不自動縮放長度
                    view.OptionsCustomization.AllowFilter = false;//取消查詢
                    view.OptionsCustomization.AllowSort = false;//取消排版
                    view.OptionsBehavior.Editable = false;//Cell不可編輯
                    view.OptionsSelection.EnableAppearanceFocusedCell = false;
                    view.OptionsView.ShowColumnHeaders = false;//隱藏CoumnHeader
                    view.OptionsCustomization.AllowBandMoving = false;//GridBand 不可移動

                    GridBand gridtime = new GridBand() { Caption = "時間" }; //建立&宣告Band (時間)
                    gridtime.AppearanceHeader.Options.UseTextOptions = true;
                    gridtime.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;//文字置中
                    GridBand[] AreagridBand = new GridBand[AreaStr.Count];
                    GridBand[] DiskBoxeBand = new GridBand[DiskBoxeStr.Count];
                    GridBand[] ElectricBand = new GridBand[ElectricStr.Count];
                    GridBand[] G_MingridColumn = new GridBand[ElectricStr.Count];//最小值
                    GridBand[] G_MaxgridColumn = new GridBand[ElectricStr.Count];//最大值
                    GridBand[] G_AvggridColumn = new GridBand[ElectricStr.Count];//平均值
                    BandedGridColumn[] MingridColumn = new BandedGridColumn[ElectricStr.Count];//溫度最小值
                    BandedGridColumn[] MaxgridColumn = new BandedGridColumn[ElectricStr.Count];//溫度最大值
                    BandedGridColumn[] AvggridColumn = new BandedGridColumn[ElectricStr.Count];//溫度平均值
                    BandedGridColumn coltime = new BandedGridColumn() { Caption = "時間", FieldName = "Time", Visible = true, Width = 100 }; //建立Colum (時間)
                    for (int i = 0; i < AreaStr.Count; i++)
                    {
                        AreagridBand[i] = new GridBand() { Caption = AreaStr[i] };
                    }
                    for (int i = 0; i < DiskBoxeStr.Count; i++)
                    {
                        DiskBoxeBand[i] = new GridBand() { Caption = DiskBoxeStr[i], Width = 200 };
                    }
                    for (int i = 0; i < ElectricStr.Count; i++)
                    {
                        ElectricBand[i] = new GridBand() { Caption = ElectricStr[i] };
                        G_MingridColumn[i] = new GridBand() { Caption = "最小值" };
                        G_MaxgridColumn[i] = new GridBand() { Caption = "最大值" };
                        G_AvggridColumn[i] = new GridBand() { Caption = "平均值" };
                        MingridColumn[i] = new BandedGridColumn() { Caption = "最小值", FieldName = $"Kwh{i}Min", Visible = true, Width = 200 };
                        MaxgridColumn[i] = new BandedGridColumn() { Caption = "最大值", FieldName = $"Kwh{i}Max", Visible = true, Width = 200 };
                        AvggridColumn[i] = new BandedGridColumn() { Caption = "平均值", FieldName = $"Kwh{i}Avg", Visible = true, Width = 200 };
                    }
                    view.Bands.Add(gridtime);
                    view.Bands.AddRange(AreagridBand);
                    CurrentgridControl.DataSource = dataTable;
                    CurrentgridControl.ViewCollection.Add(view);
                    CurrentgridControl.MainView = view;

                    ElectricIndex = 0;
                    DiskBoxe = 0;
                    for (int i = 0; i < AreaStr.Count; i++)
                    {
                        var Areadata = DeviceSetting.KwNames.SingleOrDefault(g => g.Name == AreaStr[i]);
                        for (int Index = 0; Index < Areadata.DiskBoxes.Count; Index++)
                        {
                            AreagridBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;//文字置中
                            AreagridBand[i].Children.Add(DiskBoxeBand[DiskBoxe]);
                            DiskBoxe++;
                        }
                    }
                    for (int i = 0; i < AreaStr.Count; i++)
                    {
                        for (int DisBoxIndex = 0; DisBoxIndex < DiskBoxeStr.Count; DisBoxIndex++)
                        {
                            foreach (var item in DeviceSetting.KwNames)
                            {
                                if (item.Name == AreaStr[i])
                                {
                                    foreach (var DiskBoxesitem in item.DiskBoxes)
                                    {
                                        if (DiskBoxesitem.Name == DiskBoxeStr[DisBoxIndex])
                                        {
                                            for (int Index = 0; Index < DiskBoxesitem.DeviceName.Count; Index++)
                                            {
                                                DiskBoxeBand[DisBoxIndex].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;//文字置中
                                                DiskBoxeBand[DisBoxIndex].Children.Add(ElectricBand[ElectricIndex]);
                                                ElectricIndex++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    for (int i = 0; i < ElectricStr.Count; i++)
                    {
                        ElectricBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;//文字置中
                        ElectricBand[i].Children.Add(G_MingridColumn[i]);
                        ElectricBand[i].Children.Add(G_MaxgridColumn[i]);
                        ElectricBand[i].Children.Add(G_AvggridColumn[i]);
                        G_MingridColumn[i].Columns.Add(MingridColumn[i]);
                        G_MaxgridColumn[i].Columns.Add(MaxgridColumn[i]);
                        G_AvggridColumn[i].Columns.Add(AvggridColumn[i]);
                        MingridColumn[i].AppearanceCell.Options.UseTextOptions = true;//啟用文字控制
                        MingridColumn[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;//文字靠左
                        MaxgridColumn[i].AppearanceCell.Options.UseTextOptions = true;//啟用文字控制
                        MaxgridColumn[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;//文字靠左
                        AvggridColumn[i].AppearanceCell.Options.UseTextOptions = true;//啟用文字控制
                        AvggridColumn[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;//文字靠左
                    }
                    gridtime.Columns.Add(coltime);
                    view.OptionsCustomization.AllowChangeColumnParent = false;
                    CloseProgressPanel(handle);
                }
                else
                {
                    FlyoutAction action = new FlyoutAction();
                    action.Caption = "時間查詢錯誤";
                    action.Description = "請設定好時間條件再進行查詢";
                    action.Commands.Add(FlyoutCommand.OK);
                    FlyoutDialog.Show(FindForm(), action);
                }
            };
            #endregion
            #region 匯出按鈕
            ExportsimpleButton.Click += (s, e) =>
            {
                if (CurrentgridControl.DataSource != null)
                {
                    XlsxExportOptionsEx options = new XlsxExportOptionsEx();
                    options.ShowColumnHeaders = DefaultBoolean.False;
                    //options.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Xlsx|*xlsx";
                    saveFileDialog.Title = "Export Data";
                    saveFileDialog.DefaultExt = ".xlsx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        CurrentgridView.ExportToXlsx($"{saveFileDialog.FileName}", options);
                    }
                }
                else
                {
                    FlyoutAction action = new FlyoutAction();
                    action.Caption = "匯出報表錯誤";
                    action.Description = "請查詢報表再進行匯出動作";
                    action.Commands.Add(FlyoutCommand.OK);
                    FlyoutDialog.Show(FindForm(), action);
                }
            };
            #endregion
        }
        private string Select_Current(int i, string sql, DeviceName item, DateTime StartTime, DateTime EndTime)
        {
            sql += $"DECLARE @Kwh{i} AS TABLE ([Time] nvarchar(13), [Name] nvarchar(50), [Min] DECIMAL(18,2), [Max] DECIMAL(18,2), [Avg] DECIMAL(18,2))  " +
                   $"INSERT INTO @Kwh{i}  ([Time], [Name],[Min],[Max],[Avg]) " +
                   $"SELECT  convert(varchar(13),[Timestamp], 120)AS[Time], MAX(Name)AS[Name],MIN(CAST([Value] AS DECIMAL(18,2)))AS[Min],MAX(CAST([Value] AS DECIMAL(18,2)))AS[Max],AVG(CAST([Value] AS DECIMAL(18,2)))AS [Avg] FROM [dbo].[Table_KW] " +
                   $"WHERE NAME = '{item.TagName}' " +
                   $"AND [Timestamp] >= '{StartTime.ToString("yyyy/MM/dd HH:mm:ss")}' AND [Timestamp] <= '{EndTime.ToString("yyyy/MM/dd HH:mm:ss") }' " +
                   $"GROUP BY convert(varchar(13),[Timestamp],120) " +
                   $"ORDER BY convert(varchar(13),[Timestamp],120) ";
            return sql;
        }
        private string SelectFunction(int Index, string sql)
        {
            sql += $" SELECT convert(VARCHAR(13),mainT.ttimen,120) AS [Time]";
            for (int i = 0; i < Index; i++)
            {
                sql += $" ,Kwh{i}.[Min] AS [Kwh{i}Min]";
                sql += $" ,Kwh{i}.[Max] AS [Kwh{i}Max]";
                sql += $" ,Kwh{i}.[Avg] AS [Kwh{i}Avg]";
            }
            sql += " FROM @mainTemp AS mainT";
            for (int i = 0; i < Index; i++)
            {
                sql += $" LEFT JOIN @Kwh{i} AS Kwh{i} ON  convert(varchar(13),mainT.[ttimen],120) = Kwh{i}.[Time]";
            }
            return sql;
        }
    }
}
