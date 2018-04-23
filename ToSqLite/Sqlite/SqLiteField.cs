using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSqLite.Sqlite
{
    public class SqLiteField
    {
        public string Name { get; set; }

        public string DataType { get; set; }

        public int Length { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsNotNull { get; set; }

        public bool IsIdentity { get; set; }
    }
}
