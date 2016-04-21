Ext.define('Chaching.model.financials.accounts.SubAccountsModel', {
    extend: 'Chaching.model.base.BaseModel',
    fields: [
        { name: 'subAccountId', type: 'int', isPrimaryKey: true },
        { name: 'caption', type: 'string' },
        { name: 'description', type: 'string' },
        { name: 'displaySequence', type: 'int' },
        { name: 'subAccountNumber', type: 'string' },
        { name: 'accountingLayoutItemId', type: 'int' },
        { name: 'groupCopyLabel', type: 'string' },
        { name: 'isAccountSpecific', type: 'boolean' },
        { name: 'isMandatory', type: 'boolean' },
        { name: 'isBudgetInclusive', type: 'boolean' },
        { name: 'isCorporateSubAccount', type: 'boolean' },
        { name: 'isProjectSubAccount', type: 'boolean' },
        { name: 'entityId', type: 'int' },
        { name: 'typeOfInactiveStatusId', type: 'auto' },
        { name: 'isApproved', type: 'boolean' },
        { name: 'isActive', type: 'boolean' },
        { name: 'isEnterable', type: 'boolean' },
        { name: 'searchOrder', type: 'int' },
        { name: 'searchNo', type: 'string' },
        { name: 'organizationUnitId', type: 'int' },
        { name: 'typeofSubAccount', type: 'string' },
        { name: 'typeofSubAccountId', type: 'auto' }
    ]
});
