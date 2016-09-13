/**
 * CompanySettings model used represent settings company/tenant wize.
 */
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
        { name: 'pOAutoNumberingforDivisions', type: "boolean" },
        { name: 'pOAutoNumberingforProjects', type: 'boolean' },
        { name: 'arAgingDate', type: 'boolean' },
        { name: 'apAgingDate', type: 'boolean' },
        { name: 'setDefaultAPTerms', type: 'int', defaultVaule: null, convert: nullHandler },
        { name: 'setDefaultARTerms', type: 'int', defaultVaule: null, convert: nullHandler },
        { name: 'depositGracePeriods', type: 'int', defaultVaule: null, convert: nullHandler },
        { name: 'paymentsGracePeriods', type: 'int', defaultVaule: null, convert: nullHandler },
        { name: 'defaultAPPostingDate', type: 'boolean' },
        { name: 'defaultBank', type: 'int', defaultVaule: null, convert: nullHandler },
        { name: 'allowTransactionsJobWithGL', type: "boolean" }
    ]
});