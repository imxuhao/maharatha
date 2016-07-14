/**
 * DataModel to represent entity schema for Vendor Alias.
 */
Ext.define('Chaching.model.payables.vendors.VendorAliasModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'VendorAlias'
    }, fields: [
        { name: 'vendorAliasId', type: 'int',isPrimaryKey:true },
        { name: 'vendorId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'aliasName', type: 'string' },
         //{ name: 'vendorId', reference: 'vendors.VendorsModel' }
    ],
    belongsTo: 'payables.vendors.VendorsModel'
});