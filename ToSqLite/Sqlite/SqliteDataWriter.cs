using Savory.Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSqLite.Sqlite
{
    class SqliteDataWriter
    {
        public int WriteEntityList(string sql, List<dynamic> entityList, SQLiteConnection sqliteConn)
        {
            return sqliteConn.Execute(sql, entityList);
        }
    }
}
