/**
 * This file contains the custom logic require for the cellediting plugin.
 * Author: Krishna Garad
 * Date:09/05/2016
 */
/**
 * The Ext.grid.plugin.CellEditing plugin injects editing at a cell level for a Grid. Only a single
 * cell will be editable at a time. The field that will be used for the editor is defined at the
 * {@link Ext.grid.column.Column#editor editor}. The editor can be a field instance or a field configuration.
 *
 * If an editor is not specified for a particular column then that cell will not be editable and it will
 * be skipped when activated via the mouse or the keyboard.
 *
 * The editor may be shared for each column in the grid, or a different one may be specified for each column.
 * An appropriate field type should be chosen to match the data structure that it will be editing. For example,
 * to edit a date, it would be useful to specify {@link Ext.form.field.Date} as the editor.
 *
 * If the `editor` config on a column contains a `field` property, then the `editor` config is used to create
 * the wrapping {@link Ext.grid.CellEditorCellEditor}, and the `field` property is used to create the editing 
 * input field.
 *
 * ## Example
 *
 * A grid with editor for the name and the email columns:
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
 *                     completeOnEnter: false,
 *
 *                     // If the editor config contains a field property, then
 *                     // the editor config is used to create the {@link Ext.grid.CellEditor CellEditor}
 *                     // and the field property is used to create the editing input field.
 *                     field: {
 *                         xtype: 'textfield',
 *                         allowBlank: false
 *                     }
 *                 }
 *             },
 *             {header: 'Phone', dataIndex: 'phone'}
 *         ],
 *         selModel: 'cellmodel',
 *         plugins: {
 *             ptype: 'cellediting',
 *             clicksToEdit: 1
 *         },
 *         height: 200,
 *         width: 400,
 *         renderTo: Ext.getBody()
 *     });
 *
 * This requires a little explanation. We're passing in `store` and `columns` as normal, but
 * we also specify a {@link Ext.grid.column.Column#field field} on two of our columns. For the
 * Name column we just want a default textfield to edit the value, so we specify 'textfield'.
 * For the Email column we customized the editor slightly by passing allowBlank: false, which
 * will provide inline validation.
 *
 * To support cell editing, we also specified that the grid should use the 'cellmodel'
 * {@link Ext.grid.Panel#selModel selModel}, and created an instance of the CellEditing plugin,
 * which we configured to activate each editor after a single click.
 *
 */
