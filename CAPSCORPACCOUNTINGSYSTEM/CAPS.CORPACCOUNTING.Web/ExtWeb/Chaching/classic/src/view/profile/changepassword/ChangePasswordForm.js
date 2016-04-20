
Ext.define('Chaching.view.profile.changepassword.ChangePasswordForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['changepassword.editView'],

    requires: [
        'Chaching.view.profile.changepassword.ChangePasswordFormController'
    ],

    controller: 'changepassword-changepasswordform',

    name: 'ChangePassword',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'vbox',
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'top',
        blankText: app.localize('MandatoryToolTipText')
    },
    //  defaultFocus: 'textfield#tenancyName',

    items: [
        {
            xtype: 'textfield',
            name: 'password',
            itemId: 'password',
            allowBlank: false,
            inputType: 'password',
            fieldLabel: app.localize('ChangePassword').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            reference: "password",
            emptyText: app.localize('ChangePassword'),
            listeners: {
                focuseneter: 'passwordenter',
                focusleave: 'passwordleave'
            }
        },
    {
        xtype: 'label',
        name: 'newpassword',
        text: app.localize('PasswordChangeDontRememberMessage', '').initCap(),
        width: '100%',
        reference: "lablepassword"
    },
    {
        xtype: 'component',
        autoEl: {
            tag: 'a',
            href: abp.appPath + 'Account/ForgotPassword',
            html: app.localize('ClickHere').initCap(),
        }
    },
    {
        xtype: 'textfield',
        name: 'newpassword',
        allowBlank: false,
        inputType: 'password',
        reference: "newpassword",
        fieldLabel: app.localize('NewPassword').initCap(),
        width: '100%',
        ui: 'fieldLabelTop',
        emptyText: app.localize('NewPassword')
    },
    {
        xtype: 'textfield',
        name: 'newpasswordrepeat',
        allowBlank: false,
        inputType: 'password',
        reference: "newpasswordrepeat",
        fieldLabel: app.localize('NewPasswordRepeat').initCap(),
        width: '100%',
        ui: 'fieldLabelTop',
        emptyText: app.localize('NewPasswordRepeat')
    }, ]
});
