
Ext.define('Chaching.view.administration.companysetup.CompanyLogoView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    requires: [
        'Chaching.view.administration.companysetup.CompanyLogoForm'
    ],
    height: 250,
    width: 400,
    layout: 'fit',
    title: app.localize("CompanyLogo"),
    autoShow : true,
    initComponent: function (config) {
        var me = this;
        var form = Ext.create('Chaching.view.administration.companysetup.CompanyLogoForm', {
            height: '100%',
            width: '100%',
            itemId: 'CompanyLogoItemId',
            name: 'CompanyLogo1'
        });
        me.items = [form];

        me.callParent(arguments);
    }
});
