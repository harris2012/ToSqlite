using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdf2Sqlite.Sqlite
{
    public class SqliteTable
    {
        public string Name { get; set; }

        public List<SqLiteField> FieldList { get; set; }
    }
}
