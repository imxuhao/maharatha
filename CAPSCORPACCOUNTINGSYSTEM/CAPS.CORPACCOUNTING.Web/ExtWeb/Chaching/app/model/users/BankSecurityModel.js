/**
 * DataModel to represent Bank Security Model.
 */
Ext.define('Chaching.model.users.BankSecurityModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'bankAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'bankName', type: 'int'},
        { name: 'accountName', type: 'string', width: '8%' },
        { name: 'bankAccountNumber', type: 'string' },
        { name: 'isActive', type: 'boolean', width: '8%' },
        { name: 'wasActive', type: 'boolean' }//used for local operations only
    ]
});

