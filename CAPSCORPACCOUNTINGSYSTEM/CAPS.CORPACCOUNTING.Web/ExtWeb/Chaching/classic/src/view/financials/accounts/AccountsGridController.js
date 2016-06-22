Ext.define('Chaching.view.financials.accounts.AccountsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials-accounts-accountsgrid',
    doAfterCreateAction: function (createMode, formView, isEdit) {
        var me = this;
        var viewModel = formView.getViewModel();
        var form = formView.getForm();
        var typeOfCurrency = viewModel.getStore('typeOfCurrencyList');
        typeOfCurrency.load();
        var typeOfCurrencyRate = viewModel.getStore('typeOfCurrencyRateList');
        typeOfCurrencyRate.load();
        var typeOfAccount = viewModel.getStore('typeOfAccountList');
        typeOfAccount.load();
        var typeofConsolidation = viewModel.getStore('typeofConsolidationList');
        typeofConsolidation.load();

        if (!isEdit) {
            if (formView.parentGrid.coaId != undefined) {
                form.findField("chartOfAccountId").setValue(formView.parentGrid.coaId);
            } else {
                var chartOfAcountStore = Ext.create('Chaching.store.financials.accounts.ChartOfAccountStore');
                    chartOfAcountStore.load({
                    scope: this,
                    callback: function (records, operation, success) {
                        if (success) {
                            var res = Ext.decode(operation._response.responseText);
                            var items = Ext.decode(operation._response.responseText).result.items;
                            if (items.length > 0) {
                                formView.parentGrid.coaId = items[0].coaId;
                                form.findField("chartOfAccountId").setValue(formView.parentGrid.coaId);
                                me.loadLinkAccounts(viewModel, formView.parentGrid.coaId);
                            }
                        } else {
                            console.log('Error in getting chart of account');
                        }
                    }
                });
            }
            
        }
        else {
            me.loadLinkAccounts(viewModel, formView.parentGrid.coaId);
        }
    },

    loadLinkAccounts: function (viewModel, coaId) {
        var linkAccountStore = viewModel.getStore('linkAccountListByCoaId');
        linkAccountStore.getProxy().setExtraParam('id', coaId);
        linkAccountStore.load();
    }
});
