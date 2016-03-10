
Ext.define('LoginApp.view.login.LoginView', {
    extend: 'Ext.form.Panel',

    requires: [
        'LoginApp.view.login.LoginViewController',
        'LoginApp.view.login.LoginViewModel',
        'Ext.form.FieldSet',
        'Ext.form.field.Base'
    ],
    xtype: 'loginView',
    controller: 'login-loginview',
    viewModel: {
        type: 'login-loginview'
    },
    width: '100%',
    frame: false,
    border: false,
    baseCls: '',
    url:abp.appPath+'Account/LoginExt?returnUrl=/Home/Home',
    bodyStyle: {
        'background-color': 'transparent'
    },
    defaults: {
        baseCls: '',
        bodyStyle: {
            'background-color': 'transparent'
        },
        //padding: 10,
        width: '100%'
    },
    items: [
        {
            xtype: 'fieldset',
            title: 'Login',
            border: true,
            width: '100%',
            layout: {
                type: 'form',
                pack: 'center'
            },
            items: [
                {
                    xtype: 'textfield',
                    name: 'TenancyName',
                    emptyText: 'Tenancy name'
                }, {
                    xtype: 'textfield',
                    name: 'UserNameOrEmailAddress',
                    emptyText: 'User name or email',
                    allowBlank: false,
                    msgTarget: 'side'
                }, {
                    xtype: 'textfield',
                    name: 'Password',
                    inputType:'password',
                    emptyText: 'Password',
                    allowBlank: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'fieldcontainer',
                    width:'100%',
                    layout: {
                        type: 'absolute'
                    },
                    padding:0,
                    items: [
                        {
                            xtype: 'checkbox',
                            name: 'RememberMe',
                            boxLabel: 'Remember Me',
                            x: 80,
                            y: 10
                            
                        },
                        {
                            xtype: 'button',
                            text: 'LOG IN',
                            x: 0,
                            y: 10,
                            listeners: {
                                click:'onLoginClick'
                            }
                        }
                    ]
                }
            ]
        }
    ]
});
