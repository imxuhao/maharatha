namespace CAPS.CORPACCOUNTING.Helpers
{
    public class Filters
    {
        public string Entity { get; set; }

        public string Property { get; set; }
        public string SearchTerm { get; set; }
        public int Comparator { get; set; }
        public string SearchTerm2 { get; set; }

        public DataTypes DataType { get; set; }

        public bool IsMultiRange { get; set; } = false;
    }
}