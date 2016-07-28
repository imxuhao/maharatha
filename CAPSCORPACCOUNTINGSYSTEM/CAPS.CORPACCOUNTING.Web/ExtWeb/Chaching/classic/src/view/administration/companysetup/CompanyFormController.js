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
        //load company set up
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

    getDefaultCompanySettings : function(form) {
        var me = this,
           view = me.getView();
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/tenantSettings/GetAllTenantSettings',
            method: 'POST',
            success: function (response) {
                var result = Ext.decode(response.responseText);
                if (result.success) {
                    //load company setup information
                    var record = Ext.create('Ext.data.Model');
                    // load company settings
                    if (result.result.companySettings) {
                        Ext.apply(record.data, result.result.companySettings);
                    }
                    // load general information
                    if (result.result.companySettings) {
                        Ext.apply(record.data, result.result.general);
                        me.initialTimezone = result.result.general.timezone;
                        me.usingDefaultTimeZone = result.result.general.timezoneForComparison === abp.setting.values["Abp.Timing.TimeZone"];
                    }
                    // load userManagement information
                    if (result.result.companySettings) {
                        Ext.apply(record.data, result.result.userManagement);
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

    onCompanySetUpTabChange : function(tabPanel, newCard,oldCard) {
        var me = this;
        if (newCard.itemId == 'companyPreferencesTab') {
            me.getDefaultCompanySettings(newCard.getForm());
        }
    },

    loadCityStateAndCountry: function (zip) {
        var me = this,
           view = me.getView(),
           form = view.getForm(),
           cityCombo = form.findField('city'),
           stateCombo = form.findField('state'),
           countryCombo = form.findField('country');
           cityCombo.reset();
           stateCombo.reset();
           countryCombo.reset();
           if (zip.trim().length > 2) {
               $.getJSON('http://maps.googleapis.com/maps/api/geocode/json?address=' + zip).success(function (response) {
                   var city = [],
                       state = [],
                       country = [];
                   //find the city , state and country
                   if (response.results[0]) {
                       var address_components = response.results[0].address_components;
                       Ext.each(address_components, function (component) {
                           var types = component.types;
                           Ext.each(types, function (type) {
                               if (type == 'locality') {
                                   city.push({ name: component.long_name, value: component.short_name });
                               }
                               if (type == 'administrative_area_level_1') {
                                   state.push({ name: component.long_name, value: component.short_name });
                               }
                               if (type == 'country') {
                                   country.push({ name: component.long_name, value: component.short_name });
                               }

                           });
                       });
                       if (cityCombo && city.length > 0) {
                           cityCombo.getStore().loadData(city);
                           cityCombo.select(cityCombo.getStore().first());
                       }
                       if (stateCombo && state.length > 0) {
                           stateCombo.getStore().loadData(state);
                           stateCombo.select(stateCombo.getStore().first());
                       }
                       if (countryCombo && country.length > 0) {
                           countryCombo.getStore().loadData(country);
                           countryCombo.select(countryCombo.getStore().first());
                       }
                   }

               });
           }
        
    },

    onPostalCodeEnter: function (field, e) {
        var me = this;
        var task = new Ext.util.DelayedTask(function () {
            me.loadCityStateAndCountry(field.getValue());
        });
        task.delay(500);

        //var task = new Ext.util.DelayedTask(function () {
        //    me.loadCityStateAndCountry(field.getValue());
        //});

        //// Wait 500ms before calling our function. If the user presses another key
        //// during that 500ms, it will be cancelled and we'll wait another 500ms.
        //field.on('keypress', function () {
        //    task.delay(1000);
        //});
    },

    onCompanyLogoClick: function (btn) {
        var me = this;
        var companyLogoView = Ext.create('Chaching.view.administration.companysetup.CompanyLogoView');
        var companyLogoForm = companyLogoView.down('form');
        companyLogoForm.getController().parentController = me;
    },
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
        var myMask = new Ext.LoadMask({
            msg: 'Please wait...',
            target: view
        });
        myMask.show();
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
                myMask.hide();
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    var src = "";
                    var companyLogo = view.down('image[itemId=companyLogo]');
                    var addressField = form.findField('addressId');
                    var tenantExtendedId = form.findField('tenantExtendedId');
                    var main = null;
                    var headerView = null;
                    if (res.result.companyLogo) {
                        src = 'data:image/jpeg;base64,' + res.result.companyLogo;
                    }
                    if (addressField) {
                        addressField.setValue(res.result.addressId);
                    }
                    if (tenantExtendedId) {
                        tenantExtendedId.setValue(res.result.tenantExtendedId);
                    }
                    headerView = Ext.ComponentQuery.query('chachingheader')[0];
                    if (headerView && res.result.companyLogo) {
                        var headerCompanyLogo = headerView.down('image[itemId=companyLogoImage]');
                        headerCompanyLogo.setSrc(src);
                    }
                    if (companyLogo && form.findField('companyLogoId') && res.result.companyLogoId) {
                        companyLogo.setSrc(src);
                        form.findField('companyLogoId').setValue(res.result.companyLogoId);
                    }
                    abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
                } else {
                    abp.message.error(res.error.message);
                }
            },
            failure: function (response, opts) {
                myMask.hide();
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
