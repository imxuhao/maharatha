Ext.define('Chaching.view.purchaseorders.entry.PurchaseOrderDetailGridController', {
    extend: 'Chaching.view.common.grid.ChachingTransactionDetailGridController',
    alias: 'controller.purchaseorders-entry-purchaseorderdetailgrid',
    doModuleSpecificBeforeEdit:function(editor, context, eOpts) {
        var dataIndex = context.field,
            record = context.record,
            returnVal = true;

        switch (dataIndex) {
            case "typeOfCheck":
                if (!record.get('invoiceReference')) returnVal = false;
                break;
            case "amount":///TODO: Check based on remainig and pending amount
                //if (record.get('accountingItemOrigAmount') !== record.get('amount')) returnVal = false;
                break;
            default:
                break;
        }
        return returnVal;
    }
    
});
