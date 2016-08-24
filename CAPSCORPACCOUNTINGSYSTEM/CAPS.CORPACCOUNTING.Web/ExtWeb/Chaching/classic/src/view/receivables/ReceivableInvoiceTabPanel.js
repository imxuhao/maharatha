Ext.define('Chaching.view.receivables.ReceivableInvoiceTabPanel', {
    extend: 'Chaching.view.common.tab.ChachingTabPanel',
    name: 'Receivables.Invoices',
    modulePermissions: {
        read:abp.auth.isGranted('Pages.Receivables.Invoices')
    },
    staticTabItems:null
});