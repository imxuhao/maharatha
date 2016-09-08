Ext.define('Chaching.view.receivables.invoices.AccountsReceivableGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.receivables-invoices-accountsreceivablegrid',
    doAfterCreateAction: function (createMode, formPanel, isEdit, record) {
        var form = undefined;
        var viewModel = undefined;
        if (formPanel) {
            form = formPanel.getForm();
            viewModel = formPanel.getViewModel();
        }
        if (form && viewModel) {
            var currencyStore = viewModel.getStore('typeOfCurrencyList');
            currencyStore.load();
        }
        
    }
});