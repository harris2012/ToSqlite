using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdf2Sqlite.Mssql
{
    public class MssqlTable
    {
        public string Name { get; set; }

        public List<MssqlField> FieldList { get; set; }
    }
}
