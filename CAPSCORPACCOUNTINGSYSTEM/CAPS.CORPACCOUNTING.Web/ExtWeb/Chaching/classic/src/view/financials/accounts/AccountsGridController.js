Ext.define('Chaching.view.financials.accounts.AccountsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials-accounts-accountsgrid',
    doAfterCreateAction: function (createMode, formView, isEdit) {
        if (!isEdit) {
            var form = formView.getForm();
            form.findField("chartOfAccountId").setValue(formView.parentGrid.parentAccountId);
        }
        else {
            var viewModel = formView.getViewModel();
            var typeOfCurrency = viewModel.getStore('typeOfCurrencyList');
            typeOfCurrency.load();
            var typeOfCurrencyRate = viewModel.getStore('typeOfCurrencyRateList');
            typeOfCurrencyRate.load();
            var typeOfAccount = viewModel.getStore('typeOfAccountList');
            typeOfAccount.load();
            var typeofConsolidation = viewModel.getStore('typeofConsolidationList');
            typeofConsolidation.load();
        }
    },
});
