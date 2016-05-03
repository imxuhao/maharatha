
Ext.define('Chaching.view.projects.projectmaintenance.ProjectLocations',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.projects.projectmaintenance.ProjectLocationsController'
    ],
    xtype: 'widget.projects.projectmaintenance.projectLocations',
    controller: 'projects-projectmaintenance-projectlocations',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
        create: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete'),
    },
    padding: 5,
    store: {
        model: 'Chaching.model.Jobcasting.JobLocationsModel',
        data:[]
    },
    headerButtonsConfig: [{
        xtype: 'displayfield',
        value: app.localize('JobLocations'),
        ui: 'headerTitle'
    },'->', {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          action: 'create',
          text: abp.localization.localize("Add").toUpperCase(),
          tooltip: app.localize('CreateJobLocation'),
          checkPermission: true,
          iconCls: 'fa fa-plus',
          routeName: 'project.projectmaintenance.projects.create',
          iconAlign: 'left'
      }],
    requireExport: false,
    requireMultiSearch: false,
    requireMultisort: false,
    isEditable: true,
    editingMode: 'cell',
    columnLines: true,
    multiColumnSort: false,
    createNewMode: 'inline',
    isSubMenuItemTab: false,
    showPagingToolbar:false,
    columns:[
    {
        xtype: 'gridcolumn',
        dataIndex: 'locationName',
        text: app.localize('Location').initCap(),
        sortable: false,
        groupable: false,
        width: '45%',
        editor: {//todo: replace with combo
            xtype: 'textfield'
        }
    }, {
        xtype: 'gridcolumn',
        dataIndex: 'LocationSiteDate',
        text: app.localize('LocationDate').initCap(),
        renderer: Chaching.utilities.ChachingRenderers.renderDateOnly,
        sortable: false,
        groupable: false,
        width: '45%',
        editor: {
            xtype: 'datefield',
        }
    }]
});
