Ext.define('Chaching.view.payables.invoices.BatchGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.payables-invoices-batchgrid',
    doAfterCreateAction: function (createNewMode, form, isEdit, record) {
        var form = form.getForm();
        var typeOfBatchCombo = form.findField('typeOfBatchId');
        var typeOfBatchStore = typeOfBatchCombo.getStore();
        typeOfBatchStore.load();
    }
}
);