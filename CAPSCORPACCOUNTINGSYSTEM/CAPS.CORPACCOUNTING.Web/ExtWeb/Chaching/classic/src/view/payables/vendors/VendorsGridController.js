Ext.define('Chaching.view.payables.vendors.VendorsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.payables-vendors-vendorsgrid',
    doBeforeInlineAddUpdate: function (record) {
        if (record) {
            var address = record._address;
            if (address) {
                record.set('address',Ext.apply(address.data, record.address));
            }
        }
        return true;
    },
}); 
