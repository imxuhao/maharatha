Ext.define('Chaching.view.financials.accounts.AccountsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials-accounts-accountsgrid',
    doAfterCreateAction: function (createMode, formView, isEdit) {
        if (!isEdit) {
            var form = formView.getForm();
            form.findField("chartOfAccountId").setValue(formView.parentGrid.parentAccountId);
        }
    },
});
