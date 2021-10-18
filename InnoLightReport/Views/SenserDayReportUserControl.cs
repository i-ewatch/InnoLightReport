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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SunnineReport.Views
{
    public partial class SenserDayReportUserControl : Field4UserControl
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
        /// 感測器數量
        /// </summary>
        private int SenserIndex { get; set; }
        /// <summary>
        /// 感測器名稱
        /// </summary>
        private List<string> SenserStr { get; set; } = new List<string>();
        /// <summary>
        /// 區域名稱
        /// </summary>
        private List<string> AreaStr { get; set; } = new List<string>();
        public SenserDayReportUserControl(MssqlMethod method, DeviceSetting device)
        {
            InitializeComponent();
            DeviceSetting = device;
            MssqlMethod = method;
            #region 設備下拉選單
            if (DeviceSetting != null)
            {
                foreach (var DisBoxitem in DeviceSetting.SenserNames)
                {
                    DevicecheckedComboBoxEdit.Properties.Items.Add(DisBoxitem.Name, false);
                    //foreach (var item in DisBoxitem.DeviceName)
                    //{
                    //    if (item.Name != "")
                    //    {
                    //        DevicecheckedComboBoxEdit.Properties.Items.Add(item.Name, false);
                    //    }
                    //    else
                    //    {
                    //        DevicecheckedComboBoxEdit.Properties.Items.Add(item.TTagName, false);
                    //    }
                    //}
                }
            }
            #endregion
            #region 查詢按鈕
            SearchsimpleButton.Click += (s, e) =>
            {
                if (StartdateEdit.DateTime < EnddateEdit.DateTime)
                {
                    handle = SplashScreenManager.ShowOverlayForm(FindForm());
                    _Index = 0;
                    AreaStr = new List<string>();
                    SenserStr = new List<string>();
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
                    $"SET @regTIme = DATEADD(day,1,@regTIme) " +
                    $"END ";
                    for (int i = 0; i < Device.Length; i++)
                    {
                        foreach (var DisBoxitem in DeviceSetting.SenserNames)
                        {
                            if (DisBoxitem.Name == Device[i].Trim())
                            {
                                foreach (var item in DisBoxitem.DeviceName)
                                {
                                    sql = Select_RT(Index, sql, item, StartTime, EndTime);
                                    sql = Select_RH(Index, sql, item, StartTime, EndTime);
                                    Index++;
                                    SenserStr.Add(item.Name);
                                    var AreaData = AreaStr.SingleOrDefault(g => g == DisBoxitem.Name);
                                    if (AreaData == null)
                                    {
                                        AreaStr.Add(DisBoxitem.Name);
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
                        Log.Error(ex, $"查詢感測器天報表錯誤");
                        CloseProgressPanel(handle);
                    }
                    BandedGridView view = new BandedGridView(SensergridControl);
                    view.MinBandPanelRowCount = 1; // Band欄位高度
                    view.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;//Colum 自動調整高度
                                                                                  //view.OptionsPrint.AutoWidth = false;//報表匯出不自動縮放長度
                                                                                  //view.OptionsPrint.PrintHeader = false;//不顯示Columns標頭    
                                                                                  //view.Appearance.Row.FontSizeDelta = 9; //數值大小
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
                    GridBand[] AreagridBand = new GridBand[AreaStr.Count];//區域Band
                    GridBand[] SensergridBand = new GridBand[SenserStr.Count];//感測器Band
                    GridBand[] TempgridBand = new GridBand[SenserStr.Count];//溫度Band
                    GridBand[] HumiditygridBand = new GridBand[SenserStr.Count];//濕度Band
                    GridBand[] G_TMingridColumn = new GridBand[SenserStr.Count];//溫度最小值
                    GridBand[] G_TMaxgridColumn = new GridBand[SenserStr.Count];//溫度最大值
                    GridBand[] G_TAvggridColumn = new GridBand[SenserStr.Count];//溫度平均值
                    GridBand[] G_HMingridColumn = new GridBand[SenserStr.Count];//濕度最小值
                    GridBand[] G_HMaxgridColumn = new GridBand[SenserStr.Count];//濕度最大值
                    GridBand[] G_HAvggridColumn = new GridBand[SenserStr.Count];//濕度平均值
                    BandedGridColumn[] TMingridColumn = new BandedGridColumn[SenserStr.Count];//溫度最小值
                    BandedGridColumn[] TMaxgridColumn = new BandedGridColumn[SenserStr.Count];//溫度最大值
                    BandedGridColumn[] TAvggridColumn = new BandedGridColumn[SenserStr.Count];//溫度平均值
                    BandedGridColumn[] HMingridColumn = new BandedGridColumn[SenserStr.Count];//濕度最小值
                    BandedGridColumn[] HMaxgridColumn = new BandedGridColumn[SenserStr.Count];//濕度最大值
                    BandedGridColumn[] HAvggridColumn = new BandedGridColumn[SenserStr.Count];//濕度平均值
                    BandedGridColumn coltime = new BandedGridColumn() { Caption = "時間", FieldName = "Time", Visible = true, Width = 100 }; //建立Colum (時間)

                    for (int i = 0; i < AreaStr.Count; i++)
                    {
                        AreagridBand[i] = new GridBand() { Caption = AreaStr[i] };
                    }
                    for (int i = 0; i < SenserStr.Count; i++)
                    {
                        SensergridBand[i] = new GridBand() { Caption = SenserStr[i] };
                        TempgridBand[i] = new GridBand() { Caption = "溫度" + " (\xb0" + "C)" };
                        HumiditygridBand[i] = new GridBand() { Caption = "濕度 (%)" };
                        G_TMingridColumn[i] = new GridBand() { Caption = "最小值" };
                        G_TMaxgridColumn[i] = new GridBand() { Caption = "最大值" };
                        G_TAvggridColumn[i] = new GridBand() { Caption = "平均值" };
                        G_HMingridColumn[i] = new GridBand() { Caption = "最小值" };
                        G_HMaxgridColumn[i] = new GridBand() { Caption = "最大值" };
                        G_HAvggridColumn[i] = new GridBand() { Caption = "平均值" };
                        TMingridColumn[i] = new BandedGridColumn() { Caption = "最小值", FieldName = $"T{i}Min", Visible = true };
                        TMaxgridColumn[i] = new BandedGridColumn() { Caption = "最大值", FieldName = $"T{i}Max", Visible = true };
                        TAvggridColumn[i] = new BandedGridColumn() { Caption = "平均值", FieldName = $"T{i}Avg", Visible = true };
                        HMingridColumn[i] = new BandedGridColumn() { Caption = "最小值", FieldName = $"H{i}Min", Visible = true };
                        HMaxgridColumn[i] = new BandedGridColumn() { Caption = "最大值", FieldName = $"H{i}Max", Visible = true };
                        HAvggridColumn[i] = new BandedGridColumn() { Caption = "平均值", FieldName = $"H{i}Avg", Visible = true };
                        TMingridColumn[i].AppearanceCell.Options.UseTextOptions = true;//啟用文字控制
                        TMaxgridColumn[i].AppearanceCell.Options.UseTextOptions = true;//啟用文字控制
                        TAvggridColumn[i].AppearanceCell.Options.UseTextOptions = true;//啟用文字控制
                        HMingridColumn[i].AppearanceCell.Options.UseTextOptions = true;//啟用文字控制
                        HMaxgridColumn[i].AppearanceCell.Options.UseTextOptions = true;//啟用文字控制
                        HAvggridColumn[i].AppearanceCell.Options.UseTextOptions = true;//啟用文字控制
                        TMingridColumn[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;//文字置中
                        TMaxgridColumn[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;//文字置中
                        TAvggridColumn[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;//文字置中
                        HMingridColumn[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;//文字置中
                        HMaxgridColumn[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;//文字置中
                        HAvggridColumn[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;//文字置中
                    }
                    view.Bands.Add(gridtime);
                    view.Bands.AddRange(AreagridBand);
                    SensergridControl.DataSource = dataTable;
                    SensergridControl.ViewCollection.Add(view);
                    SensergridControl.MainView = view;

                    SenserIndex = 0;//感測器欄位建置好後歸零，給區域做運算用
                    for (int i = 0; i < AreaStr.Count; i++)
                    {
                        var Areadata = DeviceSetting.SenserNames.SingleOrDefault(g => g.Name == AreaStr[i]);
                        for (int Index = 0; Index < Areadata.DeviceName.Count; Index++)
                        {
                            AreagridBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;//文字置中
                            AreagridBand[i].Children.Add(SensergridBand[SenserIndex]);
                            SenserIndex++;
                        }
                    }
                    for (int i = 0; i < SenserStr.Count; i++)
                    {
                        SensergridBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;//文字置中
                        SensergridBand[i].Children.Add(TempgridBand[i]);
                        SensergridBand[i].Children.Add(HumiditygridBand[i]);
                        TempgridBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;//文字置中
                        TempgridBand[i].Children.Add(G_TMingridColumn[i]);
                        TempgridBand[i].Children.Add(G_TMaxgridColumn[i]);
                        TempgridBand[i].Children.Add(G_TAvggridColumn[i]);
                        G_TMingridColumn[i].Columns.Add(TMingridColumn[i]);
                        G_TMaxgridColumn[i].Columns.Add(TMaxgridColumn[i]);
                        G_TAvggridColumn[i].Columns.Add(TAvggridColumn[i]);
                        HumiditygridBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;//文字置中
                        HumiditygridBand[i].Children.Add(G_HMingridColumn[i]);
                        HumiditygridBand[i].Children.Add(G_HMaxgridColumn[i]);
                        HumiditygridBand[i].Children.Add(G_HAvggridColumn[i]);
                        G_HMingridColumn[i].Columns.Add(HMingridColumn[i]);
                        G_HMaxgridColumn[i].Columns.Add(HMaxgridColumn[i]);
                        G_HAvggridColumn[i].Columns.Add(HAvggridColumn[i]);
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
                if (SensergridControl.DataSource != null)
                {
                    XlsxExportOptionsEx op = new XlsxExportOptionsEx();
                    op.ShowColumnHeaders = DefaultBoolean.False;
                    //op.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Xlsx|*xlsx";
                    saveFileDialog.Title = "Export Data";
                    saveFileDialog.DefaultExt = ".xlsx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        SensergridControl.ExportToXlsx($"{saveFileDialog.FileName}", op);
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
        private string Select_RT(int i, string sql, RTHDeviceName item, DateTime StartTime, DateTime EndTime)
        {
            sql += $"DECLARE @temp{i} AS TABLE ([Time] nvarchar(10), [Name] nvarchar(20), [Min] nvarchar(50) , [Max] nvarchar(50), [Avg] DECIMAL(18,2)) " +
                   $"INSERT INTO @temp{i}  ([Time], [Name], [Min], [Max], [Avg]) " +
                   $"SELECT convert(varchar(10),[TIMESTAMP], 120)AS[Time], MAX(NAME)AS[Name],MIN(CAST([VALUE] AS DECIMAL(18,2)))AS[Min],MAX(CAST([VALUE] AS DECIMAL(18,2)))AS[Max] ,AVG(CAST([VALUE] AS DECIMAL(18,2))) AS [Avg] FROM[dbo].[RTH] " +
                   $"WHERE NAME = 'LED.{item.TTagName}.404099' " +
                   $"AND [TIMESTAMP] >= '{StartTime.ToString("yyyy/MM/dd HH:mm:ss")}' AND [TIMESTAMP] <= '{EndTime.ToString("yyyy/MM/dd HH:mm:ss") }' " +
                   $"GROUP BY convert(varchar(10),[TIMESTAMP],120) " +
                   $"ORDER BY convert(varchar(10),[TIMESTAMP],120) ";
            return sql;
        }
        private string Select_RH(int i, string sql, RTHDeviceName item, DateTime StartTime, DateTime EndTime)
        {
            sql += $"DECLARE @humidity{i} AS TABLE ([Time] nvarchar(10), [Name] nvarchar(20), [Min] nvarchar(50), [Max] nvarchar(50), [Avg] DECIMAL(18,2)) " +
                   $"INSERT INTO @humidity{i}  ([Time], [Name], [Min], [Max], [Avg]) " +
                   $"SELECT convert(varchar(10),[TIMESTAMP], 120)AS[Time], MAX(NAME)AS[Name],MIN(CAST([VALUE] AS DECIMAL(18,2)))AS[Min],MAX(CAST([VALUE] AS DECIMAL(18,2)))AS[Max] ,AVG(CAST([VALUE] AS DECIMAL(18,2))) AS [Avg] FROM[dbo].[RTH] " +
                   $"WHERE NAME = 'LED.{item.HTagName}.404099' " +
                   $"AND [TIMESTAMP] >= '{StartTime.ToString("yyyy/MM/dd HH:mm:ss")}' AND [TIMESTAMP] <= '{EndTime.ToString("yyyy/MM/dd HH:mm:ss") }' " +
                   $"GROUP BY convert(varchar(10),[TIMESTAMP],120) " +
                   $"ORDER BY convert(varchar(10),[TIMESTAMP],120) ";
            return sql;
        }
        private string SelectFunction(int Index, string sql)
        {
            sql += $" SELECT convert(VARCHAR(10),mainT.ttimen,120) AS [Time]";
            for (int i = 0; i < Index; i++)
            {
                //sql += $",ISNULL(T{i}.[Min],0) AS [T{i}Min]" +
                //       $",ISNULL(T{i}.[Max],0) AS [T{i}Max]" +
                //       $",ISNULL(T{i}.[Avg],0) AS [T{i}Avg]" +
                //       $",ISNULL(H{i}.[Min],0) AS [H{i}Min]" +
                //       $",ISNULL(H{i}.[Max],0) AS [H{i}Max]" +
                //       $",ISNULL(H{i}.[Avg],0) AS [H{i}Avg]";
                sql += $",T{i}.[Min] AS [T{i}Min]" +
                       $",T{i}.[Max] AS [T{i}Max]" +
                       $",T{i}.[Avg] AS [T{i}Avg]" +
                       $",H{i}.[Min] AS [H{i}Min]" +
                       $",H{i}.[Max] AS [H{i}Max]" +
                       $",H{i}.[Avg] AS [H{i}Avg]";
            }
            sql += " FROM @mainTemp AS mainT";
            for (int i = 0; i < Index; i++)
            {
                sql += $" LEFT JOIN @temp{i} AS T{i} ON  convert(varchar(10),mainT.[ttimen],120) = T{i}.[Time]" +
                       $" LEFT JOIN @humidity{i} AS H{i} ON convert(varchar(10),mainT.[ttimen],120) = H{i}.[Time]";
            }
            return sql;
        }
    }
}
