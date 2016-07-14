/**
 * DataModel to represent entity schema for Purchase Order Distribution.
 */
Ext.define('Chaching.model.purchaseorders.entry.PurchaseOrderDetailModel', {
    extend: 'Chaching.model.base.TransactionDetailsModel',

    fields: [
        { name: 'isPrePaid', type: 'boolean' },
        { name: 'isPoPurchase', type: 'boolean' },
        { name: 'isPoRental', type: 'boolean' },
        { name: 'vendorId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'vendorName', type: 'string' },
        { name: 'typeOfCheck', type: 'string' },
        { name: 'typeOfCheckId', type: 'int' }
    ]
});
