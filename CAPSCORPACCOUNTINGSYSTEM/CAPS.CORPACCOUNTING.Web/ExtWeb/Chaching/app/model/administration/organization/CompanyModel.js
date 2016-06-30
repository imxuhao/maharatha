Ext.define('Chaching.model.administration.organization.CompanyModel', {
    extend: 'Chaching.model.base.BaseModel',
    requires: ['Chaching.model.address.AddressModel', 'Chaching.model.administration.organization.CompanySettingsModel'],
    config: {
        searchEntityName: 'Organization'
    },
    fields: [
           // { name: 'companyId', type: 'int',  mapping: 'id', isPrimaryKey: true },
            { name: 'id', type: 'int', isPrimaryKey: true },
            { name: 'parentId', type: 'int', defaultVaule: null, convert: nullHandler },
            { name: 'code', type: 'string' },
            { name: 'displayName', type: 'string' },
            { name: 'memberCount', type: 'int', defaultVaule: null, convert: nullHandler },
            { name: 'lastModificationTime', type: 'date' },
            { name: 'lastModifierUserId', type: 'int', defaultVaule: null, convert: nullHandler },
            { name: 'creationTime', type: 'date' },
            { name: 'creatorUserId', type: 'int', defaultVaule: null, convert: nullHandler },
            { name: 'transmitterContactName', type: 'string' },
            { name: 'transmitterEmailAddress', type: 'string' },
            { name: 'transmitterCode', type: 'string' },
            { name: 'transmitterControlCode', type: 'string' },
            { name: 'federalTaxId', type: 'string' },
            { name: 'transmitterContactName', type: 'string' },
            {
                name: 'address',
                reference: {
                    parent: 'address.AddressModel'
                }
            },
            {
                name: 'organizationSettings',
                reference: {
                    parent: 'organizationSettings.CompanySettingsModel'
                }
            }
            
    ]
});















