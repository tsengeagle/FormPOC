using System.Collections.Generic;

namespace POC
{
    public class FormBase
    {
        public string Name { get; set; }
        public IEnumerable<ColumnBase> Contents { get; set; }
    }

    public class ColumnBase
    {
        public string Name { get; set; }
        public string ColumnType { get; set; }
    }

    public class StringColumn : ColumnBase
    {
    }

    public class CheckColumn : ColumnBase
    {

    }

}