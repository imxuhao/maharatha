Ext.define('Chaching.view.financials.accounts.AccountsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    requires: [
        'Chaching.view.financials.accounts.AccountsGridController'
    ],
    controller: 'financials-accounts-accountsgrid',

    xtype: 'widget.financials.accounts.accounts',
    store: 'financials.accounts.AccountsStore',
    name: 'Financials.Accounts.Accounts',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
        create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete'),
    },
    padding: 5,
    gridId: 9,
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("FinancialAccounts"),
          ui: 'headerTitle'
      }, '->', {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          action: 'create',
          text: abp.localization.localize("CreateNewAccount").toUpperCase(),
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
        title: app.localize('EditAccount'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewAccount'),
        iconCls: 'fa fa-plus'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    columns: [
         {
             xtype: 'gridcolumn',
             text: app.localize('AccountNumber'),
             dataIndex: 'accountNumber',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '15%',
                 emptyText: app.localize('AccountSearch')
             }, editor: {
                 xtype: 'textfield',
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('Description'),
             dataIndex: 'caption',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('DescriptionSearch'),
             }, editor: {
                 xtype: 'textfield',
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('NewAccount'),
             dataIndex: 'typeOfAccount',
             sortable: true,
             groupable: true,
             width: '8%',
             filterField: {
                 xtype: 'combobox',
                 valueField: 'typeOfAccountId',
                 displayField: 'typeOfAccount',
                 bind: {
                     store: '{typeOfAccountList}'
                 }
             }
         }
         , {
             xtype: 'gridcolumn',
             text: app.localize('Consolidation'),
             dataIndex: 'typeofConsolidation',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'combobox',
                 valueField: 'typeofConsolidationId',
                 displayField: 'typeofConsolidation',
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
             width: '8%',
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
             dataIndex: 'isRollupAccount',
             sortable: true,
             groupable: true,
             width: '8%',
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
             text: app.localize('EliminationAccount'),
             dataIndex: 'isElimination',
             sortable: true,
             groupable: true,
             width: '8%',
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
         , {
             xtype: 'gridcolumn',
             text: app.localize('Currency'),
             dataIndex: 'typeOfCurrency',
             sortable: true,
             groupable: true,
             width: '8%',
             filterField: {
                 xtype: 'combobox',
                 valueField: 'typeOfCurrencyId',
                 displayField: 'typeOfCurrency',
                 bind: {
                     store: '{typeOfCurrencyList}'
                 }
             }
         }
          , {
              xtype: 'gridcolumn',
              text: app.localize('RateTypeOverride'),
              dataIndex: 'typeOfCurrencyRate',
              sortable: true,
              groupable: true,
              width: '8%',
              filterField: {
                  xtype: 'combobox',
                  valueField: 'typeOfCurrencyRateId',
                  displayField: 'typeOfCurrencyRate',
                  bind: {
                      store: '{typeOfCurrencyRateList}'
                  }
              }
          }
          ,
         {
             xtype: 'gridcolumn',
             text: app.localize('Multi-CurrencyReval'),
             dataIndex: 'isAccountRevalued',
             sortable: true,
             groupable: true,
             width: '8%',
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
         }, {
             xtype: 'gridcolumn',
             text: app.localize('NewAccount'),
             dataIndex: 'linkAccount',
             sortable: true,
             groupable: true,
             width: '8%'
             , editor: {
                 xtype: 'combobox',
                 typeAhead: true,
                 valueField: 'linkAccountId',
                 displayField: 'linkAccount',
                 bind: {
                     store: '{linkAccountListByCoaId}'
                 }
             }
         }
    ]
})