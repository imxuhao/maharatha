
Ext.define('Chaching.view.financials.journals.JournalTransactionDetailGrid',{
    extend: 'Chaching.view.common.grid.ChachingTransactionDetailGrid',
    xtype:'widget.financials.journals.entry.transactionDetails',
    requires: [
        'Chaching.view.financials.journals.JournalTransactionDetailGridController'
    ],

    controller: 'financials-journals-journaltransactiondetailgrid',
    store: 'financials.journals.JournalStore',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Journals.Entry'),
        create: abp.auth.isGranted('Pages.Financials.Journals.Entry.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Journals.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Journals.Entry.Delete')
    },
    groupedHeaderBaseConfig: [{
        groupHeaderText: app.localize('Credits'),
        columnName:'credits',
        childColumnNames: ['jobId', 'accountId', 'subAccountId1'],
        childColumnWidths:[100,80,100]
    }],
    isGroupedHeader:true,
    moduleColumns:[
    {
        text: app.localize('Debits'),
        name: 'debits',
        columns: [{
            xtype: 'gridcolumn',
            dataIndex: 'jobId',///TODO: change to jobName once field is available
            name: 'jobId',
            text: app.localize('JobDivision'),
            itemId: 'duplicateJobId',
            width: 100,
            filterField: {
                xtype: 'textfield',
                emptyText: app.localize('SearchText')
            }, editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            dataIndex: 'accountId',///TODO: change to combo
            name: 'accountId',
            itemId: 'duplicateAccountId',
            text: app.localize('LineNumber'),
            width: 80,
            filterField: {
                xtype: 'textfield',
                emptyText: app.localize('SearchText')
            }, editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            dataIndex: 'subAccountId1',///TODO: change to combo
            name: 'subAccountId1',
            text: app.localize('SubAccount1'),
            itemId: 'duplicateSubAccountId1',
            width: 100,
            filterField: {
                xtype: 'textfield',
                emptyText: app.localize('SearchText')
            }, editor: {
                xtype: 'textfield'
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
