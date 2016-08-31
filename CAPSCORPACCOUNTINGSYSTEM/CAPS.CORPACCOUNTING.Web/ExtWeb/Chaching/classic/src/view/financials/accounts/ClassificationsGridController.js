Ext.define('Chaching.view.financials.accounts.ClassificationsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials-accounts-classificationsgrid',
    doAfterCreateAction: function (createMode, formView, isEdit) {
        var viewModel = formView.getViewModel();
        //var typeOfCurrency = viewModel.getStore('typeOfCurrencyList');
        //typeOfCurrency.load();
    }
});
