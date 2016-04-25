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
            var formView = controller.createNewRecord(grid.xtype, grid.createNewMode, true, grid.editWndTitleConfig);

            var modelField = gridStore.getModel().getFields();
            if (modelField) {
                Ext.each(modelField, function(field) {
                    if (field.isPrimaryKey) {
                        widgetRec.set('id', widgetRec.get(field.name));
                        return;
                    }
                });
            }

            var form, formPanel;
            if (formView && formView.isWindow) {
                formPanel = formView.down('form'),
                    form = formPanel.getForm();
                form.setValues(widgetRec.data);
                form.loadRecord(widgetRec);
            } else if (formView) {
                form = formView.getForm();
                form.setValues(widgetRec.data);
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
            gridStore.setAutoSync(true);
            var modelField = gridStore.getModel().getFields();
            if (modelField) {
                Ext.each(modelField, function (field) {
                    if (field.isPrimaryKey) {
                        widgetRec.set('id', widgetRec.get(field.name));
                        return;
                    }
                });
            }

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
        if (success) {
            var action = operation.getAction();
            if (action === "create" || action === "destroy") {
                var controller = operation.controller;
                controller.doReloadGrid();
            }
            Ext.toast({
                html: 'Operation completed successfully.',
                title: 'Success',
                ui: 'chachingWindow',
                alwaysOnTop: true,
                saveDelay: 500,
                animateShadow: true,
                align: 'tr'
            });
        } else {
            var response = Ext.decode(operation.getResponse().responseText);
            var message = '',
                title = 'Error';
            if (response && response.error) {
                if (response.error.message && response.error.details) {
                    title = response.error.message;
                    message = response.error.details.replaceAll(' - ', '</br>-');
                    var myMsg = Ext.create('Ext.window.MessageBox', {
                        // set closeAction to 'destroy' if this instance is not
                        // intended to be reused by the application
                        closeAction: 'destroy',
                        ui: 'chachingWindow'
                    }).show({
                        title: title,
                        message: message,
                        buttons: Ext.Msg.OKCANCEL,
                        icon: Ext.Msg.INFO
                    });
                    return;
                }
                title = response.error.message;
                message = response.error.details ? response.error.details : title;
            }
            Ext.toast(message);
        }
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
    doAfterCreateAction: function (createNewMode, form) { },
    createNewRecord: function (type, createMode, isEdit, titleConfig) {
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
            var parentTabPanel = tabPanel.up('tabpanel');
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
        me.doAfterCreateAction(createMode, formView, isEdit);
        return formView;

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
