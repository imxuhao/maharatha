
Ext.define('Chaching.view.tenants.TenantsGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.tenants.TenantsGridController',
        'Chaching.view.tenants.TenantsGridModel'
    ],

    controller: 'tenants-tenantsgrid',
    viewModel: {
        type: 'tenants-tenantsgrid'
    },
    xtype:'host.tenants',
    store: 'Personnel',
    title:'Personnel',
    padding: 5,
    headerButtonsConfig:[
    {
        xtype: 'displayfield',
        value: abp.localization.localize("Tenants"),
        ui:'headerTitle'
    }, {
        xtype: 'displayfield',
        value: abp.localization.localize("TenantsHeaderInfo"),
        ui: 'headerSubTitle'
    }, '->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        text: abp.localization.localize("CreateNewTenant").toUpperCase(),
        iconCls: 'fa fa-plus',
        iconAlign:'left'
    }],
    requireExport: true,
    requireMultiSearch:true,
    requireMultisort:true,
    columns: [
        {
            text: 'Name',
            dataIndex: 'name',
            stateId: 'name',
            sortable: true,
            width: 160,
            // simplest filter configuration
                            
            filterField: {
                xtype: 'textfield',
                width:160,
                emptyText:'Enter name to search'
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
            flex: 1

            // equivalent to filterField:true
            // as textfield is created by default
            ,
            filterField: {
                xtype: 'textfield',
                flex: 1,
                emptyText: 'Enter email to search'
            }
        }, {
            text: 'Phone',
            dataIndex: 'phone',
            sortable: true,
            width: 110,
            filterField: {
                xtype: 'textfield',
                width: 160,
                emptyText: 'Enter phone to search'
            }
        }
    ]
});
