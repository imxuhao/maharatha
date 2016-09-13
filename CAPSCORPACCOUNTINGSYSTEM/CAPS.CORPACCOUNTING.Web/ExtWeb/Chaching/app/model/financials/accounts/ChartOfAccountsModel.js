/**
 * DataModel to represent entity schema for COA.
 */
Ext.define('Chaching.model.financials.accounts.ChartOfAccountsModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'COA'
    },
    fields: [
            { name: 'coaId', type: 'int', isPrimaryKey: true },
            { name: 'accountId', type: 'int', mapping: 'coaId'  },
            { name: 'caption', type: 'string', headerText: 'Description', hidden: false, width : '8%' },
            { name: 'description', type: 'string', headerText: 'Description', hidden: false, width: '8%' },
            { name: 'isApproved', type: 'boolean' },
            { name: 'isPrivate', type: 'boolean' },
            { name: 'isCorporate', type: 'boolean' },
            { name: 'isNumeric', type: 'boolean' },
            { name: 'standardGroupTotalId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'typeOfChartId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'typeOfChart', type: 'string' },
            { name: 'linkChartOfAccountID', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'linkChartOfAccountName', type: 'string' },
            { name: 'standardGroupTotal', type: 'string' },
            { name: 'rollupAccountId', type: 'int', defaultValue: null, convert: nullHandler },
            { name: 'rollupDivisionId', type: 'int', defaultValue: null, convert: nullHandler },
        { name: 'chartOfAccountId', type: 'int', mapping: 'coaId' }, { name: 'budgetFormatCaption',type:'string',mapping:'caption' }
    ]
});
