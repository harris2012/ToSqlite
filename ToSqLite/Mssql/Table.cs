using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSqLite.Mssql
{
    public class Table
    {
        public string Name { get; set; }

        public List<Field> FieldList { get; set; }
    }
}
