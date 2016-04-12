Ext.define('Chaching.store.linkedaccounts.LinkedAccountsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.linkedaccounts.LinkedAccountsModel',   
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type':'application/json;charset=UTF-8'
        },
        writer: {
            type:'json'
        },
        api: {
            create: abp.appPath + 'api/services/app/userLink/LinkToUserInput',
            read: abp.appPath + 'api/services/app/userLink/GetLinkedUsers',
            destroy: abp.appPath + 'api/services/app/userLink/unlinkUser'
        },
        reader: {
            type: 'json',
            rootProperty: 'result.items',
            totalProperty: 'result.totalCount'
        }
    },
    idPropertyField:'id'
});