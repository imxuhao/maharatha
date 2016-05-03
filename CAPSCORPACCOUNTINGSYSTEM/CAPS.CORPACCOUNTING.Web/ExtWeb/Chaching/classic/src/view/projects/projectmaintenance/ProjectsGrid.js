
Ext.define('Chaching.view.projects.projectmaintenance.ProjectsGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.projects.projectmaintenance.ProjectsGridController'
    ],
    xtype:'widget.projects.projectmaintenance.projects',
    controller: 'projects-projectmaintenance-projectsgrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
        create: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete'),
    },
    padding: 5,
    gridId:16,
    store:'projects.projectmaintenance.ProjectsStore',
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("Projects"),
          ui: 'headerTitle'
      }, '->', {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          action: 'create',
          text: abp.localization.localize("Add").toUpperCase(),
          tooltip: app.localize('CreateNewProject'),
          checkPermission: true,
          iconCls: 'fa fa-plus',
          routeName: 'project.projectmaintenance.projects.create',
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
        title: app.localize('EditProject').initCap(),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewProject').initCap(),
        iconCls: 'fa fa-plus'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,   
    columns: [
         {
             xtype: 'gridcolumn',
             text: app.localize('JobNumber'),
             dataIndex: 'jobNumber',
             sortable: true,
             groupable: true,
             width: '10%',
             renderer: Chaching.utilities.ChachingRenderers.rendererHyperLink,
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipJobNumber')
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('JobName').initCap(),
             dataIndex: 'caption',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipJobName')
             }, editor: {
                 xtype: 'textfield',
             }
         },{
             xtype: 'gridcolumn',
             text: app.localize('DetailReport').initCap(),
             dataIndex: 'detailTransactions',//TODO: render hyperlink based on transactions count
             sortable: false,
             groupable: false,
             width: '13%'
         }, {
             xtype: 'gridcolumn',
             text: app.localize('ProductName').initCap(),
             dataIndex: 'productName',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipProductName')
             }, editor: {
                 xtype: 'textfield'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('Director'),
             dataIndex: 'directorEmployeeId',//TODO: change to name
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipDirector')
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('Agency'),
             dataIndex: 'agencyName',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipAgency')
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('POLog').initCap(),
             dataIndex: 'poLogCount',//TODO: render hyperlink based on po log count
             sortable: false,
             groupable: false,
             width: '15%'
         }, {
             xtype: 'gridcolumn',
             text: app.localize('ProjectType').initCap(),
             dataIndex: 'typeofProjectName',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipProjectType')
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('ShootDate').initCap(),
             dataIndex: 'shootingDate',
             sortable: true,
             groupable: true,
             width: '20%',
             renderer: Chaching.utilities.ChachingRenderers.renderDateTime,
             filterField: {
                 xtype: 'dateSearchField',
                 dataIndex: 'shootingDate',
                 width: '100%',
                 emptyText: app.localize('ToolTipShootDate')
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('WrapUpInsurance').initCap(),
             dataIndex: 'isWrapUpInsurance',
             sortable: false,
             groupable: false,
             renderer: Chaching.utilities.ChachingRenderers.statusRenderer,
             width: '15%'
         }, {
             xtype: 'gridcolumn',
             text: app.localize('Status'),
             dataIndex: 'jobStatusName',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipStatus')
             }
         },
    ]
});
