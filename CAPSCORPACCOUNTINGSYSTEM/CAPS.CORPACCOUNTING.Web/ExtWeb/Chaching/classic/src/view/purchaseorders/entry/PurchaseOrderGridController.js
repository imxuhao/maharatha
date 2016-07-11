Ext.define('Chaching.view.purchaseorders.entry.PurchaseOrderGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.purchaseorders-entry-purchaseordergrid',
    onCloseSelectionClicked:function() {
        alert('Close');
    },
    doAfterCreateAction: function(createMode, formPanel, isEdit, record) {
        var form = undefined;
        var viewModel = undefined;
        if (formPanel) {
            form = formPanel.getForm();
            viewModel = formPanel.getViewModel();
        }
        if (form && viewModel) {
            var currencyStore = viewModel.getStore('typeOfCurrencyList');
            currencyStore.load();

            if (isEdit && record) {
                var vendorField = form.findField('vendorId'),
                    vendorStore = vendorField.getStore();
                if (record.get('vendorId') || record.get('vendorName')) {
                    vendorStore.getProxy().setExtraParam('query', record.get('vendorName'));
                    vendorStore.load();
                }

                if (record.get('isRetired')) {
                    var historyGrid = formPanel.down('gridpanel[isHistoryGrid=true]'),
                        historyStore = historyGrid.getStore();
                    historyGrid.show();
                    historyStore.getProxy().setExtraParam('accountingDocumentId', record.get('accountingDocumentId'));
                    historyStore.load();
                }
            }
        }
    }

});
