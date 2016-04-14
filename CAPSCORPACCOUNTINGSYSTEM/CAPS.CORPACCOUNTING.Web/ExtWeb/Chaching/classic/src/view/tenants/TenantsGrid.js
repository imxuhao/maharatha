
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
        action:'create',
        text: abp.localization.localize("CreateNewTenant").toUpperCase(),
        checkPermission: true,
        iconCls: 'fa fa-plus',
        routeName: 'host.tenants.create',
        iconAlign: 'left'
    }],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable:true,
    editingMode: 'row',
    createNewMode:'popup',
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditTenant'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewTenant'),
        iconCls: 'fa fa-plus'
    },
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('TenancyCodeName'),
            dataIndex: 'tenancyName',
            stateId: 'tenancyName',
            sortable: true,
            width: '28%',
            groupable:true,
            // simplest filter configuration
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('TTenancyCodeName')
                //plugins: [{
                //    ptype: 'saki-ficn'
                //    , iconCls: 'fa fa-info'
                //    , qtip: 'Enter name to search'
                //}]
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Name'),
            dataIndex: 'name',
            sortable: true,
            groupable: true,
            width: '30%'
            // equivalent to filterField:true
            // as textfield is created by default
            ,
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('TName')
            },
            editor: {
                xtype: 'textfield'
            }
        },
        /*{//uncomment if required
            xtype: 'gridcolumn',
            text: app.localize('Edition'),
            dataIndex: 'editionDisplayName',
            sortable: true,
            groupable: true,
            width: '20%',
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('TEdition')
            },
            editor: {
                xtype: 'combobox',
                name:'editionDisplayName',
                displayField: 'editionDisplayName',
                valueField: 'editionId',
                bind: {
                    store: '{editionsForComboBox}'
                },
                listeners: {
                    change:'onEditionChange'
                }
            }
        },*/
        {
            xtype: 'gridcolumn',
            text: app.localize('Active'),
            dataIndex: 'isActive',
            sortable: true,
            groupable: true,
            width: '10%',
            renderer: Chaching.utilities.ChachingRenderers.statusRenderer,
            filterField: {
                xtype: 'combobox',
                valueField: 'value',
                displayField:'text',
                store: {
                    fields:[{name:'text'},{name:'value'}],
                    data:[{text:'YES',value:'true'},{text:'NO',value:'false'}]
                }
            },editor: {
                xtype: 'checkboxfield',
                inputValue:true,
                name: 'isActive'
            }
        },
         {
             xtype: 'gridcolumn',
             text: app.localize('CreationTime'),
             dataIndex: 'creationTime',
             sortable: true,
             groupable: true,
             width: '25%',
             renderer: Chaching.utilities.ChachingRenderers.renderDateTime,
             filterField: {
                 xtype: 'dateSearchField',
                 dataIndex: 'creationTime',
                 width: '100%'
                 //emptyText: app.localize('TAdminEmailAddress')
             }
         }
    ]
});
