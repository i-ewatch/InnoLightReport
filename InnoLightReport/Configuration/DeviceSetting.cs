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
        /// 感測器名稱
        /// </summary>
        public List<RTHDiskBox> SenserNames { get; set; } = new List<RTHDiskBox>();
    }
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
        public string TagName { get; set; }
        public int TagNum { get; set; }
    }
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
}
