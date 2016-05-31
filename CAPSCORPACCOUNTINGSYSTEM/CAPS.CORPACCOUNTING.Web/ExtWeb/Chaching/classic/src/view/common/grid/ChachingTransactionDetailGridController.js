Ext.define('Chaching.view.common.grid.ChachingTransactionDetailGridController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-grid-chachingtransactiondetailgrid',
    onNewClicked: function() {
        var me = this, view = me.getView(), detailStore = view.getStore();
        if (detailStore) {
            var multiplyOfField = view.down('numberfield[itemId=multiplyOf]'), multiplyOf = multiplyOfField.getValue();
            var modelClass = detailStore.getModel(),
                className = modelClass.$className;
            if (multiplyOfField.validate()) {
                Ext.suspendLayouts();
                for (var i = 0; i < multiplyOf; i++) {
                    var rec = Ext.create(className);
                    detailStore.insert(0, rec);
                }
                Ext.resumeLayouts();
            } else {
                abp.notify.info(app.localize('AddNewValidationMessage'), app.localize('ValidationFailed'));
            }
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
            abp.notify.info(app.localize('SelectRecordToDelete'), app.localize('ValidationFailed'));
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
    onSplitClicked: function () {
        var me = this,
            view = me.getView(),
            selectionModel = view.getSelectionModel(),
            selectedRecords = selectionModel.getSelection(),
            gridStore = view.getStore();
        if (selectedRecords && selectedRecords.length === 1) {
            var parentRecord = selectedRecords[0],
                parentIndex = gridStore.indexOf(parentRecord);
            if (parentRecord.get('isSplit')) {
                abp.notify.info(app.localize('AlreadySplit'), app.localize('ValidationFailed'));
                return;
            }
            if (!parentRecord.get('amount')) {
                abp.notify.info(app.localize('EnterAmountToSplit'), app.localize('ValidationFailed'));
                return;
            }
            var multiplyOfField = view.down('numberfield[itemId=multiplyOf]'), multiplyOf = multiplyOfField.getValue();
            var modelClass = gridStore.getModel(),
                className = modelClass.$className;
            if (multiplyOfField.validate()) {
                var amount = parentRecord.get('amount') / multiplyOf,
                firstValue = (Math.floor(amount * 100) / 100),
                decimalAmount = (parentRecord.get('amount') - (firstValue * multiplyOf)).toFixed(2);
                parentRecord.set('amount', +firstValue.toFixed(2));
                parentRecord.set('isSplit', true);
                Ext.suspendLayouts();
                for (var i = 1; i < multiplyOf; i++) {
                    var rec = Ext.create(className);
                    Ext.apply(rec.data, parentRecord.data);
                    rec.set('accountingItemOrigId', parentRecord.get('accountingItemOrigId'));
                    rec.set('accountingItemId', 0);
                    rec.set('isSplit', true);
                    rec.set('creatorUserId', null);
                    rec.set('lastModifierUserId', null);
                    rec.set('creationTime', null);
                    rec.set('lastModificationTime', null);
                    gridStore.insert(parentIndex + 1, rec);
                }
                Ext.resumeLayouts();
                parentRecord.set('amount', (firstValue + +decimalAmount).toFixed(2));
            }
        } else {
            if (selectedRecords&&selectedRecords.length === 0)
                abp.notify.info(app.localize('SplitRecordSelect'), app.localize('ValidationFailed'));
            else if (selectedRecords && selectedRecords.length > 1)
                abp.notify.info(app.localize('SingleSplit'), app.localize('ValidationFailed'));
        }
    },
    beforeAccountQuery: function (queryPlan, eOpts) {
        var me = this,
            view = me.getView(),
            editingPlugin = view.getPlugin('editingPlugin'),
            combo = queryPlan.combo,
            comboStore = combo.getStore();
        if (editingPlugin&&editingPlugin.activeRecord) {
            var activeRec = editingPlugin.activeRecord,
                jobId = activeRec.get(combo.valueField);
            if (jobId > 0) {
                comboStore.getProxy().setExtraParam('jobId', jobId);
            } else comboStore.getProxy().setExtraParam('jobId', null);
        }
    },
    onBeforeGridEdit: function (editor, context, eOpts) {
        var me = this, view = me.getView();
        var moduleSpecificChecks = me.doModuleSpecificBeforeEdit(editor, context, eOpts);
        if (!moduleSpecificChecks) {
            return false;
        }
        var cell = view.getView().getCell(context.record, context.column);
        if (cell) {
            cell.removeCls("x-invalid-cell-value");
            cell.removeCls("x-mandatory-cell-value");
            cell.set({ 'data-errorqtip': '' });
        }
        return true;
    },
    doModuleSpecificBeforeEdit:function(editor, context, eOpts) {
        return true;
    }

});
