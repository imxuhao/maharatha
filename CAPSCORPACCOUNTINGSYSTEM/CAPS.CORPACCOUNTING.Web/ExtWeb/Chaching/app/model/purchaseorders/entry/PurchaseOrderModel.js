/**
 * DataModel to represent entity schema for Purchase Order Header.
 */
Ext.define('Chaching.model.purchaseorders.entry.PurchaseOrderModel', {
    extend: 'Chaching.model.base.TransactionHeaderModel',
    config: {
        searchEntityName: 'PurchaseOrders'
    },

    fields: [
        { name: 'vendorId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'vendorName', type: 'string' },
        { name: 'paymentTermId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'paymentTerms', type: 'string' },
        { name: 'bankAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'bankAccountNumber', type: 'string' },
        { name: 'isCreditCard', type: 'boolean' },
        { name: 'isShipping', type: 'boolean' },
        { name: 'isPrintRequired', type: 'boolean' },
        { name: 'isEnterable', type: 'boolean' },
        { name: 'isHistory', type: 'boolean' },
        { name: 'isRetired', type: 'boolean' },
        { name: 'sourcePoAccountingDocumentId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'invoiceAccountingDocumentId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'uploadDocumentLogId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'uploadDocumentLogDesc', type: 'string' },
        { name: 'isWillBill', type: 'boolean' },
        { name: 'isAdditionalBill', type: 'boolean' },
        { name: 'isPettyCash', type: 'boolean' },
        { name: 'isPaymentCheck', type: 'boolean' },
        { name: 'isDepositCheck', type: 'boolean' },
        { name: 'isPartial', type: 'boolean' },
        { name: 'isOverage', type: 'boolean' },
        { name: 'isReimbursement', type: 'boolean' },
        { name: 'isReinstated', type: 'boolean' },
        { name: 'dateNeededBy', type: 'date', dateFormat: 'c' },
        { name: 'timeNeededBy', type: 'string' },
        { name: 'reinstatedPoDocumentId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'controllingBankAccountId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'controllingBankAccountNumber', type: 'string' },
        { name: 'isApproveEmail', type: 'boolean' },
        { name: 'poOriginalAmount', type: 'float' },
        { name: 'jobNumber', type: 'string' },
        { name: 'accountNumber', type: 'string' },
        { name: 'approvedBy', type: 'string' },
        { name: 'remainingBalance', type: 'float' },
        { name: 'pendingTrans', type: 'string' }
    ]
});
