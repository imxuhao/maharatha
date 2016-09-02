/**
 * Accounts list to display all records of Accounts Receivable Invoices controller.
 */
Ext.define('Chaching.view.receivables.invoices.AccountsReceivableFormController',
{
    extend: 'Chaching.view.common.form.ChachingTransactionFormPanelController',
    alias: 'controller.receivables-invoices-accountsreceivableform',
    onFormResize: function (formPanel, width, height, oldWidth, oldHeight, eOpts) {
        if (formPanel) {
            var transactionDetailContainer = formPanel.down('*[itemId=transactionDetails]');
            if (transactionDetailContainer) {
                var heightForDetailGrid = 200;//height - (170 + 130);
                transactionDetailContainer.down('gridpanel').setHeight(heightForDetailGrid);
            }
            formPanel.updateLayout();
        }
    },
    onInvoiceTypeChange: function (field, newValue, oldValue) {
        var me = this,
            view = me.getView(),
            form = view.getForm(),
            adjustInvoice = view.down('*[itemId=adjustInvoice]');
        if (newValue) {
            switch (newValue.typeOfInvoiceId) {
                case '1':
                    adjustInvoice.setHidden(true);
                    break;
                case '2':
                    adjustInvoice.setHidden(false);
                    break;
            }
        }
        
        //Ext.Ajax.request({
        //    url: abp.appPath + 'api/services/app/arInvoiceEntryDocument/GetSalesRepList',
        //    jsonData: Ext.encode(''),
        //    success: function (response, a, b) { debugger;},
        //    failure: function (response, a, b) { debugger}
        //});



    }
});