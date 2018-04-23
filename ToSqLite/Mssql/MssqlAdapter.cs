using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToSqLite.Mssql.Entity;

namespace ToSqLite.Mssql
{
    public class MssqlAdapter
    {
        public static List<Table> GetTableList()
        {
            List<Table> tableList = null;

            var reader = new MssqlReader();

            using (var sqlConn = ConnectionProvider.GetMssqlConn())
            {
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
                    using (var sqlConn = ConnectionProvider.GetMssqlConn())
                    {
                        var fieldEntityList = reader.GetSchema(table.Name, sqlConn);
                        if (fieldEntityList != null && fieldEntityList.Count > 0)
                        {
                            table.FieldList = ToModel(fieldEntityList);
                        }
                    }

                    if (table.FieldList != null && table.FieldList.Count > 0)
                    {
                        using (var sqlConn = ConnectionProvider.GetMssqlConn())
                        {
                            var primaryKeyColumnId = reader.GetPrimaryColumnId(table.Name, sqlConn);
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
    }
}
