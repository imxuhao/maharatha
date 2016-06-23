Ext.define('Chaching.view.financials.accounts.DivisionsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials-accounts-divisionsgrid',
    doAfterCreateAction: function(createMode, formView, isEdit) {
        var viewModel = formView.getViewModel();
        var typeOfCurrency = viewModel.getStore('typeOfCurrencyList');
        typeOfCurrency.load();
    }

});
