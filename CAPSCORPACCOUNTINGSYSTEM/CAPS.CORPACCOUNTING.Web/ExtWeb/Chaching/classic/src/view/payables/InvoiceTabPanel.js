Ext.define('Chaching.view.payables.InvoiceTabPanel', {
    extend: 'Chaching.view.common.tab.ChachingTabPanel',
    //xtype: 'payables.invoices',
    name: 'Payables.Invoices',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Payables.Invoices')
    },
    staticTabItems: null
});