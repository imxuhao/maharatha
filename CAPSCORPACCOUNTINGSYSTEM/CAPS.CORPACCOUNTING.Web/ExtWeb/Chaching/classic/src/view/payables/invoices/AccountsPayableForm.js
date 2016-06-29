
Ext.define('Chaching.view.payables.invoices.AccountsPayableForm',{
    extend: 'Chaching.view.common.form.ChachingTransactionFormPanel',
    alias: ['widget.payables.invoices.create', 'widget.payables.invoices.edit'],
    requires: [
        'Chaching.view.payables.invoices.AccountsPayableFormController',
        'Chaching.view.payables.invoices.AccountsPayableDetailGrid'
    ],

    controller: 'payables-invoices-accountspayableform',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Payables.Invoices'),
        create: abp.auth.isGranted('Pages.Payables.Invoices.Create'),
        edit: abp.auth.isGranted('Pages.Payables.Invoices.Edit'),
        destroy: abp.auth.isGranted('Pages.Payables.Invoices.Delete')
    },
    openInPopupWindow: false,
    layout: 'fit',
    autoScroll: false,
    border: false,
    frame: false,
    tbar: ['->', { xtype: 'button', text: 'Apply' }],
    rbar: [{ xtype: 'panel',layout:{type:'vbox',pack:'center'}, title: 'Vendor Snapshot',collapsed:true,collapsible:true,collapseDirection:'right',headerPosition:'top',flex:1,width:250}]
});
