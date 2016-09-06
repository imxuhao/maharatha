/**
 * The viewController class for Credit Card Company Grid.
 * Author: kamal
 * Date: 28/04/2016
 */
/**
 * @class Chaching.view.creditcard.entry.CreditCardCompanyGridController
 * ViewController class for Credit card company.
 * @alias controller.creditcard-entry-creditcardcompanygrid
 */
Ext.define('Chaching.view.creditcard.entry.CreditCardCompanyGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.creditcard-entry-creditcardcompanygrid',
    doAfterCreateAction: function (createNewMode, formPanel, isEdit, record) {
        var me = this,
            form = formPanel.getForm(),
            clearingAccountCombo = form.findField('clearingAccountId'),
            jobDivisionCombo = form.findField('jobId'),
            cardTypeCombo = form.findField('typeOfBankAccountId'),
            ccVendorCombo = form.findField('vendorId'),
            uploadMethodCombo = form.findField('typeOfUploadFileId'),
            batchCombo = form.findField('batchId');

        // load clearing account combo
        if (clearingAccountCombo) {
            var clearingAccountComboStore = clearingAccountCombo.getStore(),
                clearingAccountStoreProxy = clearingAccountComboStore.getProxy();
            clearingAccountStoreProxy.setExtraParams({ 'typeOfAccountId': "Credit Card" }); //
            clearingAccountCombo.getStore().load();
        }
        // load job/division combo
        if (jobDivisionCombo) {
            jobDivisionCombo.getStore().load();
        }
        // load credit card type combo
        if (cardTypeCombo) {
            cardTypeCombo.getStore().load();
        }
        // load credit card vendor combo
        if (ccVendorCombo) {
            var vendorComboStore = ccVendorCombo.getStore(),
                 vendorStoreProxy = clearingAccountComboStore.getProxy();
            vendorStoreProxy.setExtraParams({ 'typeofVendorId': 2 }); // 2: credit card
            vendorComboStore.load();
        }
        //load upload method
        if (uploadMethodCombo) {
            var uploadMethodStore = uploadMethodCombo.getStore();
            //uploadMethodStore.getProxy().setExtraParams({ 'typeOfBatchId': 1 });
            uploadMethodStore.load();
        }
        // load batch combo
        if (batchCombo) {
            var batchStore = batchCombo.getStore();
            batchStore.getProxy().setExtraParams({ 'typeOfBatchId' : 3}); //3: account payable batch
            batchStore.load();
        }
    }
});
