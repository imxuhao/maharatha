
Ext.define('Chaching.view.profile.changepassword.ChangePasswordForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['changepassword.editView'],

    requires: [
        'Chaching.view.profile.changepassword.ChangePasswordFormController'
    ],

    controller: 'changepassword-changepasswordform',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.Users'),
        create: abp.auth.isGranted('Pages.Administration.Users.Create'),
        edit: abp.auth.isGranted('Pages.Administration.Users.Edit'),
        destroy: abp.auth.isGranted('Pages.Administration.Users.Delete')
    },
    name: 'ChangePassword',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    
    layout: 'vbox',
    titleConfig : {
        title : app.localize('ChangePassword')
    },
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        // labelAlign: 'top',
        labelWidth : 150,
        blankText: app.localize('MandatoryToolTipText')
    },
    items: [
        {
            xtype: 'textfield',
            name: 'password',
            itemId: 'password',
            allowBlank: false,
            inputType: 'password',
            fieldLabel: app.localize('CurrentPassword').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            reference: "password",
            maxLength : 6,
            emptyText: app.localize('CurrentPassword'),
            listeners: {
                focusenter: 'passwordenter',
                focusleave: 'passwordleave'
            }
        },
    //{
    //    xtype: 'label',
    //    name: 'newpassword',
    //    text: app.localize('PasswordChangeDontRememberMessage', '').initCap(),
    //    width: '100%',
    //    reference: "lablepassword"
    //},
    {
        xtype: 'component',
        reference: "labelPassword",
        html: '<span class= "helpText">' + app.localize('PasswordChangeDontRememberMessage', '') + '</span>' + '<a class= "forgotPassword" href="' + abp.appPath + 'Account/ForgotPassword' + '"> ' + app.localize('ClickHere').initCap() + '.</a>'
       
    },
    {
        xtype: 'textfield',
        name: 'newpassword',
        allowBlank: false,
        inputType: 'password',
        reference: "newpassword",
        fieldLabel: app.localize('NewPassword').initCap(),
        width: '100%',
        maxLength: 6,
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
        maxLength: 6,
        ui: 'fieldLabelTop',
        emptyText: app.localize('NewPasswordRepeat'),
        /*
        * Custom validator implementation - checks that the value matches what was entered into
        * the password1 field.
        */
        validator: function (value) {
            var password1 = this.previousSibling('[name=newpassword]');
            return (value === password1.getValue()) ? true : 'Passwords do not match.'
        }
    }]
});
