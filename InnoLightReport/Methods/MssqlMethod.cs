using SunnineReport.Configuration;
using System.Data.SqlClient;

namespace SunnineReport.Methods
{
    public class MssqlMethod
    {
        public SqlConnectionStringBuilder scsb { get; set; }
        public MssqlMethod(SystemSetting setting)
        {
            if (setting != null)
            {
                scsb = new SqlConnectionStringBuilder()
                {
                    DataSource = setting.DataSource,
                    InitialCatalog = setting.InitialCatalog,
                    UserID = setting.UserID,
                    Password = setting.Password
                    //ConnectTimeout = 0
                };
            }
        }
    }
}
