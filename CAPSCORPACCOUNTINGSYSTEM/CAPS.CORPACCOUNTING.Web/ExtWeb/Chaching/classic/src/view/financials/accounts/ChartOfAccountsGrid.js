
Ext.define('Chaching.view.financials.accounts.ChartOfAccountsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.financials.accounts.ChartOfAccountsGridController'
    ],

    controller: 'financials-accounts-chartofaccountsgrid',

    xtype: 'financials.accounts.coa',
    store: 'financials.accounts.ChartOfAccountStore',
    name: 'Financials.Accounts.ChartOfAccounts',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts.ChartOfAccounts'),
        create: abp.auth.isGranted('Pages.Financials.Accounts.ChartOfAccounts.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Accounts.ChartOfAccounts.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Accounts.ChartOfAccounts.Delete')
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
    viewWndTitleConfig: {
        title: app.localize('ViewCOA'),
        iconCls: 'fa fa-th'
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
             //width: '50%',
             flex: 1,
             renderer:Chaching.utilities.ChachingRenderers.rendererHyperLink,
             filterField: {
                 xtype: 'textfield',
                 width: '100%'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('ChartType'),
             dataIndex: 'typeOfChart',
             sortable: true,
             groupable: true,
             //width: '15%',
             //hidden: true,
             flex: 1,
             filterField: {
                 xtype: 'combobox',
                 width: '100%',
                 displayField: 'typeOfChart',
                 valueField: 'typeOfChartId',
                 queryMode: 'local',
                 searchProperty: 'typeOfChartId',
                 forceSelection: true,
                 isEnum: true,
                 isViewmodelStore: true,
                 loadStoreOnCreate: true,
                 bind: {
                     store: '{TypeOfChartList}'
                 }
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('StdGroupTotal'),
             dataIndex: 'standardGroupTotal',
             sortable: true,
             groupable: true,
             //width: '20%',
             flex: 1,
             hidden: false,
             filterField: {
                 xtype: 'combobox',
                 width: '100%',
                 displayField: 'standardGroupTotal',
                 valueField: 'standardGroupTotalId',
                 searchProperty: 'standardGroupTotalId',
                 forceSelection: true,
                 queryMode:'local',
                 isEnum: true,
                 isViewmodelStore: true,
                 loadStoreOnCreate: true,
                 bind: {
                     store: '{StandardGroupTotalList}'
                 }
             }
         }, /*{
             xtype: 'gridcolumn',
             text: app.localize('IsApproved'),
             dataIndex: 'isApproved',
             sortable: true,
             groupable: true,
             maxWidth: 120,
             //hidden: true,
             flex: 1,
             renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
             filterField: {
                 xtype: 'combobox',
                 forceSelection: true,
                 valueField: 'value',
                 displayField: 'text',
                 store: {
                     fields: [{ name: 'text' }, { name: 'value' }],
                     data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                 }
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('IsPrivate'),
             dataIndex: 'isPrivate',
             sortable: true,
             groupable: true,
             //hidden: true,
             maxWidth: 120,
             flex: 1,
             renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
             filterField: {
                 xtype: 'combobox',
                 forceSelection: true,
                 valueField: 'value',
                 displayField: 'text',
                 store: {
                     fields: [{ name: 'text' }, { name: 'value' }],
                     data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                 }
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('IsCorporate'),
             dataIndex: 'isCorporate',
             sortable: true,
             groupable: true,
             maxWidth: 120,
             //hidden: true,
             flex: 1,
             renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
             filterField: {
                 xtype: 'combobox',
                 forceSelection: true,
                 valueField: 'value',
                 displayField: 'text',
                 store: {
                     fields: [{ name: 'text' }, { name: 'value' }],
                     data: [{ text: 'YES', value: 'true' }, { text: 'NO', value: 'false' }]
                 }
             }, editor: {
                 xtype: 'checkbox'
             }
         },*/ {
             xtype: 'gridcolumn',
             text: app.localize('IsNumeric'),
             dataIndex: 'isNumeric',
             sortable: true,
             groupable: true,
             flex: 1,
             maxWidth: 120,
             //hidden: true,
             renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
             filterField: {
                 xtype: 'combobox',
                 forceSelection: true,
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
            text: app.localize('MappingChart').initCap(),
            dataIndex: 'linkChartOfAccountName',
            sortable: true,
            groupable: true,
            flex: 1,
             filterField: {
                 xtype: 'combobox',
                 forceSelection: true,
                 width: '100%',
                 displayField: 'linkChartOfAccount',
                 valueField: 'linkChartOfAccountID',
                 searchProperty: 'linkChartOfAccountID',
                 queryMode: 'local',
                 isEnum: true,
                 isViewmodelStore: true,
                 loadStoreOnCreate: true,
                 bind: {
                     store: '{linkChartOfAccountList}'
                 }
             }
        }
    ]
});
