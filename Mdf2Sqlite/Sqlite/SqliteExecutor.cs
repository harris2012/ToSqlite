using Savory.Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdf2Sqlite.Sqlite
{
    class SqliteExecutor
    {
        public static int Execute(string sql, SQLiteConnection sqlConn)
        {
            return sqlConn.Execute(sql);
        }
    }
}
