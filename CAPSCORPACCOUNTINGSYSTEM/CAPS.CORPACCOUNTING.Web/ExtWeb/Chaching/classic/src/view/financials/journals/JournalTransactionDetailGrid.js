
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
        groupHeaderText: app.localize('Debits'),
        columnName:'debits',
        childColumnNames: ['job', 'account', 'subAccount1'],
        childColumnWidths:[100,100,100]
    }],
    isGroupedHeader: true,
    moduleColumns:[
    {
        text: app.localize('Credits'),
        name: 'credits',
        columns: [{
            xtype: 'gridcolumn',
            dataIndex: 'creditJob',
            name: 'creditJob',
            text: app.localize('JobDivision'),
            //itemId: 'duplicatejob',
            width: 100,
            hideable: false,
            valueField: 'jobId',///NOTE: Important to update record idField when replicating like excel
            dataLoadClass: 'Chaching.store.utilities.autofill.JobDivisionStore',
            isMandatory:true,
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
            },
            editor: {
                xtype: 'combobox',
                store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                valueField: 'creditJobId',
                displayField: 'creditJob',
                queryMode: 'remote',
                minChars: 2,
                listConfig: Chaching.utilities.ChachingGlobals.comboListConfig
            }
        }, {
            xtype: 'gridcolumn',
            dataIndex: 'creditAccount',
            name: 'creditAccount',
            //itemId: 'duplicateaccount',
            text: app.localize('LineNumber'),
            width: 100,
            hideable: false,
            valueField: 'accountId',
            isMandatory: true,
            dataLoadClass: 'Chaching.store.utilities.autofill.AccountsStore',
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
                valueField: 'creditAccountId',
                displayField: 'creditAccount',
                queryMode: 'remote',
                minChars: 2,
                listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                listeners: {
                    beforequery: 'beforeAccountQuery'
                }
            }
        }, {
            xtype: 'gridcolumn',
            dataIndex: 'creditSubAccount1',
            name: 'creditSubAccount1',
            text: app.localize('SubAccount1'),
            //itemId: 'duplicatesubAccount1',
            width: 100,
            valueField: 'creditSubAccountId1',
            dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
            filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('creditSubAccount1', 'creditSubAccount1'),
            editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('creditSubAccountId1', 'creditSubAccount1')
            ////TODO: add remaining combo once accounting field configuration is done
        }]
    }, {
        xtype: 'gridcolumn',
        text: app.localize('Vendor'),
        dataIndex: 'vendor',
        name: 'vendor',
        width: '10%',
        valueField: 'vendorId',
        dataLoadClass: 'Chaching.store.utilities.autofill.VendorsStore',
        filterField: {
            xtype: 'combobox',
            store: new Chaching.store.utilities.autofill.VendorsStore(),
            valueField: 'vendor',
            displayField: 'vendor',
            queryMode: 'remote',
            minChars: 2,
            useDisplayFieldToSearch: true,
            listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
            emptyText: app.localize('SearchText')
        },editor: {
            xtype: 'combobox',
            store: new Chaching.store.utilities.autofill.VendorsStore(),
            valueField: 'vendorId',
            displayField: 'vendor',
            queryMode: 'remote',
            minChars: 2,
            useDisplayFieldToSearch: true,
            listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
            emptyText: app.localize('SearchText')
        }
    }],
    columnOrder: ['amount', 'debits', 'credits', 'itemMemo', 'vendor', 'accountRef1', 'taxRebate', 'isAsset']
});
