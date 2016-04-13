
Ext.define('Chaching.view.coa.ChartOfAccountView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.coa.createView', 'widget.coa.editView'],
    requires: [
        'Chaching.view.coa.ChartOfAccountViewController',
        'Chaching.view.coa.ChartOfAccountViewModel',
        'Chaching.view.coa.ChartOfAccountForm'
    ],

    controller: 'coa-chartofaccountview',
    viewModel: {
        type: 'coa-chartofaccountview'
    },
    height: 600,
    width: 500,
    layout: 'fit',
    title: app.localize("ChartOfAccount"),
    //defaultFocus: 'tenancyName',
    initComponent: function(config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.coa.ChartOfAccountForm', {
            height: '100%',
            width: '100%',
            name:'Chart Of Account'
        });
        me.items = [form];

        me.callParent(arguments);
    }
});
