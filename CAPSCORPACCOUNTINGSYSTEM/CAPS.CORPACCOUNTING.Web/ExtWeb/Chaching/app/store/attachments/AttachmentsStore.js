Ext.define('Chaching.store.attachments.AttachmentsStore',
{
    extend: 'Ext.data.Store',
    model: 'Chaching.model.attachments.AttachmentsModel',

    data: {
        items: [
            { id: 1, filename: 'Jean Luc', filetype: "txt",description:'Dummy Data', creationTime: new Date(), createdUser: 'Krishna Garad' },
            { id: 2, filename: 'Worf', filetype: "xls", description: 'Dummy Data', creationTime: new Date(), createdUser: 'Krishna Garad' },
            { id: 3, filename: 'Deanna', filetype: "mp3", description: 'Dummy Data', creationTime: new Date(), createdUser: 'Krishna Garad' }
        ]
    },

    proxy: {
        type: 'memory',
        reader: {
            type: 'json',
            rootProperty: 'items'
        }
    }
});
