using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToSqLite.Mssql;
using ToSqLite.Mssql.Entity;
using ToSqLite.Sqlite;
using ToSqLite.Template;

namespace ToSqLite
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Table> tableList = MssqlAdapter.GetTableList();

            List<SqliteTable> sqliteTableList = ToSqlite(tableList);
            if (sqliteTableList != null && sqliteTableList.Count > 0)
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

        private static List<SqliteTable> ToSqlite(List<Table> tableList)
        {
            List<SqliteTable> returnValue = new List<SqliteTable>();

            foreach (var mssqlTable in tableList)
            {
                SqliteTable sqliteTable = new SqliteTable();

                sqliteTable.Name = mssqlTable.Name;
                sqliteTable.FieldList = new List<SqLiteField>();
                foreach (var mssqlField in mssqlTable.FieldList)
                {
                    SqLiteField sqliteField = new SqLiteField();

                    sqliteField.Name = mssqlField.Name;
                    sqliteField.DataType = GetSqliteFieldType(mssqlField.ColType);
                    sqliteField.IsPrimaryKey = mssqlField.IsPrimaryKey;

                    sqliteTable.FieldList.Add(sqliteField);
                }

                returnValue.Add(sqliteTable);
            }

            return returnValue;
        }

        private static string GetSqliteFieldType(string mssqlFieldType)
        {
            switch (mssqlFieldType)
            {
                case "int":
                case "bigint":
                    return "INT";
                case "nvarchar":
                    return "VARCHAR";
                case "datetime":
                    return "DATETIME";
                default:
                    return "ERROR_FIELD_TYPE";
            }
        }
    }
}
