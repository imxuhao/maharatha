/**
 * DataModel to represent entity schema for Users.
 */
Ext.define('Chaching.model.users.UsersModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'User'
    },
    fields: [
        { name: 'id', type: 'int', isPrimaryKey: true, defaultValue: null, convert: nullHandler },
        { name: 'name', type: 'string' },
        { name: 'surname', type: 'string' },
        {
            name: 'fullName', type: 'string', convert: function (val, record) {
                return record.get('name') + " " + record.get('surname')
            }
        },
        { name: 'userName', type: 'string' },
        { name: 'emailAddress', type: 'string' },
        { name: 'defaultRoleId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'defaultRole', type: 'string' },
        { name: 'isEmailConfirmed', type: 'boolean' },
        { name: 'isActive', type: 'boolean' },
        { name: 'isLocked', type: 'boolean' },
        { name: 'lastLoginTime', type: "date", format: 'Y-m-d' },
        { name: 'addedTime', type: "date", format: 'Y-m-d' },
        { name: 'createTime', type: "date", format: 'Y-m-d' }
    ]
});

