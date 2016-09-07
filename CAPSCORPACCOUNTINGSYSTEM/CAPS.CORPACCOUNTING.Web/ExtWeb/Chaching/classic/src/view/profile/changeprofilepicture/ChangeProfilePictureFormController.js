Ext.define('Chaching.view.profile.changeprofilepicture.ChangeProfilePictureFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.profile-changeprofilepicture-changeprofilepictureform',
    onSaveClicked: function (btn) {
        var me = this,
        view = me.getView(),
        profilePictureWin = view.ownerCt,
        viewPort = profilePictureWin.viewModel.getParent(),
        mainChachingView = viewPort.getView(),
        viewPanel = mainChachingView.getComponent('contentPanel'),
        usersGrid = viewPanel.down('users');
        //centerPort=ChachingGlobals.getCenterPanel(),
        data = view.dataobject;
        if (Ext.isEmpty(view.getForm().findField('changeProfilePicture').getValue())) {
            abp.message.error(app.localize('SelectUserProfilePicture'), app.localize('Error'));
            saveButton = view.down('button[itemId=BtnSave]');
            saveButton.setDisabled(true);
            return;
        }
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/profile/UpdateProfilePicture',
            jsonData: Ext.encode(data),
            success: function(response, opts) {
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    me.getProfilePicture();
                    if (usersGrid) {
                        //var userGrid = centerPort.child('component[routeId=users]');
                        //if (userGrid)
                        usersGrid.getStore().reload();
                    }
                } else {
                    abp.message.error(res.error.message);
                }
            },
            failure: function (response) {
                //function to show error details (Chaching.utilities.ChachingGlobals)
                ChachingGlobals.showPageSpecificErrors(response);
            }
        });
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
                    var headerView = Ext.ComponentQuery.query('chachingheader')[0];
                    if (headerView) {
                        var profilePic = headerView.down('image[itemId=AccountPic]');
                        var button = headerView.down('*[itemId=AccountBtn]');
                        var src = 'data:image/jpeg;base64,' + res.result.image;
                        if (button.icon)
                            button.setIcon(src);
                        else
                            profilePic.setSrc(src);
                        abp.notify.success(app.localize('YourProfilePictureHasChangedSuccessfully'));
                    }
                }
                else {
                    abp.message.error(res.error.message);
                }
            },
            failure: function (response) {
                //function to show error details (Chaching.utilities.ChachingGlobals)
                ChachingGlobals.showPageSpecificErrors(response);
            }
        });
    },

    filechange: function (file,e,value) {             
        var me = this,
        view = me.getView();
        if (file.value === "")
        {
            return;
        }
        var newvalue = file.value.replace(/^c:\\fakepath\\/i, '');
        file.setRawValue(newvalue);
        if (file.value && !/^.*\.(Png|gif|jpg|jpeg|jfif|tiff|bmp)$/i.test(file.value)) {
            abp.message.error(app.localize('ProfilePicture_Warn_FileType'), app.localize('Error'));
            file.setRawValue("");
            saveButton = view.down('button[itemId=BtnSave]');
            saveButton.setDisabled(true);
            return;
        };
        if (file.fileInputEl && file.fileInputEl.dom && file.fileInputEl.dom.files && file.fileInputEl.dom.files[0].size > 2097152) {
            abp.message.error(app.localize('ProfilePicture_Warn_SizeLimit'), app.localize('Error'));
            file.setRawValue("");
            saveButton = view.down('button[itemId=BtnSave]');
            saveButton.setDisabled(true);
            return;
        }       
        view.submit({
            url: abp.appPath + 'Profile/UploadProfilePicture',
            headers: { 'Content-Type': 'multipart/form-data' },
            success: function (form, response) {
                if (response.result) {
                    form.findField('changeProfilePicture').value = "gjhsagjd";
                    var win = form.owner,
                        saveButton = win.down('button[itemId=BtnSave]');
                        saveButton.enable(true);
                    var data = response.result.result;
                    if (response.success) {
                        view.filePath = data.tempFilePath;
                        view.dataobject = data;
                       // abp.notify.success(app.localize('UploadSuccess').initCap());
                    }
                }
            },
            failure: function (form, action) {
                abp.message.error(app.localize('Failed').initCap(), app.localize('Error'));
            }
        });

    },

    disableSaveButton: function (a,b,c) {
        var me = this,
        view = me.getView(),
        saveButton = view.down('button[itemId=BtnSave]');
        saveButton.setDisabled(true);
    }

});
