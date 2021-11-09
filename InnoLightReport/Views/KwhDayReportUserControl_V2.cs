using Dapper;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
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
    public partial class KwhDayReportUserControl_V2 : Field4UserControl
    {
        public KwhDayReportUserControl_V2(MssqlMethod method, DeviceSetting device)
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
                if (StartdateEdit.DateTime <= EnddateEdit.DateTime)
                {
                    handle = SplashScreenManager.ShowOverlayForm(FindForm());
                    DateTime StartTime = Convert.ToDateTime(StartdateEdit.DateTime.ToString("yyyy/MM/dd 00:00:00"));
                    DateTime EndTime = Convert.ToDateTime(EnddateEdit.DateTime.ToString("yyyy/MM/dd 23:59:59"));
                    string[] Device = DevicecheckedComboBoxEdit.Text.Split(',');
                    string sql = $"SELECT NAME, CAST(MIN([Value])AS DECIMAL(18,2)) AS [Min] ,CAST(MAX([Value]) AS DECIMAL(18,2)) AS [Max] ,(CAST(MAX([Value])AS DECIMAL(18,2))-CAST(MIN([Value]) AS DECIMAL(18,2))) AS [Total] FROM [dbo].[Table_KWH] " +
                                 $"WHERE [Timestamp] >= '{StartTime.ToString("yyyy/MM/dd HH:mm:ss")}' AND [Timestamp] <= '{EndTime.ToString("yyyy/MM/dd HH:mm:ss")}' GROUP BY NAME";
                    List<KwhData> DeviceData = new List<KwhData>();
                    List<KwhData> ReportData = new List<KwhData>();
                    try
                    {
                        using (var con = new SqlConnection(MssqlMethod.scsb.ConnectionString))
                        {
                            DeviceData = con.Query<KwhData>(sql).ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, $"查詢區域累計量錯誤");
                        CloseProgressPanel(handle);
                    }
                    for (int i = 0; i < Device.Length; i++)
                    {
                        foreach (var Systemitem in DeviceSetting.ElectricNames)
                        {
                            if (Device[i].Trim() == Systemitem.Name)
                            {
                                foreach (var DiskBoxesitem in Systemitem.DiskBoxes)
                                {
                                    foreach (var item in DiskBoxesitem.DeviceName)
                                    {
                                        var data = DeviceData.SingleOrDefault(g => g.NAME == $"{item.TagName.Trim()}");
                                        if (data != null)
                                        {
                                            data.Area = Systemitem.Name;
                                            data.DiskBox = DiskBoxesitem.Name;
                                            data.TagNum = item.TagNum;
                                            if (item.Name != "")
                                            {
                                                data.NAME = item.Name;
                                            }
                                            ReportData.Add(data);
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                    for (int i = 0; i < Device.Length; i++)
                    {
                        foreach (var Systemitem in DeviceSetting.ElectricNames)
                        {
                            if (Device[i].Trim() == Systemitem.Name)
                            {
                                foreach (var DiskBoxesitem in Systemitem.DiskBoxes)
                                {
                                    var data = ReportData.Where(g => g.DiskBox == $"{DiskBoxesitem.Name.Trim()}" & g.Area == Device[i].Trim()).ToList();
                                    decimal SumTotal = 0;
                                    foreach (var item in data)
                                    {
                                        SumTotal = SumTotal + item.Total;
                                    }
                                    data.ForEach(v => v.SumTotal = SumTotal);
                                }
                                break;
                            }
                        }
                    }
                    if (KwhgridView.Columns.Count > 0)
                    {
                        KwhgridView.Columns.Clear();
                    }
                    KwhgridControl.DataSource = ReportData;
                    KwhgridView.OptionsView.AllowCellMerge = true;//Cell合併儲存格
                    if (KwhgridView.Columns.Count >= 1)
                    {
                        KwhgridView.OptionsBehavior.Editable = false;
                        KwhgridView.OptionsSelection.EnableAppearanceFocusedCell = false;
                        KwhgridView.Columns["Area"].Caption = "區域";
                        KwhgridView.Columns["DiskBox"].Caption = "位置";
                        KwhgridView.Columns["TagNum"].Caption = "編號";
                        KwhgridView.Columns["TagNum"].OptionsColumn.AllowMerge = DefaultBoolean.False;
                        KwhgridView.Columns["TagNum"].AppearanceCell.Options.UseTextOptions = true;
                        KwhgridView.Columns["TagNum"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                        KwhgridView.Columns["NAME"].Caption = "迴路名稱";
                        KwhgridView.Columns["NAME"].OptionsColumn.AllowMerge = DefaultBoolean.False;
                        KwhgridView.Columns["Min"].Caption = "起始電能";
                        KwhgridView.Columns["Min"].OptionsColumn.AllowMerge = DefaultBoolean.False;
                        KwhgridView.Columns["Min"].AppearanceCell.Options.UseTextOptions = true;
                        KwhgridView.Columns["Min"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                        KwhgridView.Columns["Max"].Caption = "結束電能";
                        KwhgridView.Columns["Max"].OptionsColumn.AllowMerge = DefaultBoolean.False;
                        KwhgridView.Columns["Max"].AppearanceCell.Options.UseTextOptions = true;
                        KwhgridView.Columns["Max"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                        KwhgridView.Columns["Total"].Caption = "用電量";
                        KwhgridView.Columns["Total"].OptionsColumn.AllowMerge = DefaultBoolean.False;
                        KwhgridView.Columns["Total"].AppearanceCell.Options.UseTextOptions = true;
                        KwhgridView.Columns["Total"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                        KwhgridView.Columns["SumTotal"].Caption = "合計";
                        KwhgridView.Columns["SumTotal"].AppearanceCell.Options.UseTextOptions = true;
                        KwhgridView.Columns["SumTotal"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                    }
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
                if (KwhgridControl.DataSource != null)
                {
                    XlsxExportOptionsEx options = new XlsxExportOptionsEx();
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
        private class KwhData
        {
            public string Area { get; set; }
            public string DiskBox { get; set; }
            public int TagNum { get; set; }
            public string NAME { get; set; }
            public string Min { get; set; }
            public string Max { get; set; }
            public decimal Total { get; set; }
            public decimal SumTotal { get; set; }
        }
    }
}
