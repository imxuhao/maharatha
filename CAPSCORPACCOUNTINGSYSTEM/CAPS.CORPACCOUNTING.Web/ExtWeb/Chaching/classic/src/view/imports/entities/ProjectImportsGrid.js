
Ext.define('Chaching.view.imports.entities.ProjectImportsGrid',{
    extend: 'Chaching.view.projects.projectmaintenance.ProjectsGrid',
    xtype:'imports.entities.projectsImports',
    requires: [
        'Chaching.view.imports.entities.ProjectImportsGridController'
    ],

    controller: 'imports-entities-projectimportsgrid',
    columns: [
         {
             xtype: 'gridcolumn',
             text: app.localize('JobNumber'),
             dataIndex: 'jobNumber',
             sortable: true,
             groupable: true,
             flex: 1,
             editor: {
                 xtype: 'textfield'
             }
             //renderer: Chaching.utilities.ChachingRenderers.rendererHyperLink
         }, {
             xtype: 'gridcolumn',
             text: app.localize('JobName'),
             dataIndex: 'caption',
             sortable: true,
             groupable: true,
             flex: 1,
             //renderer: Chaching.utilities.ChachingRenderers.rendererHyperLink,
             editor: {
                 xtype: 'textfield'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('ProjectType'),
             dataIndex: 'typeofProjectName',
             sortable: true,
             groupable: true,
             flex:1,
             editor: {
                 xtype: 'combobox',
                 allowBlank: false,
                 queryMode: 'local',
                 store: 'utilities.ProjectTypeStore',
                 valueField: 'typeofProjectId',
                 displayField: 'typeofProjectName'
             }
         },{
             xtype: 'gridcolumn',
             text: app.localize('BudgetFormat'),
             dataIndex: 'budgetFormatCaption',
             sortable: true,
             groupable: true,
             flex: 1,
             editor: {
                 xtype: 'combobox',
                 queryMode: 'local',
                 store: 'projects.projectmaintenance.ProjectCoaStore',
                 valueField: 'chartOfAccountId',
                 displayField: 'budgetFormatCaption',
                 width: '100%'
             }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('RollUpAccount'),
            dataIndex: 'accountNumber',
            sortable: true,
            groupable: true,
            flex: 1,
            editor: {
                xtype: 'chachingcombobox',
                store: new Chaching.store.utilities.autofill.RollupAccountListStore(),
                width: '100%',
                valueField: 'accountId',
                displayField: 'accountNumber',
                queryMode: 'remote',
                minChars: 2,
                modulePermissions: {
                    read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
                    create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
                    edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
                    destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete')
                },
                primaryEntityCrudApi: {
                    read: abp.appPath + 'api/services/app/accountUnit/GetRollupAccountsList',
                    create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
                    update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
                    destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
                },
                createEditEntityType: 'financials.accounts.accounts',
                createEditEntityGridController: 'financials-accounts-accountsgrid',
                entityType: 'Account',
                isTwoEntityPicker: false
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('RollUpDivision'),
            dataIndex: 'divisionJobNumber',
            sortable: true,
            groupable: true,
            flex: 1,
            editor: {
                xtype: 'chachingcombobox',
                store: new Chaching.store.utilities.autofill.DivisionListStore(),
                valueField: 'rollupJobId',
                displayField: 'divisionJobNumber',
                queryMode: 'remote',
                minChars: 2,
                modulePermissions: {
                    read: abp.auth.isGranted('Pages.Financials.Accounts.Divisions'),
                    create: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Create'),
                    edit: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Edit'),
                    destroy: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Delete')
                },
                primaryEntityCrudApi: {
                    read: abp.appPath + 'api/services/app/jobUnit/GetDivisionList',
                    create: abp.appPath + 'api/services/app/divisionUnit/CreateDivisionUnit',
                    update: abp.appPath + 'api/services/app/divisionUnit/UpdateDivisionUnit',
                    destroy: abp.appPath + 'api/services/app/divisionUnit/DeleteDivisionUnit'
                },
                createEditEntityType: 'financials.accounts.divisions',
                createEditEntityGridController: 'financials-accounts-divisionsgrid',
                entityType: 'Division',
                identificationKey: 'isDivision'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('TaxCredit'),
            dataIndex: 'taxCreditName',
            sortable: true,
            groupable: true,
            flex: 1,
            editor: {
                xtype: 'combobox',
                queryMode: 'local',
                bind: {
                    store: '{getTaxCreditList}'
                },
                valueField: 'taxCreditId',
                displayField: 'taxCreditName'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Currency'),
            dataIndex: 'typeOfCurrency',
            sortable: true,
            groupable: true,
            flex: 1,
            editor: {
                xtype: 'combobox',
                queryMode: 'local',
                bind: {
                    store: '{typeOfCurrencyList}'
                },
                valueField: 'typeOfCurrencyId',
                displayField: 'typeOfCurrency'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Status'),
            dataIndex: 'jobStatusName',
            sortable: true,
            groupable: true,
            flex: 1,
            editor: {
                xtype: 'combobox',
                queryMode: 'local',
                store: 'utilities.ProjectStatusStore',
                valueField: 'typeOfJobStatusId',
                displayField: 'jobStatusName'
            }
        }
    ]
});
