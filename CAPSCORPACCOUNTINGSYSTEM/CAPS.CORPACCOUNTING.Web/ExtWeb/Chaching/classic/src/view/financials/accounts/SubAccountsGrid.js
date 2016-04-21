
Ext.define('Chaching.view.financials.accounts.SubAccountsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.financials.accounts.SubAccountsGridController'
    ],

    controller: 'financials-accounts-subaccountsgrid',

    xtype: 'widget.financials.accounts.subaccounts',
    store: 'financials.accounts.SubAccountsStore',
    name: 'Financials.Accounts.SubAccounts',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts.SubAccounts'),
        create: abp.auth.isGranted('Pages.Financials.Accounts.SubAccounts.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Accounts.SubAccounts.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Accounts.SubAccounts.Delete'),
    },
    padding: 5,
    gridId: 9,
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("SubAccounts"),
          ui: 'headerTitle'
      }, '->', {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          action: 'create',
          text: abp.localization.localize("CreateNewSubAccounts").toUpperCase(),
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
        title: app.localize('EditSubAccount'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewSubAccounts'),
        iconCls: 'fa fa-plus'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    columns: [
         {
             xtype: 'gridcolumn',
             text: app.localize('Number'),
             dataIndex: 'subAccountNumber',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '15%',
                 emptyText: 'Enter Number to search'
             }, editor: {
                 xtype: 'textfield',
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
                 width: '100%',
                 emptyText: 'Enter Description to search'
             }, editor: {
                 xtype: 'textfield',
             }
         },
          {
              xtype: 'gridcolumn',
              text: app.localize('TypeofSubAccount'),
              dataIndex: 'typeofSubAccount',
              sortable: true,
              groupable: true,
              width: '15%',
              filterField: {
                  xtype: 'combobox',
                  valueField: 'typeofSubAccountId',
                  displayField: 'typeofSubAccount',                  
                  bind: {
                      store: '{typeOfSubAccountList}'
                  }
              }
          },

         {
             xtype: 'gridcolumn',
             text: app.localize('JournalsAllowed'),
             dataIndex: 'isActive',
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
             text: app.localize('CorpSubAccount'),
             dataIndex: 'isCorporateSubAccount',
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
             text: app.localize('ProjectSubAccount'),
             dataIndex: 'isProjectSubAccount',
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
             text: app.localize('AccountSpecific'),
             dataIndex: 'isAccountSpecific',
             sortable: true,
             groupable: true,
             width: '6%',
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
             text: app.localize('MandatoryEntry'),
             dataIndex: 'isMandatory',
             sortable: true,
             groupable: true,
             width: '6%',
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
             text: app.localize('BudgetInclusive'),
             dataIndex: 'isBudgetInclusive',
             sortable: true,
             groupable: true,
             width: '6%',
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
             text: app.localize('GroupCopyLabel'),
             dataIndex: 'groupCopyLabel',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter Group Copy Label to search'
             }, editor: {
                 xtype: 'textfield',
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('Trans#'),
             dataIndex: 'subAccountId',
             sortable: true,
             groupable: true,
             width: '10%',
             hidden: true
         }
    ]
});
