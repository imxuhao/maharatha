Ext.define('Chaching.store.utilities.AddressListStore', {
    extend: 'Chaching.store.base.BaseStore',
    typeofAddressList: {
        fields: [{ name: 'name' }, { name: 'value' }, {
            name: 'typeofAddress', convert: function (value, record) {
                return record.get('name');
            }
        }, {
            name: 'typeofAddressId', convert: function (value, record) {
                return record.get('value');
            }
        }],
        xtype: 'ajax',
        proxy: {
            actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
            type: 'chachingProxy',
            url: abp.appPath + 'api/services/app/vendorUnit/GetTypeofAddressList',
            reader: {
                type: 'json',
                rootProperty: 'result'
            }
        }
    },
    getCountryList: {
        fields: [{ name: 'name' }, { name: 'value' }, {
            name: 'country', convert: function (value, record) {
                return record.get('name');
            }
        }, {
            name: 'countryId', convert: function (value, record) {
                return record.get('value');
            }
        }],
        xtype: 'ajax',
        proxy: {
            actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
            type: 'chachingProxy',
            url: abp.appPath + 'api/services/app/vendorUnit/GetCountryList',
            reader: {
                type: 'json',
                rootProperty: 'result'
            }
        }
    },
    getStateOrRegionList: {
        fields: [{ name: 'name' }, { name: 'value' }, {
            name: 'state', convert: function (value, record) {
                return record.get('name');
            }
        }, {
            name: 'stateId', convert: function (value, record) {
                return record.get('value');
            }
        }],
        xtype: 'ajax',
        proxy: {
            actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
            type: 'chachingProxy',
            url: abp.appPath + 'api/services/app/vendorUnit/GetRegionList',
            reader: {
                type: 'json',
                rootProperty: 'result'
            }
        }
    }
});