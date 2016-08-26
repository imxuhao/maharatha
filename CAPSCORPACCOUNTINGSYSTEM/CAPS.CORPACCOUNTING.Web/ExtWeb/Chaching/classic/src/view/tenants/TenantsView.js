
Ext.define('Chaching.view.tenants.TenantsView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.host.tenants.createView', 'widget.host.tenants.editView'],
    requires: [
        //'Chaching.view.tenants.TenantsViewController',
        'Chaching.view.tenants.TenantsForm'
    ],

    //controller: 'tenants-tenantsview',
    height: '72%', //570,
    width: '40%',//570,
    layout: 'fit',
    title: app.localize("Tenants"),
    defaultFocus: 'combobox#organizationId',
    initComponent: function(config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.tenants.TenantsForm', {
            name:'Tenants'
        });
        me.items = [form];

        me.callParent(arguments);
    }
});
