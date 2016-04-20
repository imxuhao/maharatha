
Ext.define('Chaching.view.languages.LanguagesView',{
    extend: 'Chaching.view.common.window.ChachingWindowPanel',

    columndata:null,
    height: 600,
    width: 800,
    layout: 'fit',
    title: app.localize("LanguageTexts"),  
    initComponent: function (config) {       
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.languages.LanguagesForm', {
            height: '100%',
            width: '100%',
            name: 'LanguageTexts'
        });
        me.items = [form];   
       
        me.callParent(arguments);
    }
});
