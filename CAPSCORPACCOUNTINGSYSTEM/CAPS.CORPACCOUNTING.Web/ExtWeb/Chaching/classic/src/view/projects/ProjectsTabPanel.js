/**
 * The class is created to as host for projects submenu items
 * Author: Krishna Garad
 * Date: 28/04/2016
 */
/**
 * @class Chaching.view.projects.ProjectsTabPanel
 * Host for projects subMenuItems
 * @alias projects.projectmaintenance
 */
Ext.define('Chaching.view.projects.ProjectsTabPanel',{
    extend: 'Chaching.view.common.tab.ChachingTabPanel',

    requires: [
        'Chaching.view.projects.ProjectsTabPanelController'
    ],
    xtype: 'projects.projectmaintenance',
    controller: 'projects-projectstabpanel',
    name: 'Projects.ProjectMaintenance',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance')
    },
    staticTabItems: null
   
});
