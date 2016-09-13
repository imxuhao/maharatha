/**
 * DataModel to represent entity schema for Users Login Attempts.
 */
Ext.define('Chaching.model.profile.loginAttempts.LoginAttemptModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'LoginAttempts'
    },
    fields: [
        { name: 'browserInfo', type: 'string' },
        { name: 'clientIpAddress', type: 'string' },
        { name: 'clientName', type: 'string' },
        { name: 'result', type: 'string',convert:function(value) {
            return value === 'Success' ? app.localize('Success') :
                   app.localize('Failed');
        } },
        { name: 'tenancyName', type: 'string' },
        { name: 'userNameOrEmail', type: 'string' },
        {
            name: 'attemptedOn',
            type: 'string',
            convert: function(value, record) {
                return Chaching.utilities.ChachingRenderers.renderDateTimeWithFromNow(record.get('creationTime'));
            }
        },
        {
            name: 'badgeClass',
            type: 'string',
            convert: function(value, record) {
                var result = record.get('result');
                if (result === "Success")
                    return 'label-success';
                else return 'label-danger';
            }
        }
    ]
});
