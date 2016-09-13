/**
 * Entity schema model for Audit Log.
 */
Ext.define('Chaching.model.auditlogs.AuditLogsModel', {
    extend: 'Chaching.model.base.BaseModel',
    config : {
        searchEntityName: 'AuditLog'
    },
    fields: [
            { name: 'userId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'userName', type: 'string' },
            { name: 'impersonatorTenantId', type: 'int', defaultValue : null, convert : nullHandler },
            { name: 'impersonatorUserId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'serviceName', type: 'string' },
            { name: 'methodName', type: 'string' },
            { name: 'parameters', type: 'string' },
            { name: 'executionTime', type: "date", format: 'Y/m/d H:i:s' },
            { name: 'executionDuration', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'clientIpAddress', type: 'string' },
            { name: 'clientName', type: 'string' },
            { name: 'browserInfo', type: 'string' },
            { name: 'exception', type: 'string' },
            { name: 'customData', type: 'string' }
    ]
});

