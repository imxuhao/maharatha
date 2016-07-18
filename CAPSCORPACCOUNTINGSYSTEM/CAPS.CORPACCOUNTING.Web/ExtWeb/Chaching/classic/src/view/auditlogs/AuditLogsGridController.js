Ext.define('Chaching.view.auditlogs.AuditLogsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.auditlogs-auditlogsgrid',
    onRefreshClick: function (btn) {
        var me = this,
        auditLogGrid = me.getView(),
        plugin = auditLogGrid.getPlugin('gms'),
        auditLogGridStore = auditLogGrid.getStore();
        if (auditLogGridStore.getFilters().length > 0) {
            plugin.clearValues(true);
            auditLogGridStore.clearFilter();
        } else {
            auditLogGridStore.load();
        }
        
    },
    showAuditLogDetailView: function (grid, rowIndex, colIndex) {
        var rec = grid.getStore().getAt(rowIndex);
        var detailView = Ext.create('Chaching.view.auditlogs.AuditLogDetailView', { autoShow: true });
        detailView.down('dataview').getStore().add(rec);
    }
    
});
