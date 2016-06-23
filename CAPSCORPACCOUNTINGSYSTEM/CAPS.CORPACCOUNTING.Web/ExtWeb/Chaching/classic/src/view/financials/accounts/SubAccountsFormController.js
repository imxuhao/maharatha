Ext.define('Chaching.view.financials.accounts.SubAccountsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.financials-accounts-subaccountsform',
    onAccountSpecificChange:function(check, newValue, oldValue) {
        var me = this,
            view = me.getView(),
            dragDropControl = view.down('chachingGridDragDrop'),
            leftStore = dragDropControl.getLeftStore(),
            rightStore = dragDropControl.getRightStore(),
            values = view.getForm().getValues();
        if (newValue) {
            leftStore.getProxy().setExtraParam('subAccountId', values.subAccountId);
            leftStore.load();

            rightStore.getProxy().setExtraParam('subAccountId', values.subAccountId);
            rightStore.load();
            dragDropControl.show();
        } else dragDropControl.hide();
    },
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
            view = me.getView(),
            dragDropControl = view.down('chachingGridDragDrop'),
            leftStore = dragDropControl.getLeftStore(),
            rightStore = dragDropControl.getRightStore(),
            subAccountRestrictionList = [],
            rightRecords = rightStore.getRange(),
            leftRecords = leftStore.getRange(),
            rightLength = rightRecords.length,
            leftLength = leftRecords.length;
        if (rightLength > 0) {
            for (var i = 0; i < rightLength; i++) {
                var rightRec = rightRecords[i];
                if (rightRec.get('subAccountRestrictionId') === 0) {
                    subAccountRestrictionList.push({
                        subAccountRestrictionId: rightRec.get('subAccountRestrictionId'),
                        accountId: rightRec.get('accountId'),
                        isActive: rightRec.get('isActive'),
                        organizationUnitId: rightRec.get('organizationUnitId')
                    });
                }
            }
        }
        if (leftLength>0) {
            for (var j = 0; j < leftLength; j++) {
                var leftRec = leftRecords[j];
                if (leftRec.get('wasActive') && !leftRec.get('isActive') && leftRec.get('subAccountRestrictionId')>0) {
                    subAccountRestrictionList.push({
                        subAccountRestrictionId: leftRec.get('subAccountRestrictionId'),
                        accountId: leftRec.get('accountId'),
                        isActive: leftRec.get('isActive'),
                        organizationUnitId: leftRec.get('organizationUnitId')
                    });
                }
            }
        }
        record.data.subAccountRestrictionList = subAccountRestrictionList;
        values.subAccountRestrictionList = subAccountRestrictionList;
        return record;
    }
    
});
