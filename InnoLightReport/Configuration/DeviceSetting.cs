using System.Collections.Generic;

namespace SunnineReport.Configuration
{
    public class DeviceSetting
    {
        /// <summary>
        /// 電表名稱
        /// </summary>
        public List<Area> ElectricNames { get; set; } = new List<Area>();
        /// <summary>
        /// 即時功率名稱
        /// </summary>
        public List<Area> KwNames { get; set; } = new List<Area>();
        /// <summary>
        /// 電表名稱
        /// </summary>
        public List<Area> CurrentNames { get; set; } = new List<Area>();
        /// <summary>
        /// 電表名稱
        /// </summary>
        public List<Area> VoltageNames { get; set; } = new List<Area>();
        /// <summary>
        /// 冷凍冷藏名稱
        /// </summary>
        public List<RTHDiskBox> SenserNames { get; set; } = new List<RTHDiskBox>();
        /// <summary>
        /// 空調名稱
        /// </summary>
        public List<RTHDiskBox> AirNames { get; set; } = new List<RTHDiskBox>();
        /// <summary>
        /// 攪拌機名稱
        /// </summary>
        public List<RTHDiskBox> BlenderNames { get; set; } = new List<RTHDiskBox>();
    }
    #region 累積量、平均電流、平均電壓
    /// <summary>
    /// 區域
    /// </summary>
    public class Area
    {
        public string Name { get; set; }
        public List<DiskBox> DiskBoxes { get; set; } = new List<DiskBox>();
    }
    /// <summary>
    /// 盤名
    /// </summary>
    public class DiskBox
    {
        public string Name { get; set; }
        public List<DeviceName> DeviceName { get; set; } = new List<DeviceName>();
    }
    /// <summary>
    /// 設備
    /// </summary>
    public class DeviceName
    {
        public string Name { get; set; }
        /// <summary>
        /// 累積量
        /// </summary>
        public string TagName { get; set; }
        public int TagNum { get; set; }
    }
    #endregion
    #region 溫濕度
    /// <summary>
    /// 盤名區域
    /// </summary>
    public class RTHDiskBox
    {
        public string Name { get; set; }
        public List<RTHDeviceName> DeviceName { get; set; } = new List<RTHDeviceName>();
    }
    /// <summary>
    /// 設備
    /// </summary>
    public class RTHDeviceName
    {
        public string Name { get; set; }
        public string TTagName { get; set; }
        public string HTagName { get; set; }
    }
    #endregion
}
