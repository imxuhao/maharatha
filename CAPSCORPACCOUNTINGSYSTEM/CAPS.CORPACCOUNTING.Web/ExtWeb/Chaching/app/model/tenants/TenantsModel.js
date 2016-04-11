Ext.define('Chaching.model.tenants.TenantsModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'Tenant'
    },
    fields: [
        { name: 'id', type: 'int' },
        { name: 'editionDisplayName', type: 'string' },
        { name: 'editionId', type: 'int',defaultValue:null },
        { name: 'isActive', type: 'boolean' },
        { name: 'name', type: 'string' },
        { name: 'tenancyName', type: 'string' },
        { name: 'adminEmailAddress', type: 'string' },
        { name: 'shouldChangePasswordOnNextLogin', type: 'boolean',defaultValue:false },
        { name: 'sendActivationEmail', type: 'boolean', defaultValue: false }
    ]
});
