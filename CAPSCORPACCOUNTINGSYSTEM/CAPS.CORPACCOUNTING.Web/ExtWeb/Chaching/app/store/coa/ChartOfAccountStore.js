Ext.define('Chaching.store.coa.ChartOfAccountStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.coa.ChartOfAccountModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/coaUnit/CreateCoaUnit',
            read: abp.appPath + 'api/services/app/coaUnit/GetCoaUnits',
            update: abp.appPath + 'api/services/app/coaUnit/UpdateCoaUnit',
            destroy: abp.appPath + 'api/services/app/coaUnit/DeleteCoaUnit'
        }
    },
    idPropertyField: 'coaId'//important to set for add/update of records
});
