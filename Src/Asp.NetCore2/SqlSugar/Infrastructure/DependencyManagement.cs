using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace SqlSugar
{
    internal class DependencyManagement
    {
        private static bool IsTryJsonNet = false;
        private static bool IsTryMySqlData = false;
        private static bool IsTrySqlite = false;
        private static bool IsTryOracle = false;
        private static bool IsTryPgSql = false;
        private static bool IsTryDm = false;
        private static bool IsTryKd = false;
        private static bool IsTryOscar = false;
        public static void TryJsonNet()
        {
            if (!IsTryJsonNet)
            {
                try
                {
                    new SerializeService().SerializeObject(new { });
                    IsTryJsonNet = true;
                }
                catch
                {
                    var message = ErrorMessage.GetThrowMessage(
                        " SqlSugar Some functions are used in newtonsoft ,Nuget references Newtonsoft.Json 9.0.0.1 + .",
                        " SqlSugar 部分功能用到Newtonsoft.Json.dll，需要在Nuget上安装 Newtonsoft.Json 9.0.0.1及以上版本，如果有版本兼容问题请先删除原有引用");
                    throw new Exception(message);
                }
            }
        }

        public static void TryMySqlData()
        {
            if (!IsTryMySqlData)
            {
                {
                    var message = ErrorMessage.GetThrowMessage(
                     "You need to refer to MySql.Data.dll",
                     "需要引用MySql.Data.dll，请在Nuget安装最新稳定版本,如果有版本兼容问题请先删除原有引用");
                    throw new Exception(message);
                }
            }
        }

        public static void TryPostgreSQL()
        {
            if (!IsTryPgSql)
            {
                {
                    var message = ErrorMessage.GetThrowMessage(
                     "You need to refer to Npgsql 3.2.7",
                     "你需要引用 Npgsql 3.2.7及以上版本");
                    throw new Exception(message);
                }
            }
        }

        public static void TryOracle()
        {
            if (!IsTryOracle)
            {
                {
                    var message = ErrorMessage.GetThrowMessage(
                     "You need to refer to Oracle.ManagedDataAccess.Core",
                     "你需要引用 Oracle.ManagedDataAccess.Core");
                    throw new Exception(message);
                }
            }
        }

        public static void TrySqlite()
        {
            if (!IsTrySqlite)
            {
                try
                {
                    SqliteProvider db = new SqliteProvider();
                    var conn = db.GetAdapter();
                    IsTrySqlite = true;
                }
                catch (Exception ex)
                {
                    var message = ErrorMessage.GetThrowMessage(
                     "You need to refer to Microsoft.Data.Sqlite." + ex.Message,
                    "你需要引用Microsoft.Data.Sqlite,如果有版本兼容问题请先删除原有引用");
                    throw new Exception(message);
                }
            }
        }

        public static void TryKdbndb()
        {
            if (!IsTryKd)
            {
                {
                    var message = "需要引用Kdbndp.dll,Github搜索sqlsugar源码里面有";
                    throw new Exception(message);
                }
            }
        }

        public static void TryDm()
        {
            if (!IsTryDm)
            {
                {
                    var message = "需要引用DmProvider.dll,Github搜索sqlsugar源码里面有";
                    throw new Exception(message);
                }
            }
        }

        public static void TryOscar()
        {
            if (!IsTryOscar)
            {
                {
                    var message = "需要引用Oscar.Data.SqlClient.dll,Github搜索sqlsugar源码里面有";
                    throw new Exception(message);
                }
            }
        }
    }
}