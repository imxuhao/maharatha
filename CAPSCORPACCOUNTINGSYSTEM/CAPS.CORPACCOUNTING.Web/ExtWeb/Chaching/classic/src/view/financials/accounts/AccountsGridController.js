Ext.define('Chaching.view.financials.accounts.AccountsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials-accounts-accountsgrid',
    doModuleSpecificBeforeEdit: function(editor, context, eOpts) {
        var grid = editor.grid,
            ed = editor.getEditor(),
            form = ed.getForm(),
            record=context.record,
            linkAccount = form.findField('linkAccount');
        if (linkAccount && !grid.allowAccountMapping) {
            linkAccount.extraParams = undefined;
            Ext.defer(function() {
                    linkAccount.hide(true);
                },50);
        } else {
            linkAccount.extraParams = {
                "id": grid.coaId,
                "query": record.get('linkAccount')
            };
            linkAccount.show();
        }
         return true;
    },
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
        var mappingAccountCombo = formView.getForm().findField('linkAccountId'),
            mappingStore = mappingAccountCombo.getStore();
        if (formView.parentGrid.allowAccountMapping) {
            mappingStore.getProxy().setExtraParam('id', formView.parentGrid.coaId);
            mappingStore.load();
        } else {
            mappingAccountCombo.setValue(null);
            mappingAccountCombo.hide();
        }
    },
    doBeforeDataImportSaveOperation: function (data, parentViewObj) {
        var coaId = parentViewObj.coaId;
        for (var i = 0; i < data.length; i++) {
            data[i].chartOfAccountId = coaId;
        }
        return data;
    }
});
