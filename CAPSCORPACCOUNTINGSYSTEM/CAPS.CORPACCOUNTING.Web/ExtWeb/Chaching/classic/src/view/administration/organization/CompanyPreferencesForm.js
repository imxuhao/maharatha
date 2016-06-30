Ext.define('Chaching.view.administration.organization.CompanyPreferencesForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.administration.organizationunits.companypreferences'],
    requires: [
        'Chaching.view.administration.organization.CompanyPreferencesFormController'
    ],
    controller: 'administration-organization-companypreferencesform',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.OrganizationUnits'),
        create: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree'),
        edit: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree'),
        destroy: abp.auth.isGranted('Pages.Administration.OrganizationUnits.ManageOrganizationTree')
    },
    name: 'companypreferences',
   // itemId: 'companyPreferencesFormId',
    openInPopupWindow: false,
    hideDefaultButtons: true,
    autoScroll: true,
    border: false,
    showFormTitle: false,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CompanyPreferences").initCap()
    },
    items: [
        {
            xtype: 'fieldset',
            ui: 'transparentFieldSet',
            title: abp.localization.localize("CompanyPreferences").initCap(),
            collapsible: true,
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
                // {
                //    xtype: 'combobox',
                //    name: 'setDefaultAPTerms',
                //    emptyText: app.localize('SelectOption'),
                //    width: '100%',
                //    ui: 'fieldLabelTop',
                //    displayField: 'setDefaultAPTerms',
                //    valueField: 'setDefaultAPTerms',
                //    fieldLabel: app.localize('SetDefaultAPTerms')

                // },
                //{
                //    xtype: 'combobox',
                //    name: 'setDefaultARTerms',
                //    emptyText: app.localize('SelectOption'),
                //    width: '100%',
                //    ui: 'fieldLabelTop',
                //    displayField: 'setDefaultARTerms',
                //    valueField: 'setDefaultARTerms',
                //    fieldLabel: app.localize('SetDefaultARTerms')

                //}, 
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
                     name: 'poAutoNumbering',
                     labelAlign: 'right',
                     inputValue: true,
                     uncheckedValue: false,
                     width: '100%',
                     ui: 'default',
                     boxLabelCls: 'checkboxLabel',
                     boxLabel: app.localize('POAutoNumbering')
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
                items: [  {
                     xtype: 'radiogroup',
                     labelStyle: 'padding-top: 8px !important;',
                     fieldLabel: app.localize('ARAgingDate'),
                     width: '100%',
                     ui: 'fieldLabelTop',
                     columns: 1,
                     vertical: true,
                     items: [{
                         boxLabel: app.localize('AgeByInvoiceDate').initCap(),
                         name: 'arAgingDate',
                         inputValue: 'invoiceDate',
                         ui: 'default',
                         boxLabelCls: 'checkboxLabel',
                         uncheckedValue: 'false'
                     }, {
                         boxLabel: app.localize('AgeByDueDate').initCap(),
                         name: 'arAgingDate',
                         inputValue: 'dueDate',
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
                     items: [{
                         boxLabel: app.localize('AgeByInvoiceDate').initCap(),
                         name: 'apAgingDate',
                         inputValue: 'invoiceDate',
                         ui: 'default',
                         boxLabelCls: 'checkboxLabel',
                         uncheckedValue: 'false'
                     }, {
                         boxLabel: app.localize('AgeByDueDate').initCap(),
                         name: 'apAgingDate',
                         inputValue: 'dueDate',
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
                     items: [{
                         boxLabel: app.localize('CompanyInvoiceDate').initCap(),
                         name: 'defaultAPPostingDate',
                         inputValue: 'invoiceDate',
                         ui: 'default',
                         boxLabelCls: 'checkboxLabel',
                         uncheckedValue: 'false'
                     }, {
                         boxLabel: app.localize('CurrentDate').initCap(),
                         name: 'defaultAPPostingDate',
                         inputValue: 'currentDate',
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
                           displayField: 'setDefaultAPTerms',
                           valueField: 'setDefaultAPTerms',
                           fieldLabel: app.localize('SetDefaultAPTerms')

                           },
                        {
                            xtype: 'combobox',
                            name: 'setDefaultARTerms',
                            emptyText: app.localize('SelectOption'),
                            width: '100%',
                            ui: 'fieldLabelTop',
                            displayField: 'setDefaultARTerms',
                            valueField: 'setDefaultARTerms',
                            fieldLabel: app.localize('SetDefaultARTerms')

                        },
                             {
                                    xtype: 'numberfield',
                                    maxValue: 99,
                                    minValue: 0,
                                    hideTrigger: true,
                                    allowDecimals: false,
                                    keyNavEnabled: false,
                                    mouseWheelEnabled : false,
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
                                }, {
                                    xtype: 'chachingcombobox',
                                    store: new Chaching.store.utilities.autofill.BankAccountListStore(),
                                    fieldLabel: app.localize('DefaultBank'),
                                    ui: 'fieldLabelTop',
                                    width: '100%',
                                    name: 'defaultBank',
                                    valueField: 'bankAccountId',
                                    displayField: 'bankAccountNumber',
                                    queryMode: 'remote',
                                    minChars: 2,
                                    useDisplayFieldToSearch: true,
                                    modulePermissions: {
                                        read: abp.auth.isGranted('Pages.Banking.BankSetup'),
                                        create: abp.auth.isGranted('Pages.Banking.BankSetup.Create'),
                                        edit: abp.auth.isGranted('Pages.Banking.BankSetup.Edit'),
                                        destroy: abp.auth.isGranted('Pages.Banking.BankSetup.Delete')
                                    },
                                    primaryEntityCrudApi: {
                                        read: abp.appPath + 'api/services/app/list/GetBankAccountList',
                                        create: abp.appPath + 'api/services/app/bankAccountUnit/CreateBankAccountUnit',
                                        update: abp.appPath + 'api/services/app/bankAccountUnit/UpdateBankAccountUnit',
                                        destroy: abp.appPath + 'api/services/app/bankAccountUnit/DeleteBankAccountUnit'
                                    },
                                    createEditEntityType: 'banking.banksetup', 
                                    createEditEntityGridController: 'banking.banksetupgrid',
                                    entityType: 'Bank Account'
                                }, {
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

                        //, {
                        //    columnWidth: .25,
                        //    padding: '20 10 0 20',
                        //    defaults: {
                        //        labelWidth: 180,
                        //        blankText: app.localize('MandatoryToolTipText')
                        //    },
                        //    items: []
                        //}

            ]
        }]
});