Ext.define('Chaching.view.banking.banksetup.BankSetupFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.banking-banksetup-banksetupform',
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
        view = me.getView();
        record = Ext.create('Chaching.model.banking.banksetup.BankSetupModel');
        Ext.apply(record.data, values);
        var checkNumberGridStore = view.down('gridpanel[itemId=checkNumberGrid]').getStore();
        var checkNumberModifyRecords = checkNumberGridStore.getModifiedRecords();
        record.set('id', values.bankAccountId);
        if (checkNumberModifyRecords && checkNumberModifyRecords.length > 0) {
            var checkNumberArray = [];
            Ext.each(checkNumberModifyRecords, function (rec) {
                var checkNumberRec = {
                    organizationUnitId: Chaching.utilities.ChachingGlobals.loggedInUserInfo.userOrganizationId,
                    bankAccountId: values.bankAccountId,
                    bankAccountPaymentRangeId : rec.get('bankAccountPaymentRangeId'),
                    startingPaymentNumber: rec.get('startingPaymentNumber'),
                    endingPaymentNumber: rec.get('endingPaymentNumber')
                };
                checkNumberArray.push(checkNumberRec);
            });
            checkNumberArray.sort(function (a, b) {
                return (b.bankAccountPaymentRangeId - a.bankAccountPaymentRangeId);
            });
            record.data.bankAccountPaymentRangeList = checkNumberArray;
        }
        return record;
    }
});
