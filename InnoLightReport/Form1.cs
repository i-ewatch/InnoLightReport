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
        /// 小時即時功率報表
        /// </summary>
        private KwHourReportUserControl KwHourReportUserControl { get; set; }
        /// <summary>
        /// 天即時功率報表
        /// </summary>
        private KwDayReportUserControl KwDayReportUserControl { get; set; }
        /// <summary>
        /// 小時電流報表
        /// </summary>
        private CurrentHourReportUserControl CurrentHourReportUserControl { get; set; }
        /// <summary>
        /// 天電流報表
        /// </summary>
        private CurrentDayReportUserControl CurrentDayReportUserControl { get; set; }
        /// <summary>
        /// 小時電壓報表
        /// </summary>
        private VoltageHourReportUserControl VoltageHourReportUserControl { get; set; }
        /// <summary>
        /// 天電壓報表
        /// </summary>
        private VoltageDayReportUserControl VoltageDayReportUserControl { get; set; }
        /// <summary>
        /// 冷凍冷藏小時報表
        /// </summary>
        private SenserHourReportUserControl SenserHourReportUserControl { get; set; }
        /// <summary>
        /// 冷凍冷藏日報表
        /// </summary>
        private SenserDayReportUserControl SenserDayReportUserControl { get; set; }
        /// <summary>
        /// 空調小時報表
        /// </summary>
        private SenserHourReportUserControl ASenserHourReportUserControl { get; set; }
        /// <summary>
        /// 空調日報表
        /// </summary>
        private SenserDayReportUserControl ASenserDayReportUserControl { get; set; }
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
                //KwhDayReportUserControl = new KwhDayReportUserControl(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                //NavigationFrame.AddPage(KwhDayReportUserControl);
                #endregion
                #region 小時累積量報表
                KwhHourReportXtraUserControl = new KwhHourReportXtraUserControl(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(KwhHourReportXtraUserControl);
                #endregion
                #region 天累積量報表
                KwhDayReportUserControl_V2 = new KwhDayReportUserControl_V2(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(KwhDayReportUserControl_V2);
                #endregion
                #region 小時即時功率報表
                KwHourReportUserControl = new KwHourReportUserControl(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(KwHourReportUserControl);
                #endregion
                #region 天即時功率報表
                KwDayReportUserControl = new KwDayReportUserControl(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(KwDayReportUserControl);
                #endregion
                #region 小時電流報表
                CurrentHourReportUserControl = new CurrentHourReportUserControl(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(CurrentHourReportUserControl);
                #endregion
                #region 天電流報表
                CurrentDayReportUserControl = new CurrentDayReportUserControl(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(CurrentDayReportUserControl);
                #endregion
                #region 小時電壓報表
                VoltageHourReportUserControl = new VoltageHourReportUserControl(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(VoltageHourReportUserControl);
                #endregion
                #region 天電壓報表
                VoltageDayReportUserControl = new VoltageDayReportUserControl(MssqlMethod, DeviceSetting) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(VoltageDayReportUserControl);
                #endregion
                #region 冷凍冷藏小時報表
                SenserHourReportUserControl = new SenserHourReportUserControl(MssqlMethod, DeviceSetting,0) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(SenserHourReportUserControl);
                #endregion
                #region 冷凍冷藏日報表
                SenserDayReportUserControl = new SenserDayReportUserControl(MssqlMethod, DeviceSetting,0) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(SenserDayReportUserControl);
                #endregion
                #region 空調小時報表
                ASenserHourReportUserControl = new SenserHourReportUserControl(MssqlMethod, DeviceSetting, 1) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(ASenserHourReportUserControl);
                #endregion
                #region 空調日報表
                ASenserDayReportUserControl = new SenserDayReportUserControl(MssqlMethod, DeviceSetting, 1) { Dock = DockStyle.Fill };
                NavigationFrame.AddPage(ASenserDayReportUserControl);
                #endregion
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
        }
    }
}
