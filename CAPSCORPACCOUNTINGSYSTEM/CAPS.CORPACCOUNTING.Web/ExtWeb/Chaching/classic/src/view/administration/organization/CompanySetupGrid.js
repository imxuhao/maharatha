
Ext.define('Chaching.view.administration.organization.CompanySetupGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.administration.organization.CompanySetupGridController'
    ],

    controller: 'administration-organization-companysetupgrid',

    xtype: 'widget.companysetup',
    name: "Administration.CompanySetUp",
    store: 'administration.organization.CompanyStore',   
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.CompanySetUp'),
        create: abp.auth.isGranted('Pages.Administration.CompanySetUp.Create'),
        edit: abp.auth.isGranted('Pages.Administration.CompanySetUp.Edit'),
        destroy: abp.auth.isGranted('Pages.Administration.CompanySetUp.Delete')
    },
    padding: 5,
    gridId: 27,
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("CompanySetup"),
          ui: 'headerTitle'
      }, '->', {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          action: 'create',
          text: abp.localization.localize("Add").toUpperCase(),
          tooltip: app.localize('CreateNewCompanySetup'),
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
        title: app.localize('EditCompanySetup'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewCompanySetup'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewCompanySetup'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,

    columns: [{
        xtype: 'gridcolumn',
        text: app.localize('CompanyId'),
        hidden: true,
        hideable : false,
        dataIndex: 'id',
        width: '15%'
    },

    //{
    //    xtype: 'gridcolumn',
    //    text: app.localize('ParentOrganizationId'),
    //    hidden: true,
    //    hideable: false,
    //    dataIndex: 'parentId',
    //    width: '15%'
    //},

    //{
    //    xtype: 'gridcolumn',
    //    text: app.localize('CompanyCode'),
    //    dataIndex: 'code',
    //    width: '15%'
    //},

    {
        xtype: 'gridcolumn',
        text: app.localize('CompanyName'),
        dataIndex: 'displayName',
        sortable: true,
        groupable: true,
        width: '15%',
        filterField: {
            xtype: 'textfield',
            width: '100%',
            emptyText: app.localize('CompanySearch')
        }, editor: {
            xtype: 'textfield'
        }
    }, {
        xtype: 'gridcolumn',
        text: app.localize('MembersCount'),
        dataIndex: 'memberCount',
        sortable: true,
        groupable: true,
        width: '15%'
    },
        {
            xtype: 'gridcolumn',
            text: app.localize('CompanyTransmitterContactName'),
            dataIndex: 'transmitterContactName',
            sortable: true,
            groupable: true,
            width: '15%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            }, editor: {
                xtype: 'textfield'
            }
        },
            {
                xtype: 'gridcolumn',
                text: app.localize('CompanyTransmitterEmailAddress'),
                dataIndex: 'transmitterEmailAddress',
                sortable: true,
                groupable: true,
                width: '15%',
                filterField: {
                    xtype: 'textfield',
                    width: '100%'
                }, editor: {
                    xtype: 'textfield'
                }
            },
                {
                    xtype: 'gridcolumn',
                    text: app.localize('CompanyTransmitterCode'),
                    dataIndex: 'transmitterCode',
                    sortable: true,
                    groupable: true,
                    width: '15%',
                    filterField: {
                        xtype: 'textfield',
                        width: '100%'
                    }, editor: {
                        xtype: 'textfield'
                    }
                },
                    {
                        xtype: 'gridcolumn',
                        text: app.localize('CompanyTransmitterControlCode'),
                        dataIndex: 'transmitterControlCode',
                        sortable: true,
                        groupable: true,
                        width: '15%',
                        filterField: {
                            xtype: 'textfield',
                            width: '100%'
                        }, editor: {
                            xtype: 'textfield'
                        }
                    },
    {
        xtype: 'gridcolumn',
        text: app.localize('DateModified'),
        dataIndex: 'lastModificationTime',
        sortable: true,
        groupable: true,
        width: '10%',
        renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer
    },
    //{
    //    xtype: 'gridcolumn',
    //    text: app.localize('UpdatedBy'),
    //    dataIndex: 'lastModifierUserId',
    //    sortable: true,
    //    groupable: true,
    //    width: '10%'
    //},
    {
        xtype: 'gridcolumn',
        text: app.localize('DateCreated'),
        dataIndex: 'creationTime',
        sortable: true,
        groupable: true,
        width: '10%',
        renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer
    }
    //,
    //{
    //    xtype: 'gridcolumn',
    //    text: app.localize('CreatedBy'),
    //    dataIndex: 'creatorUserId',
    //    sortable: true,
    //    groupable: true,
    //    width: '10%'
    //}
    ]
});
