Ext.define('Chaching.view.financials.journals.JournalEntryFormController', {
    extend: 'Chaching.view.common.form.ChachingTransactionFormPanelController',
    alias: 'controller.financials-journals-journalentryform',
    doPreSaveOperation: function(record, values, idPropertyField) {
        record.set('typeOfAccountingDocumentId', 1);
        return record;
    },
    onHeaderCollapse:function(fieldSet, eOpts) {
        var me = this,
            view = me.getView(),
            detailContainer = view.down('fieldset[isTransactionDetailContainer=true]');
        if (detailContainer) {
            var detailGrid = detailContainer.down('gridpanel');
            if (detailGrid) {
                var gridHeight = detailGrid.getHeight();
                detailGrid.originalHeight = gridHeight;
                detailGrid.setHeight(gridHeight + (fieldSet.getHeight() - 80));
            }
        }
    },
    onHeaderExpand:function(fieldSet, eOpts) {
        var me = this,
           view = me.getView(),
           detailContainer = view.down('fieldset[isTransactionDetailContainer=true]');
        if (detailContainer) {
            var detailGrid = detailContainer.down('gridpanel');
            if (detailGrid) {
                detailGrid.setHeight(detailGrid.originalHeight);
            }
        }
    }
    
});
