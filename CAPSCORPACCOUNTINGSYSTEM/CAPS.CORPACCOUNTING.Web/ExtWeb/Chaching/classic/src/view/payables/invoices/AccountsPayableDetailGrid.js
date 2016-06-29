
Ext.define('Chaching.view.payables.invoices.AccountsPayableDetailGrid',{
    extend: 'Ext.panel.Panel',

    requires: [
        'Chaching.view.payables.invoices.AccountsPayableDetailGridController'
    ],

    controller: 'payables-invoices-accountspayabledetailgrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Payables.Invoices'),
        create: abp.auth.isGranted('Pages.Payables.Invoices.Create'),
        edit: abp.auth.isGranted('Pages.Payables.Invoices.Edit'),
        destroy: abp.auth.isGranted('Pages.Payables.Invoices.Delete')
    },
    html: 'Hello, World!!'
});
