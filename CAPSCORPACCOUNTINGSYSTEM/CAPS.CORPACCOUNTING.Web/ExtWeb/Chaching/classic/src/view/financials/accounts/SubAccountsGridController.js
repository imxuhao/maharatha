Ext.define('Chaching.view.financials.accounts.SubAccountsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials-accounts-subaccountsgrid',
    doAfterCreateAction: function (createMode, formView, isEdit) {
        if (formView && isEdit) {
            var viewModel = formView.getViewModel();
            var typeOfSubAccount = viewModel.getStore('typeOfSubAccountList');
            typeOfSubAccount.load();
        }
    }
});
