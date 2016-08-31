/**
 * DataStore to perform CRUD operation on Classifications.
 */
Ext.define('Chaching.store.financials.accounts.ClassificationsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.financials.accounts.ClassificationsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            create: abp.appPath + 'api/services/app/classificationUnit/CreateClassificationUnit',
            read: abp.appPath + 'api/services/app/classificationUnit/GetClassificationUnits',
            update: abp.appPath + 'api/services/app/classificationUnit/UpdateClassificationUnit',
            destroy: abp.appPath + 'api/services/app/classificationUnit/DeleteClassificationUnit'
        }
    },
    idPropertyField: 'typeOfAccountId'//important to set for add/update of records
});
