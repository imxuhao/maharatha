Ext.define('Chaching.model.financials.AccountingDocumentModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'journals'
    },
    fields: [
{ name: 'accountingDocumentId', type: 'int', isPrimaryKey: true },
{ name: 'description', type: 'string' },
{ name: 'typeOfAccountingDocumentId', type: 'int', defaultVaule: null, convert: nullHandler },
{ name: 'typeOfObjectId', type: 'int', defaultVaule: null, convert: nullHandler },
{ name: 'recurDocId', type: 'int', defaultValue: null, convert: nullHandler },
{ name: 'reverseDocId', type: 'int', defaultValue: null, convert: nullHandler },
{ name: 'documentDate', type: 'date', dateFormat: 'c' },
{ name: 'transactionDate', type: 'date', dateFormat: 'c' },
{ name: 'datePosted', type: 'date', dateFormat: 'c' },
{ name: 'originalDocumentId', type: 'int', defaultValue: null, convert: nullHandler },
{ name: 'controlTotal', type: 'float' },
{ name: 'documentReference', type: 'string' },
{ name: 'voucherReference', type: 'string' },
{ name: 'typeOfCurrencyId', type: 'int', defaultVaule: null, convert: nullHandler },
{ name: 'currencyAdjustmentId', type: 'int', defaultVaule: null, convert: nullHandler },
{ name: 'postBatchDescription', type: 'string' },
{ name: 'isPosted', type: "boolean" },
{ name: 'isAutoPosted', type: "boolean" },
{ name: 'isChanged', type: "boolean" },
{ name: 'postedByUserId', type: 'int', defaultValue: null, convert: nullHandler },
{ name: 'bankRecControlId', type: 'int', defaultValue: null, convert: nullHandler },
{ name: 'isSelected', type: 'boolean' },
{ name: 'isActive', type: 'boolean' },
{ name: 'isApproved', type: 'boolean' },
{ name: 'typeOfInactiveStatusId', type: 'int', defaultVaule: null, convert: nullHandler },
{ name: 'isBankRecOmitted', type: 'boolean' },
{ name: 'isictJournal', type: 'boolean' },
{ name: 'ictCompanyId', type: 'int', defaultValue: null, convert: nullHandler },
{ name: 'ictAccountingDocumentId', type: 'int', defaultValue: null, convert: nullHandler },
{ name: 'currencyOverrideRate', type: 'float', defaultValue: null, convert: nullHandler },
{ name: 'functionalCurrencyControlTotal', type: 'float', defaultValue: null, convert: nullHandler },
{ name: 'typeOfCurrencyRateId', type: 'int', defaultVaule: null, convert: nullHandler },
{ name: 'memoLine', type: 'string' },
{ name: 'is13Period', type: 'boolean' },
{ name: 'homeCurrencyAmount', type: 'float', defaultValue: null, convert: nullHandler },
{ name: 'customForexRate', type: 'float', defaultValue: null, convert: nullHandler },
{ name: 'ispoSubmitForApproval', type: 'boolean' },
{ name: 'iscpasTran', type: 'boolean' },
{ name: 'cpasProjCloseId', type: 'int', defaultValue: null, convert: nullHandler },
{ name: 'cpasProjId', type: 'int', defaultValue: null, convert: nullHandler }
]
});







