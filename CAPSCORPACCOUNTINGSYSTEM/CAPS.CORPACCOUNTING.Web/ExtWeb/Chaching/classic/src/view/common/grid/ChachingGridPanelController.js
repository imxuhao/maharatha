Ext.define('Chaching.view.common.grid.ChachingGridPanelController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-grid-chachinggridpanel',
    listen: {
        controller: {
            '#': {
                unmatchedroute: 'onUnmatchedRoute'
            }
        }
    },
    routes: {
        'host.tenants.create': {
            action: 'createEditRecordInTab'
        },
        //'host.tenants.edit': {
        //    action: 'showUser',
        //    before: 'beforeShowUser',
        //    conditions: {
        //        ':id': '([0-9]+)'
        //    }
        //}
    },

    createEditRecordInTab: function (hash) {
        if (!hash) {
            hash = this.currentRedirectedRoute;
        }
        Ext.toast(hash);
    },
    onUnmatchedRoute: function (hash) {
        if (!hash) {
            hash = this.currentRedirectedRoute;
        }
        if (Chaching.utilities.RoutesNames.menuItemRoutes.indexOf(hash) === -1) {
            Ext.toast('No route found with :' + hash);
        }
    },
    currentRedirectedRoute: null,
    //Event Listeners
    quickEditActionClicked: function (menu, item, e, eOpts) {
        //do edit based on editMode of grid
        var parentMenu = menu.parentMenu,
            widgetRec = parentMenu.widgetRecord,
            widgetCol = parentMenu.widgetColumn,
            grid = widgetCol.up('grid');

        //TODO start edit by checking row allowEdit property
        if (widgetRec && grid) {
            var editingPlugin = grid.getPlugin('editingPlugin');
            if (editingPlugin) {
                widgetRec.set('passEdit', true);
                if (grid.editingMode==="cell") {
                    editingPlugin.startEdit(widgetRec, 1);
                    return;
                }
                editingPlugin.startEdit(widgetRec);
            }
        }

    },
    editActionClicked: function(menu, item, e, eOpts) {
        var parentMenu = menu.parentMenu,
            widgetRec = parentMenu.widgetRecord,
            widgetCol = parentMenu.widgetColumn,
            grid = widgetCol.up('grid'),
            controller = grid.getController(),
            gridStore = grid.getStore();

        //TODO start edit by checking row allowEdit property
        if (widgetRec && grid) {
            var formView = controller.createNewRecord(grid.xtype, grid.createNewMode, true, grid.editWndTitleConfig, widgetRec);

            //var modelField = gridStore.getModel().getFields();
            //if (modelField) {
            //    Ext.each(modelField, function(field) {
            //        if (field.isPrimaryKey) {
            //            widgetRec.set('id', widgetRec.get(field.name));
            //            return;
            //        }
            //    });
            //}

            var form, formPanel;
            if (formView && formView.isWindow) {
                formPanel = formView.down('form'),
                    form = formPanel.getForm();
                //form.setValues(widgetRec.data);
                form.loadRecord(widgetRec);
            } else if (formView) {
                form = formView.getForm();
                //form.setValues(widgetRec.data);
                form.loadRecord(widgetRec);
            }
        }
    },
    deleteActionClicked: function (menu, item, e, eOpts) {
        //do delete based on operation of grid store
        var parentMenu = menu.parentMenu,
            widgetRec = parentMenu.widgetRecord,
            widgetCol = parentMenu.widgetColumn,
            grid = widgetCol.up('grid'),
            controller = grid.getController,
            gridStore = grid.getStore();

        //Delete record
        if (widgetRec && grid) {
            var modelField = gridStore.getModel().getFields();
            if (modelField) {
                Ext.each(modelField, function (field) {
                    if (field.isPrimaryKey) {
                        widgetRec.set('id', widgetRec.get(field.name));
                        return;
                    }
                });
            }
            gridStore.setAutoSync(true);
            gridStore.remove(widgetRec);
            gridStore.setAutoSync(false);

        }
    },
    onEditComplete: function (editor, e) {
        var me = this,
            view = this.getView();
        if (editor && editor.ptype === "chachingRowediting" && editor.context) {
            var context = editor.context,
                grid = context.grid,
                gridStore = grid.getStore(),
                record = context.record,
                idPropertyField = gridStore.idPropertyField;
            var operation;
            //if record.get(id)>0 then update else add

            var modelField = gridStore.getModel().getFields();
            if (modelField) {
                Ext.each(modelField, function (field) {
                    if (field.isPrimaryKey) {
                        record.set('id', record.get(field.name));
                        return;
                    }
                });
            }
            if (me.doBeforeInlineAddUpdate(record)) {
                if (record.get(idPropertyField) > 0) {
                    operation = Ext.data.Operation({
                        params: record.data,
                        records: [record],
                        controller: me,
                        callback: me.onOperationCompleteCallBack
                    });
                    gridStore.update(operation);
                } else {
                    record.id = 0;
                    record.set('id', 0);
                    operation = Ext.data.Operation({
                        params: record.data,
                        controller: me,
                        callback: me.onOperationCompleteCallBack
                    });
                    gridStore.create(record.data, operation);
                }
            }
        }
    },
    doBeforeInlineAddUpdate:function(record) { return true; },
    doReloadGrid: function () {
        var me = this,
            view = me.getView(),
            gridStore = view.getStore();

        gridStore.reload();
    },
    onOperationCompleteCallBack: function (records, operation, success) {
        var controller = operation.controller,
               view = controller.getView();
        var promise = controller.doPostSaveOperations(records, operation, success);
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
    handleRejectResponse: function(records, operation, success, rejectResponseValue) {
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
        if (success) {
            var action = operation.getAction();
            if (action === "create" || action === "destroy"||action==="update") {
                var controller = operation.controller;
                controller.doReloadGrid();
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
  
   doPostSaveOperations: function(records, operation, success) {
        var deferred = new Ext.Deferred();
        deferred.resolve('{success:true}');
        return deferred.promise;
    },
    //editing plugin listeners
    onBeforeGridEdit: function (editor, context, eOpts) {
        ///TODO cancel edit if restricted
        //cancel edit if is actioncolumn editing
        var record = context.record;
        if (context.column.name === "ActionColumn" && !record.get('passEdit')) return false;
    },
    onCreateNewBtnClicked: function (btn) {
        var me = this,
            view = me.getView(),
            gridStore = view.getStore(),
            model = gridStore.getModel(),
            className = model.$className,
            idPropertyField = gridStore.idPropertyField,
            editingPlugin = view.getPlugin('editingPlugin');

        var modelInstance;
        if (view && view.createNewMode) {
            switch (view.createNewMode) {
                case "inline":
                    modelInstance = Ext.create(className, {
                        idPropertyField: 0,
                        passEdit: true,
                        passDelete: true
                    });
                    gridStore.insert(0, modelInstance);
                    if (view.editingMode === "cell") {
                        editingPlugin.startEdit(gridStore.getAt(0), 1);
                        break;
                    }
                    editingPlugin.startEdit(gridStore.getAt(0));
                    break;
                case "popup":
                    me.createNewRecord(view.xtype, 'popup', false, view.createWndTitleConfig);
                    break;
                case "tab":
                    if (view.isSubMenuItemTab) {
                        me.createNewRecord(view.xtype, 'tab', false, view.createWndTitleConfig);
                    } else {
                        if (!btn.routeName) Ext.Error.raise('When create/edit mode is tab for grid then routeName config to button is mandatory!!!');
                        me.currentRedirectedRoute = btn.routeName;
                        me.redirectTo(btn.routeName);
                    }
                   
                    break;
                default:
                    me.currentRedirectedRoute = null;
                    break;
            }

        }

    },
    //Do module specific tasks 
    doBeforeCreateAction: function (createNewMode) { },
    doAfterCreateAction: function (createNewMode, form,isEdit,record) { },
    createNewRecord: function (type, createMode, isEdit, titleConfig,record) {
        var me = this,
            view = me.getView(),
            formView,
            className,
            tabPanel = view.up('tabpanel');
        if (!titleConfig) Ext.Error.raise('Please provide title configuration');
        me.doBeforeCreateAction(createMode);
        if (createMode === "popup") {
            className = type + ".createView";
            formView = Ext.create({
                xtype: className,
                title: titleConfig.title,
                iconCls: titleConfig.iconCls
            });
            formView.show();
        } else if (createMode === "tab" && tabPanel) {
            var parentTabPanel = tabPanel.up('tabpanel') ? tabPanel.up('tabpanel') : tabPanel;
            if (parentTabPanel) {
                className = type + ".create";
                formView = Ext.create({
                    xtype: className,
                    hideMode: 'offsets',
                    closable: true,
                    title: titleConfig.title,
                    iconCls:titleConfig.iconCls,
                    titleConfig: titleConfig,
                    isEdit: isEdit
                });
                var tabLayout = parentTabPanel.getLayout();
                if (tabLayout) {
                    tabLayout.setActiveItem(parentTabPanel.add(formView));
                }
            }
        }
        me.setParentControl(formView, createMode);
        me.doAfterCreateAction(createMode, formView, isEdit, record);
        if (formView.isTransactionForm) {
            me.loadDetailsStore(record, formView);
        }
        return formView;

    },
    loadDetailsStore: function (record, formPanel) {
        if (formPanel) {
            var detailsGrid = formPanel.down('gridpanel[isTransactionDetailsGrid=true]'),
                detailsStore = detailsGrid.getStore(),
                transactionId = undefined;
            if (record) {
                transactionId = record.get('accountingDocumentId');
            }
            if (transactionId && transactionId > 0) {
                detailsStore.getProxy().setExtraParam('accountingDocumentId', transactionId);
                detailsStore.load();
            } else {
                detailsStore.loadDefaultRecords(15);
            }
        }
    },
    setParentControl: function (formView, createMode) {
        var me = this,
            view = me.getView();
        if (formView && createMode==="popup") {
            var form = formView.down('form');
            form.parentGrid = view;
        } else if (formView && createMode==="tab") {
            formView.parentGrid = view;
        }
    },
    clearGridFilters: function (btn) {
        var me = this,
            view = me.getView(),
            multiSearchPlugin = view.getPlugin('gms'),
            gridStore = view.getStore();

        if (multiSearchPlugin) {
            multiSearchPlugin.clearValues(true);
            gridStore.clearFilter();
        } else gridStore.clearFilter();

        gridStore.getSorters().clear();
        if (gridStore.remoteSort)
            gridStore.load({ sortList: null, filters: null });

    },
    onManageViewClicked:function() {
        var me = this,
            view = me.getView();

        var manageView = Ext.create('Chaching.view.manageView.ManageViewView', {
            parentGrid: view,
            title: app.localize("ManageUsersViewSetting") + ': ' + view.getTitle()
        });
        manageView.show();
    }

});
