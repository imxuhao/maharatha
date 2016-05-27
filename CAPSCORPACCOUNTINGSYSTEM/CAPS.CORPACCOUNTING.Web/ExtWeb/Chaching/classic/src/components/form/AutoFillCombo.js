Ext.define('Chaching.components.form.AutoFillCombo', {
    extend: 'Ext.form.field.Picker',
    xtype: 'autofillcombo',
    requires: ['Ext.grid.Panel'],

    modulePermissions: undefined,
    entityType: null,
    entityPermission : null,
    //config : {
    //    entityType : null,
    //    permissions : null
    //},

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
        me.createWindow(entityType, 'create');
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
	            multiSelect: true,
	            cls: 'chaching-transactiongrid',
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

    createGridColumns: function () {
        var me = this,
            store = Ext.create('Chaching.store.' + me.store),
           // model = me.store.model.create(),
            fileds = store.model.fields.items,
            count = fileds.length,
            columns = [];
        for (var i = 0; i < count; i++) {
            if (fileds[i].hidden == false) {
                var column = {
                    text: fileds[i].headerText == null ? fileds[i].name : fileds[i].headerText,
                    sortable: true,
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
        //add action column based upon permission
        //if (!me.modulePermissions) {
        //    me.modulePermissions = {
        //        read: abp.auth.isGranted('Pages.' + me.entityPermission),
        //        create: abp.auth.isGranted('Pages.' + me.entityPermission + '.Create'),
        //        edit: abp.auth.isGranted('Pages.' + me.entityPermission + '.Edit'),
        //        destroy: abp.auth.isGranted('Pages.' + me.entityPermission + '.Delete')
        //    };
        //}

        var actionColumn = {};
        var actionColumnItems = [];
        var editActionItem = {
            scale: 'small',
            iconCls: 'editCls',
          //  ui: 'actionButton',
            tooltip: app.localize('Edit'),
            handler : function(grid, rowIndex, colIndex) {
                var entityType = me.entityType;
                me.createWindow(entityType, 'edit');
                var rec = grid.getStore().getAt(rowIndex);
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
                minWidth : 30,
                items: actionColumnItems
            }
            columns.push(actionColumn);
        } 
        



        //for (var i = 0; i < count; i++) {
        //    if (fileds[i].name == 'id'  || fileds[i].hidden == true)
        //        continue;

        //    var newItem = new Object({
        //        text: fileds[i].alias == null ? fileds[i].name : fileds[i].alias,
        //        sortable: true,
        //        hideable: false,
        //        menuDisabled: false,
        //        filterable: true,
        //        width: fileds[i].width,
        //        hidden: fileds[i].hidden,
        //        dataIndex: fileds[i].name,
        //        flex: fileds[i].width > 0 ? null : fileds[i].flex
        //        //,
        //        //renderer: fileds[i].renderer || function (v) {
        //        //    var srch = me.isFiltering();
        //        //    if (srch == null)
        //        //        return v;
        //        //    if (me.ignoreFormat) {
        //        //        srch = me.autoFormat(srch, v);
        //        //    }
        //        //    var pattern = new RegExp(srch, "gi");
        //        //    var spanClass = 'mark-combo-match';
        //        //    var replacement = "<span class='" + spanClass + "'>$&</span>";
        //        //    v = v.toString().replace(pattern, replacement);
        //        //    return '<span class="mark-combo-notmatch">' + v + '</span>';
        //        //}
        //    });
        //    var column = Ext.create('Ext.grid.column.Column', newItem);
        //    columns.push(column);
        //}

        //if (me.columnOrder) {
        //    var dummyColumns = columns;
        //    columns = [];
        //    var order = me.columnOrder;
        //    for (var j = 0; j < order.length; j++) {
        //        var columnName = order[j];
        //        for (var k = 0; k < dummyColumns.length; k++) {
        //            if (dummyColumns[k].dataIndex === columnName) {
        //                columns.push(dummyColumns[k]);
        //                break;
        //            }
        //        }
        //    }
        //}

        return columns;
    },
    createWindow: function (xtype, operation) {
        var xtypeOfView = "";
        if (operation === 'create') {
            xtypeOfView = xtype + ".create";
        } else if (operation === 'edit') {
            xtypeOfView = xtype + ".edit";
        }

        Ext.create('Ext.window.Window', {
            title: operation.toUpperCase(),
            height: 600,
            width: 800,
            layout: 'fit',
            autoShow: true,
            modal: true,
            items: [{ 
                xtype: xtypeOfView
            }]
        });

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