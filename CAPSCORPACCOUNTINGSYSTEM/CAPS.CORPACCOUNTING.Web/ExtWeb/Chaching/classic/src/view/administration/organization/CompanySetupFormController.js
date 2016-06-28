Ext.define('Chaching.view.administration.organization.CompanySetupFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.administration-organization-companysetupform',
    doPreSaveOperation: function (record, values, idPropertyField) {
    },
    filechange: function (file, e, value) {
        var me = this;
        view = me.getView();
        if (file.value == "") {
            return;
        }
        var newvalue = file.value.replace(/^c:\\fakepath\\/i, '');
        file.setRawValue(newvalue);
        if (file.value && !/^.*\.(Png|gif|jpg|jpeg|jfif|tiff|bmp)$/i.test(file.value)) {
            var myMsg = Ext.create('Ext.window.MessageBox', {
                closeAction: 'destroy',
                ui: 'chachingWindow'
            }).show({
                title: "Error",
                message: app.localize('ProfilePicture_Warn_FileType').initCap(),
                buttons: Ext.Msg.OKCANCEL,
                icon: Ext.Msg.INFO
            });
            return;
        };
        if (file.fileInputEl && file.fileInputEl.dom && file.fileInputEl.dom.files && file.fileInputEl.dom.files[0].size > 2097152) {

            var myMsg = Ext.create('Ext.window.MessageBox', {
                closeAction: 'destroy',
                ui: 'chachingWindow'
            }).show({
                title: "Error",
                message: app.localize('ProfilePicture_Warn_SizeLimit').initCap(),
                buttons: Ext.Msg.OKCANCEL,
                icon: Ext.Msg.INFO
            });
            return;
        }
        view.submit({
            url: abp.appPath + 'Profile/UploadProfilePicture',
            success: function (form, response) {
                if (response.result) {
                    form.findField('changeprofilepicture').value = "gjhsagjd"
                    var data = response.result.result;
                    if (response.success) {
                        view.filePath = data.tempFilePath;
                        view.dataobject = data;
                        Ext.toast(app.localize('UploadSuccess').initCap());
                    }
                }
            },
            failure: function (form, action) {
                var myMsg = Ext.create('Ext.window.MessageBox', {
                    closeAction: 'destroy',
                    ui: 'chachingWindow'
                }).show({
                    title: "Error",
                    message: app.localize('Failed').initCap(),
                    buttons: Ext.Msg.OKCANCEL,
                    icon: Ext.Msg.INFO
                });
            }
        });

    }
});
