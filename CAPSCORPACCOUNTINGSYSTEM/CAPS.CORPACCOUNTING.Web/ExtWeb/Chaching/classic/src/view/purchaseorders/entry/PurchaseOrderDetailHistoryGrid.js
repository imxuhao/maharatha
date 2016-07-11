
Ext.define('Chaching.view.purchaseorders.entry.PurchaseOrderDetailHistoryGrid',{
    extend: 'Chaching.view.common.grid.ChachingTransactionDetailGrid',
    xtype: 'widget.purchaseorders.entry.transactionDetailsHistory',

    requires: [
        'Chaching.view.purchaseorders.entry.PurchaseOrderDetailHistoryGridController'
    ],

    controller: 'purchaseorders-entry-purchaseorderdetailhistorygrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.PurchaseOrders.Entry'),
        create: false,
        edit: false,
        destroy: false
    },
    store: 'purchaseorders.entry.PurchaseOrderDetailHistoryStore',
    moduleColumns: [
    {
        xtype: 'gridcolumn',
        text: app.localize('InvoiceRef'),
        dataIndex: 'invoiceReference',
        name: 'invoiceReference'
    }, {
        xtype: 'gridcolumn',
        text: app.localize('Vendor'),
        dataIndex: 'vendorName',
        name: 'vendorName',
        width: '10%',
        valueField: 'vendorId',
        entityType: 'vendors',
        filterField: {
            xtype: 'chachingcombobox',
            store: new Chaching.store.utilities.autofill.VendorsStore(),
            valueField: 'vendorId',
            displayField: 'vendorName',
            queryMode: 'remote',
            minChars: 2,
            useDisplayFieldToSearch: true,
            modulePermissions: {
                read: abp.auth.isGranted('Pages.Payables.Vendors'),
                create: abp.auth.isGranted('Pages.Payables.Vendors.Create'),
                edit: abp.auth.isGranted('Pages.Payables.Vendors.Edit'),
                destroy: abp.auth.isGranted('Pages.Payables.Vendors.Delete')
            },
            primaryEntityCrudApi: {
                read: abp.appPath + 'api/services/app/list/GetVendorList',
                create: abp.appPath + 'api/services/app/vendorUnit/CreateVendorUnit',
                update: abp.appPath + 'api/services/app/vendorUnit/UpdateVendorUnit',
                destroy: abp.appPath + 'api/services/app/vendorUnit/DeleteVendorUnit'
            },
            createEditEntityType: 'payables.vendors',
            createEditEntityGridController: 'payables-vendors-vendorsgrid',
            entityType: 'Vendor',
            isTwoEntityPicker: false

        }
    }],
    columnOrder: ['amount', 'jobNumber', 'accountNumber', 'subAccountNumber1', 'typeOf1099T4', 'itemMemo', 'subAccountNumber4', 'taxRebateNumber', 'vendorName', 'invoiceReference']
});
