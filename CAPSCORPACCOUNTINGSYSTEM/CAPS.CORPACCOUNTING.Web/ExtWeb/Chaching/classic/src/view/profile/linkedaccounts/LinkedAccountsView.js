
Ext.define('Chaching.view.profile.linkedaccounts.LinkedAccountsView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.linkedaccounts.createView'],
    requires: [
        'Chaching.view.profile.linkedaccounts.LinkedAccountsViewController',
        'Chaching.view.profile.linkedaccounts.LinkedAccountsGrid'
    ],

    controller: 'linkedaccounts-linkedaccountsview',
    height: '90%', //600,
    width: '70%',//750,
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
        me.buttons = [
            {
                xtype: 'button',
                scale: 'small',
                iconCls: 'fa fa-close',
                iconAlign: 'left',
                text: app.localize('Close').toUpperCase(),
                ui: 'actionButton',
                name: 'Cancel',
                itemId: 'btnCancel',
                reference: 'btnCancel',
                handler: 'onLinkAccountsWindowClose'
            }];
        me.buttonAlign = 'right';
        me.callParent(arguments);
    }
});
