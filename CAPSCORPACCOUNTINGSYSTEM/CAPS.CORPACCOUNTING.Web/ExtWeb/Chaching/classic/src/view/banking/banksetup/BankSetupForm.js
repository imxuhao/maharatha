Ext.define('Chaching.view.banking.banksetup.BankSetupForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.banking.banksetup.create', 'widget.banking.banksetup.edit'],
    requires: [
        'Chaching.view.banking.banksetup.BankSetupFormController'
    ],
    controller: 'banking-banksetup-banksetupform',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Banking.BankSetup'),
        create: abp.auth.isGranted('Pages.Banking.BankSetup.Create'),
        edit: abp.auth.isGranted('Pages.Banking.BankSetup.Edit'),
        destroy: abp.auth.isGranted('Pages.Banking.BankSetup.Delete')
    },
    name: 'banksetup',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    autoScroll: true,
    border: false,
    showFormTitle: false,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CreateBank").initCap()
    },
    items: {
        xtype: 'tabpanel',
        //tabPosition : 'left',
        ui: 'formTabPanels',
        items: [
        {
            title: abp.localization.localize("BankSetup").initCap(),
            iconCls: 'fa fa-gear',
            items: [{
                xtype: 'fieldset',
                ui: 'transparentFieldSet',
                title: abp.localization.localize("BankingInformation").initCap(),
                collapsible: true,
                layout: 'column',
                items: [{
                    columnWidth: .33,
                    padding: '20 10 0 20',
                    defaults: {
                        // labelAlign: 'top',
                        labelWidth: 120,
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items: [{
                        xtype: 'hiddenfield',
                        name: 'bankAccountId',
                        value: 0
                    }, {
                        xtype: 'textfield',
                        name: 'description',
                        allowBlank: false,
                        fieldLabel: app.localize('BankName').initCap(),
                        width: '100%',
                        ui: 'fieldLabelTop',
                        emptyText: app.localize('MandatoryField')
                    },
                    {
                        xtype: 'textfield',
                        name: 'bankAccountName',
                        fieldLabel: app.localize('AccountName').initCap(),
                        width: '100%',
                        ui: 'fieldLabelTop'
                    },{
                        xtype: 'combobox',
                        name: 'typeOfBankAccountId',
                        fieldLabel: app.localize('AccountType').initCap(),
                       // labelWidth: 70,
                        width: '100%',
                        ui: 'fieldLabelTop',
                        displayField: 'typeOfBankAccount',
                        valueField: 'typeOfBankAccountId',
                        emptyText: app.localize('SelectOption'),
                        store: 'utilities.AccountTypeListStore'
                       
                        }, {
                            xtype: 'textfield',
                            name: 'bankAccountNumber',
                            fieldLabel: app.localize('AccountNumber').initCap(),
                            width: '100%',
                            ui: 'fieldLabelTop'
                        }
                    ]
                },
                {
                    columnWidth: .33,
                    padding: '20 10 0 20',
                    defaults: {
                        // labelAlign: 'top',
                        labelWidth: 140,
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items: [{
                        xtype: 'textfield',
                        name: 'routingNumber',
                        fieldLabel: app.localize('RoutingNumber').initCap(),
                        width: '100%',
                        ui: 'fieldLabelTop'
                    }, {
                        xtype: 'textfield',
                        name: 'positivePayTransmitterInfo',
                        fieldLabel: app.localize('PositivePayBankNumber').initCap(),
                        width: '100%',
                        ui: 'fieldLabelTop'
                    }, {
                        //xtype: 'combobox',
                        //name: 'accountId',
                        //fieldLabel: app.localize('LedgerAccount').initCap(),
                        //width: '100%',
                        //ui: 'fieldLabelTop',
                        //displayField: 'name',
                        //valueField: 'accountId',
                        //emptyText: app.localize('SelectOption')//,
                        ////store: 'vendorTypeList'

                        xtype: 'autofillcombo',
                        width: '100%',
                        ui: 'fieldLabelTop',
                        name: 'accountId',
                        fieldLabel: app.localize('LedgerAccount'),
                        store: 'utilities.autofill.AccountListStore',
                        valueField: 'accountId',
                        displayField: 'accountNumber',
                        entityGridController: 'financials-accounts-accountsgrid',
                        nameOfEntity: 'Account',
                        entityType: 'financials.accounts.accounts',
                        entityPermission: 'Financials.Accounts.Accounts'
                    }, {

                        //xtype: 'combobox',
                        //name: 'jobId',
                        //fieldLabel: app.localize('Divisions').initCap(),
                        //width: '100%',
                        //ui: 'fieldLabelTop',
                        //displayField: 'name',
                        //valueField: 'jobId',
                        //emptyText: app.localize('SelectOption')//,
                        //// store: 'vendorTypeList'

                        xtype: 'autofillcombo',
                        name: 'jobId',
                        width: '100%',
                        ui: 'fieldLabelTop',
                        fieldLabel: app.localize('Divisions'),
                        store: 'utilities.autofill.DivisionListStore',
                        valueField: 'jobId',
                        displayField: 'jobNumber',
                        entityGridController: 'financials-accounts-divisionsgrid',
                        nameOfEntity: 'Division',
                        entityType: 'financials.accounts.divisions',
                        entityPermission: 'Financials.Accounts.Divisions'
                    }]
                },
                            {
                                columnWidth: .33,
                                padding: '20 10 0 20',
                                defaults: {
                                    // labelAlign: 'top',
                                    labelWidth: 180,
                                    blankText: app.localize('MandatoryToolTipText')
                                },
                                items: [{
                                    xtype: 'combobox',
                                    name: 'typeOfCheckStockId',
                                    fieldLabel: app.localize('CheckStock').initCap(),
                                    width: '100%',
                                    ui: 'fieldLabelTop',
                                    displayField: 'typeOfCheckStock',
                                    valueField: 'typeOfCheckStockId',
                                    emptyText: app.localize('SelectOption'),
                                    store: 'utilities.CheckStockListStore'
                                }, {
                                    xtype: 'textfield',
                                    name: 'lastCheckNumberGenerated',
                                    fieldLabel: app.localize('LastCheckNumber').initCap(),
                                    width: '100%',
                                    ui: 'fieldLabelTop'
                                }, {
                                    xtype: 'combobox',
                                    name: 'typeOfUploadFileId',
                                    fieldLabel: app.localize('UploadMethod').initCap(),
                                    width: '100%',
                                    ui: 'fieldLabelTop',
                                    displayField: 'uploadFileName',
                                    valueField: 'typeOfUploadFileId',
                                    emptyText: app.localize('SelectOption'),
                                    store: 'utilities.UploadMethodListStore'
                                }, {
                                    xtype: 'combobox',
                                    name: 'positivePayTypeOfUploadFileId',
                                    fieldLabel: app.localize('PositivePayFile').initCap(),
                                    width: '100%',
                                    ui: 'fieldLabelTop',
                                    displayField: 'positivePayTypeOfUploadFile',
                                    valueField: 'positivePayTypeOfUploadFileId',
                                    emptyText: app.localize('SelectOption'),
                                    store: 'utilities.PositivePayFileListStore'
                                }, {
                                    xtype: 'checkbox',
                                    name: 'isClosed',
                                    labelAlign: 'right',
                                    inputValue: true,
                                    checked: true,
                                    width: '100%',
                                    ui: 'default',
                                    boxLabelCls: 'checkboxLabel',
                                    boxLabel: app.localize('ClosedBankAccount')
                                }]
                            }]
            }, {
               // columnWidth: 1,
                padding: '20 10 0 20',
                items: [{
                    xtype: 'banking.banksetup.checknumbergrid',
                    itemId: 'checkNumberGrid',
                    anchor: '100% 80%',
                    autoScroll: true
                    //,
                    //layout: 'fit',
                    //width: '100%'
                }]
            }]
        },
        {
            title: abp.localization.localize("ACHDirectDepositTitle"),
            iconCls: 'fa fa-gear',
            items: [
              {
                  xtype: 'fieldset',
                  ui: 'transparentFieldSet',
                  collapsible: true,
                  title: abp.localization.localize("ACHDirectDeposit"),
                  layout: 'column',
                  items: [
        {
            columnWidth: .4,
            padding: '20 10 0 20',
            defaults: {
                // labelAlign: 'top',
                labelWidth: 150//,
                //blankText: app.localize('MandatoryToolTipText')
            },
            items: [
                 {
                     //xtype: 'checkbox',
                     //name: 'isachEnabled',
                     //itemId: 'isAchDirectDepositEnabled',
                     ////labelAlign: 'left',
                     //boxLabelAlign : 'before',
                     //inputValue: true,
                     //checked: false,
                     //maxWidth : 200,
                     //width: '100%',
                     ////fieldLabel: app.localize('EnableACHDirectDeposit'),
                     ////ui: 'fieldLabelTop',
                     //ui: 'default',
                     //style : 'padding-left:0px !important;',
                     //boxLabelCls: 'checkboxLabel',
                     //boxLabel: app.localize('EnableACHDirectDeposit')


                        xtype: 'checkbox',
                        name: 'isachEnabled',
                        boxLabelAlign: 'before',
                        inputValue: true,
                        checked: false,
                        width: '100%',
                        maxWidth: 200,
                        //labelStyle : 'padding-left : 0px !important;',
                        ui: 'default',
                        boxLabelCls: 'checkboxLabelLeftAlign',
                        boxLabel: app.localize('EnableACHDirectDeposit')


                 },
                  {
                      xtype: 'textfield',
                      name: 'achDestinationCode',
                      allowBlank: false,
                      fieldLabel: app.localize('ACHDestinationCodeLabel'),
                      width: '100%',
                      ui: 'fieldLabelTop',
                      emptyText: app.localize('MandatoryField')
                  }
                    ]
        },
                  {
                      columnWidth: .3,
                      padding: '20 10 0 20',
                      defaults: {
                          // labelAlign: 'top',
                          labelWidth: 150//,
                          //blankText: app.localize('MandatoryToolTipText')
                      },
                      items: [
                {
                    xtype: 'textfield',
                    name: 'achDestinationName',
                    allowBlank: false,
                    fieldLabel: app.localize('ACHDestinationNameLabel'),
                    width: '100%',
                    ui: 'fieldLabelTop',
                    emptyText: app.localize('MandatoryField')
                },
                    {
                        xtype: 'textfield',
                        name: 'achOriginCode',
                        allowBlank: false,
                        fieldLabel: app.localize('ACHOriginCodeLabel'),
                        width: '100%',
                        ui: 'fieldLabelTop',
                        emptyText: app.localize('MandatoryField')
                    }]
                  },
                  {
                      columnWidth: .3,
                      padding: '20 10 0 20',
                      defaults: {
                          // labelAlign: 'top',
                          labelWidth: 120//,
                          //blankText: app.localize('MandatoryToolTipText')
                      },
                      items: [{
                          xtype: 'textfield',
                          name: 'achOriginName',
                          allowBlank: false,
                          fieldLabel: app.localize('ACHOriginNameLabel'),
                          width: '100%',
                          ui: 'fieldLabelTop',
                          emptyText: app.localize('MandatoryField')
                      }]
                  }]
              }]
        }]

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
        //                text: app.localize('SaveBank').toUpperCase(),
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