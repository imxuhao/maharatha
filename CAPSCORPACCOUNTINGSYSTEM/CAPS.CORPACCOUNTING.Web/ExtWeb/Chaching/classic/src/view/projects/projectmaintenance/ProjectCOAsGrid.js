
Ext.define('Chaching.view.projects.projectmaintenance.ProjectCOAsGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
   
    requires: [
        'Chaching.view.projects.projectmaintenance.ProjectCOAsGridController'       
    ],
    controller: 'projects-projectmaintenance-projectcoasgrid',
    
    xtype: 'widget.projects.projectmaintenance.projectcoas',
    store: 'projects.projectmaintenance.ProjectCoaStore',
    name: 'Financials.Accounts.ChartOfAccounts',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs'),
        create: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Create'),
        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Edit'),
        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Delete'),
    },
    padding: 5,
    gridId: 13,
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("ProjectChartOfAccount"),
          ui: 'headerTitle'
      }, '->', {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          action: 'create',
          text: abp.localization.localize("CreatingNewProjectCOA").toUpperCase(),
          tooltip: app.localize('CreatingNewProjectCOA'),
          checkPermission: true,
          iconCls: 'fa fa-plus',
          routeName: 'coa.create',
          iconAlign: 'left'
      }],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: false,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditProjectCOA'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreatingNewProjectCOA'),
        iconCls: 'fa fa-plus'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    listeners: {
        cellclick: 'onChartOfAccountClicked'
    },
    columns: [
         {
             xtype: 'gridcolumn',
             text: app.localize('Description'),
             dataIndex: 'caption',
             sortable: true,
             groupable: true,
             width: '54%',
             renderer: Chaching.utilities.ChachingRenderers.rendererHyperLink,
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('DescriptionSearch'),
             }, editor: {
                 xtype: 'textfield',
             }
         },
          {
              xtype: 'gridcolumn',
              text: app.localize('Defaults'),
              sortable: true,
              groupable: true,
              width: '39%',
              hidden: false            
          },
        ]
    
});
