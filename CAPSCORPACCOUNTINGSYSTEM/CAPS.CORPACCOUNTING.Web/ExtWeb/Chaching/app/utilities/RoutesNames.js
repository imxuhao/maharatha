Ext.define('Chaching.utilities.RoutesNames', {
    alias:'IngnoreRoutes',
    singleton: true,
    routesNames: [
        'host.tenants.create',
        'host.tenants.edit',
        'host.editions.create',
        'host.editions.edit'
    ],
    menuItemRoutes: [
        'host.tenants',
        'host.editions',
        'users',
        'languages',
        'roles',
        'auditlogs',
        'financials.accounts',
        'projects.projectmaintenance'
    ]
});