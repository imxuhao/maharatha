Ext.define('Chaching.view.financials.journals.JournalTransactionDetailGridController', {
    extend: 'Chaching.view.common.grid.ChachingTransactionDetailGridController',
    alias: 'controller.financials-journals-journaltransactiondetailgrid',
    onDeleteClicked: function (grid, rowIndex, colIndex) {
        var me = this,
            view = me.getView(),
            detailStore = view.getStore();
        if (view.isInViewMode) return;
        var record = detailStore.getAt(rowIndex);
        if (record) {
            var accountingItemId = record.get('accountingItemId') || record.get('creditAccountingItemId');
            if (accountingItemId > 0 && me.allowDetailRowDelete(record)) {
                record.set('id', accountingItemId);
                var operation = Ext.data.Operation({
                    params: { id: accountingItemId },
                    controller: me,
                    action: 'destroy',
                    records: [record],
                    callback: me.onDetailDeleteOperationCompleteCallBack
                });
                detailStore.erase(operation);
            }
            detailStore.remove(record);
        }
    },
    beforeAccountQuery: function (queryPlan, eOpts) {
        var me = this,
            view = me.getView(),
            editingPlugin = view.getPlugin('editingPlugin'),
            combo = queryPlan.combo,
            comboStore = combo.getStore();
        if (editingPlugin && editingPlugin.activeRecord) {
            var activeRec = editingPlugin.activeRecord,
                jobId = activeRec.get('jobId');
            if (combo.valueField === "creditAccountId")jobId = activeRec.get("creditJobId");
            if (jobId > 0) {
                comboStore.getProxy().setExtraParam('jobId', jobId);
            } else comboStore.getProxy().setExtraParam('jobId', null);
        }
        if (queryPlan.lastQuery === queryPlan.query) {
            queryPlan.cancel = true;
            combo.expand();
        }
        comboStore.combo = combo;
        comboStore.on('load', function () {
            if (this.combo) this.combo.focus();
        });
    }
});
