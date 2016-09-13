/**
 * DataModel to represent entity schema for Users.
 */
Ext.define('Chaching.model.users.AccountSecurityModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'accountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'caption', type: 'string'},
        { name: 'accountNumber', type: 'string' },
        { name: 'isActive', type: 'boolean', width: '8%' },
        { name: 'wasActive', type: 'boolean' }//used for loca operations only
    ]
});

