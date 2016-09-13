Ext.define('Chaching.view.languages.LanguagesTextFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.languages-languagestextform',
    getLanguageTextsonChange: function (form) {
        var form = form.up('form');
        var store = form.down('Languagetexts').getStore();
        var data = store.getProxy();
        var filterdata = form.getValues();
        data.extraParams = {
            'TargetValueFilter': filterdata.targetValue,
            'TargetLanguageName': filterdata.targetLanguage,
            'BaseLanguageName': filterdata.baseLanguage,
            'SourceName': filterdata.source
        }
        store.load();
    }
    
});
