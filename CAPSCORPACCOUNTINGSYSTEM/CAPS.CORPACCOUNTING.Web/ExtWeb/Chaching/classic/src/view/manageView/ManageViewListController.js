Ext.define('Chaching.view.manageView.ManageViewListController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.manageview-manageviewlist',
    doBeforeInlineAddUpdate: function(record) {
        var me = this,
            view = me.getView(),
            parentGrid = view.up('window').parentGrid;
        var gridStore = parentGrid.getStore(),
            isGrouped = gridStore.isGrouped(),
            groupField = gridStore.getGroupField(),
            groupDir = gridStore.getGroupDir(),
            idPropertyField = view.getStore().idPropertyField;

        if (record.get(idPropertyField) > 0 && !record.get('isActive')) {
            return true;
        }
        if (parentGrid) {
            var parentGridColumns = parentGrid.getColumns();
            if (parentGridColumns && parentGridColumns.length > 0) {
                var userViewSetting = {};
                var viewColumns = [];
                for (var i = 0; i < parentGridColumns.length; i++) {
                    var gridCol = parentGridColumns[i];
                    if (gridCol.name !== "ActionColumn" && gridCol.xtype !== "actioncolumn") {
                        var column = {
                            hidden: gridCol.hidden,
                            width: gridCol.width,
                            dataIndex: gridCol.dataIndex
                        };
                        viewColumns.push(column);
                    }
                }
                userViewSetting.column = viewColumns;
                userViewSetting.groupInfo = {
                    isGrouped: isGrouped,
                    groupField: groupField,
                    groupDir: groupDir
                };
                if (record.get(idPropertyField) > 0) {
                    record.set('viewSettings', Ext.encode(userViewSetting));
                } else {
                    record.set('userId', Chaching.utilities.ChachingGlobals.loggedInUserInfo.userId);
                    record.set('viewId', parentGrid.gridId);
                    record.set('id', 0);
                    record.set('viewSettings', Ext.encode(userViewSetting));
                }
            }
            return true;
        }
        return false;
    },
    onBeforeRowCellClick: function (view, td, cellIndex, record, tr, rowIndex, e, eOpts) {
        record.set('ColumnIndex', cellIndex);
    },
    onBeforeApplySelectedView: function(selectionModel, record, index, eOpts) {
        var allowSelect = false;
        //allow selection only when checkbox is clicked and filter fields has values
        if (record.get('ColumnIndex') === 4) {
            allowSelect = true;
        }
        return allowSelect;
    },
    onApplySelectedView: function (selectionModel, record, index, eOpts) {
        var me = this,
            ownerCt = me.view.ownerCt,
            targetGrid = ownerCt.parentGrid,
            activeUserViewId = targetGrid.activeUserViewId,
            myStore = selectionModel.getStore();
        var existingActiveRec = myStore.findRecord('userViewId', activeUserViewId);
        Ext.Msg.confirm({
            title: 'Apply View',
            message: 'Do you really want to apply selected view setting?',
            buttons: Ext.Msg.YESNO,
            icon: Ext.Msg.QUESTION,
            fn: function (btn) {
                switch (btn) {
                    case "yes":
                        //set current view
                        if (existingActiveRec) {
                            existingActiveRec.set('isActive', false);
                            existingActiveRec.commit();
                        }
                        record.set('isActive', true);
                        record.commit();
                        if (targetGrid) {
                            var cols = targetGrid.getColumns();
                            var settingsToApply = [];
                            var rec = {
                                gridId: record.get('viewId'),
                                userViewId: record.get('userViewId'),
                                viewSettingName: record.get('viewName'),
                                viewSettings: record.get('viewSettings'),
                                isDefault: record.get('isDefault')
                            }
                            settingsToApply.push(rec);
                            targetGrid.activeUserViewId = record.get('userViewId');
                            targetGrid.applyGridViewSetting(cols, true, settingsToApply);
                        }
                        Ext.destroy(ownerCt);
                        break;
                    default:
                        selectionModel.deselectAll();
                        break;

                }
            }
        });
    }

});
