/**
 * DataModel to represent entity schema for Sub-Accounts.
 */
Ext.define('Chaching.model.financials.accounts.SubAccountsModel', {
    extend: 'Chaching.model.base.BaseModel',
    fields: [
        { name: 'subAccountId', type: 'int', isPrimaryKey: true },
        { name: 'caption', type: 'string' },
        { name: 'displaySequence', type: 'int' },
        { name: 'subAccountNumber', type: 'string', headerText: 'SubAccountNumber', hidden: false, width: '10%', minWidth:110 },
        { name: 'description', type: 'string', headerText: 'Description', hidden: false, width: '10%', minWidth: 110 },
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
        { name: 'typeofSubAccount', type: 'string' },
        { name: 'typeofSubAccountId', type: 'int', defaultValue: null, convert: nullHandler },


         {
             name: 'subAccountNumber1', type: 'string', mapping: 'subAccountNumber'
         }, {
             name: 'subAccountId1', type: 'int', mapping: 'subAccountId'
         }, {
             name: 'subAccountNumber2', type: 'string', mapping: 'subAccountNumber'
         }, {
             name: 'subAccountId2', type: 'int', mapping: 'subAccountId'
         }, {
             name: 'subAccountNumber3', type: 'string', mapping: 'subAccountNumber'
         }, {
             name: 'subAccountId3', type: 'int', mapping: 'subAccountId'
         }, {
             name: 'subAccountNumber4', type: 'string', mapping: 'subAccountNumber'
         }, {
             name: 'subAccountId4', type: 'int', mapping: 'subAccountId'
         }, {
             name: 'subAccountNumber5', type: 'string', mapping: 'subAccountNumber'
         }, {
             name: 'subAccountId5', type: 'int', mapping: 'subAccountId'
         }, {
             name: 'subAccountNumber6', type: 'string', mapping: 'subAccountNumber'
         }, {
             name: 'subAccountId6', type: 'int', mapping: 'subAccountId'
         }, {
             name: 'subAccountNumber7', type: 'string', mapping: 'subAccountNumber'
         }, {
             name: 'subAccountId7', type: 'int', mapping: 'subAccountId'
         }, {
             name: 'subAccountNumber8', type: 'string', mapping: 'subAccountNumber'
         }, {
             name: 'subAccountId8', type: 'int', mapping: 'subAccountId'
         }, {
             name: 'subAccountNumber9', type: 'string', mapping: 'subAccountNumber'
         }, {
             name: 'subAccountId9', type: 'int', mapping: 'subAccountId'
         }, {
             name: 'subAccountNumber10', type: 'string', mapping: 'subAccountNumber'
         }, {
             name: 'subAccountId10', type: 'int', mapping: 'subAccountId'
         },
    /////////////
    {
        name: 'creditSubAccountNumber1', type: 'string', mapping: 'subAccountNumber'
    }, {
        name: 'creditSubAccountId1', type: 'int', mapping: 'subAccountId'
    }, {
        name: 'creditSubAccountNumber2', type: 'string', mapping: 'subAccountNumber'
    }, {
        name: 'creditSubAccountId2', type: 'int', mapping: 'subAccountId'
    }, {
        name: 'creditSubAccountNumber3', type: 'string', mapping: 'subAccountNumber'
    }, {
        name: 'creditSubAccountId3', type: 'int', mapping: 'subAccountId'
    }, {
        name: 'creditSubAccountNumber4', type: 'string', mapping: 'subAccountNumber'
    }, {
        name: 'creditSubAccountId4', type: 'int', mapping: 'subAccountId'
    }, {
        name: 'creditSubAccountNumber5', type: 'string', mapping: 'subAccountNumber'
    }, {
        name: 'creditSubAccountId5', type: 'int', mapping: 'subAccountId'
    }, {
        name: 'creditSubAccountNumber6', type: 'string', mapping: 'subAccountNumber'
    }, {
        name: 'creditSubAccountId6', type: 'int', mapping: 'subAccountId'
    }, {
        name: 'creditSubAccountNumber7', type: 'string', mapping: 'subAccountNumber'
    }, {
        name: 'creditSubAccountId7', type: 'int', mapping: 'subAccountId'
    }, {
        name: 'creditSubAccountNumber8', type: 'string', mapping: 'subAccountNumber'
    }, {
        name: 'creditSubAccountId8', type: 'int', mapping: 'subAccountId'
    }, {
        name: 'creditSubAccountNumber9', type: 'string', mapping: 'subAccountNumber'
    }, {
        name: 'creditSubAccountId9', type: 'int', mapping: 'subAccountId'
    }, {
        name: 'creditSubAccountNumber10', type: 'string', mapping: 'subAccountNumber'
    }, {
        name: 'creditSubAccountId10', type: 'int', mapping: 'subAccountId'
    }

    ]
});
