Ext.define('Chaching.view.profile.settings.SettingsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.profile-settings-settingsform',
    onSaveClicked: function (btn) {      
        var me = this,
        view = me.getView(),
        data = view.getValues(),        
        input = new Object();
        input.Name = data.name,
        input.Surname = data.surname,
        input.UserName = data.userName,
        input.EmailAddress = data.emailAddress;
            
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/profile/UpdateCurrentUserProfile',
            jsonData: Ext.encode(input),
            success: function (response, opts) {
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    var wnd = view.up('window');
                    Ext.destroy(wnd);
                    abp.notify.info(app.localize('SavedSuccessfully'));
                }
            },
            failure: function (response, opts) {
                var res = Ext.decode(response.responseText);
                Ext.toast(res.exceptionMessage);
                console.log(response);
            }



        })
    }
    
});