Ext.define('Chaching.components.plugins.CellEditing', {
    extend: 'Ext.grid.plugin.CellEditing',
    alias: 'plugin.chachingCellediting',
    /**
     * This method is called when actionable mode is requested for a cell. 
     * @param {Ext.grid.CellContext} position The position at which actionable mode was requested.
     * @param {Boolean} skipBeforeCheck Pass `true` to skip the possible vetoing conditions like event firing.
     * @param {Boolean} doFocus Pass `true` to immediately focus the active editor.
     * @return {Boolean} `true` if this cell is actionable (editable)
     * @protected
     */
    activateCell: function(position, skipBeforeCheck, doFocus) {
        var me = this,
            record = position.record,
            column = position.column,
            context,
            cell,
            editor,
            prevEditor = me.getActiveEditor(),
            p,
            editValue;

        context = me.getEditingContext(record, column);
        if (!context || !column.getEditor(record)) {
            return;
        }

        // Activating a new cell while editing.
        // Complete the edit, and cache the editor in the detached body.
        if (prevEditor && prevEditor.editing) {
            // Silently drop actionPosition in case completion of edit causes
            // and view refreshing which would attempt to restore actionable mode
            me.view.actionPosition = null;

            if (prevEditor.completeEdit() === false) {
                return;
            }
        }

        if (!skipBeforeCheck) {
            // Allow vetoing, or setting a new editor *before* we call getEditor
            if (me.beforeEdit(context) === false || me.fireEvent('beforeedit', me, context) === false || context.cancel) {
                return;
            }
        }

        // Recapture the editor. The beforeedit listener is allowed to replace the field.
        editor = me.getEditor(record, column);

        // If the events fired above ('beforeedit' and potentially 'edit') triggered any destructive operations
        // regather the context using the ordinal position.
        if (context.cell !== context.getCell(true)) {
            context = me.getEditingContext(context.rowIdx, context.colIdx);
            position.setPosition(context);
        }

        if (editor) {
            cell = Ext.get(context.cell);

            // Ensure editor is there in the cell.
            // And will then be found in the tabbable children of the activating cell
            if (!editor.rendered) {
                editor.hidden = true;
                editor.render(cell);
            } else {
                p = editor.el.dom.parentNode;
                if (p !== cell.dom) {
                    // This can sometimes throw an error
                    // https://code.google.com/p/chromium/issues/detail?id=432392
                    try {
                        p.removeChild(editor.el.dom);
                    } catch (e) {

                    }
                    editor.container = cell;
                    cell.dom.appendChild(editor.el.dom, cell.dom.firstChild);
                }
            }

            // Refresh the contextual value in case any event handlers (either the 'beforeedit' of this
            // edit, or the 'edit' of any just terminated previous editor) mutated the record
            // https://sencha.jira.com/browse/EXTJS-19899
            editValue = context.record.get(context.column.dataIndex);
            if (editValue !== context.originalValue) {
                context.value = context.originalValue = editValue;
            }

            me.setEditingContext(context);

            //set value and rawValue for comboboxes
            if (editor.field) {
                var editorType = editor.field.xtype;
                if (editorType === "label") return false;
                if (editorType === "combo" || editorType === "combobox" || editorType === "chachingcombobox" || editorType === "chachingcombo") {
                    var editorStore = editor.field.getStore();
                    if (editor.field.extraParams) {
                        var extraParams = editor.field.extraParams,
                            paramsLength = extraParams.length,
                            storeProxy = editorStore.getProxy();
                        for (var i = 0; i < paramsLength; i++) {
                            var param = extraParams[i];
                            storeProxy.setExtraParam(param.paramName, record.get(param.paramName));
                        }
                    }
                    if (editorStore && !editorStore.isDataLoaded) {
                        editorStore.load({
                            callback: function(records, operation, success) {
                                editor.field.setRawValue(record.get(editor.field.displayField));
                                editor.field.setValue(record.get(editor.field.valueField));
                            }
                        });
                        editorStore.isDataLoaded = true;
                        editor.startEdit(cell, record.get(editor.field.valueField), doFocus || false);
                    } else {
                        editor.field.setRawValue(record.get(editor.field.displayField));
                        editor.field.setValue(record.get(editor.field.valueField));
                        editor.startEdit(cell, record.get(editor.field.valueField), doFocus || false);
                    }
                } else editor.startEdit(cell, context.value, doFocus || false);
            } else {
                // Request that the editor start.
                // Ensure that the focusing defaults to false.
                // It may veto, and return with the editing flag false.
                editor.startEdit(cell, context.value, doFocus || false);
            }
            // Set contextual information if we began editing (can be vetoed by events)
            if (editor.editing) {
                me.setActiveEditor(editor);
                me.setActiveRecord(context.record);
                me.setActiveColumn(context.column);
                me.editing = true;
                me.scroll = position.view.el.getScroll();
            }

            // Return true if the cell is actionable according to us
            return editor.editing;
        }
    },
    onEditComplete: function (ed, value, startValue) {
        var me = this,
            context = ed.context,
            view, record;

        view = context.view;
        record = context.record;
        context.value = value;

        // Only update the record if the new value is different than the
        // startValue. When the view refreshes its el will gain focus
        if (!record.isEqual(value, startValue)) {
            if (ed && ed.field) {
                //update records key:value with combo keyValue
                var editorField = ed.field;
                if (editorField && (editorField.xtype === "combo" || editorField.xtype === "combobox" || editorField.xtype === "chachingcombobox" || editorField.xtype === "chachingcombo")) {
                    record.set(context.column.dataIndex, editorField.getRawValue());
                    record.set(editorField.valueField, editorField.getValue());
                } else {
                    record.set(context.column.dataIndex, value);
                }
            } else {
                record.set(context.column.dataIndex, value);
            }
            // Changing the record may impact the position
            context.rowIdx = view.indexOf(record);
        }

        me.fireEvent('edit', me, context);

        // We clear down our context here in response to the CellEditor completing.
        // We only do this if we have not already started editing a new context.
        if (me.context === context) {
            me.setActiveEditor(null);
            me.setActiveColumn(null);
            me.setActiveRecord(null);
            me.editing = false;
        }
    }
});