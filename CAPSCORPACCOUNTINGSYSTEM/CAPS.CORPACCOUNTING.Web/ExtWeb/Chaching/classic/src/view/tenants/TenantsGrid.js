
Ext.define('Chaching.view.tenants.TenantsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.tenants.TenantsGridController'
    ],

    controller: 'tenants-tenantsgrid',
    xtype: 'host.tenants',
    store: 'tenants.TenantsStore',
    name: 'Tenants',
    padding: 5,
    gridId: 1,////*******Important to apply grid's userView setting
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Tenants'),
        create: abp.auth.isGranted('Pages.Tenants.Create'),
        edit: abp.auth.isGranted('Pages.Tenants.Edit'),
        destroy: abp.auth.isGranted('Pages.Tenants.Delete'),
        changeFeature: abp.auth.isGranted('Pages.Tenants.ChangeFeatures'),
        impersonation: abp.auth.isGranted('Pages.Tenants.Impersonation')
    },
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
    actionColumnMenuItemsConfig: [{
        text: app.localize('LoginAsThisTenant'),
        iconCls: 'fa fa-user',
        clickActionName: 'loginAsThisTenantClick',
        itemId: 'loginAsThisTenantActionMenu'
    }
    ],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable:true,
    editingMode: 'row',
    createNewMode:'popup',
    columnLines: true,
    forceFit: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditTenant'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewTenant'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewTenant'),
        iconCls: 'fa fa-th'
    },
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('TenancyName'),
            dataIndex: 'tenancyName',
            stateId: 'tenancyName',
            sortable: true,
            width: '15%',
            groupable: true,
            renderer: Chaching.utilities.ChachingRenderers.renderTenant,
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('TTenancyCodeName')
            }
        },
        {
            xtype: 'gridcolumn',
            text: app.localize('TenantGroupName'),
            dataIndex: 'organizationName',
            stateId: 'organizationName',
            sortable: false,
            width: '15%'
        },
         {
             xtype: 'gridcolumn',
             text: app.localize('Edition'),
             dataIndex: 'editionDisplayName',
             sortable: false,
             width: '15%'
         },
          {
              xtype: 'gridcolumn',
              text: app.localize('ServerName'),
              dataIndex: 'serverName',
              stateId: 'serverName',
              sortable: false,
              width: '20%',
              groupable: true
          },
        {
            xtype: 'gridcolumn',
            text: app.localize('DatabaseName'),
            dataIndex: 'databaseName',
            stateId: 'databaseName',
            sortable: false,
            width: '10%',
            groupable: true
        },
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
                inputValue: true,
                uncheckedValue : false
            }
        },
         {
             xtype: 'gridcolumn',
             text: app.localize('CreationTime'),
             dataIndex: 'creationTime',
             sortable: true,
             groupable: true,
             width: '10%',
             renderer: Chaching.utilities.ChachingRenderers.renderDateOnly,
             filterField: {
                 xtype: 'dateSearchField',
                 dataIndex: 'creationTime',
                 width: '100%'
             }
         }
    ]
});
