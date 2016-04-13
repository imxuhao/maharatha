Ext.define('Chaching.model.profile.loginAttempts.LoginAttemptModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'LoginAttempts'
    },
    fields: [
        { name: 'browserInfo', type: 'string' },
        { name: 'clientIpAddress', type: 'string' },
        { name: 'clientName', type: 'string' },
        { name: 'result', type: 'string' },
        { name: 'tenancyName', type: 'string' },
        { name: 'userNameOrEmail', type: 'string' },
        {
            name: 'attemptedOn',
            type: 'string',
            convert: function(value, record) {
                return Ext.Date.format(record.get('creationTime'), 'm/d/Y h:m:s');
            }
        },
        {
            name: 'badgeClass',
            type: 'string',
            convert: function(value, record) {
                var result = record.get('result');
                if (result === "Success")
                    return 'lable-success';
                else return 'label-danger';
            }
        }
    ]
});
