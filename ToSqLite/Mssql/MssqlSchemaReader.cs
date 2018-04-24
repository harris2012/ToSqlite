using Savory.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSqLite.Mssql
{
    public class MssqlSchemaReader
    {
        public List<Table> GetTableList()
        {
            List<Table> tableList = null;

            using (var sqlConn = ConnectionProvider.GetMssqlConn())
            {
                var tableEntityList = GetTables(sqlConn);
                if (tableEntityList != null && tableEntityList.Count > 0)
                {
                    tableList = ToModel(tableEntityList);
                }
            }

            if (tableList != null && tableList.Count > 0)
            {
                foreach (var table in tableList)
                {
                    using (var sqlConn = ConnectionProvider.GetMssqlConn())
                    {
                        var fieldEntityList = GetSchema(table.Name, sqlConn);
                        if (fieldEntityList != null && fieldEntityList.Count > 0)
                        {
                            table.FieldList = ToModel(fieldEntityList);
                        }
                    }

                    if (table.FieldList != null && table.FieldList.Count > 0)
                    {
                        using (var sqlConn = ConnectionProvider.GetMssqlConn())
                        {
                            var primaryKeyColumnId = GetPrimaryColumnId(table.Name, sqlConn);
                            var field = table.FieldList.FirstOrDefault(v => v.Name == primaryKeyColumnId);
                            if (field != null)
                            {
                                field.IsPrimaryKey = true;
                            }
                        }
                    }
                }
            }

            return tableList;
        }

        private List<FieldEntity> GetSchema(string tableName, SqlConnection sqlConn)
        {
            return sqlConn.Query<FieldEntity>(SELECT_TABLE_FIELD_LIST, new { TableName = tableName }).ToList();
        }

        private List<TableEntity> GetTables(SqlConnection sqlConn)
        {
            return sqlConn.Query<TableEntity>(SQL_SELECT_TABLES).ToList();
        }

        private string GetPrimaryColumnId(string tableName, SqlConnection sqlConn)
        {
            return sqlConn.QuerySingleOrDefault<string>(SQL_SELECT_PRIMARY_KEY, new { TableName = tableName });
        }

        private static List<Table> ToModel(List<TableEntity> tableEntityList)
        {
            List<Table> returnValue = new List<Table>();

            foreach (var tableEntity in tableEntityList)
            {
                if ("sysdiagrams".Equals(tableEntity.Name))
                {
                    continue;
                }

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
                field.IsIdentity = fieldEntity.IsIdentity == 1;
                field.ColType = fieldEntity.ColType;
                field.ColBytes = fieldEntity.ColBytes;
                field.ColLength = fieldEntity.ColLength;
                field.IsNullable = fieldEntity.IsNullable;
                field.DefaultValue = fieldEntity.DefaultValue;

                returnValue.Add(field);
            }

            return returnValue;
        }

        /// <summary>
        /// 查询单张表结构
        /// </summary>
        private string SELECT_TABLE_FIELD_LIST = @"
SELECT
    col.colorder AS ColOrder,
    col.name AS Name,
    COLUMNPROPERTY(obj.id,col.name,'IsIdentity') AS IsIdentity,
    tps.name AS ColType,
    col.length AS ColBytes,
    COLUMNPROPERTY(obj.id,col.name,'PRECISION') AS ColLength,
    col.isnullable AS IsNullable,
    isnull(cmt.text,'') AS DefaultValue
FROM syscolumns col
    INNER JOIN sysobjects obj
        ON obj.id = col.id
    INNER JOIN systypes tps
        ON col.xtype=tps.xusertype
    LEFT JOIN syscomments cmt
        ON col.cdefault = cmt.id
WHERE obj.name=@TableName
";

        /// <summary>
        /// 查找所有表
        /// </summary>
        private string SQL_SELECT_TABLES = "SELECT * FROM sysobjects WHERE xtype='u' ORDER BY Name";

        /// <summary>
        /// 查找主键列
        /// </summary>
        private string SQL_SELECT_PRIMARY_KEY = @"
SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME= @TableName
";

        class TableEntity
        {
            public string Name { get; set; }
        }

        class FieldEntity
        {
            /// <summary>
            /// smallint
            /// </summary>
            public short ColOrder { get; set; }

            /// <summary>
            /// nvarchar(128)
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// int
            /// </summary>
            public int IsIdentity { get; set; }

            /// <summary>
            /// nvarchar(128)
            /// </summary>
            public string ColType { get; set; }

            /// <summary>
            /// smallint
            /// </summary>
            public short ColBytes { get; set; }

            /// <summary>
            /// int
            /// </summary>
            public int ColLength { get; set; }

            /// <summary>
            /// int
            /// </summary>
            public int IsNullable { get; set; }

            /// <summary>
            /// nvarchar(4000)
            /// </summary>
            public string DefaultValue { get; set; }
        }
    }
}
