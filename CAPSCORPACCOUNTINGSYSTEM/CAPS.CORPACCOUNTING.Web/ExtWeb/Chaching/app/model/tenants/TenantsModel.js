Ext.define('Chaching.model.tenants.TenantsModel', {
    extend: 'Chaching.model.base.BaseModel',
    
    fields: [
        { name: 'id', type: 'int' },
        { name: 'editionDisplayName', type: 'string' },
        { name: 'editionId', type: 'int' },
        { name: 'isActive', type: 'boolean' },
        { name: 'name', type: 'string' },
        { name: 'tenancyName', type: 'string' },
        { name: 'adminEmailAddress', type: 'string' }
    ]
});
