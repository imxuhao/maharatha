Ext.define('Chaching.store.notes.NotesStore',
{
    extend: 'Ext.data.Store',
    model: 'Chaching.model.notes.NotesModel',
    proxy: {
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/attachedObjectUnit/GetAllAttachedObjectUnit',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/notedObjectUnit/GetNotedObjectForRavi',
            create: abp.appPath + 'api/services/app/notedObjectUnit/CreateNotedObjectUnit',
            update: abp.appPath + 'api/services/app/notedObjectUnit/UpdateNotedObjectUnit',
            destroy: abp.appPath + 'api/services/app/notedObjectUnit/DeleteNotedObjectUnit'
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'notedObjectId'
});
