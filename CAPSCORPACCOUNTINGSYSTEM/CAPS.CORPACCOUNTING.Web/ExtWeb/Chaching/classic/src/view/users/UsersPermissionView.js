
Ext.define('Chaching.view.users.UsersPermissionView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.users.userspermissionview'],
    requires: [
        //'Chaching.view.users.UsersPermissionViewController',
        'Chaching.view.users.UsersPermissionForm'
    ],
    //controller: 'users-usersPermissionview',
    height: '99%',
    width: '40%',
    layout: 'fit',
    title: app.localize("Permissions"),
    //defaultFocus: 'roleName',
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.users.UsersPermissionForm', {
            height: '100%',
            width: '100%',
            name: 'Administration.Users'
        });
        me.items = [form];
        me.callParent(arguments);
    }
});
