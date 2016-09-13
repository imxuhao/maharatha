
Ext.define('Chaching.view.profile.changeprofilepicture.ChangeProfilePictureView',{
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.changeprofilepicture.editView'],

    requires: [
        'Chaching.view.profile.changeprofilepicture.ChangeProfilePictureViewController',
        'Chaching.view.profile.changeprofilepicture.ChangeProfilePictureForm'
    ],

    controller: 'profile-changeprofilepicture-changeprofilepictureview',

    height: 250,
    width: 400,
    layout: 'fit',
    title: app.localize("ChangeProfilePicture"),
    dataobject:null,
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.profile.changeprofilepicture.ChangeProfilePictureForm', {
            height: '100%',
            width: '100%',
            name: 'ChangeProfilePicture'
        });
        me.items = [form];

        me.callParent(arguments);
    }
});
