namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class CustomerAndAddressDto
    {
        public CustomerUnit Customer { get; set; }

        public AddressUnit Address { get; set; }

       public string PaymentTerms { get; set; }

        public string SalesRepName { get; set; }
    }
}