/**
 * Inquiry list to display all records of Iquiry in Receivable .
 */
Ext.define('Chaching.view.receivables.inquiry.InquiryInvoiceDetailGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype: 'receivables.inquiry.invoicedetail',
    name: 'Receivables.Inquiry.InvoiceDetail',
    //requires: [
    //    'Chaching.view.receivables.inquiry.InquiryGridController'
    //],
    //controller: 'receivables-inquiry-accountsreceivableinquirygrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Receivables.Inquiry.InvoiceDetail'),
        create: abp.auth.isGranted('Pages.Receivables.Inquiry.InvoiceDetail.Create'),
        edit: abp.auth.isGranted('Pages.Receivables.Inquiry.InvoiceDetail.Edit'),
        destroy: abp.auth.isGranted('Pages.Receivables.Inquiry.InvoiceDetail.Delete')
    },
    gridId: 29,
    headerButtonsConfig: [
        {
            xtype: 'displayfield',
            value: abp.localization.localize("InvoiceDetail"),
            ui: 'headerTitle'
        }, '->'
    ],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    //createNewMode: 'tab',
    //isSubMenuItemTab: true,
    store: 'receivables.inquiry.InquiryStore',
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
            text: abp.localization.localize("DueDate"),
            dataIndex: 'duedate',
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
        }, {
            xtype: 'gridcolumn',
            text: app.localize('JobNumber'),
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
            text: app.localize('InvoiceStatus'),
            dataIndex: 'invoiceStatus',
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
            text: app.localize('InvoiceAmount'),
            dataIndex: 'invoiceAmount',
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
        }, {
            xtype: 'gridcolumn',
            text: app.localize('DateOfDeposit'),
            dataIndex: 'dateOfDeposite',
            sortable: true,
            groupable: true,
            width: '10%',
            filterField: {
                xtype: 'datefield',
                width: '100%'
            }, editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Payments'),
            dataIndex: 'payments',
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
        }, {
            xtype: 'gridcolumn',
            text: app.localize('BalanceDue'),
            dataIndex: 'balanceDue',
            sortable: true,
            groupable: true,
            width: '10%',
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('Search')
            }, editor: {
                xtype: 'numberfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Notes'),
            dataIndex: 'notes',
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
