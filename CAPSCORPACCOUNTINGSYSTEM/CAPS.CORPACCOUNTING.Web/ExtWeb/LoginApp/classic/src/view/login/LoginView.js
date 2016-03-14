
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
        width: '100%'
    },
    items: [
        {
            xtype: 'fieldset',
            title: abp.localization.localize("LogIn"),
            border: false,
            frame:false,
            width: '100%',
            layout: {
                type: 'form',
                pack: 'center'
            },
            items: [
                {
                    xtype: 'textfield',
                    name: 'TenancyName',
                    emptyText: abp.localization.localize("TenancyName"),
                    ui: 'roundedcorner',
                    tabIndex:1
                }, {
                    xtype: 'textfield',
                    name: 'UserNameOrEmailAddress',
                    emptyText: abp.localization.localize("UserNameOrEmail"),
                    allowBlank: false,
                    msgTarget: 'side',
                    ui: 'roundedcorner',
                    tabIndex: 2
                }, {
                    xtype: 'textfield',
                    name: 'Password',
                    inputType:'password',
                    emptyText: abp.localization.localize("Password"),
                    allowBlank: false,
                    msgTarget: 'side',
                    ui: 'roundedcorner',
                    tabIndex: 3
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
                            boxLabel: abp.localization.localize("RememberMe"),
                            style:{'padding-top':'5px'},
                            x: 140,
                            y: 10,
                            tabIndex: 5
                            
                        },
                        {
                            xtype: 'button',
                            text: abp.localization.localize("LogIn").toUpperCase(),
                            ui: 'roundedcorner',
                            scale: 'medium',
                            width:120,
                            x: 0,
                            y: 10,
                            tabIndex:4,
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
