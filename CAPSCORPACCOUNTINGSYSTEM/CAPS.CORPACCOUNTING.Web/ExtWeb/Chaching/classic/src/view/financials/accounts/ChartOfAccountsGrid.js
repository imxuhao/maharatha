
Ext.define('Chaching.view.financials.accounts.ChartOfAccountsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.financials.accounts.ChartOfAccountsGridController'
    ],

    controller: 'financials-accounts-chartofaccountsgrid',

    xtype: 'widget.financials.accounts.coa',
    store: 'financials.accounts.ChartOfAccountStore',
    name: 'Financials.Accounts.ChartOfAccounts',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts.ChartOfAccounts'),
        create: abp.auth.isGranted('Pages.Financials.Accounts.ChartOfAccounts.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Accounts.ChartOfAccounts.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Accounts.ChartOfAccounts.Delete'),
    },
    padding: 5,
    gridId: 9,
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("ChartOfAccount"),
          ui: 'headerTitle'
      }, '->', {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          action: 'create',
          text: abp.localization.localize("Add").toUpperCase(),
          tooltip: app.localize('CreatingNewCOA'),
          checkPermission: true,
          iconCls: 'fa fa-plus',
          routeName: 'coa.create',
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
        title: app.localize('EditCOA'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreatingNewCOA'),
        iconCls: 'fa fa-plus'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    listeners: {
        cellclick: 'onChartOfAccountClicked'
    },
    columns: [
         {
             xtype: 'gridcolumn',
             text: app.localize('Caption'),
             dataIndex: 'caption',
             sortable: true,
             groupable: true,
             width: '50%',
             renderer:Chaching.utilities.ChachingRenderers.rendererHyperLink,
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ChartOfAccountSearch')
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
                 emptyText: app.localize('DescriptionSearch'),
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
             width: '22%',
             hidden: true
        , filterField: {
            xtype: 'textfield',
            width: '100%',
            emptyText:  app.localize('ConvertToNewCOASearch')
        }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('StdGroupTotal'),
             dataIndex: 'standardGroupTotal',
             sortable: true,
             groupable: true,
             width: '20%',
             hidden: false,
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 entityName: '',
                 emptyText: app.localize('StdGroupTotalSearch')
             }
         }
    ]
});
