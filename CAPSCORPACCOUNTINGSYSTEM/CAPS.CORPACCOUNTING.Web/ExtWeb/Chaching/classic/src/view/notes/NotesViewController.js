Ext.define('Chaching.view.notes.NotesViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.notes-notesview',
    onCancelClicked: function () {
        Ext.destroy(this.getView());
    },
    onNotesAddClicked: function () {
        var me = this,
            view = me.getView(),
            form = view.down('form').getForm(),
            values = form.getValues(),
            notesDataView = view.down('dataview'),
            notesStore = notesDataView.getStore(),
            record = Ext.create('Chaching.model.notes.NotesModel',
            {
                id: 0,
                objectId: values.objectId,
                notedObjectId: 0,
                typeOfObjectId: values.typeOfObjectId,
                notes: values.notes
            });

        record.id = 0;
        record.set('id', 0);
        var operation = Ext.data.Operation({
            params: record.data,
            controller: me,
            callback: me.onNoteSaveCallBack
        });
        notesStore.create(record.data, operation);
    },
    onNoteSaveCallBack: function (records, operation, success) {
        var controller = operation.controller,
            view = controller.getView(),
            notesDataView = view.down('dataview'),
            notesStore = notesDataView.getStore();

        if (success) {
            notesStore.reload();
            var form = view.down('form').getForm(),
                notes = form.findField('notes');
            notes.reset(true);
            abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
        } else {
            ChachingGlobals.showErrorMessage(operation);
        }
    },
    doNotesAction:function(dataView,record,item,rowIdx,e,eOpts) {
        var me=this,
            clickTarget = e.getTarget(),
            button = clickTarget.nodeName,
            name,
            notesStore = dataView.getStore();
        if (clickTarget.attributes && clickTarget.attributes.name) {
            name = clickTarget.attributes.name.value;
        }

        if (button === "P" && name === "editNote") {
            var mb=Ext.MessageBox.show({
                title: app.localize('EditNote'),
                value: record.get('notes'),
                buttons: Ext.MessageBox.YESNO,
                buttonText: {                        
                    yes: app.localize('Save').toUpperCase(),
                    no: app.localize('Cancel').toUpperCase()
                },
                multiline: true,
                width: 400,
                prompt: {
                    xtype: 'textareafield',
                    ui: 'fieldLabelTop',
                    value: record.get('notes')
                },
                fn: function(btn, text) {
                    switch (btn) {
                        case "yes":
                            record.set('notes', text);
                            var operation = Ext.data.Operation({
                                params: record.data,
                                records: [record],
                                controller: me,
                                callback: me.onNoteSaveCallBack
                            });
                            notesStore.update(operation);
                        break;
                        default:
                            break;
                    }
                },
                animateTarget: clickTarget
            });
            mb.mon(Ext.getBody(),
                'click',
                function(el, e) {
                    this.close(this.closeAction);
                },
                mb,
                { delegate: '.x-mask' });
        } else if (button === "P" && name === "deleteNote") {
            abp.message.confirm(
                app.localize('DeleteWarningMessage'),
                function(isConfirmed) {
                    if (isConfirmed) {
                        var operation = Ext.data.Operation({
                            params: { id: record.get('id') },
                            controller: me,
                            action: 'destroy',
                            records: [record],
                            callback: me.onNoteSaveCallBack
                        });
                        notesStore.erase(operation);
                    }
                });
        }
    }
});
