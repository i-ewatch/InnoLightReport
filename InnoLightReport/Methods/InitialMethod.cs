using Newtonsoft.Json;
using Serilog;
using SunnineReport.Configuration;
using System;
using System.IO;
using System.Text;

namespace SunnineReport.Methods
{
    public class InitialMethod
    {
        private static string MyWorkPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        #region  DB資訊Json 建檔與讀取
        /// <summary>
        /// DB資訊Json 建檔與讀取
        /// </summary>
        /// <returns></returns>
        public static SystemSetting SystemLoad()
        {
            SystemSetting setting = null;
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\System.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<SystemSetting>(json);
                }
                else
                {
                    SystemSetting Setting = new SystemSetting()
                    {
                        DataSource = "192.168.100.104",
                        InitialCatalog = "PM",
                        UserID = "sa",
                        Password = "Ewatch001007"
                    };
                    setting = Setting;
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " DB資訊設定載入錯誤");
            }
            return setting;
        }
        #endregion
        #region 設備資訊Json 建檔與讀取
        /// <summary>
        /// 設備資訊Json 建檔與讀取
        /// </summary>
        /// <returns></returns>
        public static DeviceSetting DeviceLoad()
        {
            DeviceSetting setting = null;
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\Device.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<DeviceSetting>(json);
                }
                else
                {
                    DeviceSetting Setting = new DeviceSetting()
                    {
                        ElectricNames =
                        {
                            new Area()
                            {
                                Name = "空間名稱",
                                DiskBoxes =
                                {
                                    new DiskBox()
                                    {
                                        Name ="盤箱名稱",
                                        DeviceName =
                                        {
                                            new DeviceName()
                                            {
                                                Name ="電表1",
                                                TagName ="電表1",
                                                TagNum = 1
                                            }
                                        }
                                    }
                                }
                            }

                        },
                        SenserNames =
                        {
                            new RTHDiskBox()
                            {
                               Name ="盤箱名稱",
                               DeviceName =
                               {
                                   new RTHDeviceName()
                                   {
                                       Name ="感測器1",
                                       TTagName ="感測器1",
                                       HTagName = "感測器1"
                                   }
                               }
                            }
                        }
                    };
                    setting = Setting;
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " 設備資訊設定載入錯誤");
            }
            return setting;
        }
        #endregion
        #region 按鈕Json 建檔與讀取
        /// <summary>
        /// 按鈕Json 建檔與讀取
        /// </summary>
        /// <returns></returns>
        public static ButtonSetting ButtonLoad()
        {
            ButtonSetting setting = null;
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\button.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<ButtonSetting>(json);
                }
                else
                {
                    ButtonSetting Setting = new ButtonSetting()
                    {
                        //群組與列表按鈕設定
                        ButtonGroupSettings =
                        {
                            new ButtonGroupSetting()
                            {
                                // 0 = 群組，1 = 列表
                                ButtonStyle = 1,
                                //群組名稱
                                GroupName = "群組名稱",
                                // 群組標註
                                GroupTag = 0,
                                //列表按鈕設定
                                ButtonItemSettings=
                                {
                                    new ButtonItemSetting()
                                    {
                                        //列表名稱
                                        ItemName = "列表名稱",
                                        //列表標註
                                        ItemTag = 0,
                                        //控制畫面顯示
                                        ControlVisible = true
                                    }
                                }
                            }
                        }
                    };
                    setting = Setting;
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "按鈕載入失敗");
            }
            return setting;
        }
        #endregion
    }
}
