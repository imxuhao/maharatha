/**
 * Accounts list to display all records of Accounts Receivable Invoices Builder form.
 */
Ext.define('Chaching.view.receivables.invoices.ReceivableInvoiceBuilderForm', {
    extend: 'Chaching.view.common.form.ChachingTransactionFormPanel',
    alias: ['widget.receivables.invoices.invoiceBuilder'],
    requires: [
        'Chaching.view.receivables.invoices.ReceivableInvoiceBuilderFormController'
    ],
    controller: 'receivables-invoices-receivableinvoicebuilderform',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Receivables.Invoices'),
        create: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Create'),
        edit: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Delete')
    },
    openInPopupWindow: false,
    layout: 'fit',
    autoScroll: false,
    border: false,
    frame: false,
    title: 'New Form'



});