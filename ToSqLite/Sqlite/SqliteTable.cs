using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSqLite.Sqlite
{
    public class SqliteTable
    {
        public string Name { get; set; }

        public List<SqLiteField> FieldList { get; set; }
    }
}
