
Ext.define('Chaching.view.financials.journals.JournalTransactionDetailGrid',{
    extend: 'Chaching.view.common.grid.ChachingTransactionDetailGrid',
    xtype:'widget.financials.journals.entry.transactionDetails',
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
            valueField: 'jobId',///NOTE: Important to update record idField when replicating like excel
            dataLoadClass: 'Chaching.store.utilities.autofill.JobDivisionStore',
            isMandatory:true,
            filterField: {
                //xtype: 'combobox',
                //store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                //valueField: 'creditJobNumber',
                //displayField: 'creditJobNumber',
                //queryMode: 'remote',
                //minChars: 2,
                //useDisplayFieldToSearch: true,
                //listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                //emptyText: app.localize('SearchText')

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
                //xtype: 'combobox',
                //store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                //valueField: 'creditJobId',
                //displayField: 'creditJobNumber',
                //queryMode: 'remote',
                //minChars: 2,
                //listConfig: Chaching.utilities.ChachingGlobals.comboListConfig

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
            valueField: 'accountId',
            isMandatory: true,
            dataLoadClass: 'Chaching.store.utilities.autofill.AccountsStore',
            filterField: {
                //xtype: 'combobox',
                //store: new Chaching.store.utilities.autofill.AccountsStore(),
                //valueField: 'creditAccountNumber',
                //displayField: 'creditAccountNumber',
                //queryMode: 'remote',
                //minChars: 2,
                //useDisplayFieldToSearch:true,
                //listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                //emptyText: app.localize('SearchText')

                xtype: 'chachingcombobox',
                store: new Chaching.store.utilities.autofill.AccountsStore(),
                valueField: 'accountId',
                displayField: 'creditAccountNumber',
                queryMode: 'remote',
                minChars: 2,
                useDisplayFieldToSearch: true,
                listConfig: {
                    minWidth: 400,
                    minHeight: 150,
                    maxHeight: 250
                },
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

                //xtype: 'combobox',
                //store: new Chaching.store.utilities.autofill.AccountsStore(),
                //valueField: 'creditAccountId',
                //displayField: 'creditAccountNumber',
                //queryMode: 'remote',
                //minChars: 2,
                //listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                //listeners: {
                //    beforequery: 'beforeAccountQuery'
                //}

                xtype: 'chachingcombobox',
                store: new Chaching.store.utilities.autofill.AccountsStore(),
                valueField: 'creditAccountId',
                displayField: 'creditAccountNumber',
                queryMode: 'remote',
                minChars: 2,
                useDisplayFieldToSearch: true,
                listConfig: {
                    minWidth: 400,
                    minHeight: 150,
                    maxHeight: 250
                },
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
            dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
            filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('creditSubAccountId1', 'creditSubAccountNumber1'),
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
            dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
            filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('creditSubAccountId2', 'creditSubAccountNumber2'),
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
        dataLoadClass: 'Chaching.store.utilities.autofill.VendorsStore',
        filterField: {

            //xtype: 'combobox',
            //store: new Chaching.store.utilities.autofill.VendorsStore(),
            //valueField: 'vendorName',
            //displayField: 'vendorName',
            //queryMode: 'remote',
            //minChars: 2,
            //useDisplayFieldToSearch: true,
            //listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
            //emptyText: app.localize('SearchText')

            xtype: 'chachingcombobox',
            store: new Chaching.store.utilities.autofill.VendorsStore(),
            valueField: 'vendorId',
            displayField: 'vendorName',
            queryMode: 'remote',
            minChars: 2,
            useDisplayFieldToSearch: true,
            listConfig: {
                minWidth: 550,
                minHeight: 150,
                maxHeight: 250
            },
            modulePermissions: {
                read: abp.auth.isGranted('Pages.Payables.Vendors'),
                create: false,//abp.auth.isGranted('Pages.Payables.Vendors.Create'),
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


            //xtype: 'combobox',
            //store: new Chaching.store.utilities.autofill.VendorsStore(),
            //valueField: 'vendorId',
            //displayField: 'vendorName',
            //queryMode: 'remote',
            //minChars: 2,
            //useDisplayFieldToSearch: true,
            //listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
            //emptyText: app.localize('SearchText')


            xtype: 'chachingcombobox',
            store: new Chaching.store.utilities.autofill.VendorsStore(),
            valueField: 'vendorId',
            displayField: 'vendorName',
            queryMode: 'remote',
            minChars: 2,
            useDisplayFieldToSearch: true,
            listConfig: {
                minWidth: 550,
                minHeight: 150,
                maxHeight: 250
            },
            modulePermissions: {
                read: abp.auth.isGranted('Pages.Payables.Vendors'),
                create: false,//abp.auth.isGranted('Pages.Payables.Vendors.Create'),
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
    columnOrder: ['amount', 'debits', 'credits', 'itemMemo', 'vendorName', 'accountRef1', 'taxRebateNumber', 'isAsset']
});
