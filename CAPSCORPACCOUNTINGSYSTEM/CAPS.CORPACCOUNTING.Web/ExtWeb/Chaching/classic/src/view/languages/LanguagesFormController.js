Ext.define('Chaching.view.languages.LanguagesFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.languages-languagesform',
    onLanguageSelect: function (combo, newVal, oldVal) {
        var me = this,
        flagStore = me.lookupReference('flagCombo').getStore(),
        language = me.lookupReference('languageCombo').getSelectedRecord(),
        isFind = false;
        flagStore.each(function (record) {
            if (language) {
                if (language.get('value').indexOf(record.get('displayText')) == 0) {
                    me.lookupReference('flagCombo').setValue(record.get('value'));
                    isFind = true;
                }
            }
               
        });
        if (!isFind) {
            me.lookupReference('flagCombo').reset();
        }
    },
    doPreSaveOperation: function(record, values, idPropertyField) {
        var data= {
            language: {
                id: parseInt(values.id) === 0 ? null : values.id,
                name: values.name,
                icon: values.icon,
                tenantId: values.tenantId

            }
        }
        record.data = data;
        return record;
    }
});
