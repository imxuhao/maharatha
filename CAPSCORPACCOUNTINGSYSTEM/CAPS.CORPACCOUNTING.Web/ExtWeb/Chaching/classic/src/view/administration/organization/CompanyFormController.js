Ext.define('Chaching.view.administration.organization.CompanyFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.administration-organization-companyform',
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
        view = me.getView(),
        form = view.getForm();
        record = Ext.create('Chaching.model.administration.organization.CompanyModel');
        record.set('id', form.findField('id').getValue());
        record.set('displayName', form.findField('displayName').getValue());

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
        var organizationSettings = "";
        var preferernceView = Ext.ComponentQuery.query('#companyPreferencesFormId', me.getView());
        if (preferernceView != undefined) {
             organizationSettings = preferernceView[0].getValues();
        }

        record.data.address = address;
        record.data.organizationSettings = organizationSettings;
        record.data.logo = '';

        return record;
    }
});
