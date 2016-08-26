/**
 * The class is created to provide main UI to access projects/jobs.
 * Author: Krishna Garad
 * Date: 28/04/2016
 */
/**
 * @class Chaching.view.projects.projectmaintenance.ProjectsGrid
 * UI design for project/job.
 * @alias widget.projects.projectmaintenance.projects
 */
Ext.define('Chaching.view.projects.projectmaintenance.ProjectsGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.projects.projectmaintenance.ProjectsGridController'
    ],
    xtype:'projects.projectmaintenance.projects',
    controller: 'projects-projectmaintenance-projectsgrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
        create: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete')
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
    importConfig: {
        entity: app.localize('Projects'),
        isRequireImport: true,
        importStoreClass: 'imports.ProjectsImportStore',
        targetGrid: null,
        targetUrl: abp.appPath + 'api/services/app/jobCommercial/BulkJobInsert'
    },
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditProject'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewProject'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewProject'),
        iconCls: 'fa fa-th'
    },
    createNewMode: 'tab',
    isSubMenuItemTab: true,
    listeners: {
        cellclick:'onProjectsCellClick'
    },
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
             text: app.localize('JobName'),
             dataIndex: 'caption',
             sortable: true,
             groupable: true,
             width: '15%',
             renderer: Chaching.utilities.ChachingRenderers.rendererHyperLink,
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipJobName')
             }, editor: {
                 xtype: 'textfield',
                 allowBlank:false
             }
         },{
             xtype: 'gridcolumn',
             text: app.localize('DetailReport'),
             dataIndex: 'detailTransactions',//TODO: render hyperlink based on transactions count
             sortable: false,
             groupable: false,
             renderer: Chaching.utilities.ChachingRenderers.rendererHyperLink,
             width: '13%'
         }, {
             xtype: 'gridcolumn',
             text: app.localize('ProductName'),
             dataIndex: 'productName',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipProductName')
             },editor: {
                 xtype:'textfield'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('Director'),
             dataIndex: 'directorName',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 entityName:'',
                 emptyText: app.localize('ToolTipDirector')
             }, editor: {
                 xtype: 'combobox',
                 store: new Chaching.store.employee.EmployeeStore({
                     filters: [
                     {
                         entity: 'Employee',
                         searchTerm: true,
                         comparator: 1,
                         dataType: 3,
                         property: 'isDirector',
                         value: true
                     }]
                 }),
                 valueField: 'directorEmployeeId',
                 displayField: 'directorName',
                 queryMode:'local'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('Agency'),
             dataIndex: 'agency',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 entityName: '',
                 emptyText: app.localize('ToolTipAgency')
             }, editor: {
                 xtype: 'combobox',
                 store: 'customers.CustomersStore',
                 valueField: 'agencyId',
                 displayField: 'agency',
                 queryMode: 'local'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('POLog'),
             dataIndex: 'poLogCount',//TODO: render hyperlink based on po log count
             sortable: false,
             groupable: false,
             renderer: Chaching.utilities.ChachingRenderers.rendererHyperLink,
             width: '15%'
         }, {
             xtype: 'gridcolumn',
             text: app.localize('ProjectType'),
             dataIndex: 'typeofProjectName',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipProjectType')
             },
             editor: {
                 xtype: 'combobox',
                 allowBlank: false,
                 queryMode: 'local',
                 store:'utilities.ProjectTypeStore',
                 valueField: 'typeofProjectId',
                 displayField: 'typeofProjectName'
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('ShootDate'),
             dataIndex: 'shootingDate',
             sortable: true,
             groupable: true,
             width: '20%',
             renderer: Chaching.utilities.ChachingRenderers.renderDateOnly,
             filterField: {
                 xtype: 'dateSearchField',
                 dataIndex: 'shootingDate',
                 width: '100%',
                 emptyText: app.localize('ToolTipShootDate')
             }, editor: {
                 xtype: 'datefield'
             }
         }, {///TODO : field to be added
             xtype: 'gridcolumn',
             text: app.localize('ShootLocations'),
             dataIndex: 'shootLocations',
             sortable: true,
             groupable: true,
             width: '14%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipShootLocations')
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('WrapUpInsurance'),
             dataIndex: 'isWrapUpInsurance',
             sortable: false,
             groupable: false,
             renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
             width: '15%',
             editor: {
                 xtype: 'checkboxfield',
                 inputValue: 'true',
                 uncheckedValue: 'false'
             }
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
             },
             editor: {
                 xtype: 'combobox',
                 queryMode: 'local',
                 store: 'utilities.ProjectStatusStore',
                 valueField: 'typeOfJobStatusId',
                 displayField: 'jobStatusName'
             }
         },
         ////TODO : dummy columns for default manage view
         {
             xtype: 'gridcolumn',
             text: app.localize('TotalCost'),
             dataIndex: 'totalCost',
             sortable: true,
             groupable: true,
             hidden:true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipTotalCost')
             }
         },{
             xtype: 'gridcolumn',
             text: app.localize('BidContract'),
             dataIndex: 'bidContractTotal',
             sortable: true,
             groupable: true,
             hidden: true,
             width: '12%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipBidContract')
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('ProducersActual'),
             dataIndex: 'producersActual',
             sortable: true,
             groupable: true,
             hidden: true,
             width: '13%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipProducersActual')
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('BilledAmount'),
             dataIndex: 'billedAmount',
             sortable: true,
             groupable: true,
             hidden: true,
             width: '13%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipBilledAmount')
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('RecievedAmount'),
             dataIndex: 'recievedAmount',
             sortable: true,
             groupable: true,
             hidden: true,
             width: '13%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipRecievedAmount')
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('Variance'),
             dataIndex: 'variance',
             sortable: true,
             groupable: true,
             hidden: true,
             width: '11%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipVariance')
             }
         }, {
             xtype: 'gridcolumn',
             text: app.localize('AgencyProducer'),
             dataIndex: 'agencyProducer',
             sortable: true,
             groupable: true,
             hidden: true,
             width: '13%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('ToolTipAgencyProducer')
             }
         }

    ]
});
