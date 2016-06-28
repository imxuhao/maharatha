Ext.define('Chaching.model.administration.organization.CompanyModel', {
    extend: 'Chaching.model.base.BaseModel',
   // requires: ['Chaching.model.address.AddressModel'],
    config: {
        searchEntityName: ''
    },
    fields: [
            { name: 'companyId', type: 'int',  mapping: 'id', isPrimaryKey: true },
            { name: 'id', type: 'int' },
            { name: 'parentId', type: 'int', defaultVaule: null, convert: nullHandler },
            { name: 'code', type: 'string' },
            { name: 'displayName', type: 'string' },
            { name: 'memberCount', type: 'int', defaultVaule: null, convert: nullHandler },
            { name: 'lastModificationTime', type: 'date' },
            { name: 'lastModifierUserId', type: 'int', defaultVaule: null, convert: nullHandler },
            { name: 'creationTime', type: 'date' },
            { name: 'creatorUserId', type: 'int', defaultVaule: null, convert: nullHandler }
    ]
});















