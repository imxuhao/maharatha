/**
 * DataModel to represent entity schema for Job/Project Accounts.
 */
Ext.define('Chaching.model.projects.projectmaintenance.JobAccountsModel', {
    extend: 'Chaching.model.base.BaseModel',
    
    fields: [
        { name: 'jobAccountId', type: 'int', isPrimaryKey: true },
        { name: 'description', type: 'string' },
        { name: 'jobId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'accountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'rollupJobId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'rollupAccountId', type: 'int', defaultValue: null, convert: nullHandler }
    ]
});
