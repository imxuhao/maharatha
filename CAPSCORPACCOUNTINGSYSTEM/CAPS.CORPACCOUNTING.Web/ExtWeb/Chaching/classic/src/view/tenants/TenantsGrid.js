
Ext.define('Chaching.view.tenants.TenantsGrid',{
    extend: 'Ext.grid.Panel',

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
    dockedItems: [
        {
            xtype: 'toolbar',
            docked: 'top',
            layout: {
                type: 'hbox',
                pack:'left'
            },
            items:[
            {
                xtype:'displayfield',
                value: 'Tenants',
                width:100
            },'->', {
                text: 'CREATE NEW TENANT',
                iconCls: 'fa fa-plus',
                iconAlign:'left'
            }]
        }
    ],
    plugins: [
        {
            ptype: 'saki-gms',
            clearItemIconCls:'icon-settings',
            pluginId: 'gms',
            height:32,
            filterOnEnter: false
        }
    ],
    features:[{
        ftype:'ux-gmsrt'
,displaySortOrder:true
    }],
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
                width:'90%',
                plugins: [{
                    ptype: 'saki-ficn'
                    , iconCls: 'fa fa-info'
                    , qtip: 'Enter name to search'
                }]
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
                width:'90%'
                , plugins: [{
                    ptype: 'saki-ficn'
                    , iconCls: 'fa fa-info'
                    , qtip: 'Enter Email Address to search'
                }]
            }
        }, {
            text: 'Phone',
            dataIndex: 'phone',
            sortable: true,
            width: 110,
            //align: 'right',
            //format: '0,000',
            filterField: {
                xtype: 'textfield',
                width: '90%'
               , plugins: [{
                   ptype: 'saki-ficn'
                   , iconCls: 'fa fa-info'
                   , qtip: 'Enter phone to search'
               }]
            }
        }
    ]
});
