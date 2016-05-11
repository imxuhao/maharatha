/**
 * The class is created to provide inline add/update/delete of project/job locations
 * Author: Krishna Garad
 * Date: 03/05/2016
 */
/**
 * @class Chaching.view.projects.projectmaintenance.ProjectLocations
 * UI design for project/job locations
 * @alias widget.projects.projectmaintenance.projectLocations
 */
Ext.define('Chaching.view.projects.projectmaintenance.ProjectLocations',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype: 'widget.projects.projectmaintenance.projectLocations',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
        create: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete')
    },
    padding: 5,
    store: 'projects.projectmaintenance.ProjectLocationsStore',
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
        editor: {
            xtype: 'combobox',
            name: 'locationName',
            store: 'utilities.LocationListStore',
            valueField: 'locationId',
            displayField: 'locationName',
            queryMode:'local'
        }
    }, {
        xtype: 'gridcolumn',
        dataIndex: 'locationSiteDate',
        text: app.localize('LocationDate').initCap(),
        renderer: Chaching.utilities.ChachingRenderers.renderDateOnly,
        sortable: false,
        groupable: false,
        width: '45%',
        editor: {
            xtype: 'datefield'
        }
    }]
});
