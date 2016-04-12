Ext.define('Chaching.view.languages.LanguageTextsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.languages-languagetextsgrid',
    doAfterCreateAction: function (createMode, formView, isEdit) {      
        if (formView && isEdit) {
            var form = formView.down('form').getForm();
            form.findField('name').setReadOnly(true);
        }
    }
});
