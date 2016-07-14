/**
 * DataStore to perform Read operation on Audit Log.
 */
Ext.define('Chaching.store.auditlogs.AuditLogsStore', {
    extend: 'Chaching.store.base.BaseStore',
    model: 'Chaching.model.auditlogs.AuditLogsModel',
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/auditLog/GetAuditLogs'
        }
    },
    idPropertyField: 'id'//important to set for add/update of records
});