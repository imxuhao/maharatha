
Ext.define('Chaching.view.profile.linkedaccounts.LinkedAccountsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['linkedaccounts.createView'],
    requires: [
        'Chaching.view.profile.linkedaccounts.LinkedAccountsFormController'
    ],

    controller: 'linkedaccounts-linkedaccountsform',
    modulePermissions: {
        read: true, //abp.auth.isGranted(''),
        create: true,//abp.auth.isGranted(''),
        edit: true, //abp.auth.isGranted(''),
        destroy: true //abp.auth.isGranted('')
    },
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'vbox',
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' }
    },
    defaultFocus: 'textfield#tenancyName',
    items: [{
        xtype: 'hiddenfield',
        name: 'id',
        value:0
    }, {
        xtype: 'textfield',
        name: 'tenancyName',
        itemId: 'tenancyName',
        tabIndex : 1,
        allowBlank:true,
        fieldLabel: app.localize('TenancyName').initCap(),
        width: '100%',
        ui:'fieldLabelTop',
        emptyText: app.localize('TenancyName')
    }, {
        xtype: 'textfield',
        name: 'usernameOrEmailAddress',
        allowBlank: false,
        tabIndex: 2,
        fieldLabel: app.localize('UserName').initCap(),
        width: '100%',
        ui: 'fieldLabelTop',
        emptyText: app.localize('UserName')
    },
    {
        xtype: 'textfield',
        name: 'password',
        allowBlank: false,
        tabIndex: 3,
        inputType: 'password',
        fieldLabel: app.localize('Password').initCap(),
        width: '100%',
        ui: 'fieldLabelTop',
        emptyText: app.localize('Password')
    }
    ]
    
    
});
