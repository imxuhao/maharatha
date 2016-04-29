Ext.define('Chaching.store.projects.projectmaintenance.ProjectCoaStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.financials.accounts.ChartOfAccountsModel',
    //storeId:"coaStore",
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/projectCoaUnit/CreateProjectCoaUnit',
            read: abp.appPath + 'api/services/app/projectCoaUnit/GetProjectCoaList',
            update: abp.appPath + 'api/services/app/projectCoaUnit/UpdateProjectCoaUnit',
            destroy: abp.appPath + 'api/services/app/projectCoaUnit/DeleteProjectCoaUnit'
        }
    },
    idPropertyField: 'coaId'//important to set for add/update of records
});
