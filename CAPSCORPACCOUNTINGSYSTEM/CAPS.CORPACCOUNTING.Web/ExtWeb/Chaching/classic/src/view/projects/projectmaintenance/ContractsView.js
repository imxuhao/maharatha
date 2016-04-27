
Ext.define('Chaching.view.projects.projectmaintenance.ContractsView',{
    extend: 'Ext.panel.Panel',
    alias: ['widget.projects.projectmaintenance.contracts'],
    requires: [
        'Chaching.view.projects.projectmaintenance.ContractsViewController'
    ],

    controller: 'projects-projectmaintenance-contractsview',    
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Contracts'),
        create: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Contracts.Create'),
        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Contracts.Edit'),
        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Contracts.Delete'),
    },
    html: 'Hello, World!!'
});
