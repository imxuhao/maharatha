Ext.define('Chaching.view.administration.companysetup.CompanyFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.administration-companysetup-companyform',
    initialTimezone: null,
    usingDefaultTimeZone: null,
    companyLogo : null,
    onCompanySetupRender: function () {
        var me = this,
        view = me.getView(),
        form = view.getForm(),
        timezoneCombo = me.lookupReference('timezone'),
        setDefaultApTermsCombo = me.lookupReference('setDefaultAPTerms'),
        setDefaultArTermsCombo = me.lookupReference('setDefaultARTerms'),
        timezoneStore = timezoneCombo.getStore();
        //load time zone combo
        timezoneStore.getProxy().setExtraParams({ defaultTimezoneScope: ChachingGlobals.settingsScope.tenant });
        timezoneStore.load();
        //load ap default terms
        setDefaultApTermsCombo.getStore().load();
        // load ar default terms
        setDefaultArTermsCombo.getStore().load();
        //load default combo
        var defaultBankCombo = form.findField('defaultBank');
        defaultBankCombo.getStore().load();

        me.getCompanySetupInformation(form);

    },

    getCompanySetupInformation: function (form) {
        var me = this,
            view = me.getView();
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/tenant/GetCompanySettingsForEdit',
            method: 'POST',
            success: function (response) {
                var result = Ext.decode(response.responseText);
                if (result.success) {
                    var src = 'data:image/jpeg;base64,' + result.result.companyLogo;
                    var companyLogo = view.down('image[itemId=companyLogo]');
                    if (companyLogo) {
                        companyLogo.setSrc(src);
                    }
                    //load company setup information
                    var record = Ext.create('Ext.data.Model');
                    if (result.result.address) {
                        Ext.apply(record.data, result.result.address);
                    }
                    Ext.apply(record.data, result.result);
                    // load company settings
                    if (result.result.companySettings) {
                        Ext.apply(record.data, result.result.companySettings.companySettings);
                    }
                    // load general information
                    if (result.result.companySettings) {
                        Ext.apply(record.data, result.result.companySettings.general);
                        me.initialTimezone = result.result.companySettings.general.timezone;
                        me.usingDefaultTimeZone = result.result.companySettings.general.timezoneForComparison === abp.setting.values["Abp.Timing.TimeZone"];
                    }
                    // load userManagement information
                    if (result.result.companySettings) {
                        Ext.apply(record.data, result.result.companySettings.userManagement);
                    }

                    //load company preferences
                    form.loadRecord(record);
                } else {
                    abp.message.error(result.error.message);
                }
            },

            failure: function (response) {
                var result = Ext.decode(response.responseText);
                if (!Ext.isEmpty(result.exceptionMessage)) {
                    abp.message.error(result.exceptionMessage);
                } else {
                    abp.message.error(result.message);
                }
            }
        });
    },


    onPostalCodeEnter: function (field, e) {
        var clientKey = "js-9qZHzu2Flc59Eq5rx10JdKERovBlJp3TQ3ApyC4TOa3tA8U7aVRnFwf41RpLgtE7";
        var url = "https://www.zipcodeapi.com/rest/" + clientKey + "/info.json/" + field.getValue() + "/radians";
        if (13 == e.getKey()) {
            var store = Ext.create('Ext.data.Store', {
                fields: [{ name: 'id' }],
                autoLoad: true,
                proxy: {
                    type: 'jsonp',
                    url: url,//'http://maps.googleapis.com/maps/api/geocode/json?address=37779',
                    reader: {
                        type: 'json',
                        rootProperty: 'results'
                    }
                },
                listeners: {
                    'load': function (records, operation, success) {

                    }
                }
            });
        }
    },

    onCompanyLogoClick: function (btn) {
        var me = this;
        var companyLogoView = Ext.create('Chaching.view.administration.companysetup.CompanyLogoView');
        var companyLogoForm = companyLogoView.down('form');
        companyLogoForm.getController().parentController = me;
    },

    //onFileChange: function (file, e, value) {
    //    var me = this,
    //    view = me.getView(),
    //    companySetupForm = view.down('#companySetupTab');
    //    if (file.value == "") {
    //        return;
    //    }
    //    var newvalue = file.value.replace(/^c:\\fakepath\\/i, '');
    //    file.setRawValue(newvalue);
    //    if (file.value && !/^.*\.(Png|gif|jpg|jpeg|jfif|tiff|bmp)$/i.test(file.value)) {
    //        abp.message.error(app.localize('ProfilePicture_Warn_FileType').initCap(), 'Error');
    //        return;
    //    };
    //    if (file.fileInputEl && file.fileInputEl.dom && file.fileInputEl.dom.files && file.fileInputEl.dom.files[0].size > 2097152) {
    //        abp.message.error(app.localize('ProfilePicture_Warn_SizeLimit').initCap(), 'Error');
    //        return;
    //    }
    //    companySetupForm.submit({
    //        url: abp.appPath + 'OrganizationUnits/UpdateOrganizationPicture',
    //        success: function (form, response) {
    //            if (response.result) {
    //                form.findField('companyLogo').value = "gjhsagjd"
    //                var data = response.result.result;
    //                if (response.success) {
    //                    view.filePath = data.tempFilePath;
    //                    view.dataobject = data;
    //                    abp.notify.success(app.localize('UploadSuccess').initCap(), 'Success');
    //                }
    //            }
    //        },
    //        failure: function (form, action) {
    //            abp.notify.success(app.localize('Failed').initCap(), 'Error');
    //        }
    //    });

    //},

    onSaveClicked: function (btn) {
        var me = this,
        view = me.getView(),
        form = view.getForm(),
        record = Ext.create('Chaching.model.administration.organization.CompanyModel');
        record.set('tenantExtendedId', form.findField('tenantExtendedId').getValue());
        // record.set('displayName', form.findField('displayName').getValue());
        record.set('companyLogoId', form.findField('companyLogoId').getValue());
        record.set('federalTaxId', form.findField('federalTaxId').getValue());
        record.set('transmitterContactName', form.findField('transmitterContactName').getValue());
        record.set('transmitterEmailAddress', form.findField('transmitterEmailAddress').getValue());
        record.set('transmitterCode', form.findField('transmitterCode').getValue());
        record.set('transmitterControlCode', form.findField('transmitterControlCode').getValue());

        var address = {
            addressId: form.findField('addressId').getValue(),//rec.get('addressId'),
            organizationUnitId: Chaching.utilities.ChachingGlobals.loggedInUserInfo.userOrganizationId,
            objectId: form.findField('tenantExtendedId').getValue(),
            typeofObjectId: 10, // for company(tenant)
            addressTypeId: 5, // for primary
            contactNumber: '',
            line1: form.findField('line1').getValue(),
            line2: form.findField('line2').getValue(),
            line3: form.findField('line3').getValue(),
            line4: '',
            city: form.findField('city').getValue(),
            state: form.findField('state').getValue(),
            country: form.findField('state').getValue(),
            postalCode: form.findField('postalCode').getValue(),
            fax: '',
            email: form.findField('email').getValue(),
            phone1: form.findField('phone1').getValue(),
            phone1Extension: '',
            phone2: '',
            phone2Extension: '',
            website: '',
            isPrimary: true
        }
        record.data.address = address;
        record.data.comapanyLogo = me.companyLogo == null ? null : me.companyLogo;
        // var timezoneCombo = view.down('combobox[itemId=timezone]');
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/tenant/UpdateCompanyUnit',
            jsonData: Ext.encode(record.data),
            success: function (response, opts) {
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    var src = 'data:image/jpeg;base64,' + res.result.companyLogo;
                    var companyLogo = view.down('image[itemId=companyLogo]');
                    var main = null;
                    var headerView = null;
                    if (Chaching.app)
                        main = Chaching.app.getMainView();
                    if (main)
                        headerView = main.down('chachingheader');
                    if (headerView) {
                        var headerCompanyLogo = headerView.down('image[itemId=companyLogoImage]');
                        headerCompanyLogo.setSrc(src);
                    }
                    if (companyLogo && form.findField('companyLogoId')) {
                        companyLogo.setSrc(src);
                        form.findField('companyLogoId').setValue(res.result.companyLogoId);
                    }
                    abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
                } else {
                    abp.message.error(res.error.message);
                }
            },
            failure: function (response, opts) {
                var result = Ext.decode(response.responseText);
                if (!Ext.isEmpty(result.exceptionMessage)) {
                    abp.message.error(result.exceptionMessage);
                } else {
                    abp.message.error(result.error.details);
                }
            }
        })
    },
    onSaveCompanyPreferences: function () {
        var me = this, view = me.getView(),
         generalTab = Ext.ComponentQuery.query('#companyPreferencesGeneralTab', view)[0],
         userManagementTab = Ext.ComponentQuery.query('#companyPreferencesUserManagementTab', view)[0],
         companySettingsTab = Ext.ComponentQuery.query('#companySettingsTab', view)[0],
         timezoneCombo = view.down('combobox[itemId=timezone]'),
         requestObj = {};
        if (generalTab) {
            requestObj.general = generalTab.getValues();
        }
        if (userManagementTab) {
            requestObj.userManagement = userManagementTab.getValues();
        }
        if (companySettingsTab) {
            requestObj.companySettings = companySettingsTab.getValues();
            if (Ext.isEmpty(requestObj.companySettings.setDefaultAPTerms)) {
                requestObj.companySettings.setDefaultAPTerms = 0;
            }
            if (Ext.isEmpty(requestObj.companySettings.setDefaultARTerms)) {
                requestObj.companySettings.setDefaultARTerms = 0;
            }
            if (Ext.isEmpty(requestObj.companySettings.defaultBank)) {
                requestObj.companySettings.defaultBank = 0;
            }
        }
        var myMask = new Ext.LoadMask({
            msg: 'Please wait...',
            target: view
        });
        myMask.show();
        //fire save request
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/tenantSettings/UpdateAllTenantSettings',
            jsonData: Ext.encode(requestObj),
            success: function (response, opts) {
                myMask.hide();
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
                    if (abp.clock.provider.supportsMultipleTimezone && me.usingDefaultTimeZone && me.initialTimezone !== timezoneCombo.getValue()) {
                        abp.message.info(app.localize('TimeZoneSettingChangedRefreshPageNotification')).done(function () {
                            window.location.reload();
                        });
                    }
                } else {
                    var message = '',
                        title = 'Error';
                    if (res && res.error) {
                        if (res.error.message && res.error.details) {
                            title = res.error.message;
                            message = res.error.details;
                            abp.message.warn(message, title);
                            return;
                        }
                        title = res.error.message;
                        message = res.error.details ? res.error.details : title;
                    }
                    abp.message.error(message, title);
                }
            },
            failure: function (response, opts) {
                myMask.hide();
                var result = Ext.decode(response.responseText);
                if (!Ext.isEmpty(result.exceptionMessage)) {
                    abp.message.error(result.exceptionMessage);
                } else {
                    abp.message.error(result.message);
                }
            }
        });
    }
});
