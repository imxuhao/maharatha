/**
 * DataModel to represent entity schema for Inter Linking of users accounts.
 */
Ext.define('Chaching.model.profile.linkedaccounts.LinkedAccountsModel', {
    extend: 'Chaching.model.base.BaseModel',
    
    fields: [
        { name: 'id', type: 'int' },
        { name: 'username', type: 'string' },
        { name: 'password', type: 'string' },
        { name: 'tenancyName', type: 'string' },
        { name: 'tenantUser', type: 'string', convert: function (val, record) { return record.get('tenancyName') + '/' + record.get('username') } },
        { name: 'usernameOrEmailAddress', type: 'string' }
    ]
});
