Ext.define('Chaching.view.imports.ImportsErrorViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.window-importsView',
    ErrorList:null,
    onFormShow: function (form, eOpts) {
        var me = this, panelStore, panelTpl, panelItems, 
            win= me.getView(),
            formView = win.down('form').getController().getView(),
            grid = formView.down('gridpanel[itemId=errorgridPanelItemId]');
        if (grid) {
            var gridStore = grid.getStore();
            gridStore.loadData(me.ErrorList.AccountsList);
            var gridView = grid.getView(), cell = 0;
            for (var c = 0; c < me.ErrorList.UploadErrorMessagesList.length; c++) {
                var row = '', colndex='';
                row = gridStore.getAt(c);
                for (var ct = 0; ct < me.ErrorList.UploadErrorMessagesList[c].ErrorMessageList.length; ct++) {
                    colndex = me.getColumnIndex(grid.columns, me.ErrorList.UploadErrorMessagesList[c].columnName);
                    cell = gridView.getCell(row, colndex);
                    me.applyCssOnInvalidCells(cell, me.ErrorList.UploadErrorMessagesList[c].errorMessage);
                }
            }
        }
        
    },
    applyCssOnInvalidCells: function (invalidCell, error) {
        //remove first if existing classes has been applied. Needs to remove else multiple times cls-class and tooltip will get added
        invalidCell.removeCls("x-invalid-cell-value");
        invalidCell.removeCls("x-mandatory-cell-value");
        invalidCell.set({ 'data-errorqtip': '' });
        //add again back
        invalidCell.addCls("x-invalid-cell-value");
        invalidCell.set({ 'data-errorqtip': ' ' + error });
    },
    getColumnIndex: function (gridColumns, columnName) {
        var gridDataIndices = Ext.Array.pluck(gridColumns, 'dataIndex');
        return Ext.Array.indexOf(gridDataIndices, columnName);
    }

});