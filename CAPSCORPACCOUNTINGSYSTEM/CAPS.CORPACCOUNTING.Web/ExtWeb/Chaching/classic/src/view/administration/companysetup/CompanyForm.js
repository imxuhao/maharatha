Ext.define('Chaching.view.administration.companysetup.CompanyForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    requires: ['Chaching.view.administration.companysetup.CompanyFormController'],
    //alias: [
    //    'widget.companysetup.create', 'widget.companysetup.edit'
    //],
    requires: [
    ],
    controller: 'administration-companysetup-companyform',
    xtype: 'companysetup',
    name: "Administration.CompanySetUp",
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.CompanySetUp'),
        create: abp.auth.isGranted('Pages.Administration.CompanySetUp.Create'),
        edit: abp.auth.isGranted('Pages.Administration.CompanySetUp.Edit'),
        destroy: abp.auth.isGranted('Pages.Administration.CompanySetUp.Delete')
    },
    openInPopupWindow: false,
    hideDefaultButtons: true,
    autoScroll: false,
    border: false,
    showFormTitle: false,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CreateNewCompanySetup").initCap()
    },
    layout: 'fit',
    items: [{
        xtype: 'tabpanel',
        // ui: 'formTabPanels',
        ui: 'submenuTabs',
        tabPosition: 'left',
        tabRotation: 2,
        items: [
            {
                title: abp.localization.localize("CompanySetup").initCap(),
                itemId: 'companySetupTab',
                xtype: 'form',
                items: [
                     {
                         xtype: 'hiddenfield',
                         itemId: 'companyItemId',
                         name: 'tenantExtendedId', //companyId
                         value: 0
                     },
                     {
                         xtype: 'hiddenfield',
                         name: 'addressId', //companyId
                         value: 0
                     },
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
                                 //allowBlank: false,
                                 fieldLabel: app.localize('CompanyName').initCap(),
                                 width: '100%',
                                 ui: 'fieldLabelTop'//,
                                 //emptyText: app.localize('MandatoryField')
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
                             ]
                         },
                         {
                             columnWidth: .33,
                             padding: '20 10 0 20',
                             defaults: {
                                 //labelWidth: 140,
                                 blankText: app.localize('MandatoryToolTipText')
                             },
                             items: [{
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
                                             vtype: 'email',
                                             width: '100%',
                                             ui: 'fieldLabelTop'
                                         }, {
                                             xtype: 'textfield',
                                             name: 'federalTaxId',
                                             fieldLabel: app.localize('FedTaxID').initCap(),
                                             width: '100%',
                                             ui: 'fieldLabelTop'
                                         },

                                         {
                                             xtype: 'fieldcontainer',
                                             width: '100%',
                                             layout: {
                                                 type: 'hbox'//,
                                                // align : 'stretch'
                                             },
                                             items : [{
                                                 xtype: 'image',
                                                 width: 90,
                                                 height: 60,
                                                 itemId: 'companyLogo',
                                                 flex : 1,
                                                 src: abp.appPath + 'Content/images/capslogo.png'
                                             },
                                             {
                                                xtype: 'button',
                                                itemId: 'buttonPanelId',
                                                ui: 'actionButton',
                                                margin: '10px 0px 0px 20px',
                                                scale: 'small',
                                                text: app.localize('ChangeLogo').toUpperCase(),
                                                actionButton: true,
                                                handler: 'onCompanyLogoClick'
                                               }

                                             //{
                                             //    xtype: 'panel',
                                             //    cls: 'buttonPanelCls',
                                             //    itemId: 'buttonPanel',
                                             //    margin: '20px 0px 0px 20px',
                                             //    items: [{
                                             //        xtype: 'button',
                                             //        itemId: 'buttonPanelId',
                                             //        ui: 'actionButton',
                                             //        scale: 'small',
                                             //        text: app.localize('ChangeLogo').toUpperCase(),
                                             //        actionButton: true,
                                             //        handler: 'onCompanyLogoClick'
                                             //    }]
                                             //}


                                             ]

                                            
                                         }

                                         //{
                                         //    xtype: 'filefield',
                                         //    name: 'companyLogo',
                                         //    // ui: 'default',
                                         //    // ui: 'fieldLabelTop',
                                         //    labelStyle: "font: 600 13px/17px 'Open Sans', 'Helvetica Neue', helvetica, arial, verdana, sans-serif !important;",
                                         //    fieldLabel: app.localize('CompanyLogo'),
                                         //    clearOnSubmit: false,
                                         //    anchor: '100%',
                                         //    width: '100%',
                                         //    buttonText: 'Select Logo...',
                                         //    listeners: {
                                         //        change: 'onFileChange'
                                         //    }
                                         //}, {
                                         //    xtype: 'label',
                                         //    text: app.localize('CompanyLogo_Change_Info').initCap(),
                                         //    width: '100%'
                                         //}


]
                                     }]
                     },
                     {
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
                                 vtype: 'email',
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
                             items: [{
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
                     }],
                dockedItems: [
                           {
                               xtype: 'toolbar',
                               dock: 'bottom',
                               layout: {
                                   type: 'hbox',
                                   pack: 'center'
                               },
                               items: [
                               {
                                   xtype: 'button',
                                   itemId: 'btnSaveSetup',
                                   name: 'Save',
                                   ui: 'actionButton',
                                   text: app.localize('SaveCompanySetup').toUpperCase(),
                                   iconCls: 'fa fa-save',
                                   actionButton: true,
                                   listeners: {
                                       click: 'onSaveClicked'
                                   }
                               },
                               {
                                   xtype: 'button',
                                   itemId: 'btnCancelSetup',
                                   name: 'Cancel',
                                   ui: 'actionButton',
                                   text: app.localize('Cancel').toUpperCase(),
                                   iconCls: 'fa fa-close',
                                   actionButton: true,
                                   listeners: {
                                       click: 'onCancelClicked'
                                   }
                               }]
                           }]
            },
            {
                title: abp.localization.localize("CompanyPreferences").initCap(),
                disabled: false,
                // xtype : 'form',
                itemId: 'companyPreferencesTab',
                items: [{
                    xtype: 'tabpanel',
                    ui: 'formTabPanels',
                    items: [
                        {
                            title: abp.localization.localize("General").initCap(),
                            xtype: 'form',
                            itemId: 'companyPreferencesGeneralTab',
                            padding: '20 10 0 20',
                            items: [
                                {
                                    xtype: 'combobox',
                                    name: 'timezone',
                                    //allowBlank: false,
                                    fieldLabel: app.localize('Timezone').initCap(),
                                    valueField: 'value',
                                    displayField: 'name',
                                    queryMode: 'local',
                                    width: '100%',
                                    // maxWidth : 500,
                                    ui: 'fieldLabelTop',
                                    itemId: "timezone",
                                    reference: 'timezone',
                                    store: Ext.create('Chaching.store.TimezoneStore'),
                                    editable: false
                                }
                            ]
                        },
                    {
                        title: abp.localization.localize("UserManagement").initCap(),
                        itemId: 'companyPreferencesUserManagementTab',
                        xtype: 'form',
                        //layout: 'column',
                        padding: '20 10 0 20',
                        items: [{
                            xtype: 'fieldset',
                            ui: 'transparentFieldSet',
                            title: abp.localization.localize("FormBasedRegistration").initCap(),
                            collapsible: true,
                            items: [{
                                xtype: 'checkbox',
                                name: 'allowSelfRegistration',
                                labelAlign: 'right',
                                inputValue: true,
                                uncheckedValue: false,
                                width: '100%',
                                ui: 'default',
                                boxLabelCls: 'checkboxLabel',
                                reference: 'allowSelfRegistration',
                                boxLabel: app.localize('AllowUsersToRegisterThemselves')
                            },
                          {
                              xtype: 'component',
                              reference: "allowSelfRegistrationLabel",
                              html: '<span class= "helpText">' + app.localize('AllowUsersToRegisterThemselves_Hint') + '</span>'

                          }, {
                              xtype: 'checkbox',
                              name: 'isNewRegisteredUserActiveByDefault',
                              labelAlign: 'right',
                              inputValue: true,
                              uncheckedValue: false,
                              width: '100%',
                              ui: 'default',
                              bind: {
                                  hidden: '{!allowSelfRegistration.checked}'
                              },
                              boxLabelCls: 'checkboxLabel',
                              boxLabel: app.localize('NewRegisteredUsersIsActiveByDefault')
                          },
                          {
                              xtype: 'component',
                              bind: {
                                  hidden: '{!allowSelfRegistration.checked}'
                              },
                              reference: "isNewRegisteredUserActiveByDefaultLabel",
                              html: '<span class= "helpText">' + app.localize('NewRegisteredUsersIsActiveByDefault_Hint') + '</span>'

                          }, {
                              xtype: 'checkbox',
                              name: 'useCaptchaOnRegistration',
                              labelAlign: 'right',
                              inputValue: true,
                              uncheckedValue: false,
                              checked: true,
                              width: '100%',
                              ui: 'default',
                              bind: {
                                  hidden: '{!allowSelfRegistration.checked}'
                              },
                              boxLabelCls: 'checkboxLabel',
                              boxLabel: app.localize('UseCaptchaOnRegistration')
                          }]

                        }, {
                            xtype: 'fieldset',
                            ui: 'transparentFieldSet',
                            title: abp.localization.localize("OtherSettings").initCap(),
                            collapsible: true,
                            items: [{
                                xtype: 'checkbox',
                                name: 'isEmailConfirmationRequiredForLogin',
                                labelAlign: 'right',
                                inputValue: true,
                                uncheckedValue: false,
                                width: '100%',
                                ui: 'default',
                                boxLabelCls: 'checkboxLabel',
                                boxLabel: app.localize('EmailConfirmationRequiredForLogin')
                            }]
                        }]

                    },
                    {
                        title: abp.localization.localize("CompanyPreferences").initCap(),
                        itemId: 'companySettingsTab',
                        xtype: 'form',
                        layout: 'column',
                        items: [{
                            columnWidth: .33,
                            padding: '20 10 0 20',
                            defaults: {
                                //labelWidth: 120//,
                                //blankText: app.localize('MandatoryToolTipText')
                            },
                            items: [{
                                xtype: 'checkbox',
                                name: 'isAllowDuplicateAPInvoiceNos',
                                labelAlign: 'right',
                                inputValue: true,
                                uncheckedValue: false,
                                width: '100%',
                                ui: 'default',
                                boxLabelCls: 'checkboxLabel',
                                boxLabel: app.localize('AllowDuplicateAPInvoices')
                            },
                           {
                               xtype: 'checkbox',
                               name: 'isAllowDuplicateARInvoiceNos',
                               labelAlign: 'right',
                               inputValue: true,
                               uncheckedValue: false,
                               width: '100%',
                               ui: 'default',
                               boxLabelCls: 'checkboxLabel',
                               boxLabel: app.localize('AllowDuplicateARInvoices')

                           },
                            {
                                xtype: 'checkbox',
                                name: 'isAllowAccountnumbersStartingwithZero',
                                labelAlign: 'right',
                                inputValue: true,
                                uncheckedValue: false,
                                width: '100%',
                                ui: 'default',
                                boxLabelCls: 'checkboxLabel',
                                boxLabel: app.localize('AllowAccountNumbersStartingWithZero')

                            }, {
                                xtype: 'checkbox',
                                name: 'isImportPOlogsfromProducersActualUploads',
                                labelAlign: 'right',
                                inputValue: true,
                                uncheckedValue: false,
                                width: '100%',
                                ui: 'default',
                                boxLabelCls: 'checkboxLabel',
                                boxLabel: app.localize('ImportPOLogsFromProducersActualUploads')

                            },
                            {
                                xtype: 'checkbox',
                                name: 'buildAPuponCCstatementPosting',
                                labelAlign: 'right',
                                inputValue: true,
                                uncheckedValue: false,
                                width: '100%',
                                ui: 'default',
                                boxLabelCls: 'checkboxLabel',
                                boxLabel: app.localize('BuildAPUponCCStatementPosting')
                            }, {
                                xtype: 'checkbox',
                                name: 'buildAPuponPayrollPosting',
                                labelAlign: 'right',
                                inputValue: true,
                                uncheckedValue: false,
                                width: '100%',
                                ui: 'default',
                                boxLabelCls: 'checkboxLabel',
                                boxLabel: app.localize('BuildAPUponPayrollPosting')
                            },
                              {
                                  xtype: 'checkbox',
                                  name: 'poAutoNumberingforDivisions',
                                  labelAlign: 'right',
                                  inputValue: true,
                                  uncheckedValue: false,
                                  width: '100%',
                                  ui: 'default',
                                  boxLabelCls: 'checkboxLabel',
                                  boxLabel: app.localize('POAutoNumberingforDivisions')
                              },
                               {
                                   xtype: 'checkbox',
                                   name: 'poAutoNumberingforProjects',
                                   labelAlign: 'right',
                                   inputValue: true,
                                   uncheckedValue: false,
                                   width: '100%',
                                   ui: 'default',
                                   boxLabelCls: 'checkboxLabel',
                                   boxLabel: app.localize('POAutoNumberingforProjects')
                               }
                            ]
                        },
           {
               columnWidth: .33,
               padding: '20 10 0 20',
               defaults: {
                   labelWidth: 160,
                   blankText: app.localize('MandatoryToolTipText')
               },
               items: [{
                   xtype: 'radiogroup',
                   labelStyle: 'padding-top: 8px !important;',
                   fieldLabel: app.localize('ARAgingDate'),
                   width: '100%',
                   ui: 'fieldLabelTop',
                   columns: 1,
                   vertical: true,
                   itemId: 'arAgingDateItemId',
                   items: [{
                       boxLabel: app.localize('AgeByInvoiceDate').initCap(),
                       name: 'arAgingDate',
                       inputValue: '1',
                       ui: 'default',
                       boxLabelCls: 'checkboxLabel',
                       uncheckedValue: 'false'
                   }, {
                       boxLabel: app.localize('AgeByDueDate').initCap(),
                       name: 'arAgingDate',
                       inputValue: '2',
                       ui: 'default',
                       boxLabelCls: 'checkboxLabel',
                       uncheckedValue: 'false'
                   }]
               }, {
                   xtype: 'radiogroup',
                   labelStyle: 'padding-top: 8px !important;',
                   fieldLabel: app.localize('APAgingDate'),
                   width: '100%',
                   ui: 'fieldLabelTop',
                   columns: 1,
                   vertical: true,
                   itemId: 'apAgingDateItemId',
                   items: [{
                       boxLabel: app.localize('AgeByInvoiceDate').initCap(),
                       name: 'apAgingDate',
                       inputValue: '1',
                       ui: 'default',
                       boxLabelCls: 'checkboxLabel',
                       uncheckedValue: 'false'
                   }, {
                       boxLabel: app.localize('AgeByDueDate').initCap(),
                       name: 'apAgingDate',
                       inputValue: '2',
                       ui: 'default',
                       boxLabelCls: 'checkboxLabel',
                       uncheckedValue: 'false'
                   }]
               }, {
                   xtype: 'radiogroup',
                   fieldLabel: app.localize('APPostingDateDefault'),
                   width: '100%',
                   labelStyle: 'padding-top: 8px !important;',
                   ui: 'fieldLabelTop',
                   columns: 1,
                   vertical: true,
                   itemId: 'defaultAPPostingDateItemId',
                   items: [{
                       boxLabel: app.localize('CompanyInvoiceDate').initCap(),
                       name: 'defaultAPPostingDate',
                       inputValue: '1',
                       ui: 'default',
                       boxLabelCls: 'checkboxLabel',
                       uncheckedValue: 'false'
                   }, {
                       boxLabel: app.localize('CurrentDate').initCap(),
                       name: 'defaultAPPostingDate',
                       inputValue: '2',
                       ui: 'default',
                       boxLabelCls: 'checkboxLabel',
                       uncheckedValue: 'false'
                   }]
               }
               ]
           },
                       {
                           columnWidth: .33,
                           padding: '20 10 0 20',
                           defaults: {
                               labelWidth: 160,
                               blankText: app.localize('MandatoryToolTipText')
                           },
                           items: [
                       {
                           xtype: 'combobox',
                           name: 'setDefaultAPTerms',
                           emptyText: app.localize('SelectOption'),
                           width: '100%',
                           ui: 'fieldLabelTop',
                           reference: 'setDefaultAPTerms',
                           displayField: 'description',
                           valueField: 'vendorPaymentTermId',
                           fieldLabel: app.localize('SetDefaultAPTerms'),
                           queryMode: 'local',
                           store: Ext.create('Chaching.store.APPaymentTermsListStore')

                       },
                       {
                           xtype: 'combobox',
                           name: 'setDefaultARTerms',
                           reference: 'setDefaultARTerms',
                           emptyText: app.localize('SelectOption'),
                           width: '100%',
                           ui: 'fieldLabelTop',
                           displayField: 'description',
                           valueField: 'customerPaymentTermId',
                           queryMode: 'local',
                           fieldLabel: app.localize('SetDefaultARTerms'),
                           store: Ext.create('Chaching.store.ARPaymentTermsListStore')

                       },
                            {
                                xtype: 'numberfield',
                                maxValue: 99,
                                minValue: 0,
                                hideTrigger: true,
                                allowDecimals: false,
                                keyNavEnabled: false,
                                mouseWheelEnabled: false,
                                name: 'depositGracePeriods',
                                fieldLabel: app.localize('DepositGracePeriods').initCap(),
                                width: '100%',
                                ui: 'fieldLabelTop'
                            }, {
                                xtype: 'numberfield',
                                maxValue: 99,
                                minValue: 0,
                                hideTrigger: true,
                                allowDecimals: false,
                                keyNavEnabled: false,
                                mouseWheelEnabled: false,
                                name: 'paymentsGracePeriods',
                                fieldLabel: app.localize('PaymentGracePeriods').initCap(),
                                width: '100%',
                                ui: 'fieldLabelTop'
                            },

                             {
                                 xtype: 'combobox',
                                 name: 'defaultBank',
                                 reference: 'defaultBankCombo',
                                 emptyText: app.localize('SelectOption'),
                                 width: '100%',
                                 ui: 'fieldLabelTop',
                                 displayField: 'bankAccountNumber',
                                 valueField: 'bankAccountId',
                                 queryMode: 'local',
                                 fieldLabel: app.localize('DefaultBank'),
                                 store: Ext.create('Chaching.store.utilities.autofill.BankAccountListStore', {remoteSort : false, remoteFilter : false})

                             },

                            //{
                            //    xtype: 'chachingcombobox',
                            //    store: new Chaching.store.utilities.autofill.BankAccountListStore(),
                            //    fieldLabel: app.localize('DefaultBank'),
                            //    ui: 'fieldLabelTop',
                            //    width: '100%',
                            //    name: 'defaultBank',
                            //    valueField: 'bankAccountId',
                            //    displayField: 'bankAccountNumber',
                            //    queryMode: 'remote',
                            //    minChars: 2,
                            //    useDisplayFieldToSearch: true,
                            //    modulePermissions: {
                            //        read: abp.auth.isGranted('Pages.Banking.BankSetup'),
                            //        create: abp.auth.isGranted('Pages.Banking.BankSetup.Create'),
                            //        edit: abp.auth.isGranted('Pages.Banking.BankSetup.Edit'),
                            //        destroy: abp.auth.isGranted('Pages.Banking.BankSetup.Delete')
                            //    },
                            //    primaryEntityCrudApi: {
                            //        read: abp.appPath + 'api/services/app/list/GetBankAccountList',
                            //        create: abp.appPath + 'api/services/app/bankAccountUnit/CreateBankAccountUnit',
                            //        update: abp.appPath + 'api/services/app/bankAccountUnit/UpdateBankAccountUnit',
                            //        destroy: abp.appPath + 'api/services/app/bankAccountUnit/DeleteBankAccountUnit'
                            //    },
                            //    createEditEntityType: 'banking.banksetup',
                            //    createEditEntityGridController: 'banking.banksetupgrid',
                            //    entityType: 'Bank Account'
                            //},

                            {
                                xtype: 'checkbox',
                                name: 'allowTransactionsJobWithGL',
                                labelAlign: 'right',
                                inputValue: true,
                                uncheckedValue: false,
                                width: '100%',
                                ui: 'default',
                                boxLabelCls: 'checkboxLabel',
                                boxLabel: app.localize('AllowTransactionsActionsThatHaveBeenCodedToJob+GLToAppearOnJobCost')

                            }]
                       }
                        ]
                    }
                    ]
                }],
                dockedItems: [
            {
                xtype: 'toolbar',
                dock: 'bottom',
                layout: {
                    type: 'hbox',
                    pack: 'center'
                },
                items: [
                {
                    xtype: 'button',
                    itemId: 'btnSaveSetup',
                    name: 'Save',
                    ui: 'actionButton',
                    text: app.localize('SaveCompanyPreferences').toUpperCase(),
                    iconCls: 'fa fa-save',
                    actionButton: true,
                    listeners: {
                        click: 'onSaveCompanyPreferences'
                    }
                },
                {
                    xtype: 'button',
                    itemId: 'btnCancelSetup',
                    name: 'Cancel',
                    ui: 'actionButton',
                    text: app.localize('Cancel').toUpperCase(),
                    iconCls: 'fa fa-close',
                    actionButton: true,
                    listeners: {
                        click: 'onCancelClicked'
                    }
                }]
            }]
            },
            {
                title: abp.localization.localize("Intercompany").initCap(),
                disabled: true,
                hideDefaultButtons: true,
                itemId: 'membersTab'
            }]

        //         items: [
        //{
        //    xtype: 'fieldset',
        //    ui: 'transparentFieldSet',
        //    title: abp.localization.localize("CompanyPreferences").initCap(),
        //    collapsible: true,
        //    layout: 'column',
        //    items: [{
        //        columnWidth: .33,
        //        padding: '20 10 0 20',
        //        defaults: {
        //            //labelWidth: 120//,
        //            //blankText: app.localize('MandatoryToolTipText')
        //        },
        //        items: [{
        //            xtype: 'checkbox',
        //            name: 'isAllowDuplicateAPInvoiceNos',
        //            labelAlign: 'right',
        //            inputValue: true,
        //            uncheckedValue: false,
        //            width: '100%',
        //            ui: 'default',
        //            boxLabelCls: 'checkboxLabel',
        //            boxLabel: app.localize('AllowDuplicateAPInvoices')
        //        },
        //       {
        //           xtype: 'checkbox',
        //           name: 'isAllowDuplicateARInvoiceNos',
        //           labelAlign: 'right',
        //           inputValue: true,
        //           uncheckedValue: false,
        //           width: '100%',
        //           ui: 'default',
        //           boxLabelCls: 'checkboxLabel',
        //           boxLabel: app.localize('AllowDuplicateARInvoices')

        //       },
        //        {
        //            xtype: 'checkbox',
        //            name: 'isAllowAccountnumbersStartingwithZero',
        //            labelAlign: 'right',
        //            inputValue: true,
        //            uncheckedValue: false,
        //            width: '100%',
        //            ui: 'default',
        //            boxLabelCls: 'checkboxLabel',
        //            boxLabel: app.localize('AllowAccountNumbersStartingWithZero')

        //        }, {
        //            xtype: 'checkbox',
        //            name: 'isImportPOlogsfromProducersActualUploads',
        //            labelAlign: 'right',
        //            inputValue: true,
        //            uncheckedValue: false,
        //            width: '100%',
        //            ui: 'default',
        //            boxLabelCls: 'checkboxLabel',
        //            boxLabel: app.localize('ImportPOLogsFromProducersActualUploads')

        //        },
        //        {
        //            xtype: 'checkbox',
        //            name: 'buildAPuponCCstatementPosting',
        //            labelAlign: 'right',
        //            inputValue: true,
        //            uncheckedValue: false,
        //            width: '100%',
        //            ui: 'default',
        //            boxLabelCls: 'checkboxLabel',
        //            boxLabel: app.localize('BuildAPUponCCStatementPosting')
        //        }, {
        //            xtype: 'checkbox',
        //            name: 'buildAPuponPayrollPosting',
        //            labelAlign: 'right',
        //            inputValue: true,
        //            uncheckedValue: false,
        //            width: '100%',
        //            ui: 'default',
        //            boxLabelCls: 'checkboxLabel',
        //            boxLabel: app.localize('BuildAPUponPayrollPosting')
        //        },
        //          {
        //              xtype: 'checkbox',
        //              name: 'pOAutoNumberingforDivisions',
        //              labelAlign: 'right',
        //              inputValue: true,
        //              uncheckedValue: false,
        //              width: '100%',
        //              ui: 'default',
        //              boxLabelCls: 'checkboxLabel',
        //              boxLabel: app.localize('POAutoNumberingforDivisions')
        //          },
        //           {
        //               xtype: 'checkbox',
        //               name: 'pOAutoNumberingforProjects',
        //               labelAlign: 'right',
        //               inputValue: true,
        //               uncheckedValue: false,
        //               width: '100%',
        //               ui: 'default',
        //               boxLabelCls: 'checkboxLabel',
        //               boxLabel: app.localize('POAutoNumberingforProjects')
        //           }
        //        ]
        //    },
        //    {
        //        columnWidth: .33,
        //        padding: '20 10 0 20',
        //        defaults: {
        //            labelWidth: 160,
        //            blankText: app.localize('MandatoryToolTipText')
        //        },
        //        items: [{
        //            xtype: 'radiogroup',
        //            labelStyle: 'padding-top: 8px !important;',
        //            fieldLabel: app.localize('ARAgingDate'),
        //            width: '100%',
        //            ui: 'fieldLabelTop',
        //            columns: 1,
        //            vertical: true,
        //            itemId: 'arAgingDateItemId',
        //            items: [{
        //                boxLabel: app.localize('AgeByInvoiceDate').initCap(),
        //                name: 'arAgingDate',
        //                inputValue: 'invoiceDate',
        //                ui: 'default',
        //                boxLabelCls: 'checkboxLabel',
        //                uncheckedValue: 'false'
        //            }, {
        //                boxLabel: app.localize('AgeByDueDate').initCap(),
        //                name: 'arAgingDate',
        //                inputValue: 'dueDate',
        //                ui: 'default',
        //                boxLabelCls: 'checkboxLabel',
        //                uncheckedValue: 'false'
        //            }]
        //        }, {
        //            xtype: 'radiogroup',
        //            labelStyle: 'padding-top: 8px !important;',
        //            fieldLabel: app.localize('APAgingDate'),
        //            width: '100%',
        //            ui: 'fieldLabelTop',
        //            columns: 1,
        //            vertical: true,
        //            itemId: 'apAgingDateItemId',
        //            items: [{
        //                boxLabel: app.localize('AgeByInvoiceDate').initCap(),
        //                name: 'apAgingDate',
        //                inputValue: 'invoiceDate',
        //                ui: 'default',
        //                boxLabelCls: 'checkboxLabel',
        //                uncheckedValue: 'false'
        //            }, {
        //                boxLabel: app.localize('AgeByDueDate').initCap(),
        //                name: 'apAgingDate',
        //                inputValue: 'dueDate',
        //                ui: 'default',
        //                boxLabelCls: 'checkboxLabel',
        //                uncheckedValue: 'false'
        //            }]
        //        }, {
        //            xtype: 'radiogroup',
        //            fieldLabel: app.localize('APPostingDateDefault'),
        //            width: '100%',
        //            labelStyle: 'padding-top: 8px !important;',
        //            ui: 'fieldLabelTop',
        //            columns: 1,
        //            vertical: true,
        //            itemId: 'defaultAPPostingDateItemId',
        //            items: [{
        //                boxLabel: app.localize('CompanyInvoiceDate').initCap(),
        //                name: 'defaultAPPostingDate',
        //                inputValue: 'invoiceDate',
        //                ui: 'default',
        //                boxLabelCls: 'checkboxLabel',
        //                uncheckedValue: 'false'
        //            }, {
        //                boxLabel: app.localize('CurrentDate').initCap(),
        //                name: 'defaultAPPostingDate',
        //                inputValue: 'currentDate',
        //                ui: 'default',
        //                boxLabelCls: 'checkboxLabel',
        //                uncheckedValue: 'false'
        //            }]
        //        }
        //        ]
        //    },
        //                {
        //                    columnWidth: .33,
        //                    padding: '20 10 0 20',
        //                    defaults: {
        //                        labelWidth: 160,
        //                        blankText: app.localize('MandatoryToolTipText')
        //                    },
        //                    items: [
        //                {
        //                    xtype: 'combobox',
        //                    name: 'setDefaultAPTerms',
        //                    emptyText: app.localize('SelectOption'),
        //                    width: '100%',
        //                    ui: 'fieldLabelTop',
        //                    displayField: 'setDefaultAPTerms',
        //                    valueField: 'setDefaultAPTerms',
        //                    fieldLabel: app.localize('SetDefaultAPTerms')

        //                },
        //                {
        //                    xtype: 'combobox',
        //                    name: 'setDefaultARTerms',
        //                    emptyText: app.localize('SelectOption'),
        //                    width: '100%',
        //                    ui: 'fieldLabelTop',
        //                    displayField: 'setDefaultARTerms',
        //                    valueField: 'setDefaultARTerms',
        //                    fieldLabel: app.localize('SetDefaultARTerms')

    }],
    listeners: {
        afterrender: 'onCompanySetupRender'
    }
});