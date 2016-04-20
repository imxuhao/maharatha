
Ext.define('Chaching.view.editions.EditionsView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.host.editions.createView', 'widget.host.editions.editView'],
    requires: [
        'Chaching.view.editions.EditionsViewController',
        'Chaching.view.editions.EditionsForm'
    ],

    controller: 'editions-editionsview',
    height: 500,
    width: 450,
    layout: 'fit',
    title: app.localize("Editions"),
    defaultFocus: 'displayName',
    initComponent: function(config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.editions.EditionsForm', {
            height: '100%',
            width: '100%',
            name:'Editions'
        });
        me.items = [form];

        me.callParent(arguments);
    }
});
