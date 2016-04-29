Ext.define('Chaching.store.financials.accounts.RollupAccountStore', {
    extend: 'Chaching.store.base.BaseStore',   
    fields: [{ name: 'name',type:'string' }, { name: 'value',type:'int' }],
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        extraParams: {
            Id: null
        },
        api: {
            read: abp.appPath + 'api/services/app/accountUnit/GetRollupAccountsList',
        },
        reader: {
            type: 'json',
            rootProperty:'result'
        }
    },
    idPropertyField: 'Id'
});
