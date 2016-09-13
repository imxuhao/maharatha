
Ext.define('Chaching.view.notes.NotesView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',

    requires: [
        'Chaching.view.notes.NotesViewController'
    ],
    height: '80%',
    width: '60%',
    title: app.localize('Notes'),
    iconCls: 'fa fa-sticky-note-o',
    closeAction: 'destroy',
    controller: 'notes-notesview',
    hideOnMaskTap:false,
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'form',
                frame: false,
                border: false,
                height: '100%',
                width: '100%',
                layout: 'vbox',
                scrollable: false,
                items: [
                {
                    xtype: 'fieldcontainer',
                    layout: 'hbox',
                    width: '100%',
                    items: [{
                        xtype: 'hiddenfield',
                        name: 'typeOfObjectId'
                    }, {
                        xtype: 'hiddenfield',
                        name: 'objectId'
                    }, {
                        xtype: 'textareafield',
                        name: 'notes',
                        ui: 'fieldLabelTop',
                        width: '87%',
                        emptyText: app.localize('NewNotesInfoText'),
                        allowBlank: false,
                        itemId: 'notes'
                    }, {
                        xtype: 'label',
                        width: '1%'
                    }, {
                        xtype: 'button',
                        text: app.localize('Add').toUpperCase(),
                        iconCls: 'fa fa-plus',
                        width: '8%',
                        ui: 'actionButton',
                        itemId: 'addNotesBtn',
                        formBind: true,
                        listeners: {
                            click: 'onNotesAddClicked'
                        }
                    }]
                }, {
                    xtype: 'dataview',
                    width: '100%',
                    scrollable: 'y',
                    itemId: 'notesDataview',
                    store: Ext.create('Chaching.store.notes.NotesStore'),
                    tpl: [
                 '<tpl for=".">',
                       '<div class="col-xs-12">',
                        '<div class="alert bg-grey-cararra bg-font-grey-cararra">',
                        //created by
                                   '<label class="control-label col-sm-3" style="width:15% !important;">' + app.localize('CreatedBy') + ':</label>',
                                    '<p class="form-control-static" style="padding-top:0px !important;text-align:justify; font-weight:500;">',
       '{createdUser}',
       '</p>',
       //created date
        '<label class="control-label col-sm-3" style="width:15% !important;" >' + app.localize('CreationTime') + ':</label>',
                                    '<p class="form-control-static" style="padding-top:0px !important;text-align:justify;font-weight:500;">',
       '{notedOn}',
       '</p>',
       //notes
        '<label class="control-label col-sm-3" style="width:15% !important;">' + app.localize('Note') + ':</label>',
                                    '<p class="form-control-static" style="word-wrap:break-word;padding-top:0px !important;text-align:justify;font-weight:500;">',
       '{notes}',
       '</p>',
       '<tpl if="this.allowDelete(allowDelete)">' +
                                '<p class="fa fa-trash" title="' + app.localize('Delete') + '" style="margin-top: 0px !important;cursor:pointer; color:red;float:right;padding-left:5px;font-size:17px;font-weight:600;" name="deleteNote"/>',
                                '</tpl>' +
        '<tpl if="this.allowEditing(allowEdit)">' +
                              '<p class="fa fa-edit" title="' + app.localize('Edit') + '" style="margin-top: 0px !important;cursor:pointer; color:#2913d2;float:right;padding-left:5px;font-size:17px;font-weight:500;" name="editNote"/>',
                                '</tpl>' +
      
       
       '</div>', '</div>', '</tpl>',
                        {
                            allowEditing:function(allowEdit) {
                                return allowEdit;
                            },
                            allowDelete:function(allowDelete) {
                                return allowDelete;
                            }
                        }
                    ],
                    itemSelector: 'div.col-xs-12',
                    listeners: {
                        'itemclick': 'doNotesAction'
                    }
                }]

            }
        ];
        me.bbar = ['->',
            {
                xtype: 'button',
                scale: 'small',
                iconCls: 'fa fa-close',
                iconAlign: 'left',
                text: app.localize('Close').toUpperCase(),
                ui: 'actionButton',
                name: 'CancelNotes',
                itemId: 'BtnCancelNotes',
                listeners: {
                    click: 'onCancelClicked'
                }
            }
        ];
        me.callParent(arguments);
    }
});
