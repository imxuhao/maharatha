Ext.define('Chaching.model.attachments.AttachmentsModel', {
    extend: 'Chaching.model.base.BaseModel',
    
    fields: [
        { name: 'id', type: 'int', isPrimaryKey: true },
        { name: 'filename', type: 'string' },
        { name: 'description', type: 'string' },
        { name: 'filetype', type: 'string' },
        { name: 'file', type: 'auto' },
        { name: 'filestatus', type: 'string', mapping: 'id' }
    ]
});
