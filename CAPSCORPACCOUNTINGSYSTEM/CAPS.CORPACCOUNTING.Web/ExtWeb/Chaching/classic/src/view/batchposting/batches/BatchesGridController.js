Ext.define('Chaching.view.batchposting.batches.BatchesGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.batchposting-batches-batchesgrid',
    doAfterCreateAction: function (createNewMode, form, isEdit, record) {
        var form = form.getForm();
        var typeOfBatchCombo = form.findField('typeOfBatchId');
        var typeOfBatchStore = typeOfBatchCombo.getStore();
        typeOfBatchStore.load();
    }
}
);