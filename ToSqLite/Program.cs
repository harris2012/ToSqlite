using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToSqLite.Mssql;
using ToSqLite.Sqlite;
using ToSqLite.Template;

namespace ToSqLite
{
    class Program
    {
        static void Main(string[] args)
        {
            MssqlSchemaReader reader = new MssqlSchemaReader();

            List<MssqlTable> mssqlTableList = reader.GetTableList();

            List<SqliteTable> sqliteTableList = DBBridge.ToSqlite(mssqlTableList);
            if (sqliteTableList == null || sqliteTableList.Count == 0)
            {
                return;
            }

            foreach (var mssqlTable in mssqlTableList)
            {
                SelectMssqlDataTemplate template = new SelectMssqlDataTemplate();
                template.MssqlTable = mssqlTable;
                var sql = template.TransformText();

                System.Diagnostics.Debug.WriteLine(sql);
            }

            //CreateSqliteTable(sqliteTableList);
        }

        private static void CreateSqliteTable(List<SqliteTable> sqliteTableList)
        {
            foreach (var sqliteTable in sqliteTableList)
            {
                var template = new CreateSqliteTableTemplate();
                template.SqliteTable = sqliteTable;

                var content = template.TransformText();

                System.Diagnostics.Debug.WriteLine(content);

                using (var sqliteConn = ConnectionProvider.GetSqliteConn())
                {
                    SqliteExecutor.Execute(content, sqliteConn);
                }
            }
        }
    }
}
