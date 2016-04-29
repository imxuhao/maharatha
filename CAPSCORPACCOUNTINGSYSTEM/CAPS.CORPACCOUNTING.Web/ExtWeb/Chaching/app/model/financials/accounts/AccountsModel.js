﻿Ext.define('Chaching.model.financials.accounts.AccountsModel', {
    extend: 'Chaching.model.base.BaseModel',
    config: {
        searchEntityName: 'Account'
    },
    fields: [
            { name: 'accountId', type: 'int', isPrimaryKey: true },
            { name: 'parentId', type: 'int' },
            { name: 'caption', type: 'string' },
            { name: 'description', type: 'string' },
            { name: 'chartOfAccountId', type: 'int' },
            { name: 'accountNumber', type: 'string' },
            { name: 'isAccountRevalued', type: 'boolean' },
            { name: 'isApproved', type: 'boolean' },
            { name: 'isDescriptionLocked', type: 'boolean' },
            { name: 'isElimination', type: 'boolean' },
            { name: 'isEnterable', type: 'boolean' },
            { name: 'isRollupAccount', type: 'boolean' },
            { name: 'isRollupOverridable', type: 'boolean' },
            { name: 'linkAccountId', type: 'int' },
             { name: 'linkAccount', type: 'string' },
            { name: 'linkJobId', type: 'int' },
            { name: 'rollupAccountId', type: 'int' },
            { name: 'rollupJobId', type: 'int' },
            { name: 'typeOfAccountId', type: 'int' },
            { name: 'typeOfAccount', type: 'string' },
            { name: 'typeofConsolidationId', type: 'auto' },
            { name: 'typeofConsolidation', type: 'string' },
             { name: 'typeOfCurrencyId', type: 'auto' },
            { name: 'typeOfCurrency', type: 'string' },
             { name: 'typeOfCurrencyRateId', type: 'int' },
            { name: 'typeOfCurrencyRate', type: 'string' },
            { name: 'isDocControlled', type: 'boolean' },
            { name: 'isSummaryAccount', type: 'boolean' },
            { name: 'isBalanceSheet', type: 'boolean' },
            { name: 'isUs1120BalanceSheet', type: 'boolean' },
            { name: 'isProfitLoss', type: 'boolean' },
            { name: 'isUs1120IncomeStmt', type: 'boolean' },
            { name: 'isCashFlow', type: 'boolean' },
            { name: 'cashFlowName', type: 'string' },
            { name: 'us1120BalanceSheetName', type: 'string' },
            { name: 'balanceSheetName', type: 'string' },
            { name: 'profitLossName', type: 'string' },
            { name: 'us1120IncomeStmtName', type: 'string' },
            { name: 'isActive', type: 'boolean' },
            { name: 'rollUpAccountCaption', type: 'string' },
             { name: 'RollUpDivision', type: 'string' },
            
    ]
});