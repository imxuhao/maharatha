
Ext.define('Chaching.view.linkedaccounts.LinkedAccountsView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.host.linkedaccounts.manageaccountsView'],
    requires: [
        'Chaching.view.linkedaccounts.LinkedAccountsViewController',
        'Chaching.view.linkedaccounts.LinkedAccountsViewModel',
        'Chaching.view.linkedaccounts.LinkedAccountsGrid'
    ],

    controller: 'linkedaccounts-linkedaccountsview',
    viewModel: {
        type: 'linkedaccounts-linkedaccountsview'
    },
    height: 600,
    width: 550,
    layout: 'fit',
    title: app.localize("LinkedAccounts"),   
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.linkedaccounts.LinkedAccountsGrid', {
            height: '100%',
            width: '100%',
            name: 'LinkedAccounts'
        });
        me.items = [form];

        me.callParent(arguments);
    }
});
