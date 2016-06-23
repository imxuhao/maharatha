Ext.define('Chaching.view.receivables.customers.CustomerForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.receivables.customers.create', 'widget.receivables.customers.edit'],
    requires: [
        'Chaching.view.receivables.customers.CustomerFormController'
    ],
    controller: 'receivables-customers-customersform',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Receivables.Customers'),
        create: true,//abp.auth.isGranted('Pages.Receivables.Customers.Create'),
        edit: true,//abp.auth.isGranted('Pages.Receivables.Customers.Edit'),
        destroy: true//abp.auth.isGranted('Pages.Receivables.Customers.Delete'),
    },
    name: 'Customers',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    autoScroll: true,
    border: false,
    showFormTitle: true,
    displayDefaultButtonsCenter: true,


    titleConfig: {
        title: abp.localization.localize("CreatingNewCustomers").initCap()
    },
    items: [{
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
                    columnWidth: .5,
                    padding: '20 10 0 20',
                    defaults: {
                        // labelAlign: 'top',
                        labelWidth: 140,
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items: [{
                        xtype: 'hiddenfield',
                        name: 'customerId',
                        value: 0
                    }, {
                        xtype: 'textfield',
                        name: 'lastName',
                        itemId: 'lastName',
                        allowBlank: false,
                        fieldLabel: app.localize('CompanyLastName').initCap(),
                        width: '100%',
                        ui: 'fieldLabelTop',
                        emptyText: app.localize('MandatoryField')
                    },
                         {
                             xtype: 'textfield',
                             name: 'customerNumber',
                             itemId: 'customerNumber',
                             fieldLabel: app.localize('CustomerNumber').initCap(),
                             width: '100%',
                             ui: 'fieldLabelTop'
                         }
                    ]
                },
                {
                    columnWidth: .5,
                    padding: '20 10 0 20',
                    defaults: {
                        // labelAlign: 'top',
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items: [{
                        xtype: 'textfield',
                        name: 'firstName',
                        itemId: 'firstName',
                        fieldLabel: app.localize('FirstName').initCap(),
                        width: '100%',
                        ui: 'fieldLabelTop'
                    },
                    {
                        xtype: 'checkbox',
                        name: 'isActive',
                        itemId: 'isActive',
                        labelAlign: 'right',
                        inputValue: true,
                        checked: true,
                        ui: 'default',
                        boxLabelCls: 'checkboxLabel',
                        boxLabel: app.localize('Active')
                    }
                    //, {
                    //    xtype: 'textfield',
                    //    name: 'billingAccount',
                    //    itemId: 'billingAccount',
                    //    fieldLabel: app.localize('BillingAccount').initCap(),
                    //    width: '100%',
                    //    ui: 'fieldLabelTop'
                    //}

                    ]
                }

                ,
                    //{
                    //    columnWidth: .33,
                    //    padding: '20 10 0 20',
                    //    defaults: {
                    //        // labelAlign: 'top',
                    //        blankText: app.localize('MandatoryToolTipText')
                    //    },
                    //    items: [
                    //    {
                    //        xtype: 'checkbox',
                    //        name: 'isActive',
                    //        itemId: 'isActive',
                    //        labelAlign: 'right',
                    //        inputValue: true,
                    //        checked: true,
                    //        ui: 'default',
                    //        boxLabelCls: 'checkboxLabel',
                    //        boxLabel: app.localize('Active')
                    //    }
                    //    ]
                    //},


                    {
                        columnWidth: 1,
                        padding: '20 10 0 20',
                        defaults: {
                            labelAlign: 'top',
                            blankText: app.localize('MandatoryToolTipText')
                        },
                        items: [{
                            xtype: 'address',
                            itemId: 'addressGrid',
                            layout: 'fit',
                            width: '100%'
                        }]
                    }]
            }
            ]
        },
        {
            title: abp.localization.localize("Other").initCap(),
            iconCls: 'fa fa-gear',
            items: [
              {
                  xtype: 'fieldset',
                  collapsible: true,
                  title: abp.localization.localize("PaymentInformation").initCap(),
                  ui: 'transparentFieldSet',
                  layout: 'column',
                  items: [
        {
            columnWidth: .5,
            padding: '20 10 0 20',
            defaults: {
                labelWidth: 120
                //labelAlign: 'top',
                //blankText: app.localize('MandatoryToolTipText')
            },
            items: [
                 {
                     xtype: 'textfield',
                     name: 'creditLimit',
                     itemId: 'creditLimit',
                     fieldLabel: app.localize('CreditLimit').initCap(),
                     width: '100%',
                     ui: 'fieldLabelTop'
                 },

              {
                  xtype: 'combobox',
                  name: 'customerPayTermsId',
                  fieldLabel: app.localize('PaymentTerms').initCap(),
                  width: '100%',
                  ui: 'fieldLabelTop',
                  displayField: 'paymentTerms',
                  valueField: 'paymentTermsId',
                  emptyText: app.localize('SelectOption')

              }]
        },
                  {
                      columnWidth: .5,
                      padding: '20 10 0 20',
                      defaults: {
                          labelWidth: 120
                          //labelAlign: 'top',
                          //blankText: app.localize('MandatoryToolTipText')
                      },
                      items: [
                 {
                     xtype: 'combobox',
                     name: 'typeofPaymentMethodId',
                     fieldLabel: app.localize('PaymentMethods').initCap(),
                     width: '100%',
                     ui: 'fieldLabelTop',
                     displayField: 'typeofPaymentMethod',
                     valueField: 'typeofPaymentMethodId',
                     emptyText: app.localize('SelectOption')
                     //,
                     //bind: {
                     //    store: '{paymentTermsList}'
                     //}
                 },
              {
                  xtype: 'combobox',
                  name: 'salesRepId',
                  fieldLabel: app.localize('SalesRep').initCap(),
                  width: '100%',
                  ui: 'fieldLabelTop',
                  displayField: 'salesRepName',
                  valueField: 'salesRepId',
                  emptyText: app.localize('SelectOption')

              }]
                  }

                  ]
              }]


        }]

    }]
});