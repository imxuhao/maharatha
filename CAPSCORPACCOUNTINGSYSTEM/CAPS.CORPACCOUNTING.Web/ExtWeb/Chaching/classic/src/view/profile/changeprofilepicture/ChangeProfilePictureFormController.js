Ext.define('Chaching.view.profile.changeprofilepicture.ChangeProfilePictureFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.profile-changeprofilepicture-changeprofilepictureform',
    onSaveClicked: function (btn) {
        var me = this;
        view = me.getView();
        data = view.dataobject;
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/profile/UpdateProfilePicture',
            jsonData: Ext.encode(data),
            success: function (response, opts) {
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    me.getProfilePicture();
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

    getProfilePicture: function () {
        var me = this,
        view = me.getView();
        Ext.Ajax.request({
            url: abp.appPath + 'Profile/GetProfilePictureToShow',
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
                        var img = headerview.down('image[itemId=AccountPic]');
                        var button = headerview.down('*[itemId=AccountBtn]');
                        var src = 'data:image/jpeg;base64,' + res.result.image;
                        if (button.icon)
                            button.setIcon(src);
                        else
                            img.setSrc(src);
                        abp.notify.success(app.localize('YourProfilePictureHasChangedSuccessfully').initCap());
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

    filechange: function (file,e,value) {             
        var me = this;
        view = me.getView();
        if (file.value=="")
        {
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
                        abp.notify.success(app.localize('UploadSuccess').initCap());
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
