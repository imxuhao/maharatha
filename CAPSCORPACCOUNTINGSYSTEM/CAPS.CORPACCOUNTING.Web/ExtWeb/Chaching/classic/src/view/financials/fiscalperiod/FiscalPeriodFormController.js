Ext.define('Chaching.view.financials.fiscalperiod.FiscalPeriodFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.financials-fiscalperiod-fiscalperiodform',
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
             view = me.getView();
        record = Ext.create('Chaching.model.financials.fiscalperiod.FiscalYearModel');
        Ext.apply(record.data, values);
        var fiscalPeriodStore = view.down('gridpanel[itemId=fiscalPeriodGrid]').getStore();
        var fiscalPeriodModifyRecords = fiscalPeriodStore.getModifiedRecords();
       // record.set('id', values.fiscalYearId);
        if (fiscalPeriodModifyRecords && fiscalPeriodModifyRecords.length > 0) {
            var fiscalPeriods = [];
            Ext.each(fiscalPeriodModifyRecords, function (rec) {
                var periodStartDate = "";
                var periodEndDate = "";
                var monthYear = rec.get('monthYear');
                var month = monthYear.substr(0, 3);
                var year = monthYear.substr(4, 4);
                periodStartDate = periodStartDate.getDateString(month, year, true);
                periodEndDate = periodEndDate.getDateString(month, year, false);
                var fiscalPeriodRec = {
                    fiscalYearId: record.get('fiscalYearId'),
                    fiscalPeriodId: rec.get('fiscalPeriodId'),
                    organizationUnitId: Chaching.utilities.ChachingGlobals.loggedInUserInfo.userOrganizationId,
                    periodStartDate: moment(periodStartDate).format(Chaching.utilities.ChachingGlobals.defaultDateFormat),
                    periodEndDate: moment(periodEndDate).format(Chaching.utilities.ChachingGlobals.defaultDateFormat),
                    monthYear: rec.get('monthYear'),
                    isClose: rec.get('isClose'),
                    isActive: rec.get('isActive'),
                    isApproved: rec.get('isApproved'),
                    typeOfInactiveStatusId: rec.get('typeOfInactiveStatusId'),
                    isCpaClosed: rec.get('isCpaClosed'),
                    dateCpaClosed: rec.get('dateCpaClosed'),
                    cpaUserId: rec.get('cpaUserId'),
                    isYearEndAdjustmentsAllowed: rec.get('isYearEndAdjustmentsAllowed'),
                    isPreClose: rec.get('isPreClose')
                };
                fiscalPeriods.push(fiscalPeriodRec);
            });
            if (parseInt(record.get('fiscalYearId')) > 0) {
                record.data.updateFiscalPeriodUnits = fiscalPeriods;
            } else {
                record.data.createFiscalPeriodUnits = fiscalPeriods;
            }
            
        }
        return record;
    },
    onFiscalOpenYearChange: function (field, newVal, oldVal) {
        var me = this,
            view = me.getView();
        if (field.isEditMode == false && !field.getValue()) {
            abp.message.confirm('You are about to Close this fiscal year, no transactions will post to closed year', 'WARNING', function (btn) {
                if (btn) {
                    var fiscalPeriodStore = view.down('gridpanel[itemId=fiscalPeriodGrid]').getStore();
                    fiscalPeriodStore.each(function (record) {
                        record.set('isClose', true);
                    });
                }
            });
        }
    }

});
