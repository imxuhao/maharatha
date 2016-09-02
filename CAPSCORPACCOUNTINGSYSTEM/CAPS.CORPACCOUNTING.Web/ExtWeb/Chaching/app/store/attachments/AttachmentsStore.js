Ext.define('Chaching.store.attachments.AttachmentsStore',
{
    extend: 'Ext.data.Store',
    model: 'Chaching.model.attachments.AttachmentsModel',

    /*data: {
        items: [
            { id: 1, filename: 'Jean Luc', filetype: "txt",description:'Dummy Data', creationTime: new Date(), createdUser: 'Krishna Garad' },
            { id: 2, filename: 'Worf', filetype: "xls", description: 'Dummy Data', creationTime: new Date(), createdUser: 'Krishna Garad' },
            { id: 3, filename: 'Deanna', filetype: "mp3", description: 'Dummy Data', creationTime: new Date(), createdUser: 'Krishna Garad' },
            {
                id: 4,
                filename: 'Data',
                filetype: "doc",
                description: 'Dummy Data',
                creationTime: new Date(),
                createdUser: 'Krishna Garad'
            },
            {
                id: 5,
                filename: 'Jean Luc',
                filetype: "ppt",
                description: 'Dummy Data',
                creationTime: new Date(),
                createdUser: 'Krishna Garad'
            },
            {
                id: 6,
                filename: 'Worf',
                filetype: "zip",
                description: 'Dummy Data',
                creationTime: new Date(),
                createdUser: 'Krishna Garad'
            },
            {
                id: 7,
                filename: 'Deanna',
                filetype: "png",
                description: 'Dummy Data',
                creationTime: new Date(),
                createdUser: 'Krishna Garad'
            },
            {
                id: 8,
                filename: 'Data',
                filetype: "html",
                description: 'Dummy Data',
                creationTime: new Date(),
                createdUser: 'Krishna Garad'
            },
            {
                id: 9,
                filename: 'Jean Luc',
                filetype: "jpg",
                description: 'Dummy Data',
                creationTime: new Date(),
                createdUser: 'Krishna Garad'
            },
            {
                id: 10,
                filename: 'Worf',
                filetype: "rar",
                description: 'Dummy Data',
                creationTime: new Date(),
                createdUser: 'Krishna Garad'
            },
            {
                id: 11,
                filename: 'Deanna',
                filetype: "xls",
                description: 'Dummy Data',
                creationTime: new Date(),
                createdUser: 'Krishna Garad'
            },
            {
                id: 12,
                filename: 'Data',
                filetype: "xlsx",
                description: 'Dummy Data',
                creationTime: new Date(),
                createdUser: 'Krishna Garad'
            },
            {
                id: 13,
                filename: 'Jean Luc',
                filetype: "txt",
                description: 'Dummy Data',
                creationTime: new Date(),
                createdUser: 'Krishna Garad'
            },
            {
                id: 14,
                filename: 'Worf',
                filetype: "pdf",
                description: 'Dummy Data',
                creationTime: new Date(),
                createdUser: 'Krishna Garad'
            }
        ]
    },*/

    proxy: {
        type: 'memory',
        reader: {
            type: 'json',
            rootProperty: 'items'
        }
    }
});
