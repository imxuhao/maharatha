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
            clearingAccountCombo = form.findField('gLAccountId'),
            jobDivisionCombo = form.findField('jobId'),
            ccVendorCombo = form.findField('vendorId'),
            uploadMethodCombo = form.findField('typeOfUploadFileId'),
            batchCombo = form.findField('batchId');

        // load clearing account combo
        if (clearingAccountCombo) {
            clearingAccountCombo.getStore().load();
        }
        // load job/division combo
        if (jobDivisionCombo) {
            jobDivisionCombo.getStore().load();
        }
        // load credit card vendor combo
        if (ccVendorCombo) {
            var vendorComboStore = ccVendorCombo.getStore();
            vendorComboStore.load();
        }
        //load upload method
        if (uploadMethodCombo) {
            var uploadMethodStore = uploadMethodCombo.getStore();
            //uploadMethodStore.getProxy().setExtraParams({ 'typeOfBatchId': 1 });
            uploadMethodCombo.getStore().load();
        }
        // load batch combo
        if (batchCombo) {
            var batchStore = batchCombo.getStore();
            batchStore.getProxy().setExtraParams({ 'typeOfBatchId' : 1});
            batchStore.load();
        }
    }
});
