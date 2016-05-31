Ext.define('Chaching.components.form.AutoFillCombo', {
    //extend: 'Ext.form.field.Picker',
    extend: 'Ext.form.field.ComboBox',
    xtype: 'autofillcombo',
    requires: ['Ext.grid.Panel'],

    modulePermissions: undefined,
    entityType: null,
    entityPermission: null,
    entityGridController : null,
    //config : {
    //    entityType : null,
    //    permissions : null
    //},
    autoSelect:false,
    initComponent: function () {
        var me = this;
        //add create icon based upon permission
        if (!me.modulePermissions) {
            me.modulePermissions = {
                read: abp.auth.isGranted('Pages.' + me.entityPermission),
                create: abp.auth.isGranted('Pages.' + me.entityPermission + '.Create'),
                edit: abp.auth.isGranted('Pages.' + me.entityPermission + '.Edit'),
                destroy: abp.auth.isGranted('Pages.' + me.entityPermission + '.Delete')
            };
        }
        // check permission to create view
        if (me.modulePermissions.create) {
            me.plugins = [{
                  ptype: 'saki-ficn'
                , iconCls: 'fa-plus-square'
                , qtip: app.localize('Create')
            }];
        }
        me.on('iconclick', me.onIconClick, me);
        me.callParent(arguments);
    },
    
    onIconClick: function () {
        var me = this;
        var entityType = me.entityType;
        me.createWindow(entityType, 'create', null);
    },
    /**
     * Creates and returns the tree panel to be used as this field's picker.
     */
    createPicker: function () {
        var me = this,
            columnList = me.createGridColumns();
	        opts = Ext.apply({
	            shrinkWrapDock: 2,
	            manageHeight: false,
	            store: me.store,
	            displayField: me.displayField,
	            columns: columnList,
	            columnLines: true,
	            rowLines: true,
	            forceFit: true,
	            layout: 'fit',
	            floating: true,
	            multiSelect: false,
	            cls: 'chaching-transactiongrid',
	            controller : me.entityGridController,
	            selModel: {
	                selType: 'rowmodel', // rowmodel is the default selection model
	            },
	            viewConfig: {
	                stripeRows: true,
	            },
	            listeners: {
	                scope: me,
	                itemclick: me.onItemClick,
	            }
	        }, me.listConfig);
        var picker = me.picker = Ext.create('Ext.grid.Panel', opts);

        return picker;
    },

    doQuery: function (queryString, forceAll, rawQuery) {
        var me = this,
            store = me.getStore(),
            filters = store.getFilters(),
            // if we have a queryString and the queryFilter is not filtering the store, we should do a localQuery
            refreshFilters = !!queryString && me.queryFilter && (filters.indexOf(me.queryFilter) < 0),
            // Decide if, and how we are going to query the store
            queryPlan = me.beforeQuery({
                lastQuery: me.lastQuery || '',
                query: queryString || '',
                rawQuery: rawQuery,
                forceAll: forceAll,
                combo: me,
                cancel: false
            });
        // Allow veto.
        if (queryPlan !== false && !queryPlan.cancel) {
            // If they're using the same value as last time (and not being asked to query all), 
            // and the filters don't need to be refreshed, just show the dropdown
            if (me.queryCaching && !refreshFilters && queryPlan.query === me.lastQuery) {
                // The filter changing was done with events suppressed, so
                // refresh the picker DOM while hidden and it will layout on show.
                me.getPicker().getView().refresh();
                me.expand();
                me.afterQuery(queryPlan);
            } else // Otherwise filter or load the store
            {
                me.lastQuery = queryPlan.query;
                if (me.queryMode === 'local') {
                    me.doLocalQuery(queryPlan);
                } else {
                    me.doRemoteQuery(queryPlan);
                }
            }
            return true;
        } else // If the query was vetoed we still need to check the change
            // in case custom validators are used
        {
            me.startCheckChangeTask();
        }
        return false;
    },

    createGridColumns: function () {
        var me = this,
            //store = Ext.create('Chaching.store.' + me.store),
             store = me.store,
           // model = me.store.model.create(),
            fileds = store.model.fields.items,
            count = fileds.length,
            columns = [];
        for (var i = 0; i < count; i++) {
            if (fileds[i].hidden == false) {
                var column = {
                    text: fileds[i].headerText == null ? fileds[i].name : fileds[i].headerText,
                    sortable: false,
                    hideable: false,
                    menuDisabled: true,
                    filterable: true,
                    minWidth : 50,
                    width: fileds[i].width,
                    hidden: fileds[i].hidden,
                    dataIndex: fileds[i].name,
                    flex: fileds[i].width > 0 ? null : fileds[i].flex
                }
                columns.push(column);
            }
        }
      
        var actionColumn = {};
        var actionColumnItems = [];
        var editActionItem = {
            scale: 'small',
            iconCls: 'editCls',
          //  ui: 'actionButton',
            tooltip: app.localize('Edit'),
            handler : function(grid, rowIndex, colIndex) {
                var entityType = me.entityType;
                var rec = grid.getStore().getAt(rowIndex);
                me.createWindow(entityType, 'edit', rec);
               
            }
        };
        var deleteActionItem = {
            scale: 'small',
            iconCls: 'deleteCls',
           // ui: 'actionButton',
            tooltip: app.localize('Delete'),
            handler : function(grid, rowIndex, colIndex) {
                var entityType = me.entityType;
                var rec = grid.getStore().getAt(rowIndex);
                me.createWindow(entityType, 'delete', rec);
            }
        };
        if (me.modulePermissions.edit) {
            actionColumnItems.push(editActionItem);
        }

        if (me.modulePermissions.destroy) {
            actionColumnItems.push(deleteActionItem);
        }

        if (actionColumnItems.length > 0) {
            actionColumn = {
                xtype: 'actioncolumn',
                width: '8%',
                minWidth: 30,
                sortable: false,
                hideable: false,
                menuDisabled: true,
                filterable: true,
                items: actionColumnItems
            }
            columns.push(actionColumn);
        } 
       return columns;
    },
    createWindow: function (xtype, operation, record) {
        var me = this,
            picker = me.picker;
        if (!picker) me.picker = me.getPicker();
        var xtypeOfView = "";
        if (operation === 'create') {
            xtypeOfView = xtype + ".create";
        } else if (operation === 'edit') {
            xtypeOfView = xtype + ".edit";
        }
      
        var recordByIdUrl = me.store.proxy.urlToGetRecordById;
        if (operation === 'edit') {
            Ext.Ajax.request({
                url: recordByIdUrl,
                jsonData: Ext.encode({ id: 2 }),  //record.get(me.valueField)
                success: function (response, opts) {
                    var res = Ext.decode(response.responseText);
                    if (res.success) {
                        var popupWindow = me.createPopupWindow(xtypeOfView, me);
                        var formView = popupWindow.down('form');
                        var recordToLoad = me.getStore().model.create();
                        Ext.apply(recordToLoad.data, res.result);
                        var entityTypeController = me.picker.getController();
                        entityTypeController.doAfterCreateAction('popup', formView, true, recordToLoad);
                        formView.loadRecord(recordToLoad);
                    } else {
                        abp.message.error(res.error.message, 'Error');
                    }
                },
                failure: function (response, opts) {
                    var res = Ext.decode(response.responseText);
                    Ext.toast(res.exceptionMessage);
                    console.log(response);
                }
            });
        } else if (operation === 'delete') {
            Ext.Ajax.request({
                url: me.store.proxy.api.destroy,
                jsonData: Ext.encode({ id: 2 }),
                success: function (response, opts) {
                    var res = Ext.decode(response.responseText);
                    if (res.success) {
                        abp.notify.success('Operation completed successfully.', 'Success');
                    } else {
                        abp.message.error(res.error.message, 'Error');
                    }
                },
                failure: function (response, opts) {
                    var res = Ext.decode(response.responseText);
                    Ext.toast(res.exceptionMessage);
                    console.log(response);
                }
            });
        }
        else if (operation === 'create') {
            var popupWindow = me.createPopupWindow(xtypeOfView, me);
            var formView = popupWindow.down('form');
            var entityTypeController = me.picker.getController();
            entityTypeController.doAfterCreateAction('popup', formView, false, null);
        }
    },

    createPopupWindow: function (xtypeOfView, me) {
        var window = Ext.create('Chaching.view.common.window.ChachingWindowPanel', {
            layout: 'fit',
            autoShow: true,
            modal: true,
            height: '90%',
            width: '90%',
            items: [{
                xtype: xtypeOfView,
                openInPopupWindow: true,
                parentGrid: me.picker,
                showFormTitle: false
            }]
        });

        return window;
    },



    onItemClick: function (view, record, node, rowIndex, e) {

        if (!view.ownerCt.multiSelect) {

            this.selectItem(view, record);
            this.collapse();
        }
    },

    selectItem: function (view, record) {
        var me = this;

        me._setValue(record);
        me.fireEvent('select', me, record);

    },

    onSelect: function (view, record) {

    },

    onExpand: function () {
        var me = this,
            picker = me.getPicker(),
            store = picker.store,
            value = me.value,
            selected = [];

        if (!Ext.isEmpty(value)) {

            Ext.Array.each(value.split(','), function (id) {

                Ext.Array.push(selected, store.getById(id));
            }, me);
        }

        picker.getSelectionModel().select(selected);
    },

    _setValue: function (record) {
        var me = this,
        	displays = [],
        	values = [];

        if (!Ext.isArray(record) && Ext.isObject(record)) {

            displays.push(record ? record.get(me.displayField) : '');
            values.push(record ? record.get(me.valueField) : '');
        } else {

            Ext.Array.each(record, function (item) {

                Ext.Array.push(displays, (item ? item.get(me.displayField) : ''));
                Ext.Array.push(values, (item ? item.get(me.valueField) : ''));
            }, me);
        }

        me.setValue(values);
        me.setRawValue(displays);

        return me;
    },
    alignPicker: function () {
        var me = this,
            picker;

        if (me.isExpanded) {
            picker = me.getPicker();
            if (me.matchFieldWidth) {

                picker.setWidth(me.bodyEl.getWidth());
            }
            if (picker.isFloating()) {
                me.doAlign();
            }
        }
    }



});