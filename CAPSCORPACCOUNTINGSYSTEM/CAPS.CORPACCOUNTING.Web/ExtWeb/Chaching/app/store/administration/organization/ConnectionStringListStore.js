/**
 * DataStore to perform get operation on ConnectionStrings.
 */
Ext.define('Chaching.store.administration.organization.ConnectionStringListStore', {
    extend: 'Chaching.store.base.BaseStore',
    fields: [{ name: 'name' }, { name: 'value', defaultValue : null, handler : nullHandler }, { name: 'connectionStringId', mapping: 'value' }],
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        url: abp.appPath + 'api/services/app/organizationUnit/GetConnectionStrings',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});