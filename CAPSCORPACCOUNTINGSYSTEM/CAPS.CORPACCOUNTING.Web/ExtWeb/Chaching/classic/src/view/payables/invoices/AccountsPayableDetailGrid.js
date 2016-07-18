
Ext.define('Chaching.view.payables.invoices.AccountsPayableDetailGrid',{
    extend: 'Chaching.view.common.grid.ChachingTransactionDetailGrid',
    xtype: 'payables.invoices.transactionDetails',

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
    store: 'payables.invoices.AccountsPayableDetailsStore',
    moduleColumns:[
    {
        xtype: 'gridcolumn',
        text: app.localize('PO#'),
        dataIndex: 'referenceNumber',
        name: 'referenceNumber'
    }],
    columnOrder: ['amount', 'jobNumber', 'accountNumber', 'subAccountNumber1', 'typeOf1099T4', 'itemMemo', 'taxRebateNumber', 'referenceNumber', 'isAsset']
});
