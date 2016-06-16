Ext.define('Chaching.view.financials.accounts.SubAccountsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials-accounts-subaccountsgrid',
    doAfterCreateAction: function (createMode, formView, isEdit,record) {
        var subAccountId = 0;
        if (formView && isEdit) {
            var viewModel = formView.getViewModel();
            var typeOfSubAccount = viewModel.getStore('typeOfSubAccountList');
            typeOfSubAccount.load();
            subAccountId = record.data.subAccountId;
        }
        var leftStore = formView.down('chachingGridDragDrop').getLeftStore();
        leftStore.proxy.setExtraParam('subAccountId', subAccountId);
        leftStore.load();
        
        var rightStore = formView.down('chachingGridDragDrop').getRightStore();
        rightStore.proxy.setExtraParam('subAccountId', subAccountId);
        rightStore.load();

    }
});
