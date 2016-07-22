
Ext.define('Chaching.view.profile.linkedaccounts.LinkedAccountsFormView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: 'widget.manageaccounts.createView',

    requires: [
       'Chaching.view.profile.linkedaccounts.LinkedAccountsForm'
    ],

    height: 400,
    width: 450,
    layout: 'fit',
    title: app.localize("LinkedAccounts"),
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.profile.linkedaccounts.LinkedAccountsForm', {
            height: '100%',
            width: '100%',
            name: 'LinkedAccounts'
        });
        me.items = [form];

        me.callParent(arguments);
    }
});
