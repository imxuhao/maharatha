/**
 * The viewController class for Credit card open statements.
 * Author: kamal
 * Date: 28/04/2016
 */
/**
 * @class Chaching.view.creditcard.entry.OpenStatementGridController
 * ViewController class for Credit card open statements.
 * @alias controller.creditcard-entry-openstatementgrid
 */
Ext.define('Chaching.view.creditcard.entry.OpenStatementGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.creditcard-entry-openstatementgrid',
    doAfterCreateAction: function (createNewMode, formPanel, isEdit, record) {
    },
    getNewTransactionsFromCCAccount: function (menu, item, e, eOpts) {

    },
    uploadCreditCardTransClick: function (menu, item, e, eOpts) {

    },
    onStatementDateClick: function (tableView, td, cellIndex, record, tr, rowIndex, e, eOpts) {
        var me = this,
           view = me.getView(),
           fieldName = view.getColumns()[cellIndex].dataIndex,
           tabPanel = view.up('tabpanel');
        if (e && e.target && tabPanel) {
            var horizontalTabPanel = tabPanel.up('tabpanel');
            if (horizontalTabPanel && fieldName === 'statementDate') {
                var unpostedStatementGrid = Ext.create({
                    xtype: 'creditcard.entry.statementgrid',
                    hideMode: 'offsets',
                    closable: true,
                    title: abp.localization.localize("CreditCardStatement"),
                    routId: 'creditcard.entry.statementgrid'
                });
                var gridStore = unpostedStatementGrid.getStore(),
                    storeProxy = gridStore.getProxy();

                storeProxy.setExtraParams({ 'statementDate': record.get('statementDate'), 'tenantId': abp.session.tenantId, 'bankId': record.get('bankId'), 'companyId': record.get('companyId') });
                gridStore.load();
                var tabLayout = horizontalTabPanel.getLayout();
                if (tabLayout) {
                    tabLayout.setActiveItem(horizontalTabPanel.add(unpostedStatementGrid));
                }
            }
        }
    }
});
