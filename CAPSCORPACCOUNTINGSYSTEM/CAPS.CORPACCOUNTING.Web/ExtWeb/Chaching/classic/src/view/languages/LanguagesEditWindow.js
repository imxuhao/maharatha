
Ext.define('Chaching.view.languages.LanguagesEditWindow', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.Languagetexts.editView'],
    requires: [
        'Chaching.view.languages.LanguagesEditWindowController',
        'Chaching.view.languages.LanguagesEditForm'
    ],

    controller: 'languages-languageseditwindow',
    height: 500,
    width: 500,
    layout: 'fit',
    iconCls: 'fa fa-edit',
    title: app.localize("Edit text"),
    //defaultFocus: 'tenancyName',
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.languages.LanguagesEditForm', {
            height: '100%',
            width: '100%',
            name: 'Languages'
        });
        me.items = [form];
        me.callParent(arguments);
    }
});
