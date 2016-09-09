/**
 * DataStore to get type of attachment objects.
 */
Ext.define('Chaching.store.attachments.TypeOfAttachmentsStore',
{
    extend: 'Chaching.store.base.BaseStore',
    fields: [
        {
            name: 'name'
        }, { name: 'value' }, {
            name: 'typeOfAttachedObjectId', convert: function (value, record) {
                return record.get('value');
            }
        },
        {
            name: 'typeOfAttachedObject', convert: function (value, record) {
                return record.get('name');
            }
        }
    ],
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        url: abp.appPath + 'api/services/app/attachedObjectUnit/GetTypeofAttachedObjectList',
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    }
});
