using Savory.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToSqLite.Mssql.Entity;

namespace ToSqLite.Mssql
{
    public class MssqlReader
    {
        /// <summary>
        /// 查询单张表结构
        /// </summary>
        private string SELECT_TABLE_FIELD_LIST = @"
SELECT
    col.colorder AS ColOrder,
    col.name AS Name,
    COLUMNPROPERTY(obj.id,col.name,'IsIdentity') AS IsIdentity,
    (case when idxkey.colid = col.colid then 1 else 0 end) IsPrimaryKey,
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
    LEFT JOIN sysindexkeys idxkey
        ON obj.id = idxkey.id
    LEFT JOIN syscomments cmt
        ON col.cdefault = cmt.id
WHERE obj.name=@TableName
";

        /// <summary>
        /// 查找所有表
        /// </summary>
        private string SQL_SELECT_TABLES = "SELECT * FROM sysobjects WHERE xtype='u' ORDER BY Name";

        public List<FieldEntity> GetSchema(string tableName, SqlConnection sqlConn)
        {
            return sqlConn.Query<FieldEntity>(SELECT_TABLE_FIELD_LIST, new { TableName = tableName }).ToList();
        }

        public List<TableEntity>GetTables(SqlConnection sqlConn)
        {
            return sqlConn.Query<TableEntity>(SQL_SELECT_TABLES).ToList();
        }
    }
}
