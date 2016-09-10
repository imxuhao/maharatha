Ext.define('Chaching.model.attachments.AttachmentsModel', {
    extend: 'Chaching.model.base.BaseModel',
    
    fields: [
        { name: 'attachedObjectId', type: 'int', isPrimaryKey: true },
        { name: 'fileName', type: 'string' },
        { name: 'description', type: 'string' },
        { name: 'fileExtension', type: 'string' },
        { name: 'file', type: 'auto' },
        { name: 'fileStatus', type: 'string', mapping: 'attachedObjectId' },
        { name: 'fileSize', type: 'float' },
        { name: 'typeOfAttachedObject', type: 'string' },
        { name: 'typeOfAttachedObjectId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'typeOfObjectId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'objectId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'attachedDate', type: 'date', dateFormat: 'c' }
    ]
});
