Ext.define('Chaching.store.utilities.StateOrRegionListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'state', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'stateId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/vendorUnit/GetRegionList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});