
Ext.define('Chaching.view.projects.projectmaintenance.LineNumbersGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.projects.projectmaintenance.LineNumbersGridController'        
    ],
    controller: 'projects-projectmaintenance-linenumbersgrid',

    xtype: 'widget.projects.projectmaintenance.linenumbers',
    store: 'projects.projectmaintenance.LinesStore',
    name: 'Projects.Projectmaintenance.Lines',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
        create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete'),
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
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditLine'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewLine'),
        iconCls: 'fa fa-plus'
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
            width: '10%',
            filterField: {
                xtype: 'textfield',
                width: '15%',
                emptyText: app.localize('LineSearch')
            }, editor: {
                xtype: 'textfield',
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Description'),
            dataIndex: 'caption',
            sortable: true,
            groupable: true,
            width: '15%',
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('DescriptionSearch'),
            }, editor: {
                xtype: 'textfield',
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Classification'),
            dataIndex: 'typeOfAccount',
            sortable: true,
            groupable: true,
            width: '15%',
            filterField: {
                xtype: 'combobox',
                valueField: 'typeOfAccountId',
                displayField: 'typeOfAccount',
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
            width: '15%',
            filterField: {
                xtype: 'combobox',
                valueField: 'typeofConsolidationId',
                displayField: 'typeofConsolidation',
                forceSelection:true,
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
             width: '10%',
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
                 xtype: 'checkbox'
             }
         }
         ,
         {
             xtype: 'gridcolumn',
             text: app.localize('RollUpAccount'),
             dataIndex: 'rollUpAccountCaption',
             sortable: true,
             groupable: true,
             width: '15%',
             itemId: 'rollupAccountId',
             filterField: {
                 xtype: 'combobox',
                 valueField: 'rollupAccountId',
                 displayField: 'rollupAccount',
                 queryMode: 'remote',
                 forceSelection:true,
                 bind: {
                     store: '{rollupAccountList}'
                 },
                 listeners: {
                     beforequery: function (query, eOpts) {
                         var grid = this.up().grid;
                         if (grid) {
                             var coaId = grid.coaId;
                             var myStore = this.getStore();
                             myStore.getProxy().setExtraParam('Id', coaId);
                         }
                     }
                 }
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('RollUpDivision'),
             dataIndex: 'rollUpDivision',
             itemId: 'rollupJobId',
             sortable: true,
             groupable: true,
             width: '10%',             
             filterField: {
                 xtype: 'combobox',
                 valueField: 'rollupDivisionId',
                 displayField: 'rollupDivision',
                 queryMode: 'remote',
                 forceSelection: true,
                 bind: {
                     store: '{rollupDivisionList}'
                 },              
                 
             }
         },
         {
             xtype: 'gridcolumn',
             text: app.localize('Currency'),
             dataIndex: 'typeOfCurrency',
             sortable: true,
             groupable: true,
             width: '10%',
             itemId: 'typeOfCurrencyId',
             filterField: {
                 xtype: 'combobox',
                 valueField: 'typeOfCurrencyId',
                 displayField: 'typeOfCurrency',
                 queryMode: 'remote',
                 forceSelection: true,
                 bind: {
                     store: '{typeOfCurrencyList}'
                 },
                 listeners: {
                     beforequery: function (query, eOpts) {
                         var grid = this.up().grid;
                         if (grid) {                           
                             var myStore = this.getStore();                             
                         }
                     }
                 }
             }
         },
    ]
});
