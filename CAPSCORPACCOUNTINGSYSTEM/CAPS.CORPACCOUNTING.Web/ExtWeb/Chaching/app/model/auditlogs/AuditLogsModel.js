Ext.define('Chaching.model.auditlogs.AuditLogsModel', {
    extend: 'Chaching.model.base.BaseModel',

    fields: [
            { name: 'userId', type: 'int' },
            { name: 'userName', type: 'string' },
            { name: 'impersonatorTenantId', type: 'int' },
            { name: 'impersonatorUserId', type: 'int' },
            { name: 'serviceName', type: 'string' },
            { name: 'methodName', type: 'string' },
            { name: 'parameters', type: 'string' },
            { name: 'executionTime', type: "date", format: 'Y/m/d H:i:s' },
            { name: 'executionDuration', type: 'string' },
            { name: 'clientIpAddress', type: 'string' },
            { name: 'clientName', type: 'string' },
            { name: 'browserInfo', type: 'string' },
            { name: 'exception', type: 'string' },
            { name: 'customData', type: 'string' }
    ]
});

