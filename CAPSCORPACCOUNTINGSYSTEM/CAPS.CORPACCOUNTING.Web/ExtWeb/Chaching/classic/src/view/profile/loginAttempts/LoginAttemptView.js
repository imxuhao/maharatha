
Ext.define('Chaching.view.profile.loginAttempts.LoginAttemptView',{
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.profile.loginAttempts.createView', 'widget.profile.loginAttempts.editView'],
    requires: [
        'Chaching.view.profile.loginAttempts.LoginAttemptViewController',
        'Chaching.view.profile.loginAttempts.LoginAttemptList'
    ],

    controller: 'profile-loginattempts-loginattemptview',
    height: 600,
    width: 550,
    layout: 'fit',
    title: app.localize("LoginAttempts"),
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.profile.loginAttempts.LoginAttemptList', {
            height: '100%',
            width: '100%',
            name: 'LoginAttempts'
        });
        me.items = [form];
        me.buttons = [
        {
            xtype: 'button',
            scale: 'small',
            iconCls: 'fa fa-close',
            iconAlign: 'left',
            text: app.localize('Close').toUpperCase(),
            ui: 'actionButton',
            name: 'Cancel',
            itemId: 'BtnCancel',
            reference: 'BtnCancel',
            handler:'onLoginAttemptClose'
        }];
        me.buttonAlign = 'right';
        me.callParent(arguments);
    }
});
