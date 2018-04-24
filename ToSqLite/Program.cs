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
            MssqlSchemaReader mssqlSchemaReader = new MssqlSchemaReader();
            MssqlDataReader mssqlDataReader = new MssqlDataReader();
            SqliteDataWriter sqliteDataWriter = new SqliteDataWriter();

            List<MssqlTable> mssqlTableList = mssqlSchemaReader.GetTableList();

            List<SqliteTable> sqliteTableList = DBBridge.ToSqlite(mssqlTableList);

            //CreateSqliteTable(sqliteTableList);

            foreach (var mssqlTable in mssqlTableList)
            {
                string mssqlSelectSql = null;
                {
                    SelectFromMssqlTemplate template = new SelectFromMssqlTemplate();
                    template.MssqlTable = mssqlTable;
                    mssqlSelectSql = template.TransformText();

                    System.Diagnostics.Debug.WriteLine(mssqlSelectSql);
                }

                string sqliteInsertSql = null;
                {
                    InsertToSqliteTemplate template = new InsertToSqliteTemplate();
                    template.MssqlTable = mssqlTable;
                    sqliteInsertSql = template.TransformText();

                    System.Diagnostics.Debug.WriteLine(sqliteInsertSql);
                }

                List<dynamic> mssqlEntityList = mssqlDataReader.ReadEntityList(mssqlSelectSql, ConnectionProvider.GetMssqlConn());

                sqliteDataWriter.WriteEntityList(sqliteInsertSql, mssqlEntityList, ConnectionProvider.GetSqliteConn());
            }
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
