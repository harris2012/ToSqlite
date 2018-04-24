using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSqLite.Mssql
{
    public class MssqlField
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
        public bool IsIdentity { get; set; }

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

        public bool IsPrimaryKey { get; set; }
    }
}
