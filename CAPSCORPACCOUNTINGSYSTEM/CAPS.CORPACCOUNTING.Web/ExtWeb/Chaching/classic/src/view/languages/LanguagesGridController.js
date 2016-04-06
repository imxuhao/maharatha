Ext.define('Chaching.view.languages.LanguagesGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.languages-languagesgrid',
    doAfterCreateAction: function (createMode, formView, isEdit) {
        debugger;
        if (formView && isEdit) {
            var form = formView.down('form').getForm();
            form.findField('name').setReadOnly(true);
        }
    }
});
