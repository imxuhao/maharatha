Ext.define('Chaching.view.payables.vendors.VendorsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.payables-vendors-vendorsform',
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
             view = me.getView();
        record = Ext.create('Chaching.model.payables.vendors.VendorsModel');
        Ext.apply(record.data, values);

        var addressStore = view.down('grid').getStore();
        var addressGridStore = view.down('gridpanel[itemId=addressGrid]').getStore();
        var addressModifyRecords = addressGridStore.getModifiedRecords();
        record.set('id', values.vendorId);
        if (addressModifyRecords && addressModifyRecords.length > 0) {
            var arrAddress = [];
            Ext.each(addressModifyRecords, function (rec) {
                rec.set('objectId', values.vendorId);
                var addRec = {
                    addressId: rec.get('addressId'),
                    organizationUnitId : Chaching.utilities.ChachingGlobals.loggedInUserInfo.userOrganizationId,
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
            var arrVendoralias = new Array();
            Ext.each(vendorAliasModifyRecords, function (rec) {
                arrVendoralias.push(rec.data);
            });
            record.data.vendorAlias = arrVendoralias;
        }
        
        return record;


    },
    onFormPanelResize: function (formPanel, width, height, oldWidth, oldHeight, eOpts) {
        var addressGrid = formPanel.down('gridpanel[itemId=addressGrid]'),
            aliasGrid = formPanel.down('gridpanel[itemId=vendorAliasGrid]');
        if (addressGrid && aliasGrid) {
            var fieldSets = formPanel.query('fieldset');
            var generalHeight = 0,
                addressHeight = 0,
                otherHeight=0,
                aliasHeight = 0;
            for (var i = 0; i < fieldSets.length; i++) {
                var fieldSet = fieldSets[i];
                if (!fieldSet.rendered) continue;
                if (fieldSet.itemId === "generalField" || fieldSet.itemId === 'taxInfoField')
                    generalHeight += fieldSet.getHeight();
                else if (fieldSet.itemId === "defaultField" || fieldSet.itemId === 'notesField')
                    otherHeight += fieldSet.getHeight();
            }
            addressHeight = height - (generalHeight + 350);
            if (otherHeight > 0) {
                aliasHeight = height - (otherHeight + 350);
                aliasGrid.setHeight(aliasHeight < 200 ? 200 : aliasHeight);
            }
            addressGrid.setHeight(addressHeight < 200 ? 200 : addressHeight);
        }
        formPanel.updateLayout();
    }
});
