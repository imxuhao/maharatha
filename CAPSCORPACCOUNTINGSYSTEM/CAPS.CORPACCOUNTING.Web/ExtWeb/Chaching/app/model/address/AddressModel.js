Ext.define('Chaching.model.address.AddressModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'Address'
    },
    fields: [
        { name: 'addressId', type: 'int', isPrimaryKey: true },
        { name: 'objectId', type: 'int' },
        { name: 'typeofObjectId', type: 'auto' },
        { name: 'addressTypeId', type: 'auto' },
        { name: 'contactNumber', type: 'string' },
        { name: 'line1', type: 'string' },
        { name: 'line2', type: 'string' },
        { name: 'line3', type: 'string' },
        { name: 'line4', type: 'string' },
        { name: 'city', type: 'string' },
        { name: 'state', type: 'string' },
        { name: 'country', type: "string" },
        { name: 'postalCode', type: "string" },
        { name: 'fax', type: "string" },
        { name: 'email', type: "string" },
        { name: 'phone1', type: "string" },
        { name: 'phone1Extension', type: "string" },
        { name: 'phone2', type: "string" },
        { name: 'phone2Extension', type: "string" },
        { name: 'website', type: "string" },
        { name: 'isPrimary', type: "boolean" }
    ],
    //belongsTo: 'Chaching.model.payables.vendors.VendorsModel'
});