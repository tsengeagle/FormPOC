using System.Collections.Generic;

namespace POC
{
    public class FormDefine
    {
        public string Name { get; set; }

        public IEnumerable<ColumnDefine> ColumnDefine { get; internal set; }
    }
}