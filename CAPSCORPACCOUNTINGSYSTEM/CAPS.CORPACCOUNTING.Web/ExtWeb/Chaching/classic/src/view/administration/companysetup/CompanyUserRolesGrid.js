
Ext.define('Chaching.view.administration.companysetup.CompanyUserRolesGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.roles.RolesGridController'
    ],
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.Roles'),
        create: abp.auth.isGranted('Pages.Administration.Roles.Create'),
        edit: abp.auth.isGranted('Pages.Administration.Roles.Edit'),
        destroy: abp.auth.isGranted('Pages.Administration.Roles.Delete')
    },
    controller: 'roles-rolesgrid',
    xtype: 'administration.organization.companyuserrolesgrid',
    store: 'administration.organization.CompanyUserRoleListStore',
   // name: 'Administration.Roles',
    padding: 5,
   // gridId: 6,
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
    requireExport: false,
    requireMultiSearch: false,
    requireMultisort: false,
    isEditable: true,
    columnLines: true,
    multiColumnSort: false,
    createNewMode: 'inline',
    isSubMenuItemTab: false,
    showPagingToolbar: false,
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
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }, editor: {
                xtype: 'textfield',
                allowBlank : false
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

