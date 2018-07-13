using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdf2Sqlite
{
    internal static class ConnectionProvider
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mdfFilePath">G:\tmp\RantaFavorite.mdf</param>
        /// <returns></returns>
        public static SqlConnection GetMssqlConn(string mdfFilePath)
        {
            string mssqlConnString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={mdfFilePath};Integrated Security=True;Connect Timeout=30";

            var mssqlConn = new SqlConnection(mssqlConnString);
            mssqlConn.Open();

            return mssqlConn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqliteFilePath">G:\tmp\RantaFavorite.db3</param>
        /// <returns></returns>
        public static SQLiteConnection GetSqliteConn(string sqliteFilePath)
        {
            string sqliteConnString = $"Data Source={sqliteFilePath};Version=3";

            var sqliteConn = new SQLiteConnection(sqliteConnString);
            sqliteConn.Open();

            return sqliteConn;
        }
    }
}
