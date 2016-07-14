/**
 * DataModel to represent entity schema for Job/Projects.
 */
Ext.define('Chaching.model.projects.projectmaintenance.ProjectModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'Job'
    },
    fields: [
        { name: 'jobId', type: 'int', isPrimaryKey: true },
        { name: 'jobNumber', type: 'string' },
        { name: 'caption', type: 'string' },
        { name: 'rollupCenterId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isCorporateDefault', type: 'boolean' },
        { name: 'chartOfAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'rollupAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'typeOfCurrencyId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'rollupJobId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'typeOfJobStatusId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'typeOfBidSoftwareId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isApproved', type: 'boolean' },
        { name: 'isActive', type: 'boolean' },
        { name: 'isictDivision', type: 'boolean' },
        { name: 'typeofProjectId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'taxRecoveryId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'taxCreditId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'organizationUnitId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'rollupcenterid', type: 'int', defaultValue: null, convert: nullHandler },
        ///TODO: Remove convert function once service sent po log count and transaction count
        { name: 'detailTransactions', type: 'int',convert:function(val) {
            return Ext.Number.randomInt(0, 100);
        } },
        { name: 'poLogCount', type: 'int', convert:function(val) {
            return Ext.Number.randomInt(0, 100);
        } },
        { name: 'typeofProjectName', type: 'string' },
        { name: 'jobStatusName', type: 'string' }
    ]
});
