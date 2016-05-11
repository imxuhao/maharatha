Ext.define('Chaching.model.projects.projectmaintenance.PoRangeAllocationModel', {
    extend: 'Chaching.model.base.BaseModel',
    
    fields: [
        { name: 'poRangeId', type: 'int', isPrimaryKey: true },
        { name: 'jobId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'poRangeStartNumber', type: 'string' },
        { name: 'poRangeEndNumber', type: 'string' }

    ]
});
