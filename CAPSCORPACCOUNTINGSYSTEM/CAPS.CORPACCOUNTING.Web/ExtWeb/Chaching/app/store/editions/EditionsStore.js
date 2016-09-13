/**
 * DataStore to perform CRUD operation on Editions.
 */
Ext.define('Chaching.store.editions.EditionsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.editions.EditionsModel',   
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
            create: abp.appPath + 'api/services/app/edition/CreateOrUpdateEdition',
            read: abp.appPath + 'api/services/app/edition/GetAllEditions',
            update: abp.appPath + 'api/services/app/edition/CreateOrUpdateEdition',
            destroy: abp.appPath + 'api/services/app/edition/DeleteEdition'
        },
        reader: {
            type: 'json',
            rootProperty: 'result.items',
            totalProperty: 'result.totalCount'
        }
    },
    idPropertyField:'id'
});