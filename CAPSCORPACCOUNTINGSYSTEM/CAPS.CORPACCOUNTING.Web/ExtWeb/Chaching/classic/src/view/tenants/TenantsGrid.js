
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
    store: 'tenants.TenantsStore',
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
        tooltip: app.localize('CreateNewTenant'),
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
    multiColumnSort: true,
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('TenancyCodeName'),
            dataIndex: 'tenancyName',
            stateId: 'tenancyName',
            sortable: true,
            width: '25%',
            groupable:true,
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
            },
            editor: {
                xtype:'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Name'),
            dataIndex: 'name',
            sortable: true,
            groupable: true,
            width: '25%'
            // equivalent to filterField:true
            // as textfield is created by default
            ,
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: 'Enter email to search'
            },
            editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Edition'),
            dataIndex: 'editionDisplayName',
            sortable: true,
            groupable: true,
            width: '20%',
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: 'Enter phone to search'
            },
            editor: {
                xtype: 'textfield'
            }
        },
        {
            xtype: 'gridcolumn',
            text: app.localize('Active'),
            dataIndex: 'isActive',
            sortable: true,
            groupable: true,
            width: '10%',
            filterField: {
                xtype: 'combobox',
                valueField: 'value',
                displayField:'text',
                store: {
                    fields:[{name:'text'},{name:'value'}],
                    data:[{text:'YES',value:true},{text:'NO',value:false}]
                }
            }
        },
        {
            xtype: 'gridcolumn',
            text: app.localize('CreationTime'),
            dataIndex: 'creationTime',
            sortable: true,
            groupable: true,
            width: '20%',
            renderer: function(value) {
                return Ext.Date.format(value, 'm/d/Y');
            },
            filterField: {
                xtype: 'datefield'
            }
        }
    ]
});
