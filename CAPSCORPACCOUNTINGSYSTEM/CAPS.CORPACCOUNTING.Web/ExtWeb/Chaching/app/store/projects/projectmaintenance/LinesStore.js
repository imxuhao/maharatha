﻿/**
 * DataStore to perform CRUD operation on Job/Projects lines.
 */
Ext.define('Chaching.store.projects.projectmaintenance.LinesStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.financials.accounts.AccountsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/linesUnit/CreateLineUnit',
            read: abp.appPath + 'api/services/app/linesUnit/GetLinesByCoaId',
            update: abp.appPath + 'api/services/app/linesUnit/UpdateLineUnit',
            destroy: abp.appPath + 'api/services/app/linesUnit/DeleteLineUnit'
        }
    },
    idPropertyField: 'accountId'//important to set for add/update of records
});
