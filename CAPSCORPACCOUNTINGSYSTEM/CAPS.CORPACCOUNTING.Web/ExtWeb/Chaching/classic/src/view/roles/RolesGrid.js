
Ext.define('Chaching.view.roles.RolesGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.roles.RolesGridController'
    ],

    controller: 'roles-rolesgrid',
    xtype: 'roles',
    store: 'roles.RolesStore',
    name: 'Administration.Roles',
    padding: 5,
    gridId:6,
    headerButtonsConfig: [
    {
        xtype: 'displayfield',
        value: abp.localization.localize("Roles"),
        ui: 'headerTitle'
    }, {
        xtype: 'displayfield',
        value: abp.localization.localize("RolesHeaderInfo"),
        ui: 'headerSubTitle'
    }, '->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        action: 'create',
        text: abp.localization.localize("CreateNewRole").toUpperCase(),
        tooltip: app.localize('CreateNewRole'),
        checkPermission: true,
        iconCls: 'fa fa-plus',
        iconAlign: 'left'
    }],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: false,
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditRole'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewRole'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewRole'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'popup',
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('RoleName'),
            dataIndex: 'displayName',
            stateId: 'displayName',
            sortable: true,
            width: '48%',
            groupable: true,
            //renderer: function (val, meta, record, rowIndex) {
            //    debugger;
            //    return val + (record.get('isDefault') == true ? '<div style="background-color: blue;width:10%; color: white;text-align: center;padding: 1px; margin: 0cm 2cm 0cm 0cm;">' + app.localize('Default') + '</div>' : '') + (record.get('isStatic') == true ? '<div style="background-color: blue;color: white;width:10%;text-align: center;padding: 1px;float:right;">' + app.localize('Static') + '</div>' : '');
            //},
            // simplest filter configuration
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: 'Enter Role Name to search'
            }

        },
         {
             xtype: 'gridcolumn',
             text: app.localize('CreationTime'),
             dataIndex: 'creationTime',
             sortable: true,
             groupable: true,
             width: '46%',
             renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer,
             filterField: {
                 xtype: 'dateSearchField',
                 dataIndex: 'creationTime',
                 width: '100%'
             }
         }
    ]
});

