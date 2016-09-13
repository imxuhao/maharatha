Ext.define('Chaching.store.utilities.PositivePayFileListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'positivePayTypeOfUploadFile', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'positivePayTypeOfUploadFileId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/bankAccountUnit/GetPositivePayList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
