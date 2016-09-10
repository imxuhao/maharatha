
Ext.define('Chaching.view.attachments.AttachmentsView',
{
    extend: 'Chaching.view.common.window.ChachingWindowPanel',

    requires: [
        'Chaching.view.attachments.AttachmentsViewController'
    ],

    controller: 'attachments-attachmentsview',
    height: '55%',
    width: '75%',
    title: app.localize('Attachment'),
    iconCls: 'fa fa-dropbox',
    closeAction:'destroy',
    initComponent: function() {
        var me = this;
        me.callParent(arguments);
    },
    buttons: [{
            xtype: 'button',
            scale: 'small',
            iconCls: 'fa fa-close',
            iconAlign: 'left',
            text: app.localize('Close').toUpperCase(),
            ui: 'actionButton',
            name: 'CancelAttachment',
            itemId: 'BtnCancelAttachment',
            reference: 'BtnCancelAttachment',
            listeners: {
                click: 'onCancelClicked'
            }
        }
    ],
    items: [
        {
            xtype: 'form',
            frame: false,
            border: false,
            layout: 'vbox',
            items: [
                {
                    xtype: 'filefield',
                    name: 'attachFileField',
                    clearOnSubmit: false,
                    width: '100%',
                    allowBlank: false,
                    //regex: /(.)+((\.xls)|(\.xlsx)|(\.csv)(\w)?)$/i,
                    //regexText: 'Only excel files are accepted',
                    buttonText: app.localize('SelectFile').toUpperCase(),
                    emptyText: app.localize('Attachment_Select_Info'),
                    buttonConfig: {
                        ui: 'actionButton'
                    },
                    listeners: {
                        change: 'onFileAttached'
                    }
                }, {
                    xtype: 'hiddenfield',
                    name: 'typeOfObjectId'
                }, {
                    xtype: 'hiddenfield',
                    name: 'objectId'
                }, {
                    xtype: 'gridpanel',
                    itemId: 'attachmentsGrid',
                    padding: '10 0 0 0',
                    cls: 'chaching-transactiongrid',
                    width: '100%',
                    border: false,
                    viewConfig: {
                        emptyText: app.localize('NoFilesAttached_Message'),
                        deferEmptyText: false,
                        getRowClass: function (record) {
                            if (record && !record.get('typeOfAttachedObjectId')) {
                                return 'invalid-File-Upload';
                            }
                            return '';
                        }
                    },
                    store: new Chaching.store.attachments.AttachmentsStore(),
                    plugins:[
                    {
                        ptype: 'chachingRowediting',
                        pluginId: 'editingPlugin',
                        clicksToEdit: 2,
                        listeners: {
                            beforeedit:'onAttachmentBeforeEdit',
                            edit: 'onAttachmentEditComplete'
                        }
                    }],
                    columns: [
                        {
                            text: app.localize('FileName'),
                            dataIndex:'fileName',
                            flex: 1
                        }, {
                            text: app.localize('Decription'),
                            dataIndex: 'description',
                            flex: 1,
                            editor: {
                                xtype:'textfield'
                            }
                        }, {
                            text: app.localize('DocType'),
                            dataIndex: 'typeOfAttachedObject',
                            flex: 1,
                            editor: {
                                xtype: 'combo',
                                valueField: 'typeOfAttachedObjectId',
                                displayField: 'typeOfAttachedObject',
                                store: new Chaching.store.attachments.TypeOfAttachmentsStore(),
                                queryMode:'local'
                            }
                        }, {
                            text: app.localize('Size'),
                            dataIndex: 'fileSize',
                            flex: 1,
                            maxWidth:100
                        }, {
                            text: app.localize('AttachedOn'),
                            dataIndex: 'attachedDate',
                            maxWidth:140,
                            renderer: Chaching.utilities.ChachingRenderers.renderDateOnly,
                            flex: 1
                        }, {
                            text: app.localize('AttachedBy'),
                            dataIndex: 'createdUser',
                            maxWidth: 140,
                            flex: 1
                        }, {
                            xtype: 'actioncolumn',
                            dataIndex: 'fileStatus',
                            sortable: false,
                            menuDisabled: true,
                            maxWidth: 25,
                            align: 'center',
                            renderer: Chaching.utilities.ChachingRenderers.fileStatusRenderer,
                            items: [
                            {
                                iconCls: '',
                                tooltip: ''
                            }]
                        }, {
                            xtype: 'actioncolumn',
                            dataIndex: 'fileExtension',
                            sortable: false,
                            menuDisabled:true,
                            maxWidth: 50,
                            align:'center',
                            renderer: Chaching.utilities.ChachingRenderers.genericAttachment,
                            items:[
                            {
                                iconCls: '',
                                tooltip: app.localize('Download'),
                                handler:'onDownloadFileClicked'
                            }]
                        }, {
                            xtype: 'actioncolumn',
                            dataIndex: 'deleteFile',
                            sortable: false,
                            menuDisabled: true,
                            maxWidth: 50,
                            align: 'center',
                            items:[
                            {
                                iconCls: 'deleteCls',
                                tooltip: app.localize('Delete'),
                                handler:'onAttachmentDelete'
                            }]
                        }

                    ],
                    listeners: {
                        drop: {
                            element: 'el',
                            fn: 'drop'
                        },
                        dragstart: {
                            element: 'el',
                            fn: 'addDropZone'
                        },
                        dragenter: {
                            element: 'el',
                            fn: 'addDropZone'
                        },
                        dragover: {
                            element: 'el',
                            fn: 'addDropZone'
                        },
                        dragleave: {
                            element: 'el',
                            fn: 'removeDropZone'
                        },
                        dragexit: {
                            element: 'el',
                            fn: 'removeDropZone'
                        }
                    },
                    noop: function (e) {
                        e.stopEvent();
                    }
                }
            ]
        }
    ]
});
