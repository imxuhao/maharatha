//Import viewController

Ext.define('Chaching.view.import.ImportFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.ImportForm',
    parentController: null,
    entityName: null,
    onFileChange : function(file, e) {
        if (file.value == "") {
            return;
        }
        var newvalue = file.value.replace(/^c:\\fakepath\\/i, '');
        file.setRawValue(newvalue);
        var regExpress = new RegExp('([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$');
        regExpress.test(file.value);
        if (file.value && !(regExpress.test(file.value))) {
            abp.message.error(app.localize('Template_FileType_Warn').initCap(), app.localize('Error'));
            return;
        };
    },
    uploadTemplateFile: function () {
        var me = this,
        view = me.getView(),
        form = view.getForm();
        if (form.isValid()) {
            form.submit({
                url: abp.appPath + 'upload/UploadTemplateFile',
                params: {
                    'entity': me.entityName
                },
                success: function (form, response) {
                    if (response.result) {
                        var data = response.result.result;
                        if (response.success) {
                            
                        }
                    }
                },
                failure: function (form, action) {
                    abp.message.error(app.localize('Failed').initCap(), app.localize('Error'));
                }
            });
        }

    }

});
