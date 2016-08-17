
Ext.define('Chaching.view.imports.ImportsErrorView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    requires: [
        'Chaching.view.imports.ImportsErrorForm'
    ],
    controller: 'window-importsView',
    height: '90%',
    width: '70%',
    layout: 'fit',
    title: app.localize("Error"),
    iconCls: 'fa fa-bug',
    initComponent: function (config) {
        var me = this;
        var form = Ext.create('Chaching.view.imports.ImportsErrorForm');
        me.items = [form];
        me.callParent(arguments);
    },
    listeners: {
        show: 'onFormShow'
    }

});
