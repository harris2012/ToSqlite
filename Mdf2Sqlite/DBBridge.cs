﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mdf2Sqlite.Mssql;
using Mdf2Sqlite.Sqlite;

namespace Mdf2Sqlite
{
    class DBBridge
    {
        public static List<SqliteTable> Mdf2Sqlite(List<MssqlTable> tableList)
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
                    sqliteField.IsIdentity = mssqlField.IsIdentity;

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
                    return "INTEGER";
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
