using Dapper;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using Serilog;
using SunnineReport.Configuration;
using SunnineReport.Methods;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SunnineReport.Views
{
    public partial class KwhDayReportUserControl : Field4UserControl
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
        public KwhDayReportUserControl(MssqlMethod method, DeviceSetting device)
        {
            InitializeComponent();

            DeviceSetting = device;
            MssqlMethod = method;
            #region 設備下拉選單
            if (DeviceSetting != null)
            {
                foreach (var Systemitem in DeviceSetting.ElectricNames)
                {
                    foreach (var DisBoxitem in Systemitem.DiskBoxes)
                    {
                        foreach (var item in DisBoxitem.DeviceName)
                        {
                            if (item.Name != "")
                            {
                                DevicecheckedComboBoxEdit.Properties.Items.Add(item.Name, false);
                            }
                            else
                            {
                                DevicecheckedComboBoxEdit.Properties.Items.Add(item.TagName, false);
                            }
                        }
                    }
                }
            }
            #endregion
            #region 報表群組名稱更改
            KwhgridView.CustomColumnDisplayText += (s, ex) =>
            {
                ColumnView view = s as ColumnView;
                if (ex.Column.FieldName == "Name" && ex.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    foreach (var Systemitem in DeviceSetting.ElectricNames)
                    {
                        foreach (var DisBoxitem in Systemitem.DiskBoxes)
                        {
                            foreach (var item in DisBoxitem.DeviceName)
                            {
                                if (ex.Value.ToString().Split('.')[1] == item.TagName)
                                {
                                    if (item.Name != "")
                                    {
                                        ex.DisplayText = item.Name;
                                    }
                                    else
                                    {
                                        ex.DisplayText = item.TagName;
                                    }
                                }

                            }
                        }
                    }
                }
            };
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
                        DataTable dataTable = new DataTable();
                        string sql = $"DECLARE @Kwh AS TABLE ([Time] nvarchar(10) , [Name] nvarchar(20), [Min] nvarchar(50), [Max] nvarchar(50), [Total] DECIMAL(18,2)) ";
                        for (int i = 0; i < Device.Length; i++)
                        {
                            foreach (var Systemitem in DeviceSetting.ElectricNames)
                            {
                                foreach (var DisBoxitem in Systemitem.DiskBoxes)
                                {
                                    foreach (var item in DisBoxitem.DeviceName)
                                    {
                                        if (Device[i].Trim() == item.Name)
                                        {
                                            sql = SelectFunction(sql, item, StartTime, EndTime);
                                            Index++;
                                            break;
                                        }
                                        else if (Device[i].Trim() == item.TagName)
                                        {
                                            sql = SelectFunction(sql, item, StartTime, EndTime);
                                            Index++;
                                            break;
                                        }
                                    }
                                    if (BreakFlag) break;
                                }
                                if (BreakFlag) break;
                            }
                            if (Index != i) BreakFlag = false; //關閉 跳出迴圈旗標
                        }
                        sql += "SELECT * FROM @Kwh 	ORDER BY [Name], [Time]";
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
                            Log.Error(ex, $"查詢設備累計量錯誤");
                            CloseProgressPanel(handle);
                        }
                        if (KwhgridView.Columns.Count > 0)
                        {
                            KwhgridView.Columns.Clear();
                        }
                        KwhgridControl.DataSource = dataTable;
                        if (KwhgridView.Columns.Count > 1)
                        {
                            KwhgridView.OptionsBehavior.Editable = false;
                            KwhgridView.OptionsSelection.EnableAppearanceFocusedCell = false;
                            KwhgridView.OptionsPrint.ExpandAllGroups = false;
                            KwhgridView.Columns["Time"].Caption = "時間";
                            KwhgridView.Columns["Time"].DisplayFormat.FormatString = "yyyy/MM/dd";
                            KwhgridView.Columns["Name"].Caption = "電表名稱";
                            KwhgridView.Columns["Name"].Group();
                            KwhgridView.Columns["Min"].Caption = "用電開始值";
                            KwhgridView.Columns["Min"].AppearanceCell.Options.UseTextOptions = true;
                            KwhgridView.Columns["Min"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                            KwhgridView.Columns["Max"].Caption = "用電結束值";
                            KwhgridView.Columns["Max"].AppearanceCell.Options.UseTextOptions = true;
                            KwhgridView.Columns["Max"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                            KwhgridView.Columns["Total"].Caption = "累積值";
                            KwhgridView.Columns["Total"].AppearanceCell.Options.UseTextOptions = true;
                            KwhgridView.Columns["Total"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
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
                        KwhgridView.ExportToXlsx($"{saveFileDialog.FileName}",options);
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
        #region 查詢條件
        /// <summary>
        /// 查詢條件
        /// </summary>
        /// <param name="sql">查詢字串</param>
        /// <param name="item">設備名稱</param>
        /// <param name="StartTime">起始時間</param>
        /// <param name="EndTime">結束時間</param>
        private string SelectFunction(string sql, DeviceName item, DateTime StartTime, DateTime EndTime)
        {
            sql += $"INSERT INTO @Kwh ([Time], [Name], [Min], [Max], [Total])" +
                   $"SELECT convert(varchar(10),[TIMESTAMP],120)AS [Time], MAX(NAME)AS [Name],MIN(CAST([VALUE] AS DECIMAL(18,2)))AS [Min],MAX(CAST([VALUE] AS DECIMAL(18,2)))AS [Max] ,(MAX(CAST([VALUE] AS DECIMAL(18,2))) -MIN(CAST([VALUE] AS DECIMAL(18,2))))AS [Total] FROM [dbo].[PM] " +
                   $"WHERE NAME ='PM.{item.TagName.Trim()}.KWH' " +
                   $"AND [TIMESTAMP] >= '{StartTime.ToString("yyyy/MM/dd HH:mm:ss")}' AND [TIMESTAMP] <= '{EndTime.ToString("yyyy/MM/dd HH:mm:ss")}' " +
                   $"GROUP BY convert(varchar(10),[TIMESTAMP],120) " +
                   $"ORDER BY convert(varchar(10),[TIMESTAMP],120)";
            return sql;
        }
        #endregion
    }
}
