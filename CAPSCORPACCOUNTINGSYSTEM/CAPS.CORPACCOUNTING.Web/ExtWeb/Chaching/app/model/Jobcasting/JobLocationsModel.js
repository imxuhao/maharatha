Ext.define('Chaching.model.Jobcasting.JobLocationsModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
            { name: 'JobLocationId', type: 'int' ,isPrimaryKey: true},
            { name: 'jobId', type: 'int' },
            { name: 'LocationId', type: 'int'},
            { name: 'JobDetailId', type: 'int'},
            { name: 'LocationSiteDate', type: 'date'},
            { name: 'OrganizationUnitId', type: 'int'}           
    ]
});
