Ext.define('Chaching.view.auditlogs.AuditLogsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.auditlogs-auditlogsgrid',
    onRefreshClick: function (btn) {
        var me = this,
        auditLogGrid = me.getView(),
        multiSearchPlugin = auditLogGrid.getPlugin('gms'),
        auditLogGridStore = auditLogGrid.getStore();
        auditLogGridStore.currentPage = 1;
        if (auditLogGridStore.getFilters().length > 0) {
            multiSearchPlugin.clearValues(true);
            auditLogGridStore.clearFilter();
        } else {
            auditLogGridStore.loadPage(1);
        }
        auditLogGridStore.getSorters().clear();
        if (auditLogGridStore.remoteSort)
            auditLogGridStore.loadPage(1,{ sortList: null, filters: null });

    },
    showAuditLogDetailView: function (grid, rowIndex, colIndex) {
        var rec = grid.getStore().getAt(rowIndex);
        var detailView = Ext.create('Chaching.view.auditlogs.AuditLogDetailView', { autoShow: true });
        detailView.down('dataview').getStore().add(rec);
    },

    onClearFilterClick: function (btn) {
        var me = this;
            me.clearGridFilters(btn);
    }
    //,
    //auditLogCellClick:function(grid, cell, colIndex, record) {
    //    var detailView = Ext.create('Chaching.view.auditlogs.AuditLogDetailView', { autoShow: true });
    //    detailView.down('dataview').getStore().add(record);
    //}
    
});
