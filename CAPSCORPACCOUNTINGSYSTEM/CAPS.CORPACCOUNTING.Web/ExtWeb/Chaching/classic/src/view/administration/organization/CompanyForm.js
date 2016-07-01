Ext.define('Chaching.view.administration.organization.CompanyForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: [
        'widget.organizationUnits.create', 'widget.organizationUnits.edit'
    ],
    requires: [
       'Chaching.view.administration.organization.CompanyFormController',
       'Chaching.view.administration.organization.CompanySetupForm',
       'Chaching.view.administration.organization.CompanyPreferencesForm',
       'Chaching.view.administration.organization.MembersForm'
    ],
    controller: 'administration-organization-companyform',
    name: 'companysetup',
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
        title: abp.localization.localize("CreateNewCompanySetup").initCap()
    },
    layout : 'fit',
    items: {
        xtype: 'tabpanel',
       // ui: 'formTabPanels',
        ui: 'submenuTabs',
        tabPosition: 'left',
        tabRotation: 2,
        items: [
            {
                title: abp.localization.localize("CompanySetup").initCap(),
                xtype: 'administration.organizationunits.companysetup'
            },
            {
                title: abp.localization.localize("CompanyPreferences").initCap(),
                disabled: true,
                itemId: 'companyPreferencesTab',
                xtype: 'administration.organizationunits.companypreferences'
            },
            {
                layout: 'fit',
                title: abp.localization.localize("Members").initCap(),
                disabled: true,
                hideDefaultButtons: true,
                hidden : !abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageMembers'),
                itemId: 'membersTab',
                // xtype: 'administration.organizationunits.members'
                xtype: 'administration.organizationunits.companyusersgrid'
            }


        //{
        //    title: abp.localization.localize("General").initCap(),
        //    iconCls: 'fa fa-gear',
        //    items: [{
        //        xtype: 'fieldset',
        //        ui: 'transparentFieldSet',
        //        title: abp.localization.localize("GeneralInformation").initCap(),
        //        collapsible: true,
        //        layout: 'column',
        //        items: [{
        //            columnWidth: .33,
        //            padding: '20 10 0 20',
        //            defaults: {
        //                // labelAlign: 'top',
        //                labelWidth: 120,
        //                blankText: app.localize('MandatoryToolTipText')
        //            },
        //            items: [{
        //                xtype: 'hiddenfield',
        //                name: 'vendorId',
        //                value: 0
        //            }, {
        //                xtype: 'textfield',
        //                name: 'lastName',
        //                itemId: 'lastName',
        //                allowBlank: false,
        //                fieldLabel: app.localize('CompanyName').initCap(),
        //                width: '100%',
        //                ui: 'fieldLabelTop',
        //                emptyText: app.localize('MandatoryField')
        //            },
        //                 {
        //                     xtype: 'textfield',
        //                     name: 'vendorNumber',
        //                     itemId: 'vendorNumber',
        //                     fieldLabel: app.localize('VendorNumber').initCap(),
        //                     width: '100%',
        //                     ui: 'fieldLabelTop'
        //                 }
        //            ]
        //        },
        //        {
        //            columnWidth: .33,
        //            padding: '20 10 0 20',
        //            defaults: {
        //                // labelAlign: 'top',
        //                blankText: app.localize('MandatoryToolTipText')
        //            },
        //            items: [{
        //                xtype: 'textfield',
        //                name: 'firstName',
        //                itemId: 'firstName',
        //                fieldLabel: app.localize('PayToName').initCap(),
        //                width: '100%',
        //                ui: 'fieldLabelTop'
        //            }, {
        //                xtype: 'textfield',
        //                name: 'billingAccount',
        //                itemId: 'billingAccount',
        //                fieldLabel: app.localize('BillingAccount').initCap(),
        //                width: '100%',
        //                ui: 'fieldLabelTop'
        //            }]
        //        },
        //                    {
        //                        columnWidth: .33,
        //                        padding: '20 10 0 20',
        //                        defaults: {
        //                            // labelAlign: 'top',
        //                            blankText: app.localize('MandatoryToolTipText')
        //                        },
        //                        items: [{
        //                            xtype: 'combobox',
        //                            name: 'typeofVendorId',
        //                            fieldLabel: app.localize('Type').initCap(),
        //                            labelWidth: 70,
        //                            width: '100%',
        //                            ui: 'fieldLabelTop',
        //                            displayField: 'typeofvendor',
        //                            valueField: 'typeofvendorId',
        //                            emptyText: app.localize('SelectOption'),
        //                            queryMode: 'local',
        //                            bind: {
        //                                store: '{vendorTypeList}'
        //                            }
        //                        }, {
        //                            xtype: 'checkbox',
        //                            name: 'isActive',
        //                            itemId: 'isActive',
        //                            labelAlign: 'right',
        //                            inputValue: true,
        //                            checked: true,
        //                            ui: 'default',
        //                            boxLabelCls: 'checkboxLabel',
        //                            boxLabel: app.localize('Active')
        //                        }]
        //                    }, {
        //                        columnWidth: 1,
        //                        padding: '20 10 0 20',
        //                        defaults: {
        //                            labelAlign: 'top',
        //                            blankText: app.localize('MandatoryToolTipText')
        //                        },
        //                        items: [{
        //                            xtype: 'address',
        //                            itemId: 'addressGrid',
        //                            layout: 'fit',
        //                            width: '100%'
        //                        }]
        //                    }]
        //    },
        //       {
        //           xtype: 'fieldset',
        //           ui: 'transparentFieldSet',
        //           collapsible: true,
        //           title: abp.localization.localize("TaxInformation").initCap(),
        //           layout: 'column',
        //           items: [{
        //               columnWidth: .4,
        //               padding: '20 10 0 20',
        //               defaults: {
        //                   //labelAlign: 'top',
        //                   blankText: app.localize('MandatoryToolTipText')
        //               },
        //               items: [
        //                                        {
        //                                            xtype: 'combobox',
        //                                            name: 'typeofTaxId',
        //                                            fieldLabel: app.localize('Type').initCap(),
        //                                            width: '100%',
        //                                            ui: 'fieldLabelTop',
        //                                            displayField: 'typeofTax',
        //                                            valueField: 'typeofTaxId',
        //                                            emptyText: app.localize('SelectOption'),
        //                                            queryMode: 'local',
        //                                            bind: {
        //                                                store: '{typeOfTaxList}'
        //                                            }
        //                                        }, {
        //                                            xtype: 'textfield',
        //                                            name: 'dbaName',
        //                                            itemId: 'dbaName',
        //                                            fieldLabel: app.localize('LegalName').initCap(),
        //                                            width: '100%',
        //                                            ui: 'fieldLabelTop'
        //                                        }]
        //           },
        //           {
        //               columnWidth: .3,
        //               padding: '20 10 0 20',
        //               defaults: {
        //                   // labelAlign: 'top',
        //                   blankText: app.localize('MandatoryToolTipText')
        //               },
        //               items: [

        //                   {
        //                       xtype: 'textfield',
        //                       name: 'ssnTaxId',
        //                       itemId: 'ssnTaxId',
        //                       fieldLabel: app.localize('SSN/TaxID'),
        //                       width: '100%',
        //                       ui: 'fieldLabelTop'
        //                   }, {
        //                       xtype: 'combobox',
        //                       name: 'typeof1099BoxId',
        //                       fieldLabel: app.localize('1099s').initCap(),
        //                       width: '100%',
        //                       ui: 'fieldLabelTop',
        //                       displayField: 'typeof1099Box',
        //                       valueField: 'typeof1099BoxId',
        //                       emptyText: app.localize('SelectOption'),
        //                       queryMode: 'local',
        //                       bind: {
        //                           store: '{typeof1099BoxList}'
        //                       }
        //                   }]
        //           },
        //           {
        //               columnWidth: .3,
        //               padding: '20 10 0 20',
        //               defaults: {
        //                   // labelAlign: 'top',
        //                   blankText: app.localize('MandatoryToolTipText')
        //               },
        //               items: [
        //               {
        //                   xtype: 'textfield',
        //                   name: 'fedralTaxId',
        //                   itemId: 'fedralTaxId',
        //                   fieldLabel: app.localize('FedTaxID'),
        //                   width: '100%',
        //                   ui: 'fieldLabelTop'
        //               }, {
        //                   xtype: 'checkbox',
        //                   boxLabel: app.localize('W9OnFile'),
        //                   name: 'isw9OnFile',
        //                   labelAlign: 'right',
        //                   inputValue: true,
        //                   checked: false,
        //                   boxLabelCls: 'checkboxLabel'
        //               }]
        //           }]
        //       }]
        //},
        //{
        //    title: abp.localization.localize("Other").initCap(),
        //    iconCls: 'fa fa-gear',
        //    items: [
        //      {
        //          xtype: 'fieldset',
        //          collapsible: true,
        //          title: abp.localization.localize("DefaultsSection").initCap(),
        //          ui: 'transparentFieldSet',
        //          layout: 'column',
        //          items: [
        //{
        //    columnWidth: .4,
        //    padding: '20 10 0 20',
        //    defaults: {
        //        labelWidth: 120
        //        //labelAlign: 'top',
        //        //blankText: app.localize('MandatoryToolTipText')
        //    },
        //    items: [
        //      {
        //          xtype: 'combobox',
        //          name: 'paymentTermsId',
        //          fieldLabel: app.localize('PaymentTerms').initCap(),
        //          width: '100%',
        //          ui: 'fieldLabelTop',
        //          displayField: 'paymentTerms',
        //          valueField: 'paymentTermsId',
        //          emptyText: app.localize('SelectOption'),
        //          bind: {
        //              store: '{paymentTermsList}'
        //          }
        //      }, {
        //          //xtype: 'combobox',
        //          //name: 'glAccountId',
        //          //fieldLabel: app.localize('GLAccount').initCap(),
        //          //width: '100%',
        //          //ui: 'fieldLabelTop',
        //          //displayField: 'account',
        //          //valueField: 'accountId',
        //          //emptyText: app.localize('SelectOption'),
        //          //bind: {
        //          //    store: '{getAccountsList}'
        //          //}

        //          xtype: 'chachingcombobox',
        //          store: new Chaching.store.utilities.autofill.GLAccountListStore(),
        //          fieldLabel: app.localize('GLAccount'),
        //          ui: 'fieldLabelTop',
        //          width: '100%',
        //          name: 'glAccountId',
        //          valueField: 'accountId',
        //          displayField: 'accountNumber',
        //          queryMode: 'remote',
        //          minChars: 2,
        //          useDisplayFieldToSearch: true,
        //          modulePermissions: {
        //              read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
        //              create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
        //              edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
        //              destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete')
        //          },
        //          primaryEntityCrudApi: {
        //              read: abp.appPath + 'api/services/app/vendorUnit/GetAccountsList',
        //              create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
        //              update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
        //              destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
        //          },
        //          createEditEntityType: 'financials.accounts.accounts',
        //          createEditEntityGridController: 'financials-accounts-accountsgrid',
        //          entityType: 'Account',
        //          isTwoEntityPicker: false

        //      }]
        //},
        //          {
        //              columnWidth: .3,
        //              padding: '20 10 0 20',
        //              //defaults: {
        //              //    labelAlign: 'top',
        //              //    blankText: app.localize('MandatoryToolTipText')
        //              //},
        //              items: [
        //      //{
        //      //    xtype: 'textfield',
        //      //    name: 'taxCredit',
        //      //    itemId: 'taxCredit',
        //      //    fieldLabel: app.localize('TaxCredit').initCap(),
        //      //    width: '100%',
        //      //    ui: 'fieldLabelTop'
        //      //},
        //        {
        //            ///TODO: Replace with combo box once tax credit service is ready
        //            xtype: 'combobox',
        //            name: 'taxCreditId',
        //            itemId: 'taxCreditId',
        //            //allowBlank: true,
        //            //queryMode: 'local',
        //            bind: {
        //                store: '{getTaxCreditList}'
        //            },
        //            valueField: 'value',
        //            displayField: 'name',
        //            fieldLabel: app.localize('TaxCredit').initCap(),
        //            width: '100%',
        //            ui: 'fieldLabelTop',
        //            emptyText: app.localize('SelectOption')
        //        },
        //      {
        //          //xtype: 'combobox',
        //          //name: 'accountId',
        //          //fieldLabel: app.localize('Line#').initCap(),
        //          //width: '100%',
        //          //ui: 'fieldLabelTop',
        //          //displayField: 'account',
        //          //valueField: 'accountId',
        //          //emptyText: app.localize('SelectOption'),
        //          //bind: {
        //          //    store: '{getAccountsListLines}'
        //          //}

        //          xtype: 'chachingcombobox',
        //          store: new Chaching.store.utilities.autofill.LinesListStore(),
        //          fieldLabel: app.localize('Line#'),
        //          ui: 'fieldLabelTop',
        //          width: '100%',
        //          name: 'accountId',
        //          valueField: 'accountId',
        //          displayField: 'accountNumber',
        //          queryMode: 'remote',
        //          minChars: 2,
        //          useDisplayFieldToSearch: true,
        //          modulePermissions: {
        //              read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs'),
        //              create: false,//abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Create'),
        //              edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Edit'),
        //              destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Delete')
        //          },
        //          primaryEntityCrudApi: {
        //              read: abp.appPath + 'api/services/app/vendorUnit/GetAccountsList',
        //              create: abp.appPath + 'api/services/app/linesUnit/CreateLineUnit',
        //              update: abp.appPath + 'api/services/app/linesUnit/UpdateLineUnit',
        //              destroy: abp.appPath + 'api/services/app/linesUnit/DeleteLineUnit'
        //          },
        //          createEditEntityType: 'projects.projectmaintenance.linenumbers',
        //          createEditEntityGridController: 'projects-projectmaintenance-linenumbersgrid',
        //          entityType: 'Line',
        //          isTwoEntityPicker: false

        //      }


        //              ]
        //          },
        //          {
        //              columnWidth: .3,
        //              padding: '20 10 0 20',
        //              //defaults: {
        //              //    labelAlign: 'top',
        //              //    blankText: app.localize('MandatoryToolTipText')
        //              //},
        //              items: [{
        //                  //xtype: 'combobox',
        //                  //name: 'jobId',
        //                  //fieldLabel: app.localize('Division').initCap(),
        //                  //width: '100%',
        //                  //ui: 'fieldLabelTop',
        //                  //displayField: 'rollupDivision',
        //                  //valueField: 'rollupDivisionId',
        //                  //emptyText: app.localize('SelectOption'),
        //                  //bind: {
        //                  //    store: '{rollupDivisionList}'
        //                  //}

        //                  xtype: 'chachingcombobox',
        //                  store: new Chaching.store.utilities.autofill.DivisionListStore(),
        //                  fieldLabel: app.localize('Division'),
        //                  ui: 'fieldLabelTop',
        //                  width: '100%',
        //                  name: 'jobId',
        //                  valueField: 'jobId',
        //                  displayField: 'jobNumber',
        //                  queryMode: 'remote',
        //                  minChars: 2,
        //                  useDisplayFieldToSearch: true,
        //                  modulePermissions: {
        //                      read: abp.auth.isGranted('Pages.Financials.Accounts.Divisions'),
        //                      create: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Create'),
        //                      edit: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Edit'),
        //                      destroy: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Delete')
        //                  },
        //                  primaryEntityCrudApi: {
        //                      read: abp.appPath + 'api/services/app/jobUnit/GetDivisionList',
        //                      create: abp.appPath + 'api/services/app/divisionUnit/CreateDivisionUnit',
        //                      update: abp.appPath + 'api/services/app/divisionUnit/UpdateDivisionUnit',
        //                      destroy: abp.appPath + 'api/services/app/divisionUnit/DeleteDivisionUnit'
        //                  },
        //                  createEditEntityType: 'financials.accounts.divisions',
        //                  createEditEntityGridController: 'financials-accounts-divisionsgrid',
        //                  entityType: 'Division',
        //                  identificationKey: 'isDivision'

        //              }]
        //          },
        //          {
        //              columnWidth: 1,
        //              padding: '20 10 0 20',
        //              defaults: {
        //                  labelAlign: 'top',
        //                  blankText: app.localize('MandatoryToolTipText')
        //              },
        //              items: [{
        //                  xtype: 'vendoralias',
        //                  itemId: 'vendorAliasGrid',
        //                  width: 450
        //              }
        //              ]
        //          }]
        //      },

        //    {
        //        xtype: 'fieldset',
        //        title: abp.localization.localize("NotesSection").initCap(),
        //        collapsible: true,
        //        ui: 'transparentFieldSet',
        //        items: [{
        //            xtype: 'textareafield',
        //            grow: true,
        //            name: 'notes',
        //            itemId: 'notes',
        //            fieldLabel: app.localize('Notes').initCap(),
        //            anchor: '50%',
        //            ui: 'fieldLabelTop'
        //        }]

        //    }]
        //}

        ]

        //,
        //dockedItems: [
        //        {
        //            xtype: 'toolbar',
        //            dock: 'bottom',
        //            layout: {
        //                type: 'hbox',
        //                pack: 'center'
        //            },
        //            items: [
        //            {
        //                xtype: 'button',
        //                itemId: 'btnSaveSetup',
        //                ui: 'actionButton',
        //                text: app.localize('SaveVendor').toUpperCase(),
        //                iconCls: 'fa fa-save',
        //                listeners: {
        //                    click: 'onSaveClicked'
        //                }
        //            }, {
        //                xtype: 'button',
        //                itemId: 'btnCancelSetup',
        //                ui: 'actionButton',
        //                text: app.localize('Cancel').toUpperCase(),
        //                iconCls: 'fa fa-close',
        //                listeners: {
        //                    click: 'onCancelClicked'
        //                }
        //            }]
        //        }]
    }
});