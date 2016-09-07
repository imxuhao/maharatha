Ext.define('Chaching.store.utilities.autofill.MappingAccountStore', {
    extend: 'Chaching.store.base.BaseStore',
    model:'Chaching.model.utilities.autofill.MappingAccountModel',

    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        urlToGetRecordById: abp.appPath + 'api/services/app/accountUnit/GetAccountById',
        api: {
            read: abp.appPath + 'api/services/app/accountUnit/GetAccountsForMapping',
            create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
            update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
            destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'accountId'//important to set for add/update of records
});


