
Ext.define('Chaching.view.purchaseorders.entry.PurchaseOrderDetailGrid',{
    extend: 'Chaching.view.common.grid.ChachingTransactionDetailGrid',
    xtype: 'purchaseorders.entry.transactionDetails',
    requires: [
        'Chaching.view.purchaseorders.entry.PurchaseOrderDetailGridController'
    ],
    modulePermissions: {
        read: abp.auth.isGranted('Pages.PurchaseOrders.Entry'),
        create: abp.auth.isGranted('Pages.PurchaseOrders.Entry.Create'),
        edit: abp.auth.isGranted('Pages.PurchaseOrders.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.PurchaseOrders.Entry.Delete'),
        attach: abp.auth.isGranted('Pages.PurchaseOrders.Entry.Attach')
    },
    controller: 'purchaseorders-entry-purchaseorderdetailgrid',
    store: 'purchaseorders.entry.PurchaseOrderDetailsStore',
    moduleColumns: [
    {
        xtype: 'gridcolumn',
        text: app.localize('InvoiceRef'),
        dataIndex: 'invoiceReference',
        name: 'invoiceReference',
        editor: {
            xtype:'textfield'
        }
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

        }, editor: {
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
    }, {
        xtype: 'gridcolumn',
        text: app.localize('ChargeDate').initCap(),
        dataIndex: 'ledgerDate',
        name: 'ledgerDate',
        width: '8%',
        filterField: {
            xtype: 'dateSearchField',
            width: '100%'
        },editor: {
            xtype: 'datefield',
            format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat
        }
    }, {
        xtype: 'gridcolumn',
        text: app.localize('CheckType').initCap(),
        dataIndex: 'typeOfCheck',
        name: 'typeOfCheck',
        valueField: 'typeOfCheckId',
        width: '10%',
        editor: {
            xtype: 'combobox',
            multiSelect:false,
            store: {
                fields: [{ name: 'typeOfCheck' }, { name: 'typeOfCheckId' }],
                data:[
                {
                    typeOfCheck: app.localize('DepositCheck'),
                    typeOfCheckId:1
                }, {
                    typeOfCheck: app.localize('PaymentCheck'),
                    typeOfCheckId: 2
                }]
            },
            listConfig : {   
                minWidth : 200,
                getInnerTpl : function() {
                    return '<div class="x-combo-list-item"><img src="' + Ext.BLANK_IMAGE_URL + '" class="chkCombo-default-icon chkCombo" alt="" /> {typeOfCheck} </div>';
                }
            },
            displayField: 'typeOfCheck',
            valueField: 'typeOfCheckId',
            queryMode: 'local',
            filterPickList: false
        }
    }],
    columnOrder: ['amount', 'jobNumber', 'accountNumber', 'subAccountNumber1', 'typeOf1099T4', 'itemMemo', 'subAccountNumber4', 'taxRebateNumber', 'vendorName', 'invoiceReference', 'ledgerDate', 'typeOfCheck']
});
