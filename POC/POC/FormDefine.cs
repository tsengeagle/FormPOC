using System.Collections.Generic;

namespace POC
{
    public class FormDefine
    {
        public string Name { get; set; }

        public IEnumerable<ColumnDefine> ColumnDefine { get; internal set; }
    }

    public class ColumnDefine
    {
        public string Name { get; set; }
        public string FormName { get; set; }
        public string ColumnType { get; set; }
        public string DefaultContent { get; set; }
    }
}