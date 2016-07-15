
Ext.define('Chaching.view.tenants.TenantsView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.host.tenants.createView', 'widget.host.tenants.editView'],
    requires: [
        'Chaching.view.tenants.TenantsViewController',
        'Chaching.view.tenants.TenantsForm'
    ],

    controller: 'tenants-tenantsview',
    height: 570,
    width: 570,
    layout: 'fit',
    title: app.localize("Tenants"),
    defaultFocus: 'tenancyName',
    initComponent: function(config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.tenants.TenantsForm', {
           // height: '100%',
           // width: '100%',
            name:'Tenants'
        });
        me.items = [form];

        me.callParent(arguments);
    }
});
