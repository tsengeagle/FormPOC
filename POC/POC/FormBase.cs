using System.Collections.Generic;

namespace POC
{
    public class FormBase
    {
        public string Name { get; set; }
        public IEnumerable<ColumnBase> Contents { get; set; }
    }
}