Ext.define('Chaching.view.payables.vendors.VendorsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.payables-vendors-vendorsgrid',
    doBeforeInlineAddUpdate: function (record) {
        if (record) {
            var address = record._address;
            if (address) {
                record.set('address',Ext.apply(address.data, record.address));
            }
        }
        return true;
    },
    doAfterCreateAction: function (createMode, formPanel, isEdit, record) {
        debugger;
        var me = this,
            view = me.getView();
        var viewModel = formPanel.getViewModel();
        var form = formPanel.getForm();
        if (isEdit) {

            var vendorTypeList = viewModel.getStore('vendorTypeList');
            vendorTypeList.load();

            var typeOfTaxList = viewModel.getStore('typeOfTaxList');
            typeOfTaxList.load();

            var typeof1099BoxList = viewModel.getStore('typeof1099BoxList');
            typeof1099BoxList.load();

            var paymentTermsList = viewModel.getStore('paymentTermsList');
            paymentTermsList.load();

            var getAccountsList = viewModel.getStore('getAccountsList');
            getAccountsList.load();

            if (record) {
                var vendorAliases = record.get('vendorAlias');
                var vendorAliasGrid = formPanel.down('*[itemId=vendorAliasGrid]');
                if (vendorAliasGrid) {
                    var aliasStore = vendorAliasGrid.getStore();
                    aliasStore.loadData(vendorAliases);
                }
            }
           
        }
    }


    //doAfterCreateAction: function (createMode, formView, isEdit) {
    //    var viewModel = formView.getViewModel();
    //    var form = formView.getForm();
    //    if (!isEdit) {            
    //        form.findField("chartOfAccountId").setValue(formView.parentGrid.coaId);
    //    }
    //    else {           
    //        var typeOfCurrency = viewModel.getStore('typeOfCurrencyList');
    //        typeOfCurrency.load();
    //        var typeOfCurrencyRate = viewModel.getStore('typeOfCurrencyRateList');
    //        typeOfCurrencyRate.load();
    //        var typeOfAccount = viewModel.getStore('typeOfAccountList');
    //        typeOfAccount.load();
    //        var typeofConsolidation = viewModel.getStore('typeofConsolidationList');
    //        typeofConsolidation.load();
    //    }
    //    var linkAccountStore = viewModel.getStore('linkAccountListByCoaId');
    //    linkAccountStore.getProxy().setExtraParam('id', formView.parentGrid.coaId);
    //    linkAccountStore.load();
    //}
}); 
