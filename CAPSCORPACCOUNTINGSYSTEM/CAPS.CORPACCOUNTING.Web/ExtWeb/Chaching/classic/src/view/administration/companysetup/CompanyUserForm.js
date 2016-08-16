Ext.define('Chaching.view.administration.companysetup.CompanyUserForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: [
        'widget.administration.organizationunits.companyusersgrid.create', 'widget.administration.organizationunits.companyusersgrid.edit'
    ],
    requires: [
        'Chaching.view.roles.RolesGrid'
       //'Chaching.view.administration.organization.CompanyFormController',
       //'Chaching.view.administration.organization.CompanySetupForm',
       //'Chaching.view.administration.organization.CompanyPreferencesForm',
       //'Chaching.view.administration.organization.MembersForm'
    ],
   // controller: 'administration-organization-companyform',
   // name: 'companysetup',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.OrganizationUnits'),
        create: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree'),
        edit: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree'),
        destroy: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree')
    },
    openInPopupWindow: false,
    hideDefaultButtons: false,
    autoScroll: false,
    border: false,
    showFormTitle: false,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("UserSettings")
    },
    layout: 'fit',
    items: [{
        xtype: 'tabpanel',
        // ui: 'formTabPanels',
        ui: 'submenuTabs',
        tabPosition: 'left',
        tabRotation: 2,
        items: [{
            title: abp.localization.localize("UserDetailsTab"),
            iconCls: 'fa fa-gear',
            items: [{
                xtype: 'fieldset',
                ui: 'transparentFieldSet',
                title: abp.localization.localize("CompanyUserInformation"),
                collapsible: true,
                layout: 'column',
                items: [{
                    columnWidth: 0.5,
                    padding: '20 10 0 20',
                    defaults: {
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items: [{
                        xtype: 'hiddenfield',
                        name: 'userId',
                        value: 0
                    }, {
                        xtype: 'textfield',
                        name: 'fullName',
                        itemId: 'fullName',
                        allowBlank: false,
                        fieldLabel: app.localize('FullName'),
                        width: '100%',
                        ui: 'fieldLabelTop',
                        emptyText: app.localize('MandatoryField')
                    }, {
                        xtype: 'textfield',
                        name: 'userName',
                        itemId: 'userName',
                        allowBlank: false,
                        fieldLabel: app.localize('Logon'),
                        width: '100%',
                        ui: 'fieldLabelTop',
                        emptyText: app.localize('MandatoryField')
                    }, {
                        xtype: 'checkbox',
                        name: 'isActive',
                        labelAlign: 'right',
                        inputValue: true,
                        ui: 'default',
                        boxLabelCls: 'checkboxLabel',
                        boxLabel: app.localize('Active')
                    }
                    ]
                },
                {
                    columnWidth: 0.5,
                    padding: '20 10 0 20',
                    defaults: {
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items: [{
                        xtype: 'textfield',
                        name: 'password',
                        allowBlank: false,
                        fieldLabel: app.localize('UserPassword'),
                        width: '100%',
                        ui: 'fieldLabelTop',
                        emptyText: app.localize('MandatoryField')
                    }, {
                        xtype: 'textfield',
                        name: 'emailAddress',
                        allowBlank: false,
                        fieldLabel: app.localize('UserEmailAddress'),
                        width: '100%',
                        ui: 'fieldLabelTop',
                        emptyText: app.localize('MandatoryField')
                    }, {
                        xtype: 'checkbox',
                        name: 'isLocked',
                        labelAlign: 'right',
                        inputValue: true,
                        ui: 'default',
                        boxLabelCls: 'checkboxLabel',
                        boxLabel: app.localize('IsLocked')
                    }]
                }]
            }, {
                // columnWidth: 1,
                padding: '20 10 0 20',
                items: [{
                    xtype: 'roles',
                    requireExport: false,
                    requireMultiSearch: false,
                    requireMultisort: false,
                    isEditable: false,
                    columnLines: true,
                    multiColumnSort: false,
                    showPagingToolbar : false,
                   // isEditable: true,
                   // editingMode: 'cell',
                    itemId: 'rolesGridItemId',
                    anchor: '100% 80%',
                    autoScroll: true
                    //,
                    //layout: 'fit',
                    //width: '100%'
                }]
            }]
        }
          ,
           {
               xtype: 'container',
               title: abp.localization.localize("CorporateCOASecurityTab"),
               items: [{
                   html: 'CorporateCOASecurityTab'
               }]
           },
           {
               xtype: 'container',
               title: abp.localization.localize("ProjectCOASecurityTab"),
               items: [{
                   html: 'ProjectCOASecurityTab'
               }]
           }, {
               xtype: 'container',
               title: abp.localization.localize("ProjectSecurityTab"),
               items: [{
                   html: 'ProjectSecurityTab'
               }]
           }, {
               xtype: 'container',
               title: abp.localization.localize("CreditCardSecurityTab"),
               items: [{
                   html: 'CreditCardSecurityTab'
               }]
           },
           {
               xtype: 'container',
               title: abp.localization.localize("BankSecurityTab"),
               items: [{
                   html: 'BankSecurityTab'
               }]
           }
            ]
    }]
});