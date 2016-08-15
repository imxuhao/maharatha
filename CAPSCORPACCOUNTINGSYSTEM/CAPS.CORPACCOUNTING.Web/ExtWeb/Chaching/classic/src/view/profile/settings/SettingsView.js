
Ext.define('Chaching.view.profile.settings.SettingsView',{
    extend: 'Chaching.view.common.window.ChachingWindowPanel',

    requires: [
        //'Chaching.view.profile.settings.SettingsViewController',
        'Chaching.view.profile.settings.SettingsForm'
    ],

    //controller: 'profile-settings-settingsview',
    height: 450,
    width: 450,
    layout: 'fit',
    title: app.localize("MySettings"),
    defaultFocus: 'textfield#name',
    initComponent: function (config) {     
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.profile.settings.SettingsForm', {
            height: '100%',
            width: '100%',
            name: 'MySettings'
        });
        me.items = [form];

        me.callParent(arguments);
    }
});
