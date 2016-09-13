namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class CustomerAndAddressDto
    {
        /// <summary>Gets or sets the CustomerUnit. </summary>
        public CustomerUnit Customer { get; set; }

        /// <summary>Gets or sets the AddressUnit. </summary>
        public AddressUnit Address { get; set; }

        /// <summary>Gets or sets the PaymentTerms field. </summary>
        public string PaymentTerms { get; set; }

        /// <summary>Gets or sets the SalesRepName field. </summary>
        public string SalesRepName { get; set; }
    }
}