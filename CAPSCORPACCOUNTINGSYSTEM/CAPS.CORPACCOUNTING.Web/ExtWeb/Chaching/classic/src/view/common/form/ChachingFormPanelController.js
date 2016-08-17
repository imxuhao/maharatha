Ext.define('Chaching.view.common.form.ChachingFormPanelController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-form-chachingformpanel',
    //default buttons action handler
    onSaveClicked: function (btn) {
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
                    callback: me.onOperationCompleteCallBack
                });
                gridStore.create(values, operation);
            } else {
                myMask.hide();
            }
        }
    },
    changeTitleFromViewModeToEditMode: function (view) {
        //change the title of view
        if (view.parentGridController) {
            var parentView = view.parentGridController.getView(),
                titleConfig = parentView.editWndTitleConfig,
                popupWin = view.up('window');
            if (popupWin && parentView && titleConfig) {
                popupWin.setTitle(titleConfig.title);
                if (view.showFormTitle && view.title) {
                    view.down('toolbar').down('displayfield').setValue(titleConfig.title);
                }
            } else {
                view.setTitle(titleConfig.title);
                if (view.showFormTitle && view.title) {
                    view.down('toolbar').down('displayfield').setValue(titleConfig.title);
                }
            }

        }
        //end of change title code
    },
    onEditButtonClicked: function (editBtn) {
        var me = this,
            view = me.getView(),
            childGrids = view.query('gridpanel'),
            form = view.getForm(),
            fields = form.getFields().items;

        //change the title of view when moving from view to edit mode
            me.changeTitleFromViewModeToEditMode(view);
        //end of change title code
        Ext.each(fields, function (field) {
            if (field.xtype !== "hiddenfield" && !field.isFilterField) {
                field.setDisabled(false);
                if (typeof (field.setEmptyText) === "function") {
                    field.setEmptyText(field.originalEmptyText);
                }
            }
        });

        if (childGrids && childGrids.length > 0) {
            Ext.each(childGrids, function (grid) {
                grid.isInViewMode = false;
                var dockedItems = grid.getDockedItems();
                if (dockedItems && dockedItems.length > 0) {
                    Ext.each(dockedItems, function (toolbar) {
                        if (toolbar.isActionToolBar) toolbar.show();
                    });
                }
            });
        }
        if (view.hideDefaultButtons) {
            me.doModuleSpecificEditAction(view);
        }
        var defaultActionToolBar = view.defaultActionToolBar;
        if (defaultActionToolBar) {
            var defaultActionButtons = defaultActionToolBar.query('button');
            if (defaultActionButtons && defaultActionButtons.length > 0) {
                Ext.each(defaultActionButtons, function (button) {
                    if (button.name !== 'Cancel' && button.name !== "Edit" && typeof (button.hide) === "function") {
                        button.show();
                    }
                    if (button.name === "Edit") button.hide();
                });
            }
        }

    },
    //override in child classes if required to perform customOperation and return false to cancel save
    doPreSaveOperation:function(record,values,idPropertyField) { return record; },
    onCancelClicked:function(btn) {
        var me = this,
            view = me.getView();
        if (view && view.openInPopupWindow) {
            var wnd = view.up('window');
            Ext.destroy(wnd);
            return;
        }
        Ext.destroy(view);
    },
    onOperationCompleteCallBack: function (records, operation, success) {
        var controller = operation.controller,
                view = controller.getView();
        var mask = operation.operationMask;
        if (mask) mask.hide();
        var promise = controller.doPostSaveOperations(records, operation, success);
        var runner = new Ext.util.TaskRunner(),
    task;

        task = runner.newTask({
            run: function () {
                if (promise && promise.owner.completed) {
                    task.stop();
                    if (promise.owner.completionAction === "fulfill") {
                        controller.handleMainThreadResponse(records, operation, success);
                    }
                }
            },
            interval: 1000
        });

        task.start();
    },
    handleMainThreadResponse: function (records, operation, success) {
               var controller = operation.controller,
               view = controller.getView();
        if (success) {
            var action = operation.getAction();
            if (action === "create" || action === "update") {
                var gridController = operation.parentGrid.getController();
                //this code is used to set the record in auto fill combo after create and update
                if (view.openInPopupWindow) {
                    operation.parentGrid.recordToSetInComboBox = null;
                    if (operation.parentGrid.recordToSetInComboBox == null && records.length > 0) {
                        operation.parentGrid.recordToSetInComboBox = records[0];
                    }
                }
                gridController.doReloadGrid();

                if (view && view.openInPopupWindow) {
                    var wnd = view.up('window');
                    Ext.destroy(wnd);
                } else if (view) {
                    Ext.destroy(view);
                }


            }
            abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
            ///TODO: Uncomment extjs toast if needed.
            //Ext.toast({
            //    html: 'Operation completed successfully.',
            //    title: 'Success',
            //    ui: 'chachingWindow',
            //    alwaysOnTop: true,
            //    saveDelay: 500,
            //    animateShadow: true,
            //    align: 'tr'
            //});

        } else {
            // function in Chaching.utilities.ChachingGlobals to show error message
            ChachingGlobals.showErrorMessage(operation);
        }
    },   
    doPostSaveOperations: function (records, operation, success) {
        var deferred = new Ext.Deferred();
        deferred.resolve('{success:true}');        
        return deferred.promise;       
    },
    onFormPanelResize: function (formPanel, width, height, oldWidth, oldHeight, eOpts) {
        formPanel.updateLayout();
    }
    
});
