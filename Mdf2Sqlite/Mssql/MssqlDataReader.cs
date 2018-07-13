using Savory.Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdf2Sqlite.Mssql
{
    class MssqlDataReader
    {
        public List<dynamic> ReadEntityList(string sql, SqlConnection sqlConn)
        {
            return sqlConn.Query(sql).ToList();
        }
    }
}
