Ext.define('Chaching.view.payables.invoices.BatchGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.payables-invoices-batchgrid',
    doAfterCreateAction: function (createNewMode, form, isEdit, record) {
        var form = form.getForm();
        var typeOfBatchCombo = form.findField('typeOfBatchId');
        var typeOfBatchStore = typeOfBatchCombo.getStore();
        typeOfBatchStore.load();
    },

    showAllModule: function (field, newVal, oldVal, eOpts) {
        var me = this;
        var checkBoxGroup = field.up('checkboxgroup');
        if (field.checked) {
            checkBoxGroup.setValue({ BatchType: ['All', '1,2,10,11', '3', '4', '6,7,9', '5', ''] });
        } else {
            checkBoxGroup.reset();
        }
    },

    onModuleChange: function (field, newVal, oldVal, eOpts) {
        var me = this;
        var allModules = field.down('#allModuleId');
        var journalModule = field.down('#journalModuleId');
        var accountsPayableModule = field.down('#accountsPayableModuleId');
        var receivablesModule = field.down('#receivablesModuleId');
        var pettyCashModule = field.down('#pettyCashModuleId');
        var payrollModuleModule = field.down('#payrollModuleId');
        var creditCardModule = field.down('#creditCardModuleId');
        var batchType = ['All'];

        if (field.getValue().BatchType) {
            if (Ext.isArray(field.getValue().BatchType)) {
                batchType = field.getValue().BatchType;
            } else {
                batchType = field.getValue().BatchType.split();
            }
        }

        var batchStore = me.getView().getStore();

        if (!allModules.checked && batchType.indexOf('All') == -1) {
            var filter = new Ext.util.Filter({
                entity: '',
                searchTerm: batchType.join(),
                comparator: 6,
                dataType: 4,
                property: 'typeOfBatchId'
            });

            batchStore.filter(filter);
            batchStore.load();
        } else {
            batchStore.clearFilter();
            batchStore.load({ filters: null });
        }
    }
}
);