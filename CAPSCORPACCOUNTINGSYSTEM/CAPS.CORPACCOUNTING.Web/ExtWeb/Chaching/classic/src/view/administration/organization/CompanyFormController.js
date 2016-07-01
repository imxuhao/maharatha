Ext.define('Chaching.view.administration.organization.CompanyFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.administration-organization-companyform',

    onPostalCodeEnter: function (field,e) {
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

    onFileChange: function (file, e, value) {
        var me = this;
        view = me.getView();
        if (file.value == "") {
            return;
        }
        var newvalue = file.value.replace(/^c:\\fakepath\\/i, '');
        file.setRawValue(newvalue);
        if (file.value && !/^.*\.(Png|gif|jpg|jpeg|jfif|tiff|bmp)$/i.test(file.value)) {
            abp.message.error(app.localize('ProfilePicture_Warn_FileType').initCap(), 'Error');
            return;
        };
        if (file.fileInputEl && file.fileInputEl.dom && file.fileInputEl.dom.files && file.fileInputEl.dom.files[0].size > 2097152) {
            abp.message.error(app.localize('ProfilePicture_Warn_SizeLimit').initCap(), 'Error');
            return;
        }
        view.submit({
            url: abp.appPath + 'OrganizationUnits/UpdateOrganizationPicture',
            success: function (form, response) {
                if (response.result) {
                    form.findField('companyLogo').value = "gjhsagjd"
                    var data = response.result.result;
                    if (response.success) {
                        view.filePath = data.tempFilePath;
                        view.dataobject = data;
                        abp.notify.success(app.localize('UploadSuccess').initCap(), 'Success');
                    }
                }
            },
            failure: function (form, action) {
                abp.notify.success(app.localize('Failed').initCap(), 'Error');
            }
        });

    },
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
        view = me.getView(),
        form = view.getForm();
        record = Ext.create('Chaching.model.administration.organization.CompanyModel');
        record.set('id', form.findField('id').getValue());
        record.set('displayName', form.findField('displayName').getValue());
        record.set('federalTaxId', form.findField('federalTaxId').getValue());
        record.set('transmitterContactName', form.findField('transmitterContactName').getValue());
        record.set('transmitterEmailAddress', form.findField('transmitterEmailAddress').getValue());
        record.set('transmitterCode', form.findField('transmitterCode').getValue());
        record.set('transmitterControlCode', form.findField('transmitterControlCode').getValue());
       
        var address = {
            addressId: form.findField('addressId').getValue(),//rec.get('addressId'),
            organizationUnitId: Chaching.utilities.ChachingGlobals.loggedInUserInfo.userOrganizationId,
            objectId: values.id,
            typeofObjectId: 7, // for organization
            addressTypeId: 5, // for organization
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
        record.data.logo = view.dataobject == null ? null : view.dataobject;
        return record;
    },
    onSaveCompanyPreferences: function () {
        var me = this, view = me.getView(), form = view.getForm(), parentGrid = view.parentGrid;
        var organizationSettings = "";
        var preferernceView = Ext.ComponentQuery.query('#companyPreferencesTab', view);
        if (preferernceView[0] != undefined) {
            organizationSettings = preferernceView[0].getValues();
            organizationSettings.setDefaultAPTerms = organizationSettings.setDefaultAPTerms == "" ? null : organizationSettings.setDefaultAPTerms;
            organizationSettings.setDefaultARTerms = organizationSettings.setDefaultARTerms == "" ? null : organizationSettings.setDefaultARTerms;
            organizationSettings.depositGracePeriods = organizationSettings.depositGracePeriods == "" ? null : organizationSettings.depositGracePeriods;
            organizationSettings.paymentsGracePeriods = organizationSettings.paymentsGracePeriods == "" ? null : organizationSettings.paymentsGracePeriods;
            organizationSettings.defaultBank = organizationSettings.defaultBank == "" ? null : organizationSettings.defaultBank;
            organizationSettings.organizationUnitId = view.down('hiddenfield[itemId=companyItemId]').getValue();
        }

        if (parentGrid) {
            var target;
            if (view.openInPopupWindow) {
                target = view.up('window');
            } else {
                target = view;
            }
            var myMask = new Ext.LoadMask({
                msg: 'Please wait...',
                target: target
            });
            myMask.show();
            //fire save request
            Ext.Ajax.request({
                url: abp.appPath + 'api/services/app/organizationUnit/UpdateAllSettings',
                jsonData: Ext.encode(organizationSettings),
                success: function (response, opts) {
                    myMask.hide();
                    var res = Ext.decode(response.responseText);
                    if (res.success) {
                        var gridController = parentGrid.getController();
                        gridController.doReloadGrid();

                        if (view && view.openInPopupWindow) {
                            var wnd = view.up('window');
                            Ext.destroy(wnd);
                        } else if (view) {
                            Ext.destroy(view);
                        }
                        abp.notify.success('Operation completed successfully.', 'Success');
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
                    var res = Ext.decode(response.responseText);
                    Ext.toast(res.exceptionMessage);
                    console.log(response);
                }
            });
        }
    }
});
