Ext.define('Chaching.view.payables.vendors.VendorsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.payables-vendors-vendorsform',
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
             view = me.getView();
        record = Ext.create('Chaching.model.payables.vendors.VendorsModel');
        Ext.apply(record.data, values);

        addressStore = view.down('grid').getStore();
        var addressGridStore = view.down('gridpanel[itemId=addressGrid]').getStore();
        var addressModifyRecords = addressGridStore.getModifiedRecords();
        record.set('id', values.vendorId);
        if (addressModifyRecords && addressModifyRecords.length > 0) {
            arrAddress = [];
            Ext.each(addressModifyRecords, function (rec) {
                rec.set('objectId', values.vendorId);
                var addRec = {
                    addressId: rec.get('addressId'),
                    objectId: values.vendorId,
                    typeofObjectId: rec.get('typeofObjectId'),
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

        var vendorAliasGridStore = view.down('gridpanel[itemId=vendorAliasGrid]').getStore();
        var vendorAliasModifyRecords = vendorAliasGridStore.getModifiedRecords();

        if (vendorAliasModifyRecords && vendorAliasModifyRecords.length > 0) {
            arrVendoralias = new Array();
            Ext.each(vendorAliasModifyRecords, function (rec) {
                arrVendoralias.push(rec.data);
            });
            record.data.vendorAlias = arrVendoralias;
        }
        
        return record;


    }
});
