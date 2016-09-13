
Ext.define('Chaching.view.financials.accounts.AccountsMapCoaWindow', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    //alias: ['widget.financials.financials-accounts-accountsmapcoawindow'],
    requires: [ 'Chaching.view.financials.accounts.AccountsMapCoaWindowController'  ],
    controller: 'financials-accounts-accountsmapcoawindow',
    layout: 'fit',
    height: '90%',
    width: '90%',
    title: '',
    autoShow: true,
    modal: true,
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var grid = Ext.create('Chaching.view.financials.accounts.AccountsMapCoaGrid', {
            height: '100%',
            width: '100%'
        });
        me.items = [grid];
        me.callParent(arguments);
    },
    buttonAlign: 'center',
    buttons: [
        //{
        //    text: abp.localization.localize("Save").toUpperCase(),
        //    scale: 'small',
        //    ui: 'actionButton',
        //    iconCls: 'fa fa-save',
        //    iconAlign: 'left',
        //    handler: 'onMapSaveClicked'
        //},
        {
            text: abp.localization.localize("Close").toUpperCase(),
            scale: 'small',
            ui: 'actionButton',
            iconCls: 'fa fa-close',
            iconAlign: 'left',
            handler: 'onCancelClicked'
        }
    ]

});
