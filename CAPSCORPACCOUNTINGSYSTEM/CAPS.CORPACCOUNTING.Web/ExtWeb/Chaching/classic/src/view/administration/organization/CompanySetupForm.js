Ext.define('Chaching.view.administration.organization.CompanySetupForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.administration.organizationunits.companysetup'],
    requires: [
        'Chaching.view.administration.organization.CompanySetupFormController'
    ],
    controller: 'administration-organization-companysetupform',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.OrganizationUnits'),
        create: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree'),
        edit: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree'),
        destroy: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree')
    },
    //name: 'companysetup',
    openInPopupWindow: false,
    hideDefaultButtons: true,
    //autoScroll: true,
    border: false,
    showFormTitle: false,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CompanySetup").initCap()
    },
    items: [
        {
            xtype: 'hiddenfield',
            name: 'id', //companyId
            value: 0
        },
        {
            xtype: 'hiddenfield',
            name: 'addressId', //companyId
            value: 0
        },
        //{
        //    xtype: 'textfield',
        //    padding: '20 10 0 20',
        //    name: 'companyName',
        //    labelWidth: 120,
        //    allowBlank: false,
        //    fieldLabel: app.localize('CompanyName').initCap(),
        //    width: '95%',
        //    ui: 'fieldLabelTop',
        //    emptyText: app.localize('MandatoryField')
        //},
        {
        xtype: 'fieldset',
        ui: 'transparentFieldSet',
        title: abp.localization.localize("CompanyInformation").initCap(),
        collapsible: true,
        layout: 'column',
        items: [{
            columnWidth: .33,
            padding: '20 10 0 20',
            defaults: {
                labelWidth: 110,
                blankText: app.localize('MandatoryToolTipText')
            },
            items: [{
                xtype: 'textfield',
                name: 'displayName',
                allowBlank: false,
                fieldLabel: app.localize('CompanyName').initCap(),
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('MandatoryField')
            }, {
                xtype: 'textfield',
                name: 'line1',
                allowBlank: false,
                fieldLabel: app.localize('Address1').initCap(),
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('MandatoryField')
            },
            {
                xtype: 'textfield',
                name: 'line2',
               // allowBlank: false,
                fieldLabel: app.localize('Address2').initCap(),
                width: '100%',
                ui: 'fieldLabelTop'
            },
            {
                xtype: 'textfield',
                name: 'line3',
                //allowBlank: false,
                fieldLabel: app.localize('Address3').initCap(),
                width: '100%',
                ui: 'fieldLabelTop'
            }
            //,
            //{
            //    xtype: 'textfield',
            //    name: 'Address4',
            //   // allowBlank: false,
            //    fieldLabel: app.localize('Address4').initCap(),
            //    width: '100%',
            //    ui: 'fieldLabelTop'
            //}
            ]
        },
        {
            columnWidth: .33,
            padding: '20 10 0 20',
            defaults: {
                //labelWidth: 140,
                blankText: app.localize('MandatoryToolTipText')
            },
            items: [ {
                xtype: 'textfield',
                name: 'postalCode',
                fieldLabel: app.localize('PostalCode').initCap(),
                width: '100%',
                ui: 'fieldLabelTop',
                listeners: {
                    specialkey: 'onPostalCodeEnter'
                }
            },
            {
                xtype: 'combobox',
                name: 'city',
                fieldLabel: app.localize('City').initCap(),
                width: '100%',
                ui: 'fieldLabelTop',
                displayField: 'city',
                valueField: 'city',
                emptyText: app.localize('SelectOption'),
                queryMode: 'local'//,
                //store: ''
            },
            {
                xtype: 'combobox',
                name: 'state',
                fieldLabel: app.localize('CompanyState').initCap(),
                width: '100%',
                ui: 'fieldLabelTop',
                displayField: 'state',
                valueField: 'state',
                emptyText: app.localize('SelectOption'),
                queryMode: 'local'//,
                //store: ''
            },
             {
                 xtype: 'combobox',
                 name: 'country',
                 fieldLabel: app.localize('Country').initCap(),
                 width: '100%',
                 ui: 'fieldLabelTop',
                 displayField: 'country',
                 valueField: 'country',
                 emptyText: app.localize('SelectOption'),
                 queryMode: 'local'
             }
            ]
        },
                    {
                        columnWidth: .33,
                        padding: '20 10 0 20',
                        defaults: {
                           // labelWidth: 180,
                            blankText: app.localize('MandatoryToolTipText')
                        },
                        items: [{
                            xtype: 'textfield',
                            name: 'phone1',
                            fieldLabel: app.localize('Telephone').initCap(),
                            width: '100%',
                            ui: 'fieldLabelTop'
                        }, {
                            xtype: 'textfield',
                            name: 'email',
                            fieldLabel: app.localize('Email').initCap(),
                            vtype : 'email',
                            width: '100%',
                            ui: 'fieldLabelTop'
                        }, {
                            xtype: 'textfield',
                            name: 'federalTaxId',
                            fieldLabel: app.localize('FedTaxID').initCap(),
                            width: '100%',
                            ui: 'fieldLabelTop'
                        }, {
                            xtype: 'filefield',
                            name: 'companyLogo',
                            // ui: 'default',
                            // ui: 'fieldLabelTop',
                            labelStyle : "font: 600 13px/17px 'Open Sans', 'Helvetica Neue', helvetica, arial, verdana, sans-serif !important;",
                            fieldLabel: app.localize('CompanyLogo'),
                            clearOnSubmit: false,
                            anchor: '100%',
                            width: '100%',
                            buttonText: 'Select Logo...',
                            listeners: {
                               // change: 'filechange'
                            }
                        }, {
                            xtype: 'label',
                            text: app.localize('CompanyLogo_Change_Info').initCap(),
                            width: '100%'
                        }]
                    }]
        }, {
            xtype: 'fieldset',
            ui: 'transparentFieldSet',
            title: abp.localization.localize("Company1099And1096Form").initCap(),
            collapsible: true,
            layout: 'column',
            items: [{
                columnWidth: .5,
                padding: '20 10 0 20',
                defaults: {
                    labelWidth: 180,
                    blankText: app.localize('MandatoryToolTipText')
                },
                items: [{
                    xtype: 'textfield',
                    name: 'transmitterContactName',
                    fieldLabel: app.localize('TransmitterContactName').initCap(),
                    width: '100%',
                    ui: 'fieldLabelTop'
                },
                {
                    xtype: 'textfield',
                    name: 'transmitterEmailAddress',
                    vtype : 'email',
                    // allowBlank: false,
                    fieldLabel: app.localize('TransmitterEmailAddress').initCap(),
                    width: '100%',
                    ui: 'fieldLabelTop'
                }
                ]
            },
            {
                columnWidth: .5,
                padding: '20 10 0 20',
                defaults: {
                    labelWidth: 180,
                    blankText: app.localize('MandatoryToolTipText')
                },
                items: [ {
                    xtype: 'textfield',
                    name: 'transmitterCode',
                    //allowBlank: false,
                    fieldLabel: app.localize('TransmitterCode').initCap(),
                    width: '100%',
                    ui: 'fieldLabelTop'
                },
                {
                    xtype: 'textfield',
                    name: 'transmitterControlCode',
                    // allowBlank: false,
                    fieldLabel: app.localize('TransmitterControlCode').initCap(),
                    width: '100%',
                    ui: 'fieldLabelTop'
                }]
              }]
        }]
});