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
    }

    public class StringColumn : ColumnBase
    {
        public string Content { get; set; }
    }

    public class CheckColumn : ColumnBase
    {
        public CheckColumn(ColumnDefine columnDefine)
        {
            Name = columnDefine.Name;
            
        }

        public List<string> Content { get; set; }
    }

}