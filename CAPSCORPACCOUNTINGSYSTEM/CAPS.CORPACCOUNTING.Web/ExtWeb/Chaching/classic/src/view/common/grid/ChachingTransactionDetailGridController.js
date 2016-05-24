Ext.define('Chaching.view.common.grid.ChachingTransactionDetailGridController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-grid-chachingtransactiondetailgrid',
    onNewClicked:function() {
        var me = this, view = me.getView(), detailStore = view.getStore();
        if (detailStore) {
            var modelClass = detailStore.getModel(),
                className = modelClass.$className;
            var rec = Ext.create(className, {
                jobId: null,
                accountingItemId:0
            });
            detailStore.insert(0, rec);
        }
    },
    onDeleteClicked: function() {
        var me = this,
            view = me.getView(),
            detailStore = view.getStore(),
            selectedRecords = view.getSelection();
        if (detailStore && selectedRecords && selectedRecords.length > 0) {
            Ext.each(selectedRecords, function(item) {
                var accountingItemId = item.get('accountingItemId');
                if (accountingItemId > 0) {
                    item.set('accountingItemId', -accountingItemId);
                }
                detailStore.remove(item);
            });
        } else {
            abp.notify.info('Please select record(s) to delete.', 'Select Records');
        }
    },
    onRefreshClicked:function() {
        var me = this,
           view = me.getView(),
           multiSearchPlugin = view.getPlugin('gms'),
           gridStore = view.getStore();

        if (multiSearchPlugin) {
            multiSearchPlugin.clearValues(true);
            gridStore.clearFilter();
        } else gridStore.clearFilter();

        gridStore.getSorters().clear();
        gridStore.reload();
    },
    beforeAccountQuery: function (queryPlan, eOpts) {
        var me = this,
            view = me.getView(),
            editingPlugin = view.getPlugin('editingPlugin'),
            combo = queryPlan.combo,
            comboStore = combo.getStore();
        if (editingPlugin&&editingPlugin.activeRecord) {
            var activeRec = editingPlugin.activeRecord,
                jobId = activeRec.get('jobId');
            if (jobId > 0) {
                comboStore.getProxy().setExtraParam('jobId', jobId);
            } else comboStore.getProxy().setExtraParam('jobId', null);
        }
    }

});
