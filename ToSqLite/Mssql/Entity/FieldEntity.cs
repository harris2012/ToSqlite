using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSqLite.Mssql.Entity
{
    public class FieldEntity
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
        /// int
        /// </summary>
        public int IsPrimaryKey { get; set; }

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
