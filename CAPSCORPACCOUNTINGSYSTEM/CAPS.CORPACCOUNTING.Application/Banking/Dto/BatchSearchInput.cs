namespace CAPS.CORPACCOUNTING.Banking.Dto
{
    public class BatchSearchInput
    {
        /// <summary>
        /// dropdown keyword search
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        ///set TypeOfBatch id to search 
        /// </summary>
        public TypeOfBatch TypeOfBatchId { get; set; }
    }
}
