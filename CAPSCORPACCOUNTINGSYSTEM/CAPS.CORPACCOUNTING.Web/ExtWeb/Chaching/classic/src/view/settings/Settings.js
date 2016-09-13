Ext.define('Chaching.view.settings.Settings',
{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: 'widget.host.settings',
    requires: [
        'Chaching.view.settings.SettingsController'
    ],

    controller: 'settings-settingsController',
    name: 'Administration.Host.Settings',
    hideDefaultButtons: false,
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.Host.Settings'),
        create: abp.auth.isGranted('Pages.Administration.Host.Settings'),
        edit: abp.auth.isGranted('Pages.Administration.Host.Settings'),
        destroy: abp.auth.isGranted('Pages.Administration.Host.Settings')
    },
    defaultFocus: 'textfield#localAddress',
    items: [
        {
            xtype: 'tabpanel',
            ui: 'formTabPanels',
            itemId: 'settingsView',
            store: 'settings.SettingsStore',
            items: [
                {
                    xtype: 'container',
                    title: abp.localization.localize('General'),
                    itemId: 'generalView',
                    padding: '20 0 20 20',
                    iconCls: 'fa fa-cogs',
                    layout: {
                        type: 'vbox'
                    },
                    items: [
                        {
                            xtype: 'textfield',
                            itemId: 'localAddress',
                            fieldLabel: abp.localization.localize('ThisWebSiteRootAddress'),
                            width: '80%',
                           
                            labelAlign: 'left',
                            labelWidth: 150,
                            ui: 'fieldLabelTop',
                            style: {
                                color: '#9eacb4',
                                right: '50px'
                            }
                        },
                        {
                            xtype: 'label',
                            text: abp.localization.localize('ThisWebSiteRootAddress_Hint'),
                            cls: 'helpText'
                        },
                        {
                            xtype: 'combobox',
                            itemId: 'timezone',
                            allowBlank: false,
                            fieldLabel: app.localize('Timezone'),
                            valueField: 'value',
                            displayField: 'name',
                            queryMode: 'local',
                            width: '80%',
                            ui: 'fieldLabelTop',
                            reference: 'timezone',
                            labelWidth: 70,
                            store: 'Chaching.store.TimezoneStore',
                            editable: false,
                            autoLoadOnValue: true,
                            style: {
                                top: '180px'
                            },
                            listeners: {
                                'boxready': 'loadTimezoneData'
                            }
                        },
                        {
                            xtype: 'checkbox',
                            itemId: 'auditSaveToDB',
                            labelAlign: 'right',
                            inputValue: true,
                            uncheckedValue: false,
                            width: '100%',
                            ui: 'default',
                            boxLabelCls: 'checkboxLabel',
                            boxLabel: abp.localization.localize('EnableModificationsToDB')
                        },
                        {
                            xtype: 'checkbox',
                            itemId: 'useRedisCache',
                            labelAlign: 'right',
                            inputValue: true,
                            checked: true,
                            width: '100%',
                            ui: 'default',
                            boxLabelCls: 'checkboxLabel',
                            boxLabel: abp.localization.localize('UseRedisCache')
                        }
                    ]

                },
                {
                    xtype: 'panel',
                    title: abp.localization.localize('TenantManagement'),
                    itemId: 'tenantManagementView',
                    padding: '20 0 20 20',
                    iconCls: 'fa fa-male',
                    layout: {
                        type: 'vbox'
                    },

                    items: [
                        {
                            xtype: 'label',
                            text: abp.localization.localize('FormBasedRegistration'),
                            ui: 'fieldLabelTop',
                            flex: 1,
                            style: {
                                color: '#9eacb4'
                            }
                        },
                        {
                            xtype: 'checkbox',
                            name: 'allowTenantsToRegisterToSystem',
                            itemId: 'allowTenantsToRegisterToSystem',
                            labelAlign: 'right',
                            inputValue: false,
                            checked: false,
                            width: '100%',
                            ui: 'default',
                            readOnly: true,
                            boxLabelCls: 'checkboxLabel',
                            boxLabel: app.localize('AllowTenantsToRegisterThemselves'),
                            listeners: {
                                'beforerender': 'addTenantByAdmin'
                            }

                        },
                        {
                            xtype: 'label',
                            text: abp.localization.localize('AllowTenantsToRegisterThemselves_Hint'),
                            cls: 'helpText'

                        },
                        {
                            xtype: 'checkbox',
                            itemId: 'newRegisterTenantsAreActiveByDefault',
                            reference: 'newRegisterTenantsAreActiveByDefault',
                            labelAlign: 'right',
                            inputValue: true,
                            uncheckedValue: false,
                            width: '100%',
                            ui: 'default',
                            boxLabelCls: 'checkboxLabel',
                            boxLabel: app.localize('NewRegisteredTenantsIsActiveByDefault')

                        },
                        {
                            xtype: 'label',
                            itemId: 'newRegisteredTenantHint',
                            reference: 'newRegisteredTenantHint',
                            text: abp.localization.localize('NewRegisteredTenantsIsActiveByDefault_Hint'),
                            cls: 'helpText'

                        },
                        {
                            xtype: 'checkbox',
                            itemId: 'useCaptchaOnRegistration',
                            reference: 'useCaptchaOnRegistration',
                            labelAlign: 'right',
                            inputValue: true,
                            checked: true,
                            width: '100%',
                            ui: 'default',
                            boxLabelCls: 'checkboxLabel',
                            boxLabel: app.localize('UseCaptchaOnRegistration')

                        },
                        {
                            xtype: 'combobox',
                            itemId: 'editions',
                            fieldLabel: app.localize('Editions'),
                            width: '80%',
                            ui: 'fieldLabelTop',
                            emptyText: app.localize('TEdition'),
                            displayField: 'editionDisplayName',
                            valueField: 'editionId',
                            queryMode: 'local',
                            labelWidth: 60,
                            bind: {
                                store: '{editionsForComboBox}'
                            },
                            maxWidth: 300
                        }
                    ]
                },
                {
                    xtype: 'panel',
                    title: abp.localization.localize('UserManagement'),
                    itemId: 'userManagementView',
                    padding: '20 0 20 20',
                    iconCls: 'fa fa-users',
                    items:
                    [
                        {
                            xtype: 'checkbox',
                            itemId: 'emailConfig',
                            labelAlign: 'right',
                            inputValue: true,
                            checked: false,
                            width: '100%',
                            ui: 'default',
                            boxLabelCls: 'checkboxLabel',
                            boxLabel: app.localize('EmailConfirmationRequiredForLogin')
                        }
                    ]
                },
                {
                    xtype: 'panel',
                    title: abp.localization.localize('EmailSmtp'),
                    itemId: 'emailSMTPView',
                    iconCls: 'fa fa-envelope',
                    padding: '20 0 20 20',
                    items: [
                        {
                            xtype: 'textfield',
                            itemId: 'defaultFromAddress',
                            labelAlign: 'left',
                            labelWidth: 230,
                            fieldLabel: abp.localization.localize('DefaultFromAddress'),
                            width: '80%',
                            ui: 'fieldLabelTop',
                            style: {
                                color: '#9eacb4'
                            }
                        },
                        {
                            xtype: 'textfield',
                            itemId: 'defaultFromDisplayName',
                            fieldLabel: abp.localization.localize('DefaultFromDisplayName'),
                            labelAlign: 'left',
                            labelWidth: 230,
                            width: '80%',
                            ui: 'fieldLabelTop',
                            style: {
                                color: '#9eacb4'
                            }
                        },
                        {
                            xtype: 'textfield',
                            itemId: 'smtpHost',
                            fieldLabel: abp.localization.localize('SmtpHost'),
                            labelAlign: 'left',
                            labelWidth: 75,
                            width: '80%',
                            ui: 'fieldLabelTop',
                            style: {
                                color: '#9eacb4'
                            }
                        },
                        {
                            xtype: 'textfield',
                            itemId: 'smtpPort',
                            fieldLabel: abp.localization.localize('SmtpPort'),
                            labelAlign: 'left',
                            labelWidth: 75,
                            width: '80%',
                            ui: 'fieldLabelTop',
                            style: {
                                color: '#9eacb4'
                            }
                        },
                        {
                            xtype: 'checkbox',
                            itemId: 'useSsl',
                            labelAlign: 'right',
                            inputValue: true,
                            checked: false,
                            width: '100%',
                            ui: 'default',
                            boxLabelCls: 'checkboxLabel',
                            boxLabel: app.localize('UseSsl')
                        },
                        {
                            xtype: 'checkbox',
                            name: 'useDefaultCredentials',
                            itemId: 'isDefaultCredentials',
                            labelAlign: 'right',
                            inputValue: true,
                            checked: true,
                            width: '100%',
                            ui: 'default',
                            boxLabelCls: 'checkboxLabel',
                            boxLabel: app.localize('UseDefaultCredentials'),
                            listeners: {
                                'change': 'loadCredentials'
                            }
                        },
                        {
                            xtype: 'textfield',
                            itemId: 'domainName',
                            reference: 'domainName',
                            fieldLabel: abp.localization.localize('DomainName'),
                            labelAlign: 'left',
                            labelWidth: 150,
                            width: '80%',
                            hidden: true,
                            ui: 'fieldLabelTop'
                        },
                        {
                            xtype: 'textfield',
                            itemId: 'userName',
                            reference: 'userName',
                            fieldLabel: abp.localization.localize('UserName'),
                            labelAlign: 'left',
                            labelWidth: 150,
                            hidden: true,
                            width: '80%',
                            ui: 'fieldLabelTop'
                        },
                        {
                            xtype: 'textfield',
                            itemId: 'password',
                            reference: 'password',
                            inputType: 'password',
                            fieldLabel: abp.localization.localize('Password'),
                            labelAlign: 'left',
                            labelWidth: 150,
                            hidden: true,
                            width: '80%',
                            ui: 'fieldLabelTop'
                        },
                        {
                            xtype: 'textfield',
                            itemId: 'testEmailSettings',
                            fieldLabel: abp.localization.localize('TestEmailSettingsHeader'),
                            labelAlign: 'left',
                            labelWidth: 130,
                            value: abp.localization.localize('TestEmailSettings'),
                            width: '80%',
                            ui: 'fieldLabelTop',
                            style: {
                                color: '#9eacb4'
                            }
                        },
                        {
                            xtype: 'button',
                            scale: 'small',
                            ui: 'actionButton',
                            text: abp.localization.localize("SendTestEmail").toUpperCase(),
                            iconAlign: 'left',
                            listeners: {
                                'click': 'sendTestEmail'
                            }
                        }
                    ]
                }
            ]
        }
    ],

    listeners: {
        'beforerender': 'loadAllSettings'
    }

});