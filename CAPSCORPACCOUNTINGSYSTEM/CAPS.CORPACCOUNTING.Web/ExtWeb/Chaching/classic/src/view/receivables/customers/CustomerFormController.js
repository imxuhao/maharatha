Ext.define('Chaching.view.receivables.customers.CustomerFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.receivables-customers-customersform',
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
             view = me.getView();
        record = Ext.create('Chaching.model.receivables.customers.CustomersModel');
        Ext.apply(record.data, values);

        addressStore = view.down('grid').getStore();
        var addressGridStore = view.down('gridpanel[itemId=addressGrid]').getStore();
        var addressModifyRecords = addressGridStore.getModifiedRecords();
        record.set('id', values.customerId);
        if (addressModifyRecords && addressModifyRecords.length > 0) {
            arrAddress = [];
            Ext.each(addressModifyRecords, function (rec) {
                rec.set('objectId', values.customerId);
                var addRec = {
                    addressId: rec.get('addressId'),
                    organizationUnitId: Chaching.utilities.ChachingGlobals.loggedInUserInfo.userOrganizationId,
                    objectId: values.customerId,
                    typeofObjectId: 2, // typeofObjectId = 2 for customer
                    addressTypeId: rec.get('addressTypeId'),
                    contactNumber: rec.get('contactNumber'),
                    line1: rec.get('line1'),
                    line2: rec.get('line2'),
                    line3: rec.get('line3'),
                    line4: rec.get('line4'),
                    city: rec.get('city'),
                    state: rec.get('state'),
                    country: rec.get('country'),
                    postalCode: rec.get('postalCode'),
                    fax: rec.get('fax'),
                    email: rec.get('email'),
                    phone1: rec.get('phone1'),
                    phone1Extension: rec.get('phone1Extension'),
                    phone2: rec.get('phone2'),
                    phone2Extension: rec.get('phone2Extension'),
                    website: rec.get('website'),
                    isPrimary: rec.get('isPrimary')
                };
                arrAddress.push(addRec);
            });
            record.data.addresses = arrAddress;
        }

        return record;
    }
});
