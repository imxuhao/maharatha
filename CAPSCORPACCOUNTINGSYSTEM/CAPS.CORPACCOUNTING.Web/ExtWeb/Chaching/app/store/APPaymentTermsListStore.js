Ext.define('Chaching.store.APPaymentTermsListStore', {
    extend: 'Chaching.store.base.BaseStore',
    remoteSort: false,
    remoteFilter: false,
    autoLoad: false,
    fields: [
        { name: 'id', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'vendorPaymentTermId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'setDefaultAPTerms', mapping: 'vendorPaymentTermId' },
        { name: 'description', type: 'string' },
        { name: 'dueDays', type: 'int', defaultValue : null, convert : nullHandler },
        { name: 'discountDays',  type: 'int', defaultValue : null, convert : nullHandler },
        { name: 'isActive', type: 'boolean' },
        { name: 'tenantId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'organizationUnitId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'lastModificationTime', type: 'date' },
        { name: 'lastModifierUserId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creationTime', type: 'date' },
        { name: 'creatorUserId', type: 'int', defaultValue: null, convert: nullHandler }

    ],
    proxy: {
        type: 'chachingProxy',
        actionMethods: { read: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/vendorPaymentTermUnit/GetVendorPayTerms'
        },
        reader: {
            type: 'json',
            rootProperty : 'result'
        }
    }
});