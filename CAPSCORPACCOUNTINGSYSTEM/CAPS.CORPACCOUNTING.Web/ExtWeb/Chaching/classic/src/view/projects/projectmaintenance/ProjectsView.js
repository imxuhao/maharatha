
Ext.define('Chaching.view.projects.projectmaintenance.ProjectsView',{
    extend: 'Ext.panel.Panel',
    alias: ['widget.projects.projectmaintenance.projectsview.create', 'widget.projects.projectmaintenance.projectsview.edit'],
    requires: [
        'Chaching.view.projects.projectmaintenance.ProjectsViewController'       
    ],

    controller: 'projects-projectmaintenance-projectsview',
   
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
        create: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete')
    },
    html: 'Hello, World!!'
});
