Ext.define('Chaching.view.profile.loginAttempts.LoginAttemptViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.profile-loginattempts-loginattemptview',
    onLoginAttemptClose:function() {
        var me = this,
            view = me.getView();
        Ext.destroy(view);
    }
    
});
