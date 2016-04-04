Ext.define('Chaching.view.common.form.ChachingFormPanelController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-form-chachingformpanel',
    //default buttons action handler
    onSaveClicked:function(btn) {
        var me = this,
            view = me.getView(),
            parentGrid = view.parentGrid,
            values = view.getValues();

        if (parentGrid) {
            var gridStore = parentGrid.getStore(),
                idPropertyField = gridStore.idPropertyField,
                operation;
            var record = Ext.create(gridStore.model.$className);
            Ext.apply(record.data, values);
            var myMask = new Ext.LoadMask({
                msg: 'Please wait...',
                target: view
            });

            myMask.show();
            if (values && parseInt(values[idPropertyField]) > 0) {
                operation = Ext.data.Operation({
                    params: record.data,
                    parentGrid: parentGrid,
                    records: [record],
                    controller:me,
                    callback: me.onOperationCompleteCallBack
                });
                gridStore.update(operation);
            } else if (values && parseInt(values[idPropertyField]) === 0) {
                operation = Ext.data.Operation({
                    params: record.data,
                    parentGrid: parentGrid,
                    controller: me,
                    callback: me.onOperationCompleteCallBack
                });
                gridStore.create(values, operation);
            } else {
                myMask.hide();
            }
        }
    },
    onCancelClicked:function(btn) {
        var me = this,
            view = me.getView();
        if (view && view.openInPopupWindow) {
            var wnd = view.up('window');
            Ext.destroy(wnd);
        }
    },
    onOperationCompleteCallBack: function (records, operation, success) {
        if (success) {
            var action = operation.getAction();
            if (action === "create" || action === "update") {
                var gridController = operation.parentGrid.getController();
                gridController.doReloadGrid();

                var controller = operation.controller,
                    view = controller.getView();
                if (view && view.openInPopupWindow) {
                    var wnd = view.up('window');
                    Ext.destroy(wnd);
                }

            }
            Ext.toast('Operation completed successfully.');
        } else {
            var response = Ext.decode(operation.getResponse().responseText);
            var message = '',
                title = 'Error';
            if (response && response.error) {
                title = response.error.message;
                message = response.error.details ? response.error.details : title;
            }
            Ext.toast(message);
        }
    },
    
});
