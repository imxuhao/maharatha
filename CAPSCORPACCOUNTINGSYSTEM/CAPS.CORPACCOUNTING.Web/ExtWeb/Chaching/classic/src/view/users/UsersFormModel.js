Ext.define('Chaching.view.users.UsersFormModel', {
    extend: 'Chaching.view.common.form.ChachingFormPanelModel',
    alias: 'viewmodel.users-usersform',
    data: {
        name: 'Chaching'
    },
    stores: {
        rolesForCheckBox: {
          
            fields: [{ name: 'displayName' }, { name: 'name' }, {
                name: 'displayName', convert: function (value,record) {
                    return record.get('displayName');
                }
            }, {
                name: 'name', convert: function (value,  record) {
                    return record.get('name');
                }
            }],
            xtype: 'ajax',
            autoLoad: true,
            proxy: {
                actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
                type: 'chachingProxy',
                url: abp.appPath + 'api/services/app/role/GetRoles',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'result.totalCount'
                }
            }
        }
    }
});
