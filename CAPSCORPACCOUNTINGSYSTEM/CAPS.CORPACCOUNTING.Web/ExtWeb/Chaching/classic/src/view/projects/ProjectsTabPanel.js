
Ext.define('Chaching.view.projects.ProjectsTabPanel',{
    extend: 'Chaching.view.common.tab.ChachingTabPanel',

    requires: [
        'Chaching.view.projects.ProjectsTabPanelController',
        'Chaching.view.projects.ProjectsTabPanelModel'
    ],
    xtype: 'projects.projectmaintenance',
    controller: 'projects-projectstabpanel',
    viewModel: {
        type: 'projects-projectstabpanel'
    },
    name: 'Projects.ProjectMaintenance',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance')
    },
    staticTabItems: null
   
});
