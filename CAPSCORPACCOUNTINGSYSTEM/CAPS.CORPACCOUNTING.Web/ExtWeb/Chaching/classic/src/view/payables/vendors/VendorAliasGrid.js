Ext.define('Chaching.view.vendors.VendorAliasGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    requires: [
        'Chaching.view.payables.vendors.VendorAliasGridController'
    ],
    controller: 'vendors-vendoraliasgrid',
    xtype: 'vendoralias',
    name: 'vendoralias',
    modulePermissions: {
        read: true,
        create: true,
        edit: true,
        destroy: true
    },
    padding: 5,
    gridId: 18,
    headerButtonsConfig: [{
        xtype: 'displayfield',
        value: abp.localization.localize("VendorAlias"),
        ui: 'headerTitle'
    }, '->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        action: 'create',
        text: abp.localization.localize("CreatingVendorAlias").toUpperCase(),
        checkPermission: false,
        iconCls: 'fa fa-plus',
        iconAlign: 'left'
    }],
     
    requireExport: false,
    requireMultiSearch: false,
    requireMultisort: false,
    isEditable: true,
    editingMode: 'cell',
    createNewMode: 'inline',
    columnLines: true,
    multiColumnSort: false,
    manageViewSetting: false,
    showPagingToolbar: false,
    hideClearFilter: true,
    //store: {
    //    model: 'Chaching.model.payables.vendors.VendorAliasModel',
    //    data: []
    //},
    store: 'payables.vendors.VendorsAliasStore',
    columns: [{
        text: app.localize('Names'),
        dataIndex: 'aliasName',
        xtype: 'gridcolumn',
        sortable: false,
        groupable: false,
        //flex:1,
        width: '80%',
        editor: {
            xtype: 'textfield',
            name: 'aliasName'
        }
    }]
});