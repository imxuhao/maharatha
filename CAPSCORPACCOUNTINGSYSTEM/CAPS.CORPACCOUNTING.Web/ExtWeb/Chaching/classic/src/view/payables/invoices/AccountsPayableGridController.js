Ext.define('Chaching.view.payables.invoices.AccountsPayableGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.payables-invoices-accountspayablegrid',
    doAfterCreateAction: function(createMode, formPanel, isEdit, record) {
        var form = undefined;
        var viewModel = undefined;
        if (formPanel) {
            form = formPanel.getForm();
            viewModel = formPanel.getViewModel();
        }
        if (form&&viewModel) {
            var currencyStore = viewModel.getStore('typeOfCurrencyList');
            currencyStore.load();

            var checkGroupStore = viewModel.getStore('typeOfCheckGroup');
            checkGroupStore.load();

            if (isEdit&&record) {
                var vendorField = form.findField('vendorId'),
                    vendorStore = vendorField.getStore();
                if (record.get('vendorId') || record.get('vendorName')) {
                    vendorStore.getProxy().setExtraParam('query', record.get('vendorName'));
                    vendorStore.load();
                }

                var bankField = form.findField('bankAccountId'),
                    bankStore = bankField.getStore();
                if (record.get('bankAccountId')) {
                    bankStore.getProxy().setExtraParam('query', record.get('bankAccount'));
                    bankStore.load();
                }

                if (record.get('accountingDocumentId')) {
                    if (!record.get('isPosted')) {
                        var btnPost = formPanel.down('button[itemId=PostBtn]');
                        btnPost.show();
                    }
                    var btnExport = formPanel.down('button[itemId=PrintBtn]');
                    btnExport.show();
                }
            }
        }
    }

});
