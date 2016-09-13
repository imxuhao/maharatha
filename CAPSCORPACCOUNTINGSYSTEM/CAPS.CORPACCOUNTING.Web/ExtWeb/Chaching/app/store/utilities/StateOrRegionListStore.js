Ext.define('Chaching.store.utilities.StateOrRegionListStore', {
    extend: 'Chaching.store.base.BaseStore',
    remoteSort: false,
    remoteFilter : false,
    fields: [{ name: 'regionId' }, { name: 'description' },{ name: 'stateCode' },{
        name: 'state', convert: function (value, record) {
            return record.get('stateCode');
        }
    }, {
        name: 'stateId', convert: function (value, record) {
            return record.get('regionId');
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