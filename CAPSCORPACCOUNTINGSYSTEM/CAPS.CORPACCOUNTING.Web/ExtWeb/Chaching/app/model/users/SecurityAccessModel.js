/**
 * DataModel to represent entity schema for Users.
 */
Ext.define('Chaching.model.users.SecurityAccessModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'accountId', type: 'int' },
        { name: 'SubAccountRestrictionId', type: 'int', isPrimaryKey: true },
        { name: 'caption', type: 'string', headerText: 'Caption', hidden: false, width: '8%' },
        { name: 'accountNumber', type: 'string' },
        { name: 'subAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'isActive', type: 'boolean', headerText: 'AccountNumber', hidden: false, width: '8%' },
        { name: 'wasActive', type: 'boolean' }//used for loca operations only
    ]
});

