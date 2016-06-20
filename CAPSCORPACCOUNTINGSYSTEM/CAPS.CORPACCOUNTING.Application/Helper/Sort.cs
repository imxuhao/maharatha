namespace CAPS.CORPACCOUNTING.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// Gets or sets Column ParentTable Name
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// Gets or sets Column Name
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets Sorting Order
        /// </summary>
        public string Order { get; set; }
    }
}