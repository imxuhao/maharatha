Ext.define('Chaching.view.financials.accounts.DivisionsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials-accounts-divisionsgrid',
    doAfterCreateAction: function (createMode, formView, isEdit){ 
        var viewModel = formView.getViewModel();
        var typeOfCurrency = viewModel.getStore('typeOfCurrencyList');
        //var coaId = formView.findField('chartOfAccountId').value;     //set chartOfAccount Id
        //typeOfCurrency.getProxy().setExtraParam('Id', coaId);
        typeOfCurrency.load();
    },
    doBeforeDataImportSaveOperation: function (data) {
        var me = this,
            view = me.getView(),
            myStore = view.getStore(),
            extraParam = myStore.getProxy().extraParams;
        for (var i = 0; i < data.length; i++) {
            data[i].chartOfAccountId = extraParam.coaId;
        }
        return data;
    }


});
