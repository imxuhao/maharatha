/**
 * Accounts list to display all records of Accounts Receivable Invoices.
 */
Ext.define('Chaching.view.receivables.invoices.AccountsReceivableGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype: 'receivables.invoices',
    name: 'Receivables.Invoices',
    requires: [
        'Chaching.view.receivables.invoices.AccountsReceivableGridController'
    ],
    controller: 'receivables-invoices-accountsreceivablegrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Receivables.Invoices'),
        create: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Create'),
        edit: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Delete'),
        attach: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Attach')
    },
    attachmentConfig: {
        objectType: 'AccountingDocument',
        objectIdField: 'accountingDocumentId'
    },
    gridId: 29,
    headerButtonsConfig: [
        {
            xtype: 'displayfield',
            value: abp.localization.localize("AccountsReceivable"),
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
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    store: 'receivables.invoices.AccountsReceivableStore',
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('Agency/Customer'),
            dataIndex: 'customerName',
            sortable: true,
            groupable: true,
            width: '10%',
            filterField: {
                xtype: 'textfield',
                width: '100%',
                entityName: ""
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Invoice#'),
            dataIndex: 'documentReference',
            sortable: true,
            groupable: true,
            width: '10%',
            filterField: {
                xtype: 'textfield',
                width: '100%',
                entityName: ""
            }, editor: {
                xtype: 'textfield',
                width: '100%'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('InvoiceDate'),
            dataIndex: 'transactionDate',
            sortable: true,
            groupable: true,
            width: '10%',
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
            text: abp.localization.localize("Description"),
            dataIndex: 'description',
            sortable: true,
            groupable: true,
            width: '10%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            },
            editor: {
                xtype: 'textfield'
            }
        },{
            xtype: 'gridcolumn',
            text: app.localize('Amount'),
            dataIndex: 'amount',
            sortable: true,
            groupable: true,
            //flex:1,
            width: '8%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            },
            editor: {
                xtype: 'textfield',
                name: 'amount'
            }
          }, {
            xtype: 'gridcolumn',
            text: app.localize('Job#/Division'),
            dataIndex: 'jobNumber',
            sortable: true,
            groupable: true,
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
            text: app.localize('JobName'),
            dataIndex: 'jobName',
            sortable: true,
            groupable: true,
            width: '10%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }, editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Account/Line'),
            dataIndex: 'accountLine',
            sortable: true,
            groupable: true,
            isAssociationField: true,
            width: '10%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }, editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Currency'),
            dataIndex: 'typeOfCurrency',
            sortable: true,
            groupable: true,
            width: '8%',
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('Search')
            }, editor: {
                xtype: 'textfield'
            }
        }
                
    ]
});
