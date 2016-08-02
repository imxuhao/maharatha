Ext.define('Chaching.store.utilities.CountryListStore', {
    extend: 'Chaching.store.base.BaseStore',
    remoteSort: false,
    remoteFilter : false,
    fields: [{ name: 'name' }, { name: 'value' }, { name: 'isoCode' }, { name: 'description' }, { name: 'countryId' }, {
        name: 'country', mapping : 'isoCode'
        }],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/vendorUnit/GetCountryList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
