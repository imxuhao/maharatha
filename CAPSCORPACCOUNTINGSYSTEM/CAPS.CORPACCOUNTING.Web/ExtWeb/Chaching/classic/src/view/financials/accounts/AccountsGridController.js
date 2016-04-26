Ext.define('Chaching.view.financials.accounts.AccountsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials-accounts-accountsgrid',
    doAfterCreateAction: function (createMode, formView, isEdit) {
        var viewModel = formView.getViewModel();
        var form = formView.getForm();
        if (!isEdit) {            
            form.findField("chartOfAccountId").setValue(formView.parentGrid.coaId);
        }
        else {           
            var typeOfCurrency = viewModel.getStore('typeOfCurrencyList');
            typeOfCurrency.load();
            var typeOfCurrencyRate = viewModel.getStore('typeOfCurrencyRateList');
            typeOfCurrencyRate.load();
            var typeOfAccount = viewModel.getStore('typeOfAccountList');
            typeOfAccount.load();
            var typeofConsolidation = viewModel.getStore('typeofConsolidationList');
            typeofConsolidation.load();
        }
        var linkAccountStore = viewModel.getStore('linkAccountListByCoaId');
        linkAccountStore.getProxy().setExtraParam('id', formView.parentGrid.coaId);
        linkAccountStore.load();
    }
});
