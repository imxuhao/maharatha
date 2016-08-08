
Ext.define('Chaching.view.imports.ImportsView',{
    extend: 'Chaching.view.common.window.ChachingWindowPanel',

    requires: [
        'Chaching.view.imports.ImportsForm'
    ],

    height: 250,
    width: 400,
    layout: 'fit',
    title: app.localize("ImportTemplateFile"),
    autoShow: true,
    initComponent: function (config) {
        var me = this;
        var form = Ext.create('Chaching.view.imports.ImportsForm', {
            height: '100%',
            width: '100%'
        });
        me.items = [form];

        me.callParent(arguments);
    }
});
