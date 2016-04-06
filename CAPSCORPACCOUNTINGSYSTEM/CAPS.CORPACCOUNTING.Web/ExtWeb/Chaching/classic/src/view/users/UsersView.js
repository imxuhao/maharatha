
Ext.define('Chaching.view.users.UsersView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.users.createView', 'widget.users.editView'],
    requires: [
        'Chaching.view.users.UsersViewController',
        'Chaching.view.users.UsersViewModel',
        'Chaching.view.users.UsersForm'
    ],

    controller: 'users-usersview',
    viewModel: {
        type: 'users-usersview'
    },
    height: 600,
    width: 450,
    layout: 'fit',
    title: app.localize("Users"),
    //defaultFocus: 'tenancyName',
    initComponent: function (config) {
       
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.users.UsersForm', {
            height: '100%',
            width: '100%',
            name: 'Users'
        });
        me.items = [form];

        me.callParent(arguments);
    }
});
