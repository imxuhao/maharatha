Ext.define('Chaching.view.financials.accounts.AccountsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.financials-accounts-accountsform',
    beforeMappingAccountQuery: function (query, eOpts) {
        var me = this,
            view = me.getView(),
            grid = view.parentGrid;
        if (grid) {
            var coaId = grid.coaId;
            var myStore = query.combo.getStore();
            myStore.getProxy().setExtraParam('id', coaId);
        }
    }

});
