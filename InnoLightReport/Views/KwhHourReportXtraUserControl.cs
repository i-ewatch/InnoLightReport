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
    public partial class KwhHourReportXtraUserControl : Field4UserControl
    {
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
        public KwhHourReportXtraUserControl(MssqlMethod method, DeviceSetting device)
        {
            InitializeComponent();

            DeviceSetting = device;
            MssqlMethod = method;
            #region 設備下拉選單
            if (DeviceSetting != null)
            {
                foreach (var Systemitem in DeviceSetting.ElectricNames)
                {
                    DevicecheckedComboBoxEdit.Properties.Items.Add(Systemitem.Name, false);
                }
            }
            #endregion
            #region 查詢按鈕
            SearchsimpleButton.Click += (s, e) =>
            {
                handle = SplashScreenManager.ShowOverlayForm(FindForm());
                AreaStr = new List<string>();
                DiskBoxeStr = new List<string>();
                ElectricStr = new List<string>();
                Index = 0;
                DateTime StartTime = Convert.ToDateTime(Convert.ToDateTime(StartdateEdit.EditValue).ToString("yyyy/MM/dd 00:00:00"));
                DateTime EndTime = Convert.ToDateTime(Convert.ToDateTime(StartdateEdit.EditValue).ToString("yyyy/MM/dd 23:59:59"));
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
                    foreach (var Areaitem in DeviceSetting.ElectricNames)
                    {
                        if (Areaitem.Name == Device[i].Trim())
                        {
                            foreach (var DiskBoxeitem in Areaitem.DiskBoxes)
                            {
                                foreach (var item in DiskBoxeitem.DeviceName)
                                {
                                    sql = Select_Kwh(Index, sql, item, StartTime, EndTime);
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
                BandedGridView view = new BandedGridView(KwhgridControl);
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
                GridBand[] TotalBand = new GridBand[ElectricStr.Count];
                BandedGridColumn[] ElectricgridColumn = new BandedGridColumn[ElectricStr.Count];//累積量
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
                    TotalBand[i] = new GridBand() { Caption = "累積量" };
                    ElectricgridColumn[i] = new BandedGridColumn { Caption = "累積量",FieldName = $"Total{i}", Visible = true,Width=200 };
                }
                view.Bands.Add(gridtime);
                view.Bands.AddRange(AreagridBand);
                KwhgridControl.DataSource = dataTable;
                KwhgridControl.ViewCollection.Add(view);
                KwhgridControl.MainView = view;

                ElectricIndex = 0;
                DiskBoxe = 0;
                for (int i = 0; i < AreaStr.Count; i++)
                {
                    var Areadata = DeviceSetting.ElectricNames.SingleOrDefault(g => g.Name == AreaStr[i]);
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
                        foreach (var item in DeviceSetting.ElectricNames)
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
                    //ElectricBand[i].Children.Add(TotalBand[i]);
                    ElectricBand[i].Columns.Add(ElectricgridColumn[i]);
                    //TotalBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;//文字置中
                    //TotalBand[i].Columns.Add(ElectricgridColumn[i]);
                    ElectricgridColumn[i].AppearanceCell.Options.UseTextOptions = true;//啟用文字控制
                    ElectricgridColumn[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;//文字靠左
                }
                gridtime.Columns.Add(coltime);
                view.OptionsCustomization.AllowChangeColumnParent = false;
                CloseProgressPanel(handle);
            };
            #endregion
            #region 匯出按鈕
            ExportsimpleButton.Click += (s, e) =>
            {
                if (KwhgridControl.DataSource != null)
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
                        KwhgridView.ExportToXlsx($"{saveFileDialog.FileName}", options);
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
        private string Select_Kwh(int i, string sql, DeviceName item, DateTime StartTime, DateTime EndTime)
        {
            //sql += $"DECLARE @Kwh{i} AS TABLE ([Time] nvarchar(13), [Name] nvarchar(50), [Total] DECIMAL(18,2)) " +
            //       $"INSERT INTO @Kwh{i}  ([Time], [Name],[Total]) " +
            //       $"SELECT  convert(varchar(13),[Timestamp], 120)AS[Time]," +
            //       $" MAX(Name)AS[Name]," +
            //       $"(MAX(CAST([Value] AS DECIMAL(18,2))) - MIN(CAST([Value] AS DECIMAL(18,2)))) AS Total " +
            //       $"FROM [dbo].[Table_KWH] " +
            //       $"WHERE NAME = '{item.TagName}' " +
            //       $"AND [Timestamp] >= '{StartTime.ToString("yyyy/MM/dd HH:mm:ss")}' AND [Timestamp] <= '{EndTime.ToString("yyyy/MM/dd HH:mm:ss") }' " +
            //       $"GROUP BY convert(varchar(13),[Timestamp],120) " +
            //       $"ORDER BY convert(varchar(13),[Timestamp],120) ";
            sql += $"DECLARE @Kwh{i} AS TABLE ([Time] nvarchar(13), [Name] nvarchar(50), [Total] DECIMAL(18,2)) " +
                 $"INSERT INTO @Kwh{i}  ([Time], [Name],[Total]) " +
                $"SELECT T.[Time],T.[Name],T.[Total] FROM( " +
                $"SELECT CONVERT ( VARCHAR ( 13 ), [Timestamp], 120 ) AS [Time]," +
                $"[Name]," +
                $"(CAST(FIRST_VALUE ( [Value] ) OVER ( PARTITION BY CONVERT ( VARCHAR ( 13 ), [Timestamp], 120 ) ORDER BY [Timestamp] DESC )AS DECIMAL(18,2)) - " +
                $"CAST(FIRST_VALUE ( [Value] ) OVER ( PARTITION BY CONVERT ( VARCHAR ( 13 ), [Timestamp], 120 ) ORDER BY [Timestamp] )AS DECIMAL(18,2)) )AS [Total] " +
                $"FROM [dbo].[Table_KWH] WHERE  NAME = '{item.TagName}'" +
                $"AND [Timestamp] >= '{StartTime.ToString("yyyy/MM/dd HH:mm:ss")}' AND [Timestamp] <= '{EndTime.ToString("yyyy/MM/dd HH:mm:ss") }')AS T " +
                $"Group by T.[Time],T.[Name],T.[Total] " +
                $"order by T.[Time]";
            return sql;
        }
        private string SelectFunction(int Index, string sql)
        {
            sql += $" SELECT convert(VARCHAR(13),mainT.ttimen,120) AS [Time]";
            for (int i = 0; i < Index; i++)
            {
                sql += $" ,Kwh{i}.[Total] AS [Total{i}]";
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
