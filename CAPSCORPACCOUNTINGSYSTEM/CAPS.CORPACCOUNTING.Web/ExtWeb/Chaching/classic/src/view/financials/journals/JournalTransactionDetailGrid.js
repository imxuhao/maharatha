
Ext.define('Chaching.view.financials.journals.JournalTransactionDetailGrid',{
    extend: 'Chaching.view.common.grid.ChachingTransactionDetailGrid',
    xtype:'widget.financials.journals.entry.transactionDetails',
    requires: [
        'Chaching.view.financials.journals.JournalTransactionDetailGridController'
    ],

    controller: 'financials-journals-journaltransactiondetailgrid',
    store: 'financials.journals.JournalDetailsStore',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Journals.Entry'),
        create: abp.auth.isGranted('Pages.Financials.Journals.Entry.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Journals.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Journals.Entry.Delete')
    },
    groupedHeaderBaseConfig: [{
        groupHeaderText: app.localize('Credits'),
        columnName:'credits',
        childColumnNames: ['job', 'account', 'subAccount1'],
        childColumnWidths:[100,100,100]
    }],
    isGroupedHeader: true,
    moduleColumns:[
    {
        text: app.localize('Debits'),
        name: 'debits',
        columns: [{
            xtype: 'gridcolumn',
            dataIndex: 'job',///TODO: change to jobName once field is available
            name: 'job',
            text: app.localize('JobDivision'),
            itemId: 'duplicateJob',
            width: 100,
            hideable: false,
            filterField: {
                xtype: 'combobox',
                store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                valueField: 'job',
                displayField: 'job',
                queryMode: 'remote',
                minChars: 2,
                useDisplayFieldToSearch: true,
                listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                emptyText: app.localize('SearchText')
            }, editor: {
                xtype: 'combobox',
                store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                valueField: 'jobId',
                displayField: 'job',
                queryMode: 'remote',
                minChars: 2,
                listConfig: Chaching.utilities.ChachingGlobals.comboListConfig
            }
        }, {
            xtype: 'gridcolumn',
            dataIndex: 'account',
            name: 'account',
            itemId: 'duplicateAccount',
            text: app.localize('LineNumber'),
            width: 100,
            hideable: false,
            filterField: {
                xtype: 'combobox',
                store: new Chaching.store.utilities.autofill.AccountsStore(),
                valueField: 'account',
                displayField: 'account',
                queryMode: 'remote',
                minChars: 2,
                useDisplayFieldToSearch:true,
                listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                emptyText: app.localize('SearchText')
            }, editor: {
                xtype: 'combobox',
                store: new Chaching.store.utilities.autofill.AccountsStore(),
                valueField: 'accountId',
                displayField: 'account',
                queryMode: 'remote',
                minChars: 2,
                listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                listeners: {
                    beforequery: 'beforeAccountQuery'
                }
            }
        }, {
            xtype: 'gridcolumn',
            dataIndex: 'subAccount1',///TODO: change to combo
            name: 'subAccount1',
            text: app.localize('SubAccount1'),
            itemId: 'duplicateSubAccount1',
            width: 100,
            filterField: {
                xtype: 'combobox',
                store: new Chaching.store.utilities.autofill.SubAccountsStore(),
                valueField: 'subAccount1',
                displayField: 'subAccount1',
                queryMode: 'remote',
                minChars: 2,
                useDisplayFieldToSearch: true,
                listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                emptyText: app.localize('SearchText')
            }, editor: {
                xtype: 'combobox',
                store: new Chaching.store.utilities.autofill.SubAccountsStore(),
                valueField: 'subAccountId1',
                displayField: 'subAccount1',
                queryMode: 'remote',
                minChars: 2,
                listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                listeners: {
                    beforequery: 'beforeAccountQuery'
                }
            }
        }]
    }, {
        xtype: 'gridcolumn',
        text: app.localize('Vendor'),
        dataIndex: 'vendorId',
        name: 'vendorId',//TODO: Change to combo
        width: '10%',
        filterField: {
            xtype: 'textfield',
            emptyText: app.localize('SearchText')
        },editor: {
            xtype:'textfield'
        }
    }],
    columnOrder: ['amount', 'debits', 'credits', 'itemMemo', 'vendorId', 'accountRef1','taxRebateId','isAsset']
});
