Ext.define('Chaching.view.address.AddressFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.address-addressform',
    doPreSaveOperation: function(record, values, idPropertyField) {
        ///TODO: update record in parent grid.
        return false;
    }
});
