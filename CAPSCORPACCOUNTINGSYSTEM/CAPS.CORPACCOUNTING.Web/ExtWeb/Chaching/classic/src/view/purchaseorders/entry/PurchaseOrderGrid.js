
Ext.define('Chaching.view.purchaseorders.entry.PurchaseOrderGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype: 'purchaseorders.entry',
    name: 'PurchaseOrders.Entry',

    requires: [
        'Chaching.view.purchaseorders.entry.PurchaseOrderGridController'
    ],

    controller: 'purchaseorders-entry-purchaseordergrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.PurchaseOrders.Entry'),
        create: abp.auth.isGranted('Pages.PurchaseOrders.Entry.Create'),
        edit: abp.auth.isGranted('Pages.PurchaseOrders.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.PurchaseOrders.Entry.Delete'),
        attach: abp.auth.isGranted('Pages.PurchaseOrders.Entry.Attach')
    },
    attachmentConfig: {
        objectType: 'AccountingHeaderTransactionsUnit',
        objectIdField: 'accountingDocumentId'
    },
    gridId: 26,
    headerButtonsConfig: [
        {
            xtype: 'displayfield',
            value: abp.localization.localize("PurchaseOrder"),
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
        }, {
            xtype: 'button',
            scale: 'small',
            ui: 'actionButton',
            text: abp.localization.localize("Close").toUpperCase(),
            iconCls: 'fa fa-pause',
            iconAlign: 'left',
            handler:'onCloseSelectionClicked'
        }, {
            xtype: 'button',
            scale: 'small',
            ui: 'actionButton',
            text: abp.localization.localize("Print").toUpperCase(),
            iconCls: 'fa fa-print',
            iconAlign: 'left',
            menu: new Ext.menu.Menu({
                ui: 'accounts',
                items: [
                    { text: abp.localization.localize("PrintPDF").toUpperCase(), iconCls: 'fa fa-file-pdf-o', itemId: 'PrintPdf' },
                    { text: abp.localization.localize("Email").toUpperCase(), iconCls: 'fa fa-envelope-square', itemId: 'Email' }
                ]
            })
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
        title: app.localize('EditPO'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreatePO'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewPO'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    store: 'purchaseorders.entry.PurchaseOrderStore',
    columns: [
        {
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
            text: app.localize('PO#'),
            dataIndex: 'documentReference',
            sortable: true,
            groupable: true,
            width: '15%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Vendor'),
            dataIndex: 'vendorName',
            sortable: true,
            groupable: true,
            width: '15%',
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
        }, {
            xtype: 'gridcolumn',
            text: app.localize('TransDate').initCap(),
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
            text: app.localize('TotalOrder').initCap(),
            dataIndex: 'controlTotal',
            sortable: true,
            groupable: true,
            width: '15%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('RemainingBalance').initCap(),
            dataIndex: 'remainingBalance',
            sortable: true,
            groupable: true,
            width: '15%'
        }, {
            xtype: 'gridcolumn',
            text: app.localize('PendingTrans').initCap(),
            dataIndex: 'pendingTrans',
            sortable: true,
            groupable: true,
            width: '15%'
        }, {
            xtype: 'gridcolumn',
            text: app.localize('JobDivision').initCap(),
            dataIndex: 'jobNumber',
            sortable: true,
            groupable: true,
            width: '15%',
            filterField: {
                xtype: 'chachingcombobox',
                store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                valueField: 'jobId',
                displayField: 'jobNumber',
                queryMode: 'remote',
                minChars: 2,
                useDisplayFieldToSearch: true,
                modulePermissions: {
                    read: abp.auth.isGranted('Pages.Financials.Accounts.Divisions'),
                    create: false,
                    edit: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Edit'),
                    destroy: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Delete')
                },
                primaryEntityCrudApi: {
                    read: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
                    create: abp.appPath + 'api/services/app/divisionUnit/CreateDivisionUnit',
                    update: abp.appPath + 'api/services/app/divisionUnit/UpdateDivisionUnit',
                    destroy: abp.appPath + 'api/services/app/divisionUnit/DeleteDivisionUnit'
                },
                createEditEntityType: 'financials.accounts.divisions',
                createEditEntityGridController: 'financials-accounts-divisionsgrid',
                entityType: 'Division',
                isTwoEntityPicker: true,
                secondEntityDetails: {
                    editCreateModelClass: 'Chaching.model.projects.projectmaintenance.ProjectModel',
                    identificationKey: 'isDivision',
                    entityType: 'Job',
                    createEditEntityType: 'projects.projectmaintenance.projects',
                    createEditEntityGridController: 'Chaching.view.projects.projectmaintenance.ProjectsGridController',
                    modulePermissions: {
                        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
                        create: false,
                        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
                        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete')
                    },
                    secondoryEntityCrudApi: {
                        read: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
                        create: abp.appPath + 'api/services/app/jobUnit/CreateJobUnit',
                        update: abp.appPath + 'api/services/app/jobUnit/UpdateJobUnit',
                        destroy: abp.appPath + 'api/services/app/jobUnit/DeleteJobUnit'
                    }
                }

            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('LineNumber').initCap(),
            dataIndex: 'accountNumber',
            sortable: true,
            groupable: true,
            width: '15%',
            filterField: {
                xtype: 'chachingcombobox',
                store: new Chaching.store.utilities.autofill.AccountsStore(),
                valueField: 'accountId',
                displayField: 'accountNumber',
                queryMode: 'remote',
                minChars: 2,
                useDisplayFieldToSearch: true,
                modulePermissions: {
                    read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
                    create: false,//abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Create'),
                    edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
                    destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete')
                },
                primaryEntityCrudApi: {
                    read: abp.appPath + 'api/services/app/list/GetAccountsList',
                    create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
                    update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
                    destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
                },
                createEditEntityType: 'financials.accounts.accounts',
                createEditEntityGridController: 'financials-accounts-accountsgrid',
                entityType: 'Account',
                isTwoEntityPicker: true,
                secondEntityDetails: {
                    editCreateModelClass: 'Chaching.model.financials.accounts.AccountsModel',
                    identificationKey: 'isCorporate',
                    entityType: 'Line',
                    createEditEntityType: 'projects.projectmaintenance.linenumbers',
                    createEditEntityGridController: 'Chaching.view.projects.projectmaintenance.LineNumbersGridController',
                    modulePermissions: {
                        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs'),
                        create: false,//abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Create'),
                        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Edit'),
                        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Delete')
                    },
                    secondoryEntityCrudApi: {
                        read: abp.appPath + 'api/services/app/list/GetAccountsList',
                        create: abp.appPath + 'api/services/app/linesUnit/CreateLineUnit',
                        update: abp.appPath + 'api/services/app/linesUnit/UpdateLineUnit',
                        destroy: abp.appPath + 'api/services/app/linesUnit/DeleteLineUnit'
                    }
                }
            }
        }, {
            xtype: 'checkcolumn',
            dataIndex: 'close',
            text: app.localize('Close').initCap(),
            sortable: false,
            groupable: false,
            width: '5%',
            editor: {
                xtype: 'checkboxfield',
                inputValue: 'true',
                uncheckedValue: 'false'
            }
        }, {
            xtype: 'checkcolumn',
            dataIndex: 'print',
            text: app.localize('Print').initCap(),
            sortable: false,
            groupable: false,
            width: '5%'
        }, {
            xtype: 'gridcolumn',
            text: app.localize('ApprovedBy').initCap(),
            dataIndex: 'approvedBy',
            sortable: true,
            groupable: true,
            hidden:true,///TODO: checka pproval setup for hiding/showing
            width: '15%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }
        }
    ]
});
