Ext.define('Chaching.model.customers.CustomersModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'Customers'
    },

    fields: [
        { name: 'customerId', type: 'int', isPrimaryKey: true },
        { name: 'lastName', type: 'string' },
        { name: 'firstName', type: 'string' },
        { name: 'customerNumber', type: 'string' },
        { name: 'creditLimit', type: 'float' },
        { name: 'typeofPaymentMethodId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'customerPayTermsId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'salesRepId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isApproved', type: 'boolean' },
        {
            name: 'agencyId',
            type: 'int',
            convert: function(val, record) {
                val = record.get('customerId');
                if (val) {
                    return val;
                }
                return null;
            }
        },
        {
            name: 'agency',
            type: 'string',
            convert: function (val, record) {
                val = record.get('lastName');
                if (val) {
                    return val;
                }
                return null;
            }
        },
        { name: 'isActive', type: 'boolean' },
        {
            name: 'addresses',
            reference: {
                parent: 'address.AddressModel'
            }
        }
    ]
});
