Ext.define('Chaching.model.administration.organization.CompanySettingsModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: ''
    },
    fields: [
        { name: 'isAllowDuplicateAPInvoiceNos', type: 'boolean'},
        { name: 'isAllowDuplicateARInvoiceNos', type: 'boolean' },
        { name: 'isAllowAccountnumbersStartingwithZero', type: "boolean"},
        { name: 'isImportPOlogsfromProducersActualUploads', type: "boolean" },
        { name: 'buildAPuponCCstatementPosting', type: "boolean" },
        { name: 'buildAPuponPayrollPosting', type: 'boolean' },
        { name: 'poAutoNumbering', type: 'boolean' },
        { name: 'arAgingDate', type: 'boolean' },
        { name: 'apAgingDate', type: 'boolean' },
        { name: 'depositGracePeriods', type: 'string' },
        { name: 'paymentsGracePeriods', type: 'string' },
        { name: 'defaultAPPostingDate', type: 'boolean' },
        { name: 'defaultBank', type: 'int' },
        { name: 'allowTransactionsJobWithGL', type: "boolean" }
    ]//,
    //belongsTo: 'Chaching.model.payables.vendors.VendorsModel'
});