Ext.define('Chaching.view.languages.LanguagesFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.languages-languagesform',
    doPreSaveOperation: function(record, values, idPropertyField) {
        var data= {
            language: {
                id: parseInt(values.id) === 0 ? null : values.id,
                name: values.name,
                icon: values.icon
            }
        }
        record.data = data;
        return record;
    }
});
