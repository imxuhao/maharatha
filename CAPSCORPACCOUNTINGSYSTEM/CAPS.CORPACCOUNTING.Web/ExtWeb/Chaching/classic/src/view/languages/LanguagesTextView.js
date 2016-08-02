
Ext.define('Chaching.view.languages.LanguagesTextView',{
    extend: 'Chaching.view.common.window.ChachingWindowPanel',

    requires: [
        'Chaching.view.languages.LanguagesTextViewController',
        'Chaching.view.languages.LanguagesTextForm'
    ],
    controller: 'languages-languagestextview',
    columndata: null,
    //height: 680,
    //width: 1100,
    height: '90%',
    width: '80%',
    layout: 'fit',
    iconCls: 'fa fa-pencil',
    title: app.localize("LanguageTexts"),
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.languages.LanguagesTextForm', {
            height: '100%',
            width: '100%',
            name: 'LanguageTexts'
        });
        me.items = [form];

        me.callParent(arguments);
    },
    listeners: {
        resize:'onWindowResize'
    }
});
