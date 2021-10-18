using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using SunnineReport.Configuration;
using SunnineReport.Methods;

namespace SunnineReport.Views
{
    public class Field4UserControl: XtraUserControl
    {
        /// <summary>
        /// 資料庫方法
        /// </summary>
        public MssqlMethod MssqlMethod { get; set; }
        /// <summary>
        /// 資料庫資訊
        /// </summary>
        public SystemSetting SystemSetting { get; set; }
        /// <summary>
        /// 設備資訊
        /// </summary>
        public DeviceSetting DeviceSetting { get; set; }
        /// <summary>
        /// Loading物件繼承
        /// </summary>
        public IOverlaySplashScreenHandle handle { get; set; }
        /// 關閉Loading視窗
        /// </summary>
        /// <param name="handle"></param>
        public void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }
    }
}
