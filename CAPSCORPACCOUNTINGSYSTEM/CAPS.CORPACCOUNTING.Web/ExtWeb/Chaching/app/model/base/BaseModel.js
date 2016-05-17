Ext.define('Chaching.model.base.BaseModel', {
    extend: 'Ext.data.Model',
    schema: {
        namespace: 'Chaching.model'
    },
    config: {
        searchEntityName: ''
    },
    fields: [
        //common fields in all entities
        { name: 'tenantId', type: 'int' },
        { name: 'organizationUnitId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isDeleted', type: 'boolean' },
        { name: 'deletionTime', type: 'date' },
        { name: 'deleterUserId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'lastModificationTime', type: 'date', dateFormat: 'c' },
        { name: 'lastModifierUserId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'creationTime', type: 'date', dateFormat: 'c' },
        { name: 'creatorUserId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'createdUser', type: 'string' },

        //custom fields required for all entities
        { name: 'allowEdit', type: 'boolean', defaultValue: true },
        { name: 'allowDelete', type: 'boolean', defaultValue: true },
        { name: 'isRestricted', type: 'boolean', defaultValue: true },

        //local pass edit/delete action
        { name: 'passEdit', type: 'boolean', defaultValue: false },
        { name: 'passDelete', type: 'boolean', defaultValue: false }

    ]
});
