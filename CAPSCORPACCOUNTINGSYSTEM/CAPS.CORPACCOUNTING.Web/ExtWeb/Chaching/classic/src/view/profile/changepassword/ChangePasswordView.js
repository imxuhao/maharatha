
Ext.define('Chaching.view.profile.changepassword.ChangePasswordView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.changepassword.editView'],

    requires: [
        'Chaching.view.profile.changepassword.ChangePasswordViewController',
        'Chaching.view.profile.changepassword.ChangePasswordForm'
    ],

    controller: 'changepassword-changepasswordview',
    height: 400,
    width: 450,
    layout: 'fit',
    title: app.localize("ChangePassword"),
    defaultFocus: 'textfield#password',
    initComponent: function (config) {      
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.profile.changepassword.ChangePasswordForm', {
            height: '100%',
            width: '100%',
            name: 'ChangePassword'
        });
        me.items = [form];

        me.callParent(arguments);
    }
});
