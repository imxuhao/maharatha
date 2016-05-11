Ext.define('Chaching.view.payables.vendors.VendorsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.payables-vendors-vendorsgrid',
    doBeforeInlineAddUpdate: function (record) {
        if (record) {
            var address = record._address;
            if (address) {
                record.set('address', Ext.apply(address.data, record.address));
            }
        }
        return true;
    },
    doAfterCreateAction: function (createMode, formPanel, isEdit, record) {
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

            var rollupDivisionList = viewModel.getStore('rollupDivisionList');
            rollupDivisionList.load();

            var getTaxCreditList = viewModel.getStore('getTaxCreditList');
            getTaxCreditList.load();

            var glAccountList = viewModel.getStore('getAccountsList');
            glAccountList.getProxy().setExtraParams({
                value: 'true'
            });
            glAccountList.load();

            var getAccountsListLines = viewModel.getStore('getAccountsListLines');
            getAccountsListLines.getProxy().setExtraParams({
                value: 'false'
            });
            getAccountsListLines.load();

            if (record) {

                var vendorAliasGrid = formPanel.down('*[itemId=vendorAliasGrid]');
                if (vendorAliasGrid) {
                    var aliasStore = vendorAliasGrid.getStore();
                    Ext.apply(aliasStore.getProxy().extraParams, {
                        vendorId: record.get('vendorId')
                    });
                    aliasStore.load();
                }

                var vendoraddressGrid = formPanel.down('*[itemId=addressGrid]');
                if (vendoraddressGrid) {
                    var addressStore = vendoraddressGrid.getStore();
                    Ext.apply(addressStore.getProxy().extraParams, {
                        typeofObjectId: 1,
                        objectId: record.get('vendorId')
                    });
                    addressStore.load();
                }
            }
        }
        else {
            var glAccountList = viewModel.getStore('getAccountsList');
            glAccountList.getProxy().setExtraParams({
                value: 'true'
            });
            glAccountList.load();

            var getAccountsListLines = viewModel.getStore('getAccountsListLines');
            getAccountsListLines.getProxy().setExtraParams({
                value: 'false'
            });
            getAccountsListLines.load();

        }
    }
});
