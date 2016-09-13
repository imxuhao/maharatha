using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class VendorAndAddressDto
    {
        /// <summary>Gets or sets the VendorUnit.</summary>
        public VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the AddressUnit.</summary>
        public AddressUnit Address { get; set; }
        
        /// <summary>Gets or sets the AddressUnit.</summary>
        public IList<VendorAliasUnit> VendorAlias { get; set; }

        /// <summary>Gets or sets the PaymentTerms field.</summary>
        public string PaymentTerms { get; set; }
    }
}