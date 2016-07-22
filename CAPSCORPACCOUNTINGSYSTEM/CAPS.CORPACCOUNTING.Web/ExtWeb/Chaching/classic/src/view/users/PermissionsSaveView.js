
Ext.define('Chaching.view.users.PermissionsSaveView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.users.createView', 'widget.users.editView'],
    requires: [
        'Chaching.view.users.PermissionsSaveViewController',
        'Chaching.view.users.PermissionsSaveForm'
    ],
    controller: 'users-permissionssaveview',
    height: 200,
    width: 250,
    layout: 'fit',
    title: app.localize('CreateNewRole'),
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.users.PermissionsSaveForm', {
            height: '100%',
            width: '100%',
            name: 'Administration.Users'
        });
        me.items = [form];
        me.callParent(arguments);
    }
});
