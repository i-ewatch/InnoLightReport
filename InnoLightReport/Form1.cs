using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraBars.Navigation;
using InnoLightReport.Views;
using Serilog;
using SunnineReport.Configuration;
using SunnineReport.Methods;
using SunnineReport.Views;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SunnineReport
{
    public partial class Form1 : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private string MyWorkPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 軟體被開啟旗標
        /// </summary>
        private bool OpenFlag { get; set; }
        #region Json 物件
        /// <summary>
        /// 資料庫資訊
        /// </summary>
        private SystemSetting SystemSetting { get; set; }
        /// <summary>
        /// 設備資訊
        /// </summary>
        private DeviceSetting DeviceSetting { get; set; }
        /// <summary>
        /// 按鈕資訊
        /// </summary>
        private ButtonSetting ButtonSetting { get; set; }
        #endregion
        #region Method 方法
        /// <summary>
        /// 資料庫方法
        /// </summary>
        private MssqlMethod MssqlMethod { get; set; }
        /// <summary>
        /// 按鈕方法
        /// </summary>
        private ButtonMethod ButtonMethod { get; set; }
        #endregion
        #region Views
        /// <summary>
        /// 切換畫面物件
        /// </summary>
        private NavigationFrame NavigationFrame { get; set; }
        /// <summary>
        /// 累積量報表
        /// </summary>
        private KwhDayReportUserControl KwhDayReportUserControl { get; set; }
        /// <summary>
        /// 小時累積量報表
        /// </summary>
        private KwhDayReportUserControl_V2 KwhDayReportUserControl_V2 { get; set; }
        /// <summary>
        /// 天累積量報表
        /// </summary>
        private KwhHourReportXtraUserControl KwhHourReportXtraUserControl { get; set; }
        /// <summary>
        /// 感測器小時報表
        /// </summary>
        private SenserHourReportUserControl SenserHourReportUserControl { get; set; }
        /// <summary>
        /// 感測器日報表
        /// </summary>
        private SenserDayReportUserControl SenserDayReportUserControl { get; set; }
        #endregion
        public Form1()
        {

            InitializeComponent();
            #region 禁止軟體重複開啟功能
            string ProcessName = Process.GetCurrentProcess().ProcessName;
            Process[] p = Process.GetProcessesByName(ProcessName);
            if (p.Length > 1)
            {
                FlyoutAction action = new FlyoutAction();
                action.Caption = "軟體錯誤";
                action.Description = "重複開啟!";
                action.Commands.Add(FlyoutCommand.OK);
                FlyoutDialog.Show(FindForm().FindForm(), action);
                OpenFlag = true;
                Environment.Exit(1);
            }
            #endregion
            if (!OpenFlag)
            {
                #region 載入Logo
                if (!Directory.Exists($"{MyWorkPath}\\Images"))
                    Directory.CreateDirectory($"{MyWorkPath}\\Images");
                if (File.Exists($"{MyWorkPath}\\Images\\logo.png"))
                {
                    FileStream stream = new FileStream($"{MyWorkPath}\\Images\\logo.png", FileMode.Open, FileAccess.Read);
                    LogopictureEdit.Image = Image.FromStream(stream);
                    stream.Close();
                }
                #endregion
                #region Serilog initial
                Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}\\log\\log-.txt",
                                          rollingInterval: RollingInterval.Day,
                                          outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                            .CreateLogger();        //宣告Serilog初始化
                #endregion
                #region Json
                SystemSetting = InitialMethod.SystemLoad();
                DeviceSetting = InitialMethod.DeviceLoad();
                ButtonSetting = InitialMethod.ButtonLoad();
                #endregion
                #region Method
                MssqlMethod = new MssqlMethod(SystemSetting);
                #endregion
                #region 建立按鈕物件
                NavigationFrame = new NavigationFrame() { Dock = DockStyle.Fill };
                NavigationFrame.Parent = ViewpanelControl;
                ButtonMethod = new ButtonMethod() { Form1 = this, navigationFrame = NavigationFrame };
                ButtonMethod.AccordionLoad(accordionControl1, ButtonSetting);
                #endregion

                #region 累積量報表
                KwhDayReportUserControl = new KwhDayReportUserControl(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(KwhDayReportUserControl);
                #endregion
                #region 小時累積量報表
                KwhHourReportXtraUserControl = new KwhHourReportXtraUserControl(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(KwhHourReportXtraUserControl);
                #endregion
                #region 天累積量報表
                KwhDayReportUserControl_V2 = new KwhDayReportUserControl_V2(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(KwhDayReportUserControl_V2);
                #endregion
                #region 感測器小時報表
                SenserHourReportUserControl = new SenserHourReportUserControl(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(SenserHourReportUserControl);
                #endregion
                #region 感測器日報表
                SenserDayReportUserControl = new SenserDayReportUserControl(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(SenserDayReportUserControl);
                #endregion
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
        }
    }
}
