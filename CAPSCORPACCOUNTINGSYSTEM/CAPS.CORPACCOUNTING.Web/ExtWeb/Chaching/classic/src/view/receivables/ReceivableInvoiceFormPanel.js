Ext.define('Chaching.view.receivables.ReceivableInvoiceFormPanel', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    //viewModel:{},
    alias: ['widget.receivables.invoices.create', 'widget.receivables.invoices.edit'],
    controller: 'receivables-invoices-accountsreceivableformentry',
    hideDefaultButtons: true,
    items: [{
            xtype: 'tabpanel',
            ui: 'formTabPanels',
            items: [
            {
                xtype: 'accountreceivableentryform',
                title: abp.localization.localize("AREntryForm"),
                iconCls: 'fa fa-gear'
            },
            {
                xtype: 'accountreceivableinvoicebuilderform',
                title: abp.localization.localize("InvoiceBuilder"),
                iconCls: 'fa fa-gear'
            }
            ]
    }]

});