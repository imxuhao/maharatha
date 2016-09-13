
Ext.define('LoginApp.view.email.EmailActivation',{
    extend: 'Ext.form.Panel',

    requires: [
        'LoginApp.view.email.EmailActivationController',
        'LoginApp.view.email.EmailActivationModel',
          'Ext.form.FieldSet',
        'Ext.form.field.Base'
    ],

    controller: 'email-emailactivation',
    viewModel: {
        type: 'email-emailactivation'
    },

    xtype: 'emailactivationView',
    width: '100%',
    frame: false,
    border: false,
    baseCls: '',
    url: abp.appPath + 'Account/SendEmailActivationLink?returnUrl=/Home/Home',
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
            title: abp.localization.localize("EmailActivation"),
            border: false,
            frame: false,
            width: '100%',
            layout: {
                type: 'form',
                pack: 'center'
            },
            items: [
                {
                    xtype: 'displayfield',
                    value: abp.localization.localize("SendEmailActivationLink_Information")
                },
                {
                    xtype: 'textfield',
                    name: 'TenancyName',
                    emptyText: abp.localization.localize("TenancyName"),
                    ui: 'roundedcorner'
                }, {
                    xtype: 'textfield',
                    name: 'EmailAddress',
                    emptyText: abp.localization.localize("EmailAddress"),
                    allowBlank: false,
                    msgTarget: 'side',
                    ui: 'roundedcorner'
                },
                {
                    xtype: 'fieldcontainer',
                    width: '100%',
                    layout: {
                        type: 'absolute'
                    },
                    padding: 0,
                    items: [
                         {
                             xtype: 'button',
                             text: abp.localization.localize("Back").toUpperCase(),
                             ui: 'back',
                             scale: 'medium',
                             x: 0,
                             y: 10,
                             listeners: {
                                 click: 'onBackClick'
                             }
                         },
                        {
                            xtype: 'button',
                            text: abp.localization.localize("Submit").toUpperCase(),
                            ui: 'roundedcorner',
                            scale: 'medium',
                            x: 242,
                            y: 10,
                            listeners: {
                                click: 'onSubmitClick'
                            }
                        }
                    ]
                }
            ]
        }
    ]
});
