Ext.define('Chaching.view.financials.fiscalperiod.FiscalPeriodChildGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials.fiscalperiodchildgrid',
   
    onFiscalPeriodModeChange : function( checkCol, rowIndex, checked, record, eOpts ) {
        var me = this,
        view = me.getView(),
        formpanel = view.up('form');
        isYearOpen = formpanel.getForm().findField('isYearOpen').getValue();
        if (isYearOpen) {
            if (record.get('isClose') && record.get('isPreClose')) {
                abp.message.info('You can select either Close or PreClose not both at a time.', 'Information');
                if (checkCol.dataIndex == 'isClose') {
                    record.set('isClose', false);
                }
                if (checkCol.dataIndex == 'isPreClose') {
                    record.set('isPreClose', false);
                }
            }
        }
    }
});