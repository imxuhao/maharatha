/**
 * Accounts list to display all records of Accounts Receivable Invoices controller.
 */
Ext.define('Chaching.view.receivables.invoices.ReceivableInvoiceFormPanelController',
{
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.receivables-invoices-accountsreceivableformentry',
    onFormResize: function (formPanel, width, height, oldWidth, oldHeight, eOpts) {
        if (formPanel) {
            //var transactionDetailContainer = formPanel.down('*[itemId=transactionDetails]');
            //if (transactionDetailContainer) {
            //    var heightForDetailGrid = height - (170 + 130);
            //    transactionDetailContainer.down('gridpanel').setHeight(heightForDetailGrid);
            //}
            //formPanel.updateLayout();
        }
    }
});