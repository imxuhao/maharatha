/**
 * This file contains the custom datepicker control for search in grid.
 * Author: Krishna Garad
 * Date:04/12/2016
 * This class is inherited from {@link Class#Ext.form.field.Picker} class. Basically this class overrides the Picker's createPicker function to create custom picker.
 */
/**
 * @class Chaching.components.filter.DateSearchField
 * The datePicker control with for range search.
 * @alias widget.dateSearchField
 * An embedded live example:
 *
 *     @example
 *     Ext.create('Chaching.components.filter.DateSearchField', {
 *      renderTo:Ext.getBody() 
 *     });
 */
Ext.define('Chaching.components.filter.DateSearchField', {
    extend: 'Ext.form.field.Picker',
    alias: 'widget.dateSearchField',
    uses: ['Ext.picker.Date', 'Ext.menu.DatePicker'],
    displayField: 'FilterValue',
    valueField: 'FilterValue',
    forceSelection:true,
    editable: false,
    listConfig: {
        minWidth: 400
        ,width:400
    },
    initComponent: function () {
        var me = this;
        var gridStore = Ext.create('Ext.data.ArrayStore', {
            fields: [{ name: 'Before', type: 'date'
            }, {
                name: 'After', type: 'date'
            }, {
                name: 'On', type: 'date'
            }, { name: 'FilterValue', type: 'string' }, { name: 'ColumnIndex', type: 'int' },{name:'Apply'}]
        });
        gridStore.add({ Before: '', After: '', On: '', FilterValue: '' });
        me.store = gridStore;
        me.callParent(arguments);
    },
    /**
   * Creates the gridPicker control.  
   */
    createPicker: function () {
        var me = this,
	        opts = Ext.apply({
	            shrinkWrapDock: 2,
	            manageHeight: false,
	            store: me.store,
	            displayField: me.displayField,
	            columns: me.getColumns(),
	            columnLines: true,
	            rowLines: true,
	            forceFit: true,
	            layout: 'fit',
	            floating: true,
	            multiSelect: true,
	            cls: 'chaching-grid',
                plugins:[
                {
                    ptype: 'cellediting',
                    pluginId: 'editingPluginSearch',
                    clicksToEdit: 1,
                    listeners: {
                        beforeedit: me.onBeforeEdit
                    }
                }],
	            viewConfig: {
	                stripeRows: true
	            },
                listeners: {
                    beforecellclick: me.onBeforeRowCellClick
                }

	        }, me.listConfig);

        var picker = me.picker = Ext.create('Ext.grid.Panel', opts);
        me.picker = picker;
        return picker;
    },
    onBeforeEdit: function(editor, context, eOpts) {
        var me = this,
            record = context.record,
            field = context.field,
            apponentsValue;
        if (record&&field) {
            if (field === "After" || field === "Before") {
                apponentsValue = record.get('On');
                if (apponentsValue) return false;
            }else if (field==="On") {
                apponentsValue = record.get("After") || record.get('Before');
                if (apponentsValue) return false;
            }
        }
        return true;
    },
    /**
   * Event listener onBeforeRowCellClick.  Fires before row cell click.
   */
    onBeforeRowCellClick: function (view, td, cellIndex, record, tr, rowIndex, e, eOpts) {
        record.set('ColumnIndex', cellIndex);
    },
   
    clearSearchFilters: function (grid, rowIndex, colIndex, evt, e, record) {
        if (record) {
            var me = grid,
               ownerCt = me.ownerCt;
            record.set('Before', '');
            record.set('After', '');
            record.set('On', '');
            record.set('FilterValue', '');
            ownerCt.ownerCmp.setValue(null);
            ownerCt.ownerCmp.collapse();
        }
    },
    setFilters: function (grid, rowIndex, colIndex,evt,e,record) {
        if (record && (record.get('After') || record.get('Before') || record.get('On'))) {
            var me = grid,
                ownerCt = me.ownerCt;
            //build expected filter
            var filterValue = undefined;
            var after = Ext.Date.format(record.get('After'), 'm/d/Y'),
                before = Ext.Date.format(record.get('Before'), 'm/d/Y'),
                on = Ext.Date.format(record.get('On'), 'm/d/Y');
            if (after && before) {
                filterValue = 'range ' + after + ',' + before;
            }
            else if (after) {
                filterValue = '>=' + after;
            } else if (before) {
                filterValue = '<=' + before;
            } else if (on) {
                filterValue = '=' + on;
            }

            if (filterValue) {
                record.set('FilterValue', filterValue);
                ownerCt.ownerCmp.setValue(filterValue);
                ownerCt.ownerCmp.collapse();
            }
        }
    },
    /**
* Get's the columns for gridPicker.
*/
    getColumns: function () {
        var me = this;
        var columns = [
            {
                xtype: 'actioncolumn',
                dataIndex: 'Apply',
                flex: 1,
                maxWidth: 50,
                minWidth: 50,
                menuDisabled: true,
                sortable: false,
                //text: app.localize('Apply'),
                align: 'center',
                items: [{
                    iconCls: 'btn-applyFiltersDateSearch',
                    tooltip:app.localize('ApplyFilter'),
                    handler: me.setFilters
                }, {
                    iconCls: 'deleteCls',
                    tooltip: app.localize('ClearFilter'),
                    handler:me.clearSearchFilters
                }]
            },{
                xtype: 'gridcolumn',
                text: 'After',
                dataIndex: 'After',
                flex: 1,
                menuDisabled: true,
                sortable:false,
                renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer,
                editor: {
                    xtype: 'datefield',
                    enableKeyEvents:true,
                    listeners: {
                        keypress: me.onDateKeyPress,
                        specialkey: me.onDateKeyPress
                    }
                }
            },
            {
                xtype: 'gridcolumn',
                text: 'Before',
                dataIndex: 'Before',
                flex: 1,
                menuDisabled: true,
                sortable: false,
                renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer,
                editor: {
                    xtype: 'datefield',
                    enableKeyEvents: true,
                    listeners: {
                        keypress: me.onDateKeyPress,
                        specialkey: me.onDateKeyPress
                    }
                }
            },
            {
                xtype: 'gridcolumn',
                text: 'On',
                dataIndex: 'On',
                flex: 1,
                menuDisabled: true,
                sortable: false,
                renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer,
                editor: {
                    xtype: 'datefield',
                    enableKeyEvents: true,
                    listeners: {
                        keypress: me.onDateKeyPress,
                        specialkey: me.onDateKeyPress
                    }
                }
            }
        ];
        return columns;
    },
    onDateKeyPress: function (field, e, eOpts) {
        var value = field.getRawValue(),
            editor = field.up();
        if (editor) {
            var context = editor.context,
                record = context.record;
            if (context && record) {
                if (Ext.isDate(value)) record.set(field.name,value);
                else record.set(field.name,null);
            }
        }
    },
    /**
* Aligns the picker below dropdown input field as well adjust's height and width for list.
*/
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