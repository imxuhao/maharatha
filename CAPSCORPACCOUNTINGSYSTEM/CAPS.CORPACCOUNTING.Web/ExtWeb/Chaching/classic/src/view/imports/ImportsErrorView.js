
Ext.define('Chaching.view.imports.ImportsErrorView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    requires: [
        'Chaching.view.imports.ImportsErrorForm'
    ],
    controller: 'window-importsView',
    height: 600,
    width: 600,
    layout: 'fit',
    title: app.localize("Error"),
    initComponent: function (config) {
        var me = this;
        var form = Ext.create('Chaching.view.imports.ImportsErrorForm', {
            height: '100%',
            width: '100%'
        });
        me.items = [form];
        me.callParent(arguments);
    },
    listeners: {
        show: 'onFormShow'
    }

});
