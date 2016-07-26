Ext.define('Chaching.view.administration.companysetup.CompanyLogoFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.companylogoform',
    parentController: null,
    dataObject: null,

    onSaveClicked: function (btn) {
        var me = this,
        view = me.getView(),
        data = me.dataObject;
        debugger;
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/tenant/UpdateCompanyLogo',
            jsonData: Ext.encode(data),
            success: function (response, opts) {
                debugger;
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    debugger;
                    //me.getProfilePicture();
                }
                else {
                    abp.message.error(res.error.message);
                }
            },
            failure: function (response) {
                var res = Ext.decode(response.responseText);
                Ext.toast(res.error.message);
                console.log(response);
            }
        })
    },

    getCompanyLogo: function () {
        var me = this,
            main = null,
            headerview = null,
        view = me.getView(),
        data = me.dataObject;
        Ext.Ajax.request({
            url: abp.appPath + 'Profile/ShowCompanyLogo',
            jsonData: Ext.encode(data),
            success: function (response, opts) {
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    var wnd = view.up('window');
                    Ext.destroy(wnd);
                    if (Chaching.app)
                        main = Chaching.app.getMainView();
                    if (main)
                        headerview = main.down('chachingheader');
                    if (headerview) {
                       // var img = headerview.down('image[itemId=companyLogoImage]');
                        var parentView = me.parentController.getView();
                        me.parentController.companyLogo = me.dataObject;
                        var companyLogo = parentView.down('image[itemId=companyLogo]');
                        var src = 'data:image/jpeg;base64,' + res.result.image;
                        companyLogo.setSrc(src);
                    }
                }
                else {
                    abp.message.error(res.error.message);
                }
            },
            failure: function (response) {
                var res = Ext.decode(response.responseText);
                Ext.toast(res.error.message);
                console.log(response);
            }
        });
    },

   
    fileChange: function (file, e, value) {
        var me = this,
        view = me.getView(),
        form = view.getForm();
        if (file.value == "") {
            return;
        }
        var newvalue = file.value.replace(/^c:\\fakepath\\/i, '');
        file.setRawValue(newvalue);
        if (file.value && !/^.*\.(Png|gif|jpg|jpeg|jfif|tiff|bmp)$/i.test(file.value)) {
            abp.message.error(app.localize('ProfilePicture_Warn_FileType').initCap(), app.localize('Error'));
            return;
        };
        if (file.fileInputEl && file.fileInputEl.dom && file.fileInputEl.dom.files && file.fileInputEl.dom.files[0].size > 2097152) {
            abp.message.error(app.localize('ProfilePicture_Warn_SizeLimit').initCap(), app.localize('Error'));
            return;
        }
        if (form.isValid()) {
            form.submit({
                url: abp.appPath + 'Profile/UploadCompanyLogo',
                success: function (form, response) {
                    if (response.result) {
                        form.findField('companyLogoField').value = "gjhsagjd"
                        var data = response.result.result;
                        if (response.success) {
                            view.filePath = data.tempFilePath;
                            me.dataObject = data;
                            me.getCompanyLogo();
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
