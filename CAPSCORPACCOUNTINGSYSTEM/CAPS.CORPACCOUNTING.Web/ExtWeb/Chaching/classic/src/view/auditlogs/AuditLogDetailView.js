
Ext.define('Chaching.view.auditlogs.AuditLogDetailView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.auditlogdetailview'],
    requires: [
        'Chaching.view.auditlogs.AuditLogDetailViewController',
        'Chaching.view.auditlogs.AuditLogDetail'
    ],

    controller: 'auditlogdetailviewcontroller',
    height: '98%',
    width: '45%',
    layout: 'fit',
    title: app.localize("AuditLogDetail"),
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.auditlogs.AuditLogDetail', {
            height: '100%',
            width: '100%'
        });
        me.items = [form];
        me.buttons = [
        {
            xtype: 'button',
            scale: 'small',
            iconCls: 'fa fa-close',
            iconAlign: 'left',
            text: app.localize('Close').toUpperCase(),
            ui: 'actionButton',
            name: 'Cancel',
            itemId: 'btnCancel',
            reference: 'btnCancel',
            handler: 'onAuditLogDetailClose'
        }];
        me.buttonAlign = 'right';
        me.callParent(arguments);
    }
});
