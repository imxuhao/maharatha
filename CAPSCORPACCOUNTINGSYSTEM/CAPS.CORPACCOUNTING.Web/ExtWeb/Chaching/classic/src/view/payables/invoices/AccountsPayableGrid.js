
Ext.define('Chaching.view.payables.invoices.AccountsPayableGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype: 'payables.invoices',
    name: 'Payables.Invoices',
    requires: [
        'Chaching.view.payables.invoices.AccountsPayableGridController'
    ],

    controller: 'payables-invoices-accountspayablegrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Payables.Invoices'),
        create: abp.auth.isGranted('Pages.Payables.Invoices.Create'),
        edit: abp.auth.isGranted('Pages.Payables.Invoices.Edit'),
        destroy: abp.auth.isGranted('Pages.Payables.Invoices.Delete')
    },
    gridId: 23,
    headerButtonsConfig: [
        {
            xtype: 'displayfield',
            value: abp.localization.localize("AccountsPayable"),
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
        title: app.localize('EditInvoice'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewInvoice'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewInvoice'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'inline',
    isSubMenuItemTab: true,
    store: 'payables.invoices.AccountsPayableStore',
    columns: [
        {
            text: app.localize('Post'),
            xtype: 'checkcolumn',
            dataIndex: 'isPosted',
            checked: true,
            listeners: {
                checkchange: function(column, recordIndex, checked) {
                    var store = this.up('grid').getStore();
                    //Ext.each(store, function (record) {
                    //    if (record.set('batchName').trim().length() != 0)
                    //        record.set('isPrimary', false);
                    //});
                }
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('CreatedBy').initCap(),
            dataIndex: 'createdUser',
            sortable: true,
            groupable: true,
            width: '15%',
            filterField: {
                xtype: 'textfield',
                width: '100%',
                entityName: ""
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Vendor'),
            dataIndex: 'vendorName',
            sortable: true,
            groupable: true,
            width: '15%',
            filterField: {
                xtype: 'textfield',
                width: '100%',
                entityName: ""
            },editor: {
                xtype: 'textfield',
                width: '100%'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('PO#'),
            dataIndex: 'purchaseOrderReference',
            sortable: true,
            groupable: true,
            width: '15%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Invoice#'),
            dataIndex: 'documentReference',
            sortable: true,
            groupable: true,
            width: '15%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }, editor: {
                xtype: 'textfield',
                width: '100%'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('InvoiceDate').initCap(),
            dataIndex: 'transactionDate',
            sortable: true,
            groupable: true,
            width: '15%',
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
            text: app.localize('InvoiceTotal').initCap(),
            dataIndex: 'controlTotal',
            sortable: true,
            groupable: true,
            width: '15%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            },editor: {
                xtype: 'numberfield',
                allowBlank:false
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('PostingDate').initCap(),
            dataIndex: 'datePosted',
            sortable: true,
            groupable: true,
            width: '15%',
            renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer,
            filterField: {
                xtype: 'dateSearchField',
                width: '100%'
            },
            editor: {
                xtype: 'datefield',
                format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Description'),
            dataIndex: 'description',
            sortable: true,
            groupable: true,
            width: '15%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }, editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('BatchName').initCap(),
            dataIndex: 'batchName',
            sortable: true,
            groupable: true,
            width: '10%'
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Currency'),
            dataIndex: 'typeOfCurrency',
            sortable: true,
            groupable: true,
            hidden:true,
            width: '10%',
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('Search')
            }, editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('DueDate').initCap(),
            dataIndex: 'dueDate',
            sortable: true,
            groupable: true,
            hidden: true,
            width: '10%',
            filterField: {
                xtype: 'dateSearchField',
                width: '100%'
            },
            editor: {
                xtype: 'datefield',
                format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('CheckGroup').initCap(),
            dataIndex: 'typeOfCheckGroupId',//replace with name
            sortable: true,
            groupable: true,
            hidden: true,
            width: '10%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            },
            editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('MemoLine'),
            dataIndex: 'memoLine',
            sortable: true,
            groupable: true,
            hidden: true,
            width: '10%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            },
            editor: {
                xtype: 'textfield'
            }
        }
    ]
});