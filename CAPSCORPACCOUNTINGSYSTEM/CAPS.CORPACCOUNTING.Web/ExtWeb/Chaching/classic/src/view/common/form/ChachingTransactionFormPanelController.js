Ext.define('Chaching.view.common.form.ChachingTransactionFormPanelController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-form-chachingtransactionformpanel',
    //use the function to set default values
    setDefaultValues: function () { },
    //event handlers
    onSaveClicked:function() {
        var me = this;
        me.doSaveAction();
    },
    onSaveContinueClicked: function () {
        var me = this;
        me.doSaveAction(true,false);
    },
    onSaveCloneClicked: function () {
        var me = this;
        me.doSaveAction(false,true);
    },
    onCancelClicked:function() {
        var me = this,
            view = me.getView();
        if (view && view.openInPopupWindow) {
            var wnd = view.up('window');
            Ext.destroy(wnd);
            return;
        }
        Ext.destroy(view);
    },
    doSaveAction:function(saveContinue, saveClone,autoSave) {
        var me = this,
           view = me.getView(),
           parentGrid = view.parentGrid,
           values = view.getValues();
        me.disableActionGroup();
        if (parentGrid) {
            var gridStore = parentGrid.getStore(),
                idPropertyField = gridStore.idPropertyField,
                operation;
            var record = Ext.create(gridStore.model.$className);
            Ext.apply(record.data, values);
            var target;
            if (view.openInPopupWindow) {
                target = view.up('window');
            } else {
                target = view;
            }
            var myMask = new Ext.LoadMask({
                msg: 'Please wait...',
                target: target
            });

            //perform any custom operation in doPreSaveOperation function of controller.
            //if doPreSaveOperation returns false the saving will be cancel
            record = me.doPreSaveOperation(record, values, idPropertyField);
            if (!record) return record;

            myMask.show();
            if (values && parseInt(values[idPropertyField]) > 0) {
                operation = Ext.data.Operation({
                    params: record.data,
                    parentGrid: parentGrid,
                    records: [record],
                    controller: me,
                    operationMask: myMask,
                    saveContinue: saveContinue,
                    saveClone: saveClone,
                    autoSave: autoSave,
                    callback: me.onOperationCompleteCallBack
                });
                gridStore.update(operation);
            } else if (values && parseInt(values[idPropertyField]) === 0) {
                record.id = 0;
                record.set('id', 0);
                operation = Ext.data.Operation({
                    params: record.data,
                    parentGrid: parentGrid,
                    controller: me,
                    operationMask: myMask,
                    saveContinue: saveContinue,
                    saveClone: saveClone,
                    autoSave: autoSave,
                    callback: me.onOperationCompleteCallBack
                });
                gridStore.create(values, operation);
            } else {
                myMask.hide();
            }
        } else me.enableActionGroup();
    },
    disableActionGroup:function() {
        var me = this,
            view = me.getView(),
            actionGroup = view.defaultActionGroup;
        if (actionGroup) {
            actionGroup.disable(true);
        }
    },
    enableActionGroup:function() {
        var me = this,
           view = me.getView(),
           actionGroup = view.defaultActionGroup;
        if (actionGroup) {
            actionGroup.disable(false);
        }
    },
    doPreSaveOperation: function (record, values, idPropertyField) { return record; },
    onOperationCompleteCallBack: function (records, operation, success) {
        var controller = operation.controller,
                view = controller.getView();
        var mask = operation.operationMask;
        if (mask) mask.hide();
        var promise = controller.doDistributionSaveOperations(records, operation, success);
        var runner = new Ext.util.TaskRunner(),
            task = undefined;

        task = runner.newTask({
            run: function () {
                if (promise && promise.owner.completed) {
                    task.stop();
                    if (promise.owner.completionAction === "fulfill") {
                        controller.handleFulFillResponse(records, operation, success);
                    } else if (promise.owner.completionAction === "reject") {
                        controller.handleRejectResponse(records, operation, success, promise.owner.completionValue);
                    }
                }
            },
            interval: 1000
        });

        task.start();
    },
    handleRejectResponse: function (records, operation, success, rejectResponseValue) {
        var controller = operation.controller;
        controller.enableActionGroup();
        if (records && rejectResponseValue) {
            var record = records[0],
                rejectResponse = Ext.decode(rejectResponseValue);
            var message = '',
               title = 'Error';
            record.reject();
            if (rejectResponse && rejectResponse.error) {
                if (rejectResponse.error.message && rejectResponse.error.details) {
                    title = rejectResponse.error.message;
                    message = rejectResponse.error.details;
                    abp.message.warn(message, title);
                    return;
                }
                title = rejectResponse.error.message;
                message = rejectResponse.error.details ? rejectResponse.error.details : title;
            }
            abp.message.warn(message, title);
        }
    },
    handleFulFillResponse: function (records, operation, success) {
        var controller = operation.controller,
            view = controller.getView(),
            saveContinue = operation.saveContinue,
            saveClone = operation.saveClone,
            autoSave = operation.autoSave;
        controller.enableActionGroup();
        if (success) {
            var gridController = operation.parentGrid.getController();
            gridController.doReloadGrid();

            var action = operation.getAction();
            if ((action === "create" || action === "destroy" || action === "update") && !saveContinue && !saveClone && !autoSave) {
                if (view && view.openInPopupWindow) {
                    var wnd = view.up('window');
                    Ext.destroy(wnd);
                } else if (view) {
                    Ext.destroy(view);
                }
            }
            abp.notify.success('Operation completed successfully.', 'Success');
        } else {
            var response = Ext.decode(operation.getResponse().responseText);
            var message = '',
                title = 'Error';
            if (response && response.error) {
                if (response.error.message && response.error.details) {
                    title = response.error.message;
                    message = response.error.details;
                    abp.message.warn(message, title);
                    return;
                }
                title = response.error.message;
                message = response.error.details ? response.error.details : title;
            }
            abp.message.warn(message, title);
        }
    },
    doDistributionSaveOperations: function (records, operation, success) {
        ///TODO: IMplement 
        var deferred = new Ext.Deferred();
        deferred.resolve('{success:true}');
        return deferred.promise;
    },
    onFormResize:function(formPanel, width, height, oldWidth, oldHeight, eOpts) {
        if (formPanel) {
            var fieldSets = formPanel.query('fieldset');
            if (fieldSets && fieldSets.length > 1) {
                var allFieldSetsHeight = 0,
                    length = fieldSets.length,
                    transactionDetailContainer = undefined;
                for (var i = 0; i < length; i++) {
                    var fieldSet = fieldSets[i];
                    if (!fieldSet.isTransactionDetailContainer) {
                        allFieldSetsHeight += fieldSet.getHeight();
                    } else transactionDetailContainer = fieldSet;
                }
                if (allFieldSetsHeight > 0 && transactionDetailContainer) {
                    var heightForDetailGrid = height - (allFieldSetsHeight + 95);
                    transactionDetailContainer.down('gridpanel').setHeight(heightForDetailGrid);
                }
            }
            formPanel.updateLayout();
        }
    }
    
});
