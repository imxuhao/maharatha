
Ext.define('Chaching.view.tenants.TenantsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.tenants.TenantsGridController',
        'Chaching.view.tenants.TenantsGridModel'
    ],

    controller: 'tenants-tenantsgrid',
    viewModel: {
        type: 'tenants-tenantsgrid'
    },
    xtype: 'host.tenants',
    store: 'Personnel',
    name: 'Tenants',
    padding: 5,
    headerButtonsConfig: [
    {
        xtype: 'displayfield',
        value: abp.localization.localize("Tenants"),
        ui: 'headerTitle'
    }, {
        xtype: 'displayfield',
        value: abp.localization.localize("TenantsHeaderInfo"),
        ui: 'headerSubTitle'
    }, '->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        text: abp.localization.localize("CreateNewTenant").toUpperCase(),
        checkPermission: true,
        iconCls: 'fa fa-plus',
        iconAlign: 'left'
    }],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable:true,
    editingMode: 'row',
    columnLines: true,

    columns: [
        {
            text: 'Name',
            dataIndex: 'name',
            stateId: 'name',
            sortable: true,
            width: '25%',
            // simplest filter configuration
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: 'Enter name to search'
                //plugins: [{
                //    ptype: 'saki-ficn'
                //    , iconCls: 'fa fa-info'
                //    , qtip: 'Enter name to search'
                //}]
            }
        }, {
            text: 'Email',
            dataIndex: 'email',
            sortable: true,
            width: '50%'
            // equivalent to filterField:true
            // as textfield is created by default
            ,
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: 'Enter email to search'
            }
        }, {
            text: 'Phone',
            dataIndex: 'phone',
            sortable: true,
            width: '25%',
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: 'Enter phone to search'
            }
        }
    ]
});
