Ext.define('Chaching.view.profile.linkedaccounts.LinkedAccountsViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.linkedaccounts-linkedaccountsview',
    onLinkAccountsWindowClose: function () {
        var me = this,
           view = me.getView();
        Ext.destroy(view);
    }
});
