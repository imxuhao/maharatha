Ext.define('Chaching.view.receivables.customers.CustomersGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.receivables-customers-customersgrid',
    doBeforeInlineAddUpdate: function (record) {
        if (record) {
            var address = record._address;
            if (address) {
                record.set('address', Ext.apply(address.data, record.address));
            }
        }
        return true;
    },
    doAfterCreateAction: function (createMode, formPanel, isEdit, record) {
        var me = this,
            view = me.getView();
        var viewModel = formPanel.getViewModel();
        var form = formPanel.getForm();

        //var vendorTypeList = viewModel.getStore('vendorTypeList');
        //vendorTypeList.load();

        //var typeOfTaxList = viewModel.getStore('typeOfTaxList');
        //typeOfTaxList.load();

        //var typeof1099BoxList = viewModel.getStore('typeof1099BoxList');
        //typeof1099BoxList.load();

        //var paymentTermsList = viewModel.getStore('paymentTermsList');
        //paymentTermsList.load();

        //// load Divisions
        //var divisionCombo = form.findField('jobId');
        //var divisionComboStore = divisionCombo.getStore();
        //divisionComboStore.load();

        //var getTaxCreditList = viewModel.getStore('getTaxCreditList');
        //getTaxCreditList.load();

        //var glAccountList = viewModel.getStore('getAccountsList');
        //glAccountList.getProxy().setExtraParams({
        //    value: 'true'
        //});
        //glAccountList.load();

        //var getAccountsListLines = viewModel.getStore('getAccountsListLines');
        //getAccountsListLines.getProxy().setExtraParams({
        //    value: 'false'
        //});
        //getAccountsListLines.load();

        if (isEdit) {
            if (record) {

                //var vendorAliasGrid = formPanel.down('*[itemId=vendorAliasGrid]');
                //if (vendorAliasGrid) {
                //    var aliasStore = vendorAliasGrid.getStore();
                //    Ext.apply(aliasStore.getProxy().extraParams, {
                //        vendorId: record.get('vendorId')
                //    });
                //    aliasStore.load();
                //}

                var vendoraddressGrid = formPanel.down('*[itemId=addressGrid]');
                if (vendoraddressGrid) {
                    var addressStore = vendoraddressGrid.getStore();
                    Ext.apply(addressStore.getProxy().extraParams, {
                        typeofObjectId: 2,
                        objectId: record.get('customerId')
                    });
                    addressStore.load();
                }
            }
        }
        else {
            //var glAccountList = viewModel.getStore('getAccountsList');
            //glAccountList.getProxy().setExtraParams({
            //    value: 'true'
            //});
            //glAccountList.load();

            //var getAccountsListLines = viewModel.getStore('getAccountsListLines');
            //getAccountsListLines.getProxy().setExtraParams({
            //    value: 'false'
            //});
            //getAccountsListLines.load();

        }
    }

    , doPostSaveOperations: function (records, operation, success) {
        var deferred = new Ext.Deferred();
        if (records) {
            var addressRec = records[0];
            var address = {
                addressId: addressRec.get('addressId'),
                objectId: addressRec.get('customerId'),
                typeofObjectId: addressRec.get('typeofObjectId') === undefined ? 2 : addressRec.get('typeofObjectId'),
                addressTypeId: addressRec.get('addressTypeId') === undefined ? 5 : addressRec.get('addressTypeId'),
                contactNumber: addressRec.get('contactNumber') === undefined ? "" : addressRec.get('contactNumber'),
                line1: addressRec.get('line1') === undefined ? "" : addressRec.get('line1'),
                line2: addressRec.get('line2') === undefined ? "" : addressRec.get('line2'),
                line3: addressRec.get('line3') === undefined ? "" : addressRec.get('line3'),
                line4: addressRec.get('line4') === undefined ? "" : addressRec.get('line4'),
                city: addressRec.get('city') === undefined ? "" : addressRec.get('city'),
                state: addressRec.get('state') === undefined ? "" : addressRec.get('state'),
                country: addressRec.get('country') === undefined ? "" : addressRec.get('country'),
                postalCode: addressRec.get('postalCode') === undefined ? "" : addressRec.get('postalCode'),
                fax: addressRec.get('fax') === undefined ? "" : addressRec.get('fax'),
                email: addressRec.get('email') === undefined ? "" : addressRec.get('email'),
                phone1: addressRec.get('phone1') === undefined ? "" : addressRec.get('phone1'),
                phone1Extension: addressRec.get('phone1Extension') === undefined ? "" : addressRec.get('phone1Extension'),
                phone2: addressRec.get('phone2') === undefined ? "" : addressRec.get('phone2'),
                phone2Extension: addressRec.get('phone2Extension') === undefined ? "" : addressRec.get('phone2Extension'),
                website: addressRec.get('website') === undefined ? "" : addressRec.get('website'),
                isPrimary: addressRec.get('isPrimary') === undefined ? true : addressRec.get('isPrimary'),
            };

            var url = "";
            if (address.addressId === undefined) {
                url = "api/services/app/addressUnit/CreateAddressUnit";
            }
            else
                url = "api/services/app/addressUnit/UpdateAddressUnit";

            Ext.Ajax.request({
                url: abp.appPath + url,
                jsonData: Ext.encode(address),
                success: function (response, opts) {
                    var res = Ext.decode(response.responseText);
                    if (res.success) {
                        deferred.resolve(response.responseText);
                    } else {
                        deferred.reject(response.responseText);
                    }
                },
                failure: function (response, opts) {
                    deferred.reject(response.responseText);
                }
            });
        }
        return deferred.promise;
    }
});
