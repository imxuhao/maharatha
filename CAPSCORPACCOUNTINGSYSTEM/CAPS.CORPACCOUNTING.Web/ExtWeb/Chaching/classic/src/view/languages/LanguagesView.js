
Ext.define('Chaching.view.languages.LanguagesView',{
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    requires: ['Chaching.view.languages.LanguagesForm'],
    alias: ['widget.languages.createView', 'widget.languages.editView'],
    height:300,
    width: 400,
    layout: 'fit',
    defaultFocus: 'combobox#language',
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.languages.LanguagesForm', {
            height: '100%',
            width: '100%',
            name: 'LanguageTexts'
        });
        var languageNames = form.getForm().findField('name');
        var languageFlags = form.getForm().findField('icon');
        me.items = [form];
        me.callParent(arguments);
        var languagesStore = Ext.create('Chaching.store.languages.LanguagesDataStore');
        languagesStore.load(function (records, operation, success) {
            if (success && records && records.length > 0) {
                var record = records[0],
                    names = record.get('languageNames'),//languageNames(), //
                    flags = record.get('flags');//flags();//
                languageNames.getStore().loadData(names);
                languageFlags.getStore().loadData(flags);
            }
            var basicForm = form.getForm();
            var formRecord = basicForm.getRecord();
            if (formRecord)
                basicForm.setValues(basicForm.getRecord().data);
        });

    }
});
