/**
 * This file contains the custom logic require for the rowediting plugin.
 * Author: Krishna Garad
 * Date:04/01/2016
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

            //do key:value adjustments for comboboxes
            var combos = me.editor.query('combobox');
            if (combos && combos.length > 0) {
                for (var i = 0; i < combos.length; i++) {
                    context.record.set(combos[i].displayField, combos[i].getRawValue());
                }
            }
        }
    },
    updateEditingContextRecord:function(context) {
        if (context && context.newValues) {
            var newVals = context.newValues,
                record = context.record;
            for (var key in newVals) {
                if (newVals.hasOwnProperty(key)) {
                    record.set(key, newVals[key]);
                }
            }
        }
    }
});