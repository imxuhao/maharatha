
Ext.define('Chaching.view.profile.linkedaccounts.LinkedAccountsView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.linkedaccounts.createView'],
    requires: [
        'Chaching.view.profile.linkedaccounts.LinkedAccountsViewController',
        'Chaching.view.profile.linkedaccounts.LinkedAccountsGrid'
    ],

    controller: 'linkedaccounts-linkedaccountsview',
    height: 600,
    width: 550,
    layout: 'fit',
    title: app.localize("LinkedAccounts"),   
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.profile.linkedaccounts.LinkedAccountsGrid', {
            height: '100%',
            width: '100%',
            name: 'LinkedAccounts'
        });
        me.items = [form];

        me.callParent(arguments);
    }
});
