
Ext.define('Chaching.view.projects.projectmaintenance.LineNumbersGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.projects.projectmaintenance.LineNumbersGridController'        
    ],
    controller: 'projects-projectmaintenance-linenumbersgrid',

    xtype: 'projects.projectmaintenance.linenumbers',
    store: 'projects.projectmaintenance.LinesStore',
    name: 'Projects.Projectmaintenance.Lines',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
        create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete')
    },
    padding: 5,
    gridId: 14,
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("Lines"),
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
      }],
    importConfig: {
        entity: app.localize('Lines'),
        isRequireImport: true,
        importStoreClass: 'imports.LinesImportStore',
        targetGrid: null,
        targetUrl: abp.appPath + 'api/services/app/accountUnit/BulkAccountInsert',
        bulkListInputName: 'accountList'
    },
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    forceFit:true,
    editWndTitleConfig: {
        title: app.localize('EditLine'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewLine'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewLine'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('LineNumber'),
            dataIndex: 'accountNumber',
            sortable: true,
            groupable: true,
            flex:1,
            //width: '10%',
            filterField: {
                xtype: 'textfield',
                width: '15%',
                emptyText: app.localize('LineSearch')
            }, editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Description'),
            dataIndex: 'caption',
            sortable: true,
            groupable: true,
            //width: '15%',
            flex: 1,
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('DescriptionSearch')
            }, editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Classification'),
            dataIndex: 'typeOfAccount',
            sortable: true,
            groupable: true,
            itemId: 'typeOfAccountId',
            //width: '15%',
            flex: 1,
            filterField: {
                xtype: 'combobox',
                valueField: 'typeOfAccountId',
                displayField: 'typeOfAccount',
                searchProperty: 'typeOfAccountId',
                queryMode: 'local',
                loadStoreOnCreate: true,
                isViewmodelStore: true,
                forceSelection: true,
                bind: {
                    store: '{typeOfAccountList}'
                }
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Consolidation'),
            dataIndex: 'typeofConsolidation',
            sortable: true,
            groupable: true,
            flex: 1,
            //width: '15%',
            filterField: {
                xtype: 'combobox',
                valueField: 'typeofConsolidationId',
                displayField: 'typeofConsolidation',
                loadStoreOnCreate: true,
                forceSelection: true,
                isViewmodelStore: true,
                isEnum: true,
                searchProperty: 'typeofConsolidationId',
                queryMode: 'local',
                bind: {
                    store: '{typeofConsolidationList}'
                }
            }
        },
         {
             xtype: 'gridcolumn',
             text: app.localize('JournalsAllowed'),
             dataIndex: 'isEnterable',
             sortable: true,
             groupable: true,
             flex: 1,
             //width: '10%',
             renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
             filterField: {
                 xtype: 'combobox',
                 valueField: 'value',
                 displayField: 'text',
                 store: {
                     fields: [{ name: 'text' }, { name: 'value' }],
                     data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                 }
             }, editor: {
                 xtype: 'checkbox',
                 inputValue: true,
                 uncheckedValue : false
             }
         }
         ,
         {
             xtype: 'gridcolumn',
             text: app.localize('RollUpAccount'),
             dataIndex: 'rollUpAccountCaption',
             sortable: true,
             groupable: true,
             flex: 1,
             //width: '15%',
             itemId: 'rollupAccountId',            
             filterField: {
                 xtype: 'chachingcombobox',
                 store: new Chaching.store.utilities.autofill.RollupAccountListStore(),
                 width: '100%',
                 searchProperty: 'rollupAccountId',
                 valueField: 'accountId',
                 displayField: 'accountNumber',
                 forceSelection: true,
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
             dataIndex: 'rollUpDivision',
             itemId: 'rollupJobId',
             sortable: true,
             groupable: true,
             flex: 1,
             //width: '10%',
             filterField: {
                 xtype: 'chachingcombobox',
                 store: new Chaching.store.utilities.autofill.DivisionListStore(),
                 valueField: 'jobId',
                 displayField: 'jobNumber',
                 queryMode: 'remote',
                 forceSelection: true,
                 searchProperty : 'rollupJobId',
                 minChars: 2,
                 //useDisplayFieldToSearch: true,
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
                 identificationKey: 'isDivision',
                 isTwoEntityPicker: false
             }
         },
         {
             xtype: 'gridcolumn',
             text: app.localize('Currency'),
             dataIndex: 'typeOfCurrency',
             sortable: true,
             groupable: true,
             flex: 1,
            //width: '10%',
             itemId: 'typeOfCurrencyId',
             filterField: {
                 xtype: 'combobox',
                 valueField: 'typeOfCurrencyId',
                 displayField: 'typeOfCurrency',
                 queryMode: 'local',
                 searchProperty: 'typeOfCurrencyId',
                 loadStoreOnCreate: true,
                 isViewmodelStore: true,
                 forceSelection: true,
                // isEnum: true,
                 bind: {
                     store: '{typeOfCurrencyList}'
                 }
                 //,
                 //listeners: {
                 //    beforequery: function (query, eOpts) {
                 //        var grid = this.up().grid;
                 //        if (grid) {                           
                 //            var myStore = this.getStore();                             
                 //        }
                 //    }
                 //}
             }
         }
    ]
});
