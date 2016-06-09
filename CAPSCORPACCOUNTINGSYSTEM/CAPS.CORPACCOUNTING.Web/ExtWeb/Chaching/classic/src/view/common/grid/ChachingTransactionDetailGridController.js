Ext.define('Chaching.view.common.grid.ChachingTransactionDetailGridController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-grid-chachingtransactiondetailgrid',
    onNewClicked: function() {
        var me = this, view = me.getView(), detailStore = view.getStore();
        if (view.isInViewMode)return;
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
        if (view.isInViewMode) return;
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

        if (view.isInViewMode) return;

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
                //var amount = parentRecord.get('amount') / multiplyOf,
                //firstValue = (Math.floor(amount * 100) / 100),
                //decimalAmount = (parentRecord.get('amount') - (firstValue * multiplyOf)).toFixed(2);
                //parentRecord.set('amount', +firstValue.toFixed(2));
                parentRecord.set('isSplit', true);
                var lastUsedSplitGroupCls = view.lastUsedSplitGroupCls,
                    splitGroupsCls = Chaching.utilities.ChachingGlobals.splitGroupCls;

                if (!lastUsedSplitGroupCls) {
                    lastUsedSplitGroupCls = splitGroupsCls[0];
                    view.lastUsedSplitGroupCls = lastUsedSplitGroupCls;
                } else {
                    var indexOfLastGroupCls = splitGroupsCls.indexOf(lastUsedSplitGroupCls);
                    if (indexOfLastGroupCls+1 < splitGroupsCls.length) {
                        lastUsedSplitGroupCls = splitGroupsCls[indexOfLastGroupCls+1];
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
                    rec.set('accountingItemOrigId', parentRecord.get('accountingItemId'));
                    rec.set('accountingItemId', 0);
                    rec.set('isSplit', true);
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
            if (selectedRecords&&selectedRecords.length === 0)
                abp.notify.info(app.localize('SplitRecordSelect'), app.localize('ValidationFailed'));
            else if (selectedRecords && selectedRecords.length > 1)
                abp.notify.info(app.localize('SingleSplit'), app.localize('ValidationFailed'));
        }
    },
    onDetailsAmountChange: function(field, newValue, oldValue, eOpts) {
        var value = 0,
            plusMinus = "minus";
        if (!oldValue)value = newValue;
        else if (newValue > oldValue) {
            value = newValue - oldValue;
            plusMinus = "minus";
        }
        else if (newValue < oldValue) {
            value = oldValue - newValue;
            plusMinus = "plus";
        }
        var editor = field.up();
        if (editor) {
            var context = editor.context,
                record = context.record;
            if (record&&record.get('isSplit')) {
                var parentRec = record.get('parentRec');
                if (parentRec) {
                    plusMinus === "minus" ? parentRec.set('amount', parseInt(parentRec.get('amount') - value)) : parentRec.set('amount', parseInt(parentRec.get('amount') + value));
                }
            }
        }
    },
    onDetailsAmountFocus:function(field, e, eOpts) {
        if (field.getValue() === 0) {
            field.value = 0;
            field.setRawValue(null);
            return;
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

        if (view.isInViewMode) return false;
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
