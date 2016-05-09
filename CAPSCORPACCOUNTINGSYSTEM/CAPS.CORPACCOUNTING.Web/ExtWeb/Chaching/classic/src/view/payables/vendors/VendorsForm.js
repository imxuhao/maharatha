Ext.define('Chaching.view.payables.vendors.VendorsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.payables.vendors.create', 'widget.payables.vendors.edit'],
    requires: [
        'Chaching.view.payables.vendors.VendorsFormController'
    ],
    controller: 'payables-vendors-vendorsform',
    name: 'vendors',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    autoScroll: true,
    border: false,
    showFormTitle: true,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CreatingNewVendors").initCap()
    },
    items: {
        xtype: 'tabpanel',
        ui: 'formTabPanels',
        items: [
        {
            title: abp.localization.localize("General").initCap(),
            iconCls: 'fa fa-gear',
            items: [{
                xtype: 'fieldset',
                ui: 'transparentFieldSet',
                title: abp.localization.localize("GeneralInformation").initCap(),
                collapsible: true,
                layout: 'column',
                items: [{
                    columnWidth: .33,
                    padding: '20 10 0 20',
                    defaults: {
                        labelAlign: 'top',
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items: [{
                        xtype: 'hiddenfield',
                        name: 'vendorId',
                        value: 0
                    }, {
                        xtype: 'textfield',
                        name: 'lastName',
                        itemId: 'lastName',
                        allowBlank: false,
                        fieldLabel: app.localize('CompanyName').initCap() + Chaching.utilities.ChachingGlobals.mandatoryFlag,
                        width: '100%',
                        ui: 'fieldLabelTop',
                        emptyText: app.localize('MandatoryField')
                    },
                         {
                             xtype: 'textfield',
                             name: 'vendorNumber',
                             itemId: 'vendorNumber',
                             fieldLabel: app.localize('VendorNumber').initCap(),
                             width: '100%',
                             ui: 'fieldLabelTop'
                         }
                    ]
                },
                {
                    columnWidth: .33,
                    padding: '20 10 0 20',
                    defaults: {
                        labelAlign: 'top',
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items: [{
                        xtype: 'textfield',
                        name: 'firstName',
                        itemId: 'firstName',
                        fieldLabel: app.localize('PayToName').initCap(),
                        width: '100%',
                        ui: 'fieldLabelTop'
                    }, {
                        xtype: 'textfield',
                        name: 'billingAccount',
                        itemId: 'billingAccount',
                        fieldLabel: app.localize('BillingAccount').initCap(),
                        width: '100%',
                        ui: 'fieldLabelTop'
                    }]
                },
                            {
                                columnWidth: .33,
                                padding: '20 10 0 20',
                                defaults: {
                                    labelAlign: 'top',
                                    blankText: app.localize('MandatoryToolTipText')
                                },
                                items: [{
                                    xtype: 'combobox',
                                    name: 'typeofVendorId',
                                    fieldLabel: app.localize('Type').initCap(),
                                    width: '100%',
                                    ui: 'fieldLabelTop',
                                    displayField: 'typeofvendor',
                                    valueField: 'typeofvendorId',
                                    emptyText: app.localize('SelectOption'),
                                    bind: {
                                        store: '{vendorTypeList}'
                                    }
                                }, {
                                    xtype: 'checkbox',
                                    name: 'isActive',
                                    itemId: 'isActive',
                                    labelAlign: 'right',
                                    inputValue: true,
                                    checked: true,
                                    ui: 'default',
                                    boxLabelCls: 'checkboxLabel',
                                    boxLabel: app.localize('Active')
                                }]
                            }, {
                                columnWidth: 1,
                                padding: '20 10 0 20',
                                defaults: {
                                    labelAlign: 'top',
                                    blankText: app.localize('MandatoryToolTipText')
                                },
                                items: [{
                                    xtype: 'address',
                                    layout: 'fit',
                                    width: '100%'
                                }]
                            }]
            },
               {
                   xtype: 'fieldset',
                   ui: 'transparentFieldSet',
                   collapsible: true,
                   title: abp.localization.localize("TaxInformation").initCap(),
                   layout: 'column',
                   items: [{
                       columnWidth: .4,
                       padding: '20 10 0 20',
                       defaults: {
                           labelAlign: 'top',
                           blankText: app.localize('MandatoryToolTipText')
                       },
                       items: [
                                                {
                                                    xtype: 'combobox',
                                                    name: 'typeOfTaxId',
                                                    fieldLabel: app.localize('Type').initCap(),
                                                    width: '100%',
                                                    ui: 'fieldLabelTop',
                                                    displayField: 'typeOfTax',
                                                    valueField: 'typeOfTaxId',
                                                    emptyText: app.localize('SelectOption'),
                                                    bind: {
                                                        store: '{typeOfTaxList}'
                                                    }
                                                }, {
                                                    xtype: 'textfield',
                                                    name: 'dbaName',
                                                    itemId: 'dbaName',
                                                    fieldLabel: app.localize('LegalName').initCap(),
                                                    width: '100%',
                                                    ui: 'fieldLabelTop'
                                                }]
                   },
                   {
                       columnWidth: .3,
                       padding: '20 10 0 20',
                       defaults: {
                           labelAlign: 'top',
                           blankText: app.localize('MandatoryToolTipText')
                       },
                       items: [

                           {
                               xtype: 'textfield',
                               name: 'ssnTaxId',
                               itemId: 'ssnTaxId',
                               fieldLabel: app.localize('SSN/TaxID'),
                               width: '100%',
                               ui: 'fieldLabelTop'
                           }, {
                               xtype: 'combobox',
                               name: 'typeof1099BoxId',
                               fieldLabel: app.localize('1099s').initCap(),
                               width: '100%',
                               ui: 'fieldLabelTop',
                               displayField: 'typeof1099Box',
                               valueField: 'typeof1099BoxId',
                               emptyText: app.localize('SelectOption'),
                               bind: {
                                   store: '{typeof1099BoxList}'
                               }
                           }]
                   },
                   {
                       columnWidth: .3,
                       padding: '20 10 0 20',
                       defaults: {
                           labelAlign: 'top',
                           blankText: app.localize('MandatoryToolTipText')
                       },
                       items: [
                       {
                           xtype: 'textfield',
                           name: 'fedralTaxId',
                           itemId: 'fedralTaxId',
                           fieldLabel: app.localize('FedTaxID'),
                           width: '100%',
                           ui: 'fieldLabelTop'
                       }, {
                           xtype: 'checkbox',
                           boxLabel: app.localize('W9OnFile'),
                           name: 'isw9OnFile',
                           labelAlign: 'right',
                           inputValue: true,
                           checked: false,
                           boxLabelCls: 'checkboxLabel'
                       }]
                   }]
               }]
        },
        {
            title: abp.localization.localize("Other").initCap(),
            iconCls: 'fa fa-gear',
            items: [
              {
                  xtype: 'fieldset',
                  collapsible: true,
                  title: abp.localization.localize("DefaultsSection").initCap(),
                  ui: 'transparentFieldSet',
                  layout: 'column',
                  items: [
        {
            columnWidth: .4,
            padding: '20 10 0 20',
            defaults: {
                labelAlign: 'top',
                blankText: app.localize('MandatoryToolTipText')
            },
            items: [
              {
                  xtype: 'combobox',
                  name: 'paymentTermsId',
                  fieldLabel: app.localize('PaymentTerms').initCap(),
                  width: '100%',
                  ui: 'fieldLabelTop',
                  displayField: 'paymentTerms',
                  valueField: 'paymentTermsId',
                  emptyText: app.localize('SelectOption'),
                  bind: {
                      store: '{paymentTermsList}'
                  }
              }, {
                  xtype: 'combobox',
                  name: 'glAccountId',
                  fieldLabel: app.localize('GLAccount').initCap(),
                  width: '100%',
                  ui: 'fieldLabelTop',
                  displayField: '',
                  valueField: '',
                  emptyText: app.localize('SelectOption'),
                  bind: {
                      store: '{getAccountsList}'
                  }
              }]
        },
                  {
                      columnWidth: .3,
                      padding: '20 10 0 20',
                      defaults: {
                          labelAlign: 'top',
                          blankText: app.localize('MandatoryToolTipText')
                      },
                      items: [
              {
                  xtype: 'textfield',
                  name: 'taxCredit',
                  itemId: 'taxCredit',
                  fieldLabel: app.localize('TaxCredit').initCap(),
                  width: '100%',
                  ui: 'fieldLabelTop'
              }, {
                  xtype: 'combobox',
                  name: 'accountId',
                  fieldLabel: app.localize('Line#').initCap(),
                  width: '100%',
                  ui: 'fieldLabelTop',
                  displayField: '',
                  valueField: '',
                  emptyText: app.localize('SelectOption'),
                  bind: {
                      store: '{getAccountsList}'
                  }
              }]
                  },
                  {
                      columnWidth: .3,
                      padding: '20 10 0 20',
                      defaults: {
                          labelAlign: 'top',
                          blankText: app.localize('MandatoryToolTipText')
                      },
                      items: [{
                          xtype: 'combobox',
                          name: 'jobId',
                          fieldLabel: app.localize('Division').initCap(),
                          width: '100%',
                          ui: 'fieldLabelTop',
                          displayField: 'rollupDivision',
                          valueField: 'rollupDivisionId',
                          emptyText: app.localize('SelectOption'),
                          bind: {
                              store: '{rollupDivisionList}'
                          }
                      }]
                  },
                  {
                      columnWidth: 1,
                      padding: '20 10 0 20',
                      defaults: {
                          labelAlign: 'top',
                          blankText: app.localize('MandatoryToolTipText')
                      },
                      items: [{
                          xtype: 'vendoralias',
                          itemId: 'vendorAliasGrid',
                          width: 450
                          //layout: 'fit',
                          //width: '40%'
                      }
                      ]
                  }]
              },
              //{
              //    xtype: 'fieldset',
              //    title: abp.localization.localize("VendorAliasSection").initCap(),
              //    collapsible: true,
              //    ui: 'transparentFieldSet',
              //    items: [
              //        {
              //            xtype: 'vendoralias',
              //            itemId:'vendorAliasGrid',
              //            width:450
              //            //layout: 'fit',
              //            //width: '40%'
              //        }
              //    ]
              //},
            {
                xtype: 'fieldset',
                title: abp.localization.localize("NotesSection").initCap(),
                collapsible: true,
                ui: 'transparentFieldSet',
                items: [{
                    xtype: 'textareafield',
                    grow: true,
                    name: 'notes',
                    fieldLabel: app.localize('Notes').initCap(),
                    anchor: '50%',
                    ui: 'fieldLabelTop'
                }]

            }]
        }]
    }
});