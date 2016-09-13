/**
 * The viewController class for bank set up.
 * Author: kamal
 * Date: 28/04/2016
 */
/**
 * @class Chaching.view.banking.BankSetupGridController
 * ViewController class for bank set up.
 * @alias controller.banking.banksetupgrid
 */
Ext.define('Chaching.view.banking.banksetup.BankSetupGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.banking.banksetupgrid',
    doAfterCreateAction: function (createNewMode, formPanel, isEdit, record) {
        var form = formPanel.getForm();
        // load accountTypecombo
        var accountTypeCombo = form.findField('typeOfBankAccountId');
        var accountTypeStore = accountTypeCombo.getStore();
        accountTypeStore.load();
        //load check stock combo
        var checkStockCombo = form.findField('typeOfCheckStockId');
        var checkStockStore = checkStockCombo.getStore();
        checkStockStore.load();
        //load upload methods
        var uploadMethodCombo = form.findField('typeOfUploadFileId');
        var uploadMethodStore = uploadMethodCombo.getStore();
        uploadMethodStore.load();
        //load positive pay files
        var positivePayFileCombo = form.findField('positivePayTypeOfUploadFileId');
        var positivePayFileStore = positivePayFileCombo.getStore();
        positivePayFileStore.load();
        // load divisions
        var divisionCombo = form.findField('jobId');
        var divisionStore = divisionCombo.getStore();
        divisionStore.load();
        // load Ledger accounts
        var ledgerAccountsCombo = form.findField('accountId');
        var ledgerAccountsStore = ledgerAccountsCombo.getStore();
        ledgerAccountsStore.load();

        if (isEdit) {
            if (record) {
                var checkNumberGrid = formPanel.down('*[itemId=checkNumberGrid]');
                if (checkNumberGrid) {
                    var checkNumberGridStore = checkNumberGrid.getStore();
                    Ext.apply(checkNumberGridStore.getProxy().extraParams, {
                        bankAccountId: record.get('bankAccountId')
                    });
                    checkNumberGridStore.load();
                }
            }
        }

    }
});
