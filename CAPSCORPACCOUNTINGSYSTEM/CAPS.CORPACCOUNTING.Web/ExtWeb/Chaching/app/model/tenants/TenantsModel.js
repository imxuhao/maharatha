Ext.define('Chaching.model.tenants.TenantsModel', {
    extend: 'Chaching.model.base.BaseModel',
    
    fields: [
        { name: 'id', type: 'int' },
        { name: 'editionDisplayName', type: 'string' },
        { name: 'isActive', type: 'boolean' },
        { name: 'name', type: 'string' },
        { name: 'tenancyName', type: 'string' }
    ]
});
