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
    onSplitClicked: function (field) {
        var me = this,
           view = me.getView(),
           gridStore = view.getStore();
        var isInlineSplit = false,
            editor = undefined,
            cellRecordIndex = undefined;
        if (field.xtype === "numberfield") {
            editor = field.up();
            if (editor && editor.context) {
                cellRecordIndex = editor.context.record;
                isInlineSplit = true;
            }
        }

        var multiplyOfField = view.down('numberfield[itemId=multiplyOf]'), multiplyOf = multiplyOfField.getValue();
        if (isInlineSplit && cellRecordIndex) {
            view.getSelectionModel().select(cellRecordIndex);
            multiplyOfField = field;
            multiplyOf = multiplyOfField.getValue();
            if (editor && editor.editingPlugin) {
                var cellEditing = editor.editingPlugin;
                cellEditing.completeEdit();
            }
        }
        var selectionModel = view.getSelectionModel(),
            selectedRecords = selectionModel.getSelection();
        if (view.isInViewMode) return;

        if (selectedRecords && selectedRecords.length === 1) {
            var parentRecord = selectedRecords[0],
                parentIndex = gridStore.indexOf(parentRecord);
            if (parentRecord.get('isAccountingItemSplit')) {
                abp.notify.info(app.localize('AlreadySplit'), app.localize('ValidationFailed'));
                return;
            }
            if (!parentRecord.get('amount')) {
                abp.notify.info(app.localize('EnterAmountToSplit'), app.localize('ValidationFailed'));
                return;
            }

            var modelClass = gridStore.getModel(),
                className = modelClass.$className;
            if (multiplyOfField.validate()) {
                //var amount = parentRecord.get('amount') / multiplyOf,
                //firstValue = (Math.floor(amount * 100) / 100),
                //decimalAmount = (parentRecord.get('amount') - (firstValue * multiplyOf)).toFixed(2);
                //parentRecord.set('amount', +firstValue.toFixed(2));
                parentRecord.set('isAccountingItemSplit', true);
                var lastUsedSplitGroupCls = view.lastUsedSplitGroupCls,
                    splitGroupsCls = Chaching.utilities.ChachingGlobals.splitGroupCls;

                if (!lastUsedSplitGroupCls) {
                    lastUsedSplitGroupCls = splitGroupsCls[0];
                    view.lastUsedSplitGroupCls = lastUsedSplitGroupCls;
                } else {
                    var indexOfLastGroupCls = splitGroupsCls.indexOf(lastUsedSplitGroupCls);
                    if (indexOfLastGroupCls + 1 < splitGroupsCls.length) {
                        lastUsedSplitGroupCls = splitGroupsCls[indexOfLastGroupCls + 1];
                        view.lastUsedSplitGroupCls = lastUsedSplitGroupCls;
                    } else {
                        lastUsedSplitGroupCls = splitGroupsCls[0];
                        view.lastUsedSplitGroupCls = lastUsedSplitGroupCls;
                    }
                }
                parentRecord.set('SplitGroupCls', lastUsedSplitGroupCls);
                Ext.suspendLayouts();
                for (var i = 1; i < multiplyOf; i++) {
                    var rec = Ext.create(className);
                    Ext.apply(rec.data, parentRecord.data);
                    parentRecord.get('accountingItemId') === 0 ? rec.set('SplitAccountingItemId', parentRecord.get('creditAccountingItemId')) : rec.set('SplitAccountingItemId', parentRecord.get('accountingItemId'));
                    rec.set('accountingItemId', 0);
                    rec.set('creditAccountingItemId', 0);
                    rec.set('debitAccountingItemId', null);
                    rec.set('isAccountingItemSplit', true);
                    rec.set('amount', 0);
                    rec.set('creatorUserId', null);
                    rec.set('lastModifierUserId', null);
                    rec.set('creationTime', null);
                    rec.set('lastModificationTime', null);
                    rec.set('parentRec', parentRecord);
                    gridStore.insert(parentIndex + 1, rec);
                }
                Ext.resumeLayouts();
                //parentRecord.set('amount', (firstValue + +decimalAmount).toFixed(2));
            }
        } else {
            if (selectedRecords && selectedRecords.length === 0)
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
