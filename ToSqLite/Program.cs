using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToSqLite.Mssql;
using ToSqLite.Mssql.Entity;
using ToSqLite.Sqlite;

namespace ToSqLite
{
    class Program
    {
        static void Main(string[] args)
        {
            var mssqlConnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\tmp013\TempFile\HarrisBlog.mdf;Integrated Security=True;Connect Timeout=30";

            var sqliteConnString = @"Data Source=D:\HarrisBlog.db3;Version=3";

            List<Table> tableList = GetTableList(mssqlConnString);

            List<SqliteTable> sqliteTable = ToSqlite(tableList);

            var x = 0;
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

                    sqliteTable.FieldList.Add(sqliteField);
                }

                returnValue.Add(sqliteTable);
            }

            return returnValue;
        }

        private static List<Table> GetTableList(string mssqlConnString)
        {
            List<Table> tableList = null;

            var reader = new MssqlReader();

            using (var sqlConn = new SqlConnection(mssqlConnString))
            {
                sqlConn.Open();

                var tableEntityList = reader.GetTables(sqlConn);
                if (tableEntityList != null && tableEntityList.Count > 0)
                {
                    tableList = ToModel(tableEntityList);
                }
            }

            if (tableList != null && tableList.Count > 0)
            {
                foreach (var table in tableList)
                {
                    using (var sqlConn = new SqlConnection(mssqlConnString))
                    {
                        sqlConn.Open();

                        var fieldEntityList = reader.GetSchema(table.Name, sqlConn);
                        if (fieldEntityList != null && fieldEntityList.Count > 0)
                        {
                            table.FieldList = ToModel(fieldEntityList);
                        }
                    }
                }
            }

            return tableList;
        }

        private static List<Table> ToModel(List<TableEntity> tableEntityList)
        {
            List<Table> returnValue = new List<Table>();

            foreach (var tableEntity in tableEntityList)
            {
                Table table = new Table();

                table.Name = tableEntity.Name;

                returnValue.Add(table);
            }

            return returnValue;
        }

        private static List<Field> ToModel(List<FieldEntity> fieldEntityList)
        {
            List<Field> returnValue = new List<Field>();

            foreach (var fieldEntity in fieldEntityList)
            {
                Field field = new Field();

                field.ColOrder = fieldEntity.ColOrder;
                field.Name = fieldEntity.Name;
                field.IsIdentity = fieldEntity.IsIdentity;
                field.IsPrimaryKey = fieldEntity.IsPrimaryKey;
                field.ColType = fieldEntity.ColType;
                field.ColBytes = fieldEntity.ColBytes;
                field.ColLength = fieldEntity.ColLength;
                field.IsNullable = fieldEntity.IsNullable;
                field.DefaultValue = fieldEntity.DefaultValue;

                returnValue.Add(field);
            }

            return returnValue;
        }
    }
}
