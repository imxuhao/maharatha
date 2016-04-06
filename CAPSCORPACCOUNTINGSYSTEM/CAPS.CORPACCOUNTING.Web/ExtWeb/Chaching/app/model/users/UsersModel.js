Ext.define('Chaching.model.users.UsersModel', {
    extend: 'Chaching.model.base.BaseModel',

    fields: [
        { name: 'id', type: 'int' },
        { name: 'name', type: 'string' },
        { name: 'surname', type: 'string' },
        { name: 'userName', type: 'string' },
        { name: 'emailAddress', type: 'string' },
        { name: 'isEmailConfirmed', type: 'boolean' },
        { name: 'isActive', type: 'boolean' },
        { name: 'lastLoginTime', type: "date", format: 'Y-m-d' },
        { name: 'createTime', type: "date", format: 'Y-m-d' }
    ]
});

