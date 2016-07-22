/**
 * Organization model used to group tenants/companies.
 */
Ext.define('Chaching.model.administration.organization.OrganizationModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'Organization'
    },
    fields: [
            { name: 'id', type: 'int', isPrimaryKey: true,defaultVaule: null, convert: nullHandler },
            { name: 'displayName', type: 'string' },
            { name: 'lastModificationTime', type: 'date' },
            { name: 'creationTime', type: 'date' }
    ]
});















