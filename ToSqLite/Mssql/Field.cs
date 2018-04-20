using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSqLite.Mssql
{
    public class Field
    {
        public short ColOrder { get; set; }

        public string Name { get; set; }

        public int IsIdentity { get; set; }

        public int IsPrimaryKey { get; set; }

        public string ColType { get; set; }

        public short ColBytes { get; set; }

        public int ColLength { get; set; }

        public int IsNullable { get; set; }

        public string DefaultValue { get; set; }
    }
}
