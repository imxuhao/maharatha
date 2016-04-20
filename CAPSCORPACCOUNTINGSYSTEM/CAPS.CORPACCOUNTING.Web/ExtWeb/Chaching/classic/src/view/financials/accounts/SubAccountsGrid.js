
Ext.define('Chaching.view.financials.accounts.SubAccountsGrid',{
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
             text: app.localize('Caption'),
             dataIndex: 'caption',
             sortable: true,
             groupable: true,
             width: '93%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter Chart Of Account to search'
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
             hidden: true,
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter Description to search'
             }, editor: {
                 xtype: 'textfield',
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('IsApproved'),
             dataIndex: 'isApproved',
             sortable: true,
             groupable: true,
             width: '5%',
             hidden: true,
             renderer: function (val) {
                 if (val) return 'YES';
                 else return 'NO';
             },
             filterField: {
                 xtype: 'combobox',
                 valueField: 'value',
                 displayField: 'text',
                 store: {
                     fields: [{ name: 'text' }, { name: 'value' }],
                     data: [{ text: 'YES', value: true }, { text: 'NO', value: false }]
                 }
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('IsPrivate'),
             dataIndex: 'isPrivate',
             sortable: true,
             groupable: true,
             hidden: true,
             width: '5%',
             renderer: function (val) {
                 if (val) return 'YES';
                 else return 'NO';
             },
             filterField: {
                 xtype: 'combobox',
                 valueField: 'value',
                 displayField: 'text',
                 store: {
                     fields: [{ name: 'text' }, { name: 'value' }],
                     data: [{ text: 'YES', value: true }, { text: 'NO', value: false }]
                 }
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('IsCorporate'),
             dataIndex: 'isCorporate',
             sortable: true,
             groupable: true,
             width: '5%',
             hidden: true,
             renderer: function (val) {
                 if (val) return 'YES';
                 else return 'NO';
             },
             filterField: {
                 xtype: 'combobox',
                 valueField: 'value',
                 displayField: 'text',
                 store: {
                     fields: [{ name: 'text' }, { name: 'value' }],
                     data: [{ text: 'YES', value: true }, { text: 'NO', value: false }]
                 }
             }, editor: {
                 xtype: 'checkbox'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('IsNumeric'),
             dataIndex: 'isNumeric',
             sortable: true,
             groupable: true,
             width: '5%',
             hidden: true,
             renderer: function (val) {
                 if (val) return 'YES';
                 else return 'NO';
             },
             filterField: {
                 xtype: 'combobox',
                 valueField: 'value',
                 displayField: 'text',
                 store: {
                     fields: [{ name: 'text' }, { name: 'value' }],
                     data: [{ text: 'YES', value: true }, { text: 'NO', value: false }]
                 }
             }, editor: {
                 xtype: 'checkbox'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('ConvertToNewCOA'),
             dataIndex: 'linkChartOfAccountName',
             sortable: true,
             groupable: true,
             width: '15%',
             hidden: false
        , filterField: {
            xtype: 'textfield',
            width: '100%',
            emptyText: 'Enter Convert To New COA to search'
        }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('StdGroupTotal'),
             dataIndex: 'standardGroupTotal',
             sortable: true,
             groupable: true,
             width: '15%',
             hidden: false,
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter Std Group Total to search'
             }
         }
    ]
});
