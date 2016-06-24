
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
    gridId: 10,
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
        title: app.localize('EditSubAccount'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewSubAccounts'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewSubAccount'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,

    columns: [
         {
             xtype: 'gridcolumn',
             text: app.localize('SubAccountNumber'),
             dataIndex: 'subAccountNumber',
             sortable: true,
             groupable: true,
             width: '15%',

             filterField: {
                 xtype: 'textfield',
                 width: '15%',
                 emptyText: app.localize('SubAccountNumberSearch')
             }, editor: {
                 xtype: 'textfield',
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('Description'),
             dataIndex: 'description',
             sortable: true,
             groupable: true,
             width: '20%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('DescriptionSearch')
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
                  queryMode: 'local',
                  triggerAction: 'all',
                  loadStoreOnCreate: true,
                  isViewmodelStore: true,
                  forceSelection: true,
                  isEnum: true,
                  searchProperty:'typeofSubAccountId',
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
             width: '14%',
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
                 inputValue: 'true',
                 uncheckedValue: 'false'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('CorpSubAccount'),
             dataIndex: 'isCorporateSubAccount',
             sortable: true,
             groupable: true,
             width: '14%',
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
                 inputValue: 'true',
                 uncheckedValue: 'false'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('ProjectSubAccount'),
             dataIndex: 'isProjectSubAccount',
             sortable: true,
             groupable: true,
             width: '15%',
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
                 inputValue: 'true',
                 uncheckedValue: 'false'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('AccountSpecific'),
             dataIndex: 'isAccountSpecific',
             sortable: true,
             groupable: true,
             width: '12%',
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
                 inputValue: 'true',
                 uncheckedValue: 'false'
             }
         }

         //, {
         //    xtype: 'gridcolumn',
         //    text: app.localize('Trans#'),
         //    dataIndex: 'subAccountId',
         //    sortable: true,
         //    groupable: true,
         //    width: '10%',
         //    hidden: true
         //}
    ]
});
