Ext.define('Chaching.view.settings.SettingsController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.settings-settingsController',

    loadAllSettings: function (view, record, item, index, e, eOpts) {
        var tmView = view.down('panel[itemId=tenantManagementView]');
        if (tmView) {
            var viewModel = view.getViewModel();
            var editionStore = viewModel.getStore('editionsForComboBox');
            editionStore.load();
        }

        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/hostSettings/GetAllSettings',
            method: 'POST',
            success: function (result) {
                var res = Ext.decode(result.responseText),
                         data = res.result;
                var generalView = view.down('container[itemId=generalView]');
                if (generalView) {

                    var localAddressTextfield = generalView.down('textfield[itemId=localAddress]');
                    localAddressValue = data.general.webSiteRootAddress;
                    localAddressTextfield.setBind(
                        {
                            value: localAddressValue
                        });

                    var timeZoneCombo = generalView.down('combobox[itemId=timezone]');
                    timeZoneCombo.setValue(data.general.timezone);

                    var redisCacheCheckBox = generalView.down('checkbox[itemId=useRedisCache]');
                    redisCacheCheckBox.setValue(data.general.useRedisCacheByDefault);

                    var auditSaveToDBCheckBox = generalView.down('checkbox[itemId=auditSaveToDB]');
                    auditSaveToDBCheckBox.setValue(data.general.auditSaveToDB);
                }
               
                var emailView = view.down('panel[itemId=emailSMTPView]');
                if (emailView) {
                    var defaultFromAddressTextField = emailView.down('textfield[itemId=defaultFromAddress]'),
                     defaultFromAddressValue = data.email.defaultFromAddress;

                    defaultFromAddressTextField.setBind(
                        {
                            value: defaultFromAddressValue
                        });

                    var defaultFromDisplayNameTextField = emailView.down('textfield[itemId=defaultFromDisplayName]'),
                        defaultFromDisplayNameValue = data.email.defaultFromDisplayName;

                        defaultFromDisplayNameTextField.setBind(
                        {
                            value: defaultFromDisplayNameValue
                        });

                    var smtpHostTextField = emailView.down('textfield[itemId=smtpHost]'),
                        smtpHostValue = data.email.smtpHost;

                        smtpHostTextField.setBind(
                        {
                            value: smtpHostValue
                        });

                    var smtpPortTextField = emailView.down('textfield[itemId=smtpPort]'),
                        smtpPortValue = data.email.smtpPort;
                    smtpPortTextField.setValue(smtpPortValue);

                    var domainNameTextField = emailView.down('textfield[itemId=domainName]');
                    domainNameTextField.setValue(data.email.smtpDomain);

                    var useSslCheckBox = emailView.down('checkbox[itemId=useSsl]');
                    useSslCheckBox.setValue(data.email.smtpEnableSsl);

                    var passwordTextField = emailView.down('textfield[itemId=password]');
                    passwordTextField.setValue(data.email.smtpPassword);

                    var isDefaultCredentialsCheckBox = emailView.down('checkbox[itemId=isDefaultCredentials]');
                    isDefaultCredentialsCheckBox.setValue(data.email.smtpUseDefaultCredentials);

                    var userNameTextField = emailView.down('textfield[itemId=userName]');
                    userNameTextField.setValue(data.email.smtpUserName);
                }

                var tm = view.down('panel[itemId=tenantManagementView]');
                if (tm) {
                    var allowTenantsToRegisterToSystemCheckBox = tm.down('checkbox[itemId=allowTenantsToRegisterToSystem]');
                    //allowTenantsToRegisterToSystemCheckBox.setValue(data.tenantManagement.allowSelfRegistration);
                    //allowTenantsToRegisterToSystemCheckBox.disable();

                    var newRegisterTenantsAreActiveByDefaultCheckBox = tm.down('checkbox[itemId=newRegisterTenantsAreActiveByDefault]');
                    newRegisterTenantsAreActiveByDefaultCheckBox.setValue(data.tenantManagement.isNewRegisteredTenantActiveByDefault);

                    var useCaptchaOnRegistrationCheckBox = tm.down('checkbox[itemId=useCaptchaOnRegistration]');
                    useCaptchaOnRegistrationCheckBox.setValue(data.tenantManagement.useCaptchaOnRegistration);
                }

                var um = view.down('panel[itemId=userManagementView]');
                if (um) {
                    var emailConfigCheckBox = um.down('checkbox[itemId=emailConfig]');
                    emailConfigCheckBox.setValue(data.userManagement.isEmailConfirmationRequiredForLogin);
                }
            },

            failure: function () {
                abp.notify.error(app.localize('Error'));
            }
        });      
        
    },

    loadTimezoneData: function (combo, record, item, index, e, eOpts) {
        var comboStore = combo.getStore();
        comboStore.getProxy().setExtraParams({ defaultTimezoneScope: ChachingGlobals.settingsScope.application });
        comboStore.load();
    },

    loadCredentials: function () {
        var me = this,
            view = me.getView(),
            isDefaultCredentials = view.down('checkbox[itemId=isDefaultCredentials]'),
            domainName = me.lookupReference('domainName'),
            userName = me.lookupReference('userName'),
            password = me.lookupReference('password');
        if (isDefaultCredentials.checked)
        {
            domainName.hide();
            userName.hide();
            password.hide();
        }
        else
        {
            domainName.show();
            userName.show();
            password.show();
        }
        

    },

    addTenantByAdmin: function () {
        var me = this,
            view = me.getView(),
            isAllowTenantsToRegisterToSystem = view.down('checkbox[itemId=allowTenantsToRegisterToSystem]');
           
            var allowTenantsToRegisterToSystem = me.lookupReference('allowTenantsToRegisterToSystem'),
                newRegisteredTenantCheckBox = me.lookupReference('newRegisterTenantsAreActiveByDefault'),
                newRegisteredTenantHintLabel = me.lookupReference('newRegisteredTenantHint'),
                useCaptchaOnRegistrationCheckBox = me.lookupReference('useCaptchaOnRegistration');

            if (isAllowTenantsToRegisterToSystem.readOnly == false) {
                newRegisteredTenantCheckBox.show();
                newRegisteredTenantHintLabel.show();
                useCaptchaOnRegistrationCheckBox.show();
            }
            else {
                newRegisteredTenantCheckBox.hide();
                newRegisteredTenantHintLabel.hide();
                useCaptchaOnRegistrationCheckBox.hide();
            }
        
    },

    sendTestEmail: function () {
        var me = this,
            view = me.getView(),
            emailSettings = view.down('textfield[itemId=testEmailSettings]'),
            data = {
                emailAddress: emailSettings.value
            };

        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/hostSettings/SendTestEmail',
            jsonData: Ext.encode(data),

            success: function () {
                abp.notify.info(app.localize('TestEmailSentSuccessfully'));
            },

            failure: function () {
                abp.notify.error(app.localize('Error'));
            }
        })
    },

    onSaveClicked: function () {
        var me = this,
             view = me.getView();
        var emailDefaultFromAddressValue = view.down('textfield[itemId=defaultFromAddress]').value,
            emailDefaultFromDisplayName = view.down('textfield[itemId=defaultFromDisplayName]').value,
            emailSmtpDomain = view.down('textfield[itemId=domainName]').value,
            emailSmtpEnableSsl = view.down('checkbox[itemId=useSsl]').value,
            emailSmtpHost = view.down('textfield[itemId=smtpHost]').value,
            emailSmtpPassword = view.down('textfield[itemId=password]').value,
            emailSmtpPort = view.down('textfield[itemId=smtpPort]').value,
            emailSmtpUseDefaultCredentials = view.down('checkbox[itemId=isDefaultCredentials]').value,
            emailSmtpUserName = view.down('textfield[itemId=defaultFromDisplayName]').value;

        var generalTimezone = view.down('combobox[itemId=timezone]').value,
                generalWebSiteRootAddress = view.down('textfield[itemId=localAddress]').value,
                generalUseRedisCacheByDefaultValue = view.down('checkbox[itemId=useRedisCache]').value,
                generalAuditSaveToDBValue = view.down('checkbox[itemId=auditSaveToDB]').value;

        var tmAllowSelfRegistration = view.down('checkbox[itemId=allowTenantsToRegisterToSystem]').value,
                    tmEditionId = view.down('combobox[itemId=editions]').value,
                    tmIsNewRegisteredTenantActiveByDefault = view.down('checkbox[itemId=newRegisterTenantsAreActiveByDefault]').value,
                    tmUseCaptchaOnRegistration = view.down('checkbox[itemId=useCaptchaOnRegistration]').value;

        var umIsEmailConfirmationRequiredForLogin = view.down('checkbox[itemId=emailConfig]').value;

        var data = {
            email: {
                defaultFromAddress: emailDefaultFromAddressValue,
                defaultFromDisplayName: emailDefaultFromDisplayName,
                smtpDomain: emailSmtpDomain,
                smtpEnableSsl: emailSmtpEnableSsl,
                smtpHost: emailSmtpHost,
                smtpPassword: emailSmtpPassword,
                smtpPort: emailSmtpPort,
                smtpUseDefaultCredentials: emailSmtpUseDefaultCredentials,
                smtpUserName: emailSmtpUserName
            },
            general: {
                timezone: generalTimezone,
                timezoneForComparison:"UTC",
                webSiteRootAddress: generalWebSiteRootAddress,
                auditSaveToDB: generalAuditSaveToDBValue,
                useRedisCacheByDefault: generalUseRedisCacheByDefaultValue
            },
            tenantManagement: {
                allowSelfRegistration: tmAllowSelfRegistration,
                defaultEditionId:tmEditionId,
                isNewRegisteredTenantActiveByDefault: tmIsNewRegisteredTenantActiveByDefault,
                useCaptchaOnRegistration: tmUseCaptchaOnRegistration

            },
            userManagement: {
                isEmailConfirmationRequiredForLogin: umIsEmailConfirmationRequiredForLogin
            }
        };

        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/hostSettings/UpdateAllSettings',
            jsonData: Ext.encode(data),

            success: function () {
                abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
            },

            failure: function () {
                abp.notify.error(app.localize('Error'));
            }
        });

    }
});