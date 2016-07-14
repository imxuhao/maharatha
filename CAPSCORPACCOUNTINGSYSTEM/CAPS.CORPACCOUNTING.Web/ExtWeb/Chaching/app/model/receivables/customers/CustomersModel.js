/**
 * DataModel to represent entity schema for Customers.
 */
Ext.define('Chaching.model.receivables.customers.CustomersModel', {
    extend: 'Chaching.model.base.BaseModel',
    requires: ['Chaching.model.address.AddressModel'],
    config: {
        searchEntityName: 'Customer'
    },
    fields: [
            { name: 'customerId', type: 'int', isPrimaryKey: true },
            { name: 'lastName', type: 'string', headerText: 'LastName', hidden: false, width: '8%', minWidth: 80 },
            { name: 'firstName', type: 'string', headerText: 'FirstName', hidden: false, width: '8%', minWidth: 80 },
            { name: 'customerName', type: 'string', headerText: 'CustomeName', hidden: false, width: '8%', minWidth: 150, convert: function (v, rec) { return (rec.get('firstName') + " " + rec.get('lastName')) } },
            { name: 'customerNumber', type: 'string', headerText: 'CustomerNumber', hidden: false, width: '10%', minWidth: 110 },
            { name: 'creditLimit', type: 'float' },
            { name: 'typeofPaymentMethod', type: "string" },
            { name: 'typeofPaymentMethodId', type: "int", defaultVaule: null, convert: nullHandler },
            { name: 'customerPayTermsId', type: "int", defaultVaule: null, convert: nullHandler },
            { name: 'paymentTermDescription', type: "string" },
            { name: 'salesRepId', type: "int", defaultVaule: null, convert: nullHandler },
            { name: 'salesRepName', type: "string" },
            { name: 'isApproved', type: "boolean"},
            { name: 'isActive', type: "boolean" },
            { name: 'tenantId', type: "int", defaultVaule: null, convert: nullHandler },
            { name: 'organizationUnitId', type: "int", defaultVaule: null, convert: nullHandler },
            {
                name: 'address',
                reference: {
                    parent: 'address.AddressModel'
                }
            }
    ]
});















