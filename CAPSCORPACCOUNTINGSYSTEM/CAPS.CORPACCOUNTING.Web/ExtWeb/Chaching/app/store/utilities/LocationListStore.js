/**
 * This class is created as a utility store to load locations list
 * Author: Krishna Garad
 * Date Created: 12/05/2016
 */
/**
 * @class Chaching.store.utilities.LocationListStore
 * Utility Store for LocationList
 */
Ext.define('Chaching.store.utilities.LocationListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value' }, {
        name: 'locationName', convert: function (value, record) {
            return record.get('name');
        }
    }, {
        name: 'locationId', convert: function (value, record) {
            return record.get('value');
        }
    }],
    proxy: {
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/locationSetUnit/GetLocationList',
        extraParams: {
            locationSetTypeId: 1
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
