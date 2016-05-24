Ext.define('Chaching.store.payables.invoices.BatchStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.payables.invoices.BatchModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
      
        api: {
            create: abp.appPath + 'api/services/app/batchUnit/CreateBatchUnit',
            read: abp.appPath + 'api/services/app/batchUnit/GetBatchUnits',
            update: abp.appPath + 'api/services/app/batchUnit/UpdateBatchUnit',
            destroy: abp.appPath + 'api/services/app/batchUnit/DeleteBatchUnitt'
        }
    },
    idPropertyField: 'batchId',//important to set for add/update of records
    groupField: 'typeOfBatch',
    //groupHeaderTpl: '{ typeOfBatch }'
 });