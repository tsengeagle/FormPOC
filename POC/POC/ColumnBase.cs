namespace POC
{
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