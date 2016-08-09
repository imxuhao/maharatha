/**
 * DataModel to represent Project Security Model.
 */
Ext.define('Chaching.model.users.ProjectSecurityModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'projectAccessId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'jobId', type: 'int', isPrimaryKey: true },
        { name: 'caption', type: 'string', width: '8%' },
        { name: 'jobNumber', type: 'string' },
        { name: 'isActive', type: 'boolean', width: '8%' },
        { name: 'wasActive', type: 'boolean' }//used for local operations only
    ]
});

