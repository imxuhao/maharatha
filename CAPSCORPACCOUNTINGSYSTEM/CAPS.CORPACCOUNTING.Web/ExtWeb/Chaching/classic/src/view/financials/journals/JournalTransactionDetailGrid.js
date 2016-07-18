
Ext.define('Chaching.view.financials.journals.JournalTransactionDetailGrid',{
    extend: 'Chaching.view.common.grid.ChachingTransactionDetailGrid',
    xtype:'financials.journals.entry.transactionDetails',
    requires: [
        'Chaching.view.financials.journals.JournalTransactionDetailGridController'
    ],

    controller: 'financials-journals-journaltransactiondetailgrid',
    store: 'financials.journals.JournalDetailsStore',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Journals.Entry'),
        create: abp.auth.isGranted('Pages.Financials.Journals.Entry.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Journals.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Journals.Entry.Delete')
    },
    groupedHeaderBaseConfig: [{
        groupHeaderText: app.localize('Debits'),
        columnName:'debits',
        childColumnNames: ['jobNumber', 'accountNumber', 'subAccountNumber1', 'subAccountNumber2'],
        childColumnWidths:[100,100,100]
    }],
    isGroupedHeader: true,
    moduleColumns:[
    {
        text: app.localize('Credits'),
        name: 'credits',
        columns: [{
            xtype: 'gridcolumn',
            dataIndex: 'creditJobNumber',
            name: 'creditJobNumber',
            text: app.localize('JobDivision'),
            //itemId: 'duplicatejob',
            width: 100,
            hideable: false,
            valueField: 'creditJobId',///NOTE: Important to update record idField when replicating like excel
            entityType: 'jobordivision',
            isMandatory:true,
            filterField: {
                xtype: 'chachingcombobox',
                store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                valueField: 'jobId',
                displayField: 'creditJobNumber',
                queryMode: 'remote',
                minChars: 2,
                useDisplayFieldToSearch: true,
                modulePermissions: {
                    read: abp.auth.isGranted('Pages.Financials.Accounts.Divisions'),
                    create: false,//abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Create'),
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
                        create: false,//abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
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
            },
            editor: {
                xtype: 'chachingcombobox',
                store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                valueField: 'creditJobId',
                displayField: 'creditJobNumber',
                queryMode: 'remote',
                minChars: 2,
                useDisplayFieldToSearch: true,
                modulePermissions: {
                    read: abp.auth.isGranted('Pages.Financials.Accounts.Divisions'),
                    create: false,//abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Create'),
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
                        create: false,//abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
                        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
                        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete')
                    },
                    secondoryEntityCrudApi: {
                        read: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
                        create: abp.appPath + 'api/services/app/jobUnit/CreateJobUnit',
                        update: abp.appPath + 'api/services/app/jobUnit/UpdateJobUnit',
                        destroy: abp.appPath + 'api/services/app/jobUnit/DeleteJobUnit'
                    }
                },
                listeners: {
                    beforequery: 'beforeJobDivisionQuery'
                }
            }
        }, {
            xtype: 'gridcolumn',
            dataIndex: 'creditAccountNumber',
            name: 'creditAccountNumber',
            //itemId: 'duplicateaccount',
            text: app.localize('LineNumber'),
            width: 100,
            hideable: false,
            valueField: 'creditAccountId',
            isMandatory: true,
            entityType: 'accounts',
            filterField: {
                xtype: 'chachingcombobox',
                store: new Chaching.store.utilities.autofill.AccountsStore(),
                valueField: 'creditAccountId',
                displayField: 'creditAccountNumber',
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
            }, editor: {
                xtype: 'chachingcombobox',
                store: new Chaching.store.utilities.autofill.AccountsStore(),
                valueField: 'creditAccountId',
                displayField: 'creditAccountNumber',
                queryMode: 'remote',
                minChars: 2,
                useDisplayFieldToSearch: true,
                modulePermissions: {
                    read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
                    create: false,//abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
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
                },
                extraParams: [{ paramName: "creditJobId", value: null }],
                listeners: {
                    beforequery: 'beforeAccountQuery'
                }
            }
        }, {
            xtype: 'gridcolumn',
            dataIndex: 'creditSubAccountNumber1',
            name: 'creditSubAccountNumber1',
            text: app.localize('SubAccount1'),
            //itemId: 'duplicatesubAccount1',
            width: 100,
            valueField: 'creditSubAccountId1',
            entityType: 'subaccounts',
            filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('creditSubAccountId1', 'creditSubAccountNumber1', true),
            editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('creditSubAccountId1', 'creditSubAccountNumber1')
            ////TODO: add remaining combo once accounting field configuration is done
        }, {
            xtype: 'gridcolumn',
            dataIndex: 'creditSubAccountNumber2',
            name: 'creditSubAccountNumber2',
            text: app.localize('SubAccount2'),
            //itemId: 'duplicatesubAccount1',
            width: 100,
            valueField: 'creditSubAccountId2',
            entityType: 'subaccounts',
            filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('creditSubAccountId2', 'creditSubAccountNumber2', true),
            editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('creditSubAccountId2', 'creditSubAccountNumber2')
            ////TODO: add remaining combo once accounting field configuration is done
        }]
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
            isTwoEntityPicker: false,
            listeners: {
                beforequery:'onBeforeVendorQuery'
            }
        }
    }],
    columnOrder: ['amount', 'debits', 'credits', 'itemMemo', 'vendorName', 'accountRef1', 'taxRebateNumber', 'isAsset']
});
