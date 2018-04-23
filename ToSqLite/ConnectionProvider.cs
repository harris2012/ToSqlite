using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSqLite
{
    internal static class ConnectionProvider
    {
        static string mssqlConnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\tmp013\TempFile\HarrisBlog.mdf;Integrated Security=True;Connect Timeout=30";

        static string sqliteConnString = @"Data Source=D:\HarrisBlog.db3;Version=3";

        public static SqlConnection GetMssqlConn()
        {
            var mssqlConn = new SqlConnection(mssqlConnString);
            mssqlConn.Open();

            return mssqlConn;
        }

        public static SQLiteConnection GetSqliteConn()
        {
            var sqliteConn = new SQLiteConnection(sqliteConnString);
            sqliteConn.Open();

            return sqliteConn;
        }
    }
}
