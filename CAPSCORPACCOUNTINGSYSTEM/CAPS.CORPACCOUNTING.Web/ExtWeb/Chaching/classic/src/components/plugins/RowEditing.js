/**
 * This file contains the custom logic require for the rowediting plugin.
 * Author: Krishna Garad
 * Date:04/01/2016
 */
/**
 * The Chaching.components.plugins.RowEditing plugin injects editing at a row level for a Grid. When editing begins,
 * a small floating dialog will be shown for the appropriate row. Each editable column will show a field
 * for editing. There is a button to save or cancel all changes for the edit.
 *
 * The field that will be used for the editor is defined at the
 * {@link Ext.grid.column.Column#editor editor}. The editor can be a field instance or a field configuration.
 * If an editor is not specified for a particular column then that column won't be editable and the value of
 * the column will be displayed. To provide a custom renderer for non-editable values, use the 
 * {@link Ext.grid.column.Column#editRenderer editRenderer} configuration on the column.
 *
 * The editor may be shared for each column in the grid, or a different one may be specified for each column.
 * An appropriate field type should be chosen to match the data structure that it will be editing. For example,
 * to edit a date, it would be useful to specify {@link Ext.form.field.Date} as the editor.
 *
 *     @example
 *     Ext.create('Ext.data.Store', {
 *         storeId: 'simpsonsStore',
 *         fields:[ 'name', 'email', 'phone'],
 *         data: [
 *             { name: 'Lisa', email: 'lisa@simpsons.com', phone: '555-111-1224' },
 *             { name: 'Bart', email: 'bart@simpsons.com', phone: '555-222-1234' },
 *             { name: 'Homer', email: 'homer@simpsons.com', phone: '555-222-1244' },
 *             { name: 'Marge', email: 'marge@simpsons.com', phone: '555-222-1254' }
 *         ]
 *     });
 *
 *     Ext.create('Ext.grid.Panel', {
 *         title: 'Simpsons',
 *         store: Ext.data.StoreManager.lookup('simpsonsStore'),
 *         columns: [
 *             {header: 'Name', dataIndex: 'name', editor: 'textfield'},
 *             {header: 'Email', dataIndex: 'email', flex:1,
 *                 editor: {
 *                     xtype: 'textfield',
 *                     allowBlank: false
 *                 }
 *             },
 *             {header: 'Phone', dataIndex: 'phone'}
 *         ],
 *         selModel: 'rowmodel',
 *         plugins: {
 *             ptype: 'chachingRowediting',
 *             clicksToEdit: 1
 *         },
 *         height: 200,
 *         width: 400,
 *         renderTo: Ext.getBody()
 *     });
 *
 */
Ext.define('Chaching.components.plugins.RowEditing', {
    extend: 'Ext.grid.plugin.RowEditing',
    alias: 'plugin.chachingRowediting',
    completeEdit: function () {
        var me = this,
            context = me.context;

       
        if (me.editing && me.validateEdit(context)) {
            me.updateEditingContextRecord(context);
            me.editing = false;
            me.fireEvent('edit', me, context);
        }
    },
    /**
    * Starts editing the specified record, using the specified Column definition to define which field is being edited.
    * @param {Ext.data.Model} record The Store data record which backs the row to be edited.
    * @param {Ext.grid.column.Column/Number} [columnHeader] The Column object defining the column field to be focused, or index of the column.
    * If not specified, it will default to the first visible column.
    * @return {Boolean} `true` if editing was started, `false` otherwise.
    */
    startEdit: function (record, columnHeader) {
        var me = this,
            editor = me.getEditor(),
            context;

        if (Ext.isEmpty(columnHeader)) {
            columnHeader = me.grid.getTopLevelVisibleColumnManager().getHeaderAtIndex(0);
        }

        if (editor.beforeEdit() !== false) {
            context = me.getEditingContext(record, columnHeader);
            if (context && me.beforeEdit(context) !== false && me.fireEvent('beforeedit', me, context) !== false && !context.cancel) {
                me.context = context;
               
                // If editing one side of a lockable grid, cancel any edit on the other side.
                if (me.lockingPartner) {
                    me.lockingPartner.cancelEdit();
                }
                if (context.record.associations) {
                    var associationInfo = context.record.associations;
                    for (var associationKey in associationInfo) {
                        if (associationInfo.hasOwnProperty(associationKey)) {
                            var association = associationInfo[associationKey];
                            var associationRecord = record[association.instanceName];
                            if (associationRecord && editor.form) {
                                for (var key in associationRecord.data) {
                                    if (associationRecord.data.hasOwnProperty(key) && key !== "id") {
                                        context.record.data[key] = associationRecord.data[key];
                                    }
                                }
                            }
                        }
                    }
                }
                editor.startEdit(context.record, context.column, context);
                
                //set value and rawValue for combos
                if (editor.form) {
                    var formFields = editor.form.getFields();
                    for (var i = 0; i < formFields.items.length; i++) {
                        var field = formFields.items[i];
                        if (field.xtype === "combo" || field.xtype === "combobox" || field.xtype === "chachingcombobox" || field.xtype === "chachingcombo") {
                            var editorStore = field.getStore();
                            if (editorStore && !editorStore.isDataLoaded) {
                                editorStore.field = field;
                                editorStore.load({
                                    callback: function (records, operation, success) {
                                        this.field.setValue(record.get(this.field.valueField));
                                        this.field.setRawValue(record.get(this.field.displayField));
                                    }
                                });
                                editorStore.isDataLoaded = true;
                            } else {
                                field.setValue(record.get(field.valueField));
                                field.setRawValue(record.get(field.displayField));
                            }
                        }
                    }
                }
                me.editing = true;
                return true;
            }
        }
        return false;
    },
    updateEditingContextRecord: function (context) {
        var me = this,
            editor = me.editor,
            form = editor.form,
            fields = form.getFields(),
            grid = me.grid,
            columns = grid.getColumns();
        if (context && fields) {
            var record = context.record;
            var fieldItems = fields.items;
            for (var i = 0; i < fieldItems.length; i++) {
                var field = fieldItems[i];
                if (field.xtype === "combo" || field.xtype === "combobox" || field.xtype === "chachingcombobox" || field.xtype === "chachingcombo") {
                    record.set(field.valueField, field.getValue());
                    record.set(field.displayField, field.getRawValue());
                } else if (me.isAsscociationModelField(field, columns)) {
                    record.set(field.name, field.getValue());
                }

            }
        }
    },
    isAsscociationModelField: function (field, columns) {
        if (field && columns) {
            var length = columns.length;
            for (var i = 0; i < length; i++) {
                var column = columns[i];
                if ((column.dataIndex === field.name || column.dataIndex === field.itemId) && column.isAssociationField) {
                    return true;
                }
            }
        }
        return false;
    },
    /**
     * @private
     */
    initEditor: function () {
        var rowEditorCfg = this.initEditorConfig();
        rowEditorCfg.buttonUI = "actionButton";
        return new Ext.grid.RowEditor(rowEditorCfg);
    },
    // @private
    // remaps to the public API of Ext.grid.column.Column.getEditor
    getColumnField: function (columnHeader, defaultField) {
        var me = this,
            field = columnHeader.field;

        if (!(field && field.isFormField)) {
            field = columnHeader.field = me.createColumnField(columnHeader, defaultField);
        }

        if (field && field.ui === 'default' && !field.hasOwnProperty('ui')) {
            field.ui = me.defaultFieldUI;
        }
        return field;
    }
});