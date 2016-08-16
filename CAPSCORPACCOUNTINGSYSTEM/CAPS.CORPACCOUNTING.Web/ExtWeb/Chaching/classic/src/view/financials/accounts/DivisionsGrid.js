
Ext.define('Chaching.view.financials.accounts.DivisionsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype: 'financials.accounts.divisions',
    requires: [
        'Chaching.view.financials.accounts.DivisionsGridController'
    ],

    controller: 'financials-accounts-divisionsgrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts.Divisions'),
        create: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Delete')
    },
    store:'financials.accounts.DivisionsStore',
    padding: 5,
    gridId: 12,
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("Divisions"),
          ui: 'headerTitle'
      }, '->', {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          action: 'create',
          text: abp.localization.localize("Add").toUpperCase(),
          tooltip: app.localize('CreateNewDivision'),
          checkPermission: true,
          iconCls: 'fa fa-plus',
          iconAlign: 'left'
      }], importConfig: {
          entity: 'Divisions',
          isRequireImport: true
      },
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditDivision'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewDivision'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewDivision'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    columns: [
         {
             xtype: 'gridcolumn',
             text: app.localize('Description'),
             dataIndex: 'caption',
             sortable: true,
             groupable: true,
             width: '80%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter Description to search'
             }, editor: {
                 xtype: 'textfield'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('Active'),
             dataIndex: 'isActive',             
             sortable: true,
             groupable: true,
             width: '10%',
             hidden: false,
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

    ]
});
