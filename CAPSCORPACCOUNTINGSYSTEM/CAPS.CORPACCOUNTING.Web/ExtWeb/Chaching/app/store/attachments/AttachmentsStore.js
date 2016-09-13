Ext.define('Chaching.store.attachments.AttachmentsStore',
{
    extend: 'Ext.data.Store',
    model: 'Chaching.model.attachments.AttachmentsModel',
    proxy: {
        type: 'chachingProxy',
        url: abp.appPath + 'api/services/app/attachedObjectUnit/GetAllAttachedObjectUnit',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/attachedObjectUnit/GetAllAttachedObjectUnit',
            create: abp.appPath + 'Attachment/UploadAttachment',
            update: abp.appPath + 'api/services/app/attachedObjectUnit/UpdateAttachedOjectUnit',
            destroy: abp.appPath + 'api/services/app/attachedObjectUnit/DeleteAttachedObjectUnit'
        },
        reader: {
            type: 'json',
            rootProperty: 'result.attachedObjectUnitList'
        }
    },
    idPropertyField: 'attachedObjectId'
});
