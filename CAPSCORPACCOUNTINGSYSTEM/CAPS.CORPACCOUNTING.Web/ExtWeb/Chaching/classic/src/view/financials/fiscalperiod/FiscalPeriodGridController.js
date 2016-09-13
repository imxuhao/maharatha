/**
 * The viewController class for fiscal periods.
 * Author: kamal
 * Date: 28/04/2016
 */
/**
 * @class Chaching.view.financials.fiscalperiod.FiscalPeriodGridController
 * ViewController class for fiscal period.
 * @alias controller.financials.fiscalperiodgrid
 */
Ext.define('Chaching.view.financials.fiscalperiod.FiscalPeriodGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials.fiscalperiodgrid',
    doAfterCreateAction: function (createMode, formPanel, isEdit, record) {
    var me = this;
    if (isEdit) {
        if (record) {
            var fiscalPeriodGrid = formPanel.down('*[itemId=fiscalPeriodGrid]');
            if (fiscalPeriodGrid) {
                var fiscalPeriodStore = fiscalPeriodGrid.getStore();
                Ext.apply(fiscalPeriodStore.getProxy().extraParams, {
                    fiscalYearId: record.get('fiscalYearId')
                });
                fiscalPeriodStore.load();
            }
        }
    }
    else {
        //write code here to populate any store after view ready
    }
}
});
