
Ext.define('Chaching.view.administration.organization.OrganizationGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.administration.organization.OrganizationGridController'
    ],

    controller: 'administration-organization-organizationgrid',

    xtype: 'organizationUnits',
    store: 'administration.organization.OrganizationsStore',
    name: 'Administration.OrganizationUnits',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.OrganizationUnits'),
        create: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree'),
        edit: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree'),
        destroy: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree')
    },
    padding: 5,
    gridId: 25,
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("OrganizationUnits"),
          ui: 'headerTitle'
      }, '->', {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          action: 'create',
          text: abp.localization.localize("Add").toUpperCase(),
          tooltip: app.localize('CreateNewOrganization'),
          checkPermission: true,
          iconCls: 'fa fa-plus',
          //routeName: 'coa.create',
          iconAlign: 'left'
      }],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditTenantGroup'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewTenantGroup'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewTenantGroup'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'popup',
    isSubMenuItemTab: false,
    //forceFit : true,
    columns: [{
        xtype: 'gridcolumn',
        text: app.localize('OrganizationId'),
        hidden: true,
        hideable: false,
        dataIndex: 'id',
        //width: '15%'
        flex: 1
    },
    {
        xtype: 'gridcolumn',
        text: app.localize('OrganizationName'),
        hideable: false,
        dataIndex: 'displayName',
        //width: '15%',
        flex: 1,
        filterField: {
            xtype: 'textfield',
            width: '100%'
        }, editor: {
            xtype: 'textfield',
            listeners: {
                change: 'OnOrganizationNameChange'
            }
        }
    },
      {
          xtype: 'gridcolumn',
          text: app.localize('ServerName'),
          dataIndex: 'serverName',
          stateId: 'serverName',
          sortable: false,
          //width: '35%',
          flex: 1,
          groupable: true
      },
        {
            xtype: 'gridcolumn',
            text: app.localize('DatabaseName'),
            dataIndex: 'databaseName',
            stateId: 'databaseName',
            sortable: false,
            //width: '15%',
            flex: 1,
            groupable: true
        },
    {
        xtype: 'gridcolumn',
        text: app.localize('DateModified'),
        dataIndex: 'lastModificationTime',
        sortable: true,
        groupable: true,
        //width: '15%',
        flex: 1,
        renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer
    },
    {
        xtype: 'gridcolumn',
        text: app.localize('DateCreated'),
        dataIndex: 'creationTime',
        sortable: true,
        groupable: true,
        //width: '15%',
        flex: 1,
        renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer
    }
    ]
});
