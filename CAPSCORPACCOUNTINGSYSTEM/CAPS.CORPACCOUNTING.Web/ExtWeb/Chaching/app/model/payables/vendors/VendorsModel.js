Ext.define('Chaching.model.payables.vendors.VendorsModel', {
    extend: 'Chaching.model.base.BaseModel',
    requires:['Chaching.model.address.AddressModel'],
    config: {
        searchEntityName: 'Vendor'
    },
    fields: [
            { name: 'vendorId', type: 'int', isPrimaryKey: true },
            { name: 'lastName', type: 'string' },
            { name: 'firstName', type: 'string' },
            { name: 'payToName', type: 'string' },
            { name: 'dbaName', type: 'string' },
            { name: 'vendorNumber', type: 'string' },
             { name: 'contactNumber', type: 'string' },
            { name: 'vendorAccountInfo', type: 'string' },
            { name: 'fedralTaxId', type: 'string' },
            { name: 'ssnTaxId', type: 'string' },
            { name: 'creditLimit', type: 'float' },
            { name: 'typeofPaymentMethod', type: "string" },
            { name: 'typeofPaymentMethodId', type: "auto" },
            { name: 'paymentTermsId', type: "int",defaultVaule:null,convert:nullHandler },
            { name: 'typeofCurrency', type: "string" },
            { name: 'typeofCurrencyId', type: "int", defaultVaule: null, convert: nullHandler },
            { name: 'isCorporation', type: "boolean" },
            { name: 'is1099', type: "boolean" },
            { name: 'isIndependentContractor', type: "boolean" },
            { name: 'isw9OnFile', type: "boolean" },
            { name: 'typeofVendorId', type: "auto" },
            { name: 'typeof1099BoxId', type: "auto" },
            { name: 'typeof1099Box', type: "string" },
            { name: 'eddContractStartDate', type: "date" },
            { name: 'eddContractStopDate', type: "date" },
            { name: 'eddConctractAmount', type: 'float' },
            { name: 'workRegion', type: "string" },
            { name: 'iseddContractOnGoing', type: "boolean" },
            { name: 'achBankName', type: "string" },
            { name: 'achRoutingNumber', type: "string" },
            { name: 'achAccountNumber', type: "string" },
            { name: 'achWireFromBankName', type: "string" },
            { name: 'achWireFromBankAddress', type: "string" },
            { name: 'achWireFromSwiftCode', type: "string" },
            { name: 'achWireFromAccountNumber', type: "string" },
            { name: 'achWireToBankName', type: "string" },
            { name: 'achWireToSwiftCode', type: "string" },
            { name: 'achWireToBeneficiary', type: "string" },
            { name: 'achWireToAccountNumber', type: "string" },
            { name: 'achWireToIBAN', type: "string" },
            { name: 'isActive', type: "boolean" },
            { name: 'isApproved', type: "boolean" },
            { name: 'paymentTerms', type: "string" },
            { name: 'billingAccount', type: "string" },
            { name: 'typeofTaxId', type: "int", defaultVaule: null, convert: nullHandler },
            { name: 'taxCreditId', type: "int", defaultVaule: null, convert: nullHandler },
            { name: 'jobId', type: "int", defaultVaule: null, convert: nullHandler },
            { name: 'glAccountId', type: "int", defaultVaule: null, convert: nullHandler },
            { name: 'accountId', type: "int", defaultVaule: null, convert: nullHandler },
            { name: 'notes', type: "string" },
            {
                name: 'address',
                reference: {
                    parent: 'address.AddressModel'
                }
            },
            //{
            //    name: 'vendorAlias',
            //    reference: {
            //        parent: 'payables.vendors.VendorAliasModel'
            //    }
            //}
    ],
});















