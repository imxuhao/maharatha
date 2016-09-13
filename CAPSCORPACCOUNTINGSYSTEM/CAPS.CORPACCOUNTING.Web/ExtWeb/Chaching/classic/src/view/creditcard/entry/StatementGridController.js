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
Ext.define('Chaching.view.creditcard.entry.StatementGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.creditcard-entry-statementgrid',
    doAfterCreateAction: function (createNewMode, formPanel, isEdit, record) {
    },
    onStatementClick: function (tableView, td, cellIndex, record, tr, rowIndex, e, eOpts) {
        var me = this,
           view = me.getView(),
           fieldName = view.getColumns()[cellIndex].dataIndex,
           tabPanel = view.up('tabpanel');
        if (e && e.target && tabPanel) {
            var horizontalTabPanel = tabPanel.up('tabpanel');
            if (horizontalTabPanel && fieldName === 'invoiceNumber') {
                var unpostedStatementGrid = Ext.create({
                    xtype: 'projects.projectmaintenance.linenumbers',
                    hideMode: 'offsets',
                    closable: true,
                    title: abp.localization.localize("LineNumbers"),
                    routId: 'projects.projectmaintenance.linenumbers',
                    coaId: record.get('coaId')

                });
                var gridStore = unpostedStatementGrid.getStore(),
                    storeProxy = gridStore.getProxy();

                storeProxy.setExtraParams({ 'statementDate': record.get('statementDate'), 'tenantId': abp.session.tenantId, 'bankId': record.get('bankId'), 'companyId': record.get('companyId') });
                gridStore.load();
                var tabLayout = horizontalTabPanel.getLayout();
                if (tabLayout) {
                    tabLayout.setActiveItem(horizontalTabPanel.add(unpostedStatementGrid));
                }
            } else if (horizontalTabPanel && fieldName === 'buildAP' && !Ext.isEmpty(record.get('buildAP'))) {
                var apInvoiceForm = Ext.create({
                    xtype: 'payables.invoices.create',
                    hideMode: 'offsets',
                    closable: true,
                    title: app.localize('ViewInvoice'),
                    routId: 'payables.invoices.create'

                });
                var tabLayout = horizontalTabPanel.getLayout();
                if (tabLayout) {
                    tabLayout.setActiveItem(horizontalTabPanel.add(apInvoiceForm));
                }
            }
        }
    }
});
