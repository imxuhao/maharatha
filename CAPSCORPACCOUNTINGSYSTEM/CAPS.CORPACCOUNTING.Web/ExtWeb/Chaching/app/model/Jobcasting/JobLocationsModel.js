/**
 * DataModel to represent entity schema for Job Locations.
 */
Ext.define('Chaching.model.Jobcasting.JobLocationsModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
            { name: 'jobLocationId', type: 'int', isPrimaryKey: true },
            { name: 'jobId', type: 'int',defaultValue:null,convert:nullHandler },
            { name: 'locationId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'locationSiteDate', type: 'date' },
            { name: 'locationName', type: 'string' }
    ]
});
