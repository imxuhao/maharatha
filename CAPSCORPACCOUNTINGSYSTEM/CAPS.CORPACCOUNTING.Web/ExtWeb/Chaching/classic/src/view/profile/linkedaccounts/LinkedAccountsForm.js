
Ext.define('Chaching.view.profile.linkedaccounts.LinkedAccountsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['linkedaccounts.createView'],
    requires: [
        'Chaching.view.profile.linkedaccounts.LinkedAccountsFormController'
    ],

    controller: 'linkedaccounts-linkedaccountsform',
   
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'vbox',
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'top'
    },
    defaultFocus: 'textfield#usernameOrEmailAddress',
 
    items: [{
        xtype: 'hiddenfield',
        name: 'id',
        value:0
    }, {
        xtype: 'textfield',
        name: 'tenancyName',
        itemId: 'tenancyName',
        allowBlank:true,
        fieldLabel: app.localize('TenancyName').initCap(),
        width: '100%',
        ui:'fieldLabelTop',
        emptyText: app.localize('TenancyName')
    }, {
        xtype: 'textfield',
        name: 'usernameOrEmailAddress',
        allowBlank: false,
        fieldLabel: app.localize('UserName').initCap(),
        width: '100%',
        ui: 'fieldLabelTop',
        emptyText: app.localize('UserName')
    },
    {
        xtype: 'textfield',
        name: 'password',
        allowBlank: false,
        inputType: 'password',
        fieldLabel: app.localize('Password').initCap(),
        width: '100%',
        ui: 'fieldLabelTop',
        emptyText: app.localize('Password')
    },
    ]
    
    
});
