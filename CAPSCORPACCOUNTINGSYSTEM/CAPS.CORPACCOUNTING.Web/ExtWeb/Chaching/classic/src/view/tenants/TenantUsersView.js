
Ext.define('Chaching.view.tenants.TenantUsersView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.tenantusers', 'widget.tenantusers.createView', 'widget.tenantusers.editView'],
    requires: [
       'Chaching.view.tenants.TenantUsersViewController'
    ],

    controller: 'tenants-tenantusersview',
    height: 600,
    width: 600,
    layout: 'fit',
    title: app.localize("SelectAUser"),
    buttonAlign: 'right',
    autoShow: true,
    tenantId: null,
    defaultButton: 'loginAsThisUser',
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var tenantUserGrid = Ext.create('Ext.grid.Panel', {
            cls: 'chaching-grid',
            height: '100%',
            width: '100%',
            selModel: {
                selType: 'checkboxmodel',
                mode : 'SINGLE'
            },
            plugins: [{
                ptype: 'saki-gms',
                iconColumn: false,
                clearItemIconCls: 'icon-settings',
                pluginId: 'gms',
                height: 32,
                filterOnEnter: false
            }],
            columns: [{
                xtype: 'gridcolumn',
                text: app.localize('Name'),
                dataIndex: 'name',
                sortable: true,
                groupable: true,
                width: '20%',
                flex : 1,
                filterField: {
                    xtype: 'textfield',
                    width: '100%'
                }
            }],
            store: 'tenants.TenantUserListStore',
            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: 'tenants.TenantUserListStore',
                displayInfo: true,
                dock: 'bottom',
                width: '100%',
                tabIndex: -1,
                ui: 'plainBottom'
            }]
        });

        me.items = [tenantUserGrid];
        me.buttons = [{ text: app.localize('LoginAsThisUser').toUpperCase(), reference: 'loginAsThisUser', ui: 'actionButton', iconCls: 'fa fa-user', scale: 'small', handler: 'onLogInThisUserClick' },
                      { text: app.localize('Cancel').toUpperCase(), ui: 'actionButton', iconCls: 'fa fa-close', scale: 'small', handler: 'onTenantUsersCancel' }
        ];

        me.callParent(arguments);
    }
});
