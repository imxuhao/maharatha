Ext.define('Chaching.view.auditlogs.AuditLogDetailViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.auditlogdetailviewcontroller',
    onAuditLogDetailClose: function () {
        var me = this,
            view = me.getView();
        Ext.destroy(view);
    }

});
