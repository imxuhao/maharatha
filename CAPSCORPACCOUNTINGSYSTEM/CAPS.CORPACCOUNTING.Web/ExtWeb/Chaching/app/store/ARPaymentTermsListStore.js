Ext.define('Chaching.store.ARPaymentTermsListStore', {
    extend: 'Chaching.store.base.BaseStore',
    remoteSort: false,
    remoteFilter: false,
    autoLoad: false,
    fields: [
        { name: 'customerPaymentTermId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'paymentInstruction', type: 'string' },
        { name: 'description', type: 'string' },
        { name: 'discountPercent', type: 'string' },
        { name: 'overnightInstructions', type: 'string' },
        { name: 'wiringInstructions', type: 'string' },
        { name: 'footerMessage', type: 'string' },
        { name: 'logoCaption', type: 'string' },
        { name: 'isDefault', type: 'boolean' },
        { name: 'dueDays', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'discountDays', type: 'int', defaultValue: null, convert: nullHandler },
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
            read: abp.appPath + 'api/services/app/customerPaymentTermUnit/GetCustomerPayTerms'
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});