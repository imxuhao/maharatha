/**
 * The class is created to provide inline add/update/delete of Purchase Order range for project/job
 * Author: Krishna Garad
 * Date: 11/05/2016
 */
/**
 * @class Chaching.view.projects.projectmaintenance.PoRangeAllocationGrid
 * UI design for PO Range for project/job
 * @alias widget.projects.projectmaintenance.porangeallocation
 */
Ext.define('Chaching.view.projects.projectmaintenance.PoRangeAllocationGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',
    xtype: 'projects.projectmaintenance.porangeallocation',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
        create: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete')
    },
    padding: 5,
    store: 'projects.projectmaintenance.PoRangeAllocationStore',
    headerButtonsConfig: [{
        xtype: 'displayfield',
        value: app.localize('POAllocation'),
        ui: 'headerTitle'
    }, '->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        action: 'create',
        text: abp.localization.localize("Add").toUpperCase(),
        tooltip: app.localize('AddNewRange'),
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
    showPagingToolbar: false,
    columns: [
    {
        xtype: 'gridcolumn',
        dataIndex: 'poRangeStartNumber',
        text: app.localize('StartingPo'),
        sortable: false,
        groupable: false,
        width: '42%',
        editor: {
            xtype: 'numberfield',
            minValue:0
        }
    }, {
        xtype: 'gridcolumn',
        dataIndex: 'poRangeEndNumber',
        text: app.localize('EndingPo'),
        sortable: false,
        groupable: false,
        width: '42%',
        editor: {
            xtype: 'numberfield',
            minValue: 0
        }
    }]
});
