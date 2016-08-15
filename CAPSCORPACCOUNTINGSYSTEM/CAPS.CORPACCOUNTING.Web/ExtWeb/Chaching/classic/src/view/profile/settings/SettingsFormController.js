Ext.define('Chaching.view.profile.settings.SettingsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.profile-settings-settingsform',
    initialTimezone: null,
    onSaveClicked: function (btn) {
        var me = this,
        view = me.getView(),
        timezoneCombo = view.down('combobox[itemId=timezone]'),
        data = view.getValues(),        
        input = new Object();
        input.Name = data.name,
        input.Surname = data.surname,
        input.UserName = data.userName,
        input.EmailAddress = data.emailAddress;
        if (timezoneCombo.getValue() && !timezoneCombo.isHidden()) {
            input.timezone = data.timezone;
        } else {
            input.timezone = me.initialTimezone;
        }

        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/profile/UpdateCurrentUserProfile',
            jsonData: Ext.encode(input),
            success: function(response, opts) {
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    var wnd = view.up('window');
                    Ext.destroy(wnd);
                    abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
                    if (abp.clock.provider.supportsMultipleTimezone && me.initialTimezone !== input.timezone) {
                        abp.message.info(app.localize('TimeZoneSettingChangedRefreshPageNotification'))
                            .done(function() {
                                window.location.reload();
                            });
                    }
                }
            },
            failure: function(response, opts) {
                //function to show error details (Chaching.utilities.ChachingGlobals)
                ChachingGlobals.showPageSpecificErrors(response);
            }
        });
    }
    
});
