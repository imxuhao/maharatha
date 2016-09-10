
Ext.define('Chaching.view.pettycash.entry.PettyCashGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype:'pettycash.entry',
    requires: [
        'Chaching.view.pettycash.entry.PettyCashGridController'
    ],
    name:'PettyCash.Entry',
    controller: 'pettycash-entry-pettycashgrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.PettyCash.Entry'),
        create: abp.auth.isGranted('Pages.PettyCash.Entry.Create'),
        edit: abp.auth.isGranted('Pages.PettyCash.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.PettyCash.Entry.Delete'),
        attach: abp.auth.isGranted('Pages.PettyCash.Entry.Attach')
    },
    attachmentConfig: {
        objectType: 'AccountingDocument',
        objectIdField: 'accountingDocumentId'
    },
    gridId: 30,
    headerButtonsConfig: [
        {
            xtype: 'displayfield',
            value: abp.localization.localize("PettyCash"),
            ui: 'headerTitle'
        }, '->', {
            xtype: 'button',
            scale: 'small',
            ui: 'actionButton',
            action: 'create',
            text: abp.localization.localize("Add").toUpperCase(),
            checkPermission: true,
            iconCls: 'fa fa-plus',
            iconAlign: 'left'
        }
    ],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditPettyCash'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreatePettyCash'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewPettyCash'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    store: 'payables.invoices.AccountsPayableStore',//pettycash.entry.PettyCashStore
    columns:[
     {
        xtype: 'gridcolumn',
        text: app.localize('Vendor'),
        dataIndex: 'vendorName',
        sortable: true,
        groupable: true,
        flex:1,
        filterField: {
            xtype: 'chachingcombobox',
            store: new Chaching.store.utilities.autofill.VendorsStore({
                filters: [
                {
                    entity: 'vendors',
                    searchTerm: 3,
                    comparator: 2,
                    dataType: 0,
                    property: 'typeofVendorId',
                    value: 3//Only PC vendors
                }]
            }),
            valueField: 'vendorId',
            displayField: 'vendorName',
            searchProperty:'vendorId',
            queryMode: 'remote',
            minChars: 2,
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
            store: new Chaching.store.utilities.autofill.VendorsStore({
                filters:[
                {
                    entity: 'vendors',
                    searchTerm: 3,
                    comparator: 2,
                    dataType: 0,
                    property: 'typeofVendorId',
                    value: 3//Only PC vendors
                }]
            }),
            valueField: 'vendorId',
            displayField: 'vendorName',
            queryMode: 'remote',
            minChars: 2,
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
         text: app.localize('Description'),
         dataIndex: 'description',
         sortable: true,
         groupable: true,
         flex: 1,
         filterField: {
             xtype: 'textfield',
             width: '100%'
         }, editor: {
             xtype: 'textfield'
         }
     }, {
        xtype: 'gridcolumn',
        text: app.localize('Envelope#'),
        dataIndex: 'documentReference',
        sortable: true,
        groupable: true,
        flex: 1,
        filterField: {
            xtype: 'textfield',
            width: '100%'
        }, editor: {
            xtype: 'textfield',
            width: '100%'
        }
    }, {
        xtype: 'gridcolumn',
        text: app.localize('EnvelopeDate').initCap(),
        dataIndex: 'transactionDate',
        sortable: true,
        groupable: true,
        flex: 1,
        renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer,
        filterField: {
            xtype: 'dateSearchField',
            width: '100%',
            dataIndex: 'transactionDate'
        }, editor: {
            xtype: 'datefield',
            format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat
        }
    }, {
        xtype: 'gridcolumn',
        text: app.localize('EnvelopeTotal').initCap(),
        dataIndex: 'controlTotal',
        sortable: true,
        groupable: true,
        flex: 1,
        filterField: {
            xtype: 'textfield',
            width: '100%'
        }, editor: {
            xtype: 'numberfield',
            allowBlank: false
        }
    }, {
        xtype: 'gridcolumn',
        text: app.localize('AdvanceAmount').initCap(),
        dataIndex: 'advanceAmount',
        sortable: true,
        groupable: true,
        flex: 1,
        filterField: {
            xtype: 'textfield',
            width: '100%'
        }, editor: {
            xtype: 'numberfield',
            allowBlank: true
        }
    }, {
        xtype: 'actioncolumn',
        name: 'attachment',
        dataIndex: 'attachment',
        flex: 1,
        maxWidth: 120,
        minWidth: 100,
        menuDisabled: true,
        sortable:false,
        text: app.localize('Attachment'),
        align: 'center',
        renderer: Chaching.utilities.ChachingRenderers.attachmentsRenderer,
        items: [{
            iconCls: '',
            handler: 'attachementHandler'
        }]
    }]
});
