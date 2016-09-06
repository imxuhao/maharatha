Ext.define('Chaching.view.receivables.customers.CustomerForm',
{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.receivables.customers.create', 'widget.receivables.customers.edit'],
    requires: [
        'Chaching.view.receivables.customers.CustomerFormController'
    ],
    controller: 'receivables-customers-customersform',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Receivables.Customers'),
        create: abp.auth.isGranted('Pages.Receivables.Customers.Create'),
        edit: abp.auth.isGranted('Pages.Receivables.Customers.Edit'),
        destroy: abp.auth.isGranted('Pages.Receivables.Customers.Delete'),
        attach: abp.auth.isGranted('Pages.Receivables.Customers.Attach')
    },
    name: 'Customers',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    autoScroll: true,
    border: false,
    showFormTitle: false,
    items: [
        {
            xtype: 'tabpanel',
            ui: 'formTabPanels',
            items: [
                {
                    title: abp.localization.localize("ContactInformation"),
                    iconCls: 'fa fa-gear',
                    layout: 'column',
                    items: [
                        {
                            columnWidth: .5,
                            padding: '20 10 0 20',
                            defaults: {
                                labelWidth: 170,
                                blankText: app.localize('MandatoryToolTipText')
                            },
                            items: [
                                {
                                    xtype: 'hiddenfield',
                                    name: 'customerId',
                                    value: 0
                                }, {
                                    xtype: 'textfield',
                                    name: 'lastName',
                                    itemId: 'lastName',
                                    allowBlank: false,
                                    fieldLabel: app.localize('AgencyCompanyName'),
                                    width: '100%',
                                    ui: 'fieldLabelTop',
                                    emptyText: app.localize('MandatoryField')
                                //}, {
                                //    xtype: 'textfield',
                                //    name: 'customerNumber',
                                //    itemId: 'customerNumber',
                                //    fieldLabel: app.localize('CustomerNumber'),
                                //    width: '100%',
                                //    ui: 'fieldLabelTop'
                                }
                            ]
                        },
                        {
                            columnWidth: .5,
                            padding: '20 10 0 20',
                            defaults: {
                                blankText: app.localize('MandatoryToolTipText')
                            },
                            items: [
                                {
                                    xtype: 'textfield',
                                    name: 'customerNumber',
                                    itemId: 'customerNumber',
                                    fieldLabel: app.localize('CustomerNumber'),
                                    width: '100%',
                                    ui: 'fieldLabelTop'
                                }
                                //{
                                //    xtype: 'textfield',
                                //    name: 'firstName',
                                //    itemId: 'firstName',
                                //    fieldLabel: app.localize('FirstName'),
                                //    width: '100%',
                                //    ui: 'fieldLabelTop'
                                //},
                                //{
                                //    xtype: 'checkbox',
                                //    name: 'isActive',
                                //    itemId: 'isActive',
                                //    labelAlign: 'right',
                                //    inputValue: true,
                                //    checked: true,
                                //    ui: 'default',
                                //    boxLabelCls: 'checkboxLabel',
                                //    boxLabel: app.localize('Active')
                                //}
                            ]
                        },
                        {
                            columnWidth: 1,
                            defaults: {
                                labelAlign: 'top',
                                blankText: app.localize('MandatoryToolTipText')
                            },
                            layout: 'fit',
                            items: [
                                {
                                    xtype: 'address',
                                    itemId: 'addressGrid',
                                    layout: 'fit',
                                    width: '100%'
                                }
                            ]
                        }
                    ]
                },
                {
                    title: abp.localization.localize("CreditInformation"),
                    iconCls: 'fa fa-gear',
                    layout: 'column',
                    items: [
                        {
                            columnWidth: .5,
                            padding: '20 10 0 20',
                            defaults: {
                                labelWidth: 120
                            },
                            items: [
                                {
                                    xtype: 'amountfield',
                                    name: 'creditLimit',
                                    itemId: 'creditLimit',
                                    fieldLabel: app.localize('CreditLimit'),
                                    width: '100%',
                                    ui: 'fieldLabelTop'
                                },
                                {
                                    xtype: 'combobox',
                                    name: 'customerPayTermsId',
                                    fieldLabel: app.localize('PaymentTerms'),
                                    width: '100%',
                                    ui: 'fieldLabelTop',
                                    displayField: 'paymentTerms',
                                    valueField: 'paymentTermsId',
                                    emptyText: app.localize('SelectOption')

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
                                }
                            ]
                        },
                        {
                            columnWidth: .5,
                            padding: '20 10 0 20',
                            defaults: {
                                labelWidth: 120
                            },
                            items: [
                                //{
                                //    xtype: 'combobox',
                                //    name: 'typeofPaymentMethodId',
                                //    fieldLabel: app.localize('PaymentMethods'),
                                //    width: '100%',
                                //    ui: 'fieldLabelTop',
                                //    displayField: 'typeofPaymentMethod',
                                //    valueField: 'typeofPaymentMethodId',
                                //    emptyText: app.localize('SelectOption')
                                //    //,
                                //    //bind: {
                                //    //    store: '{paymentTermsList}'
                                //    //}
                                //},
                                {
                                    xtype: 'combobox',
                                    name: 'typeOfCurrencyId',
                                    itemId: 'typeOfCurrencyId',
                                    queryMode: 'local',
                                    bind: {
                                        store: '{typeOfCurrencyList}'
                                    },
                                    valueField: 'typeOfCurrencyId',
                                    displayField: 'typeOfCurrency',
                                    width: '100%',
                                    ui: 'fieldLabelTop',
                                    fieldLabel: app.localize('Currency'),
                                    emptyText: app.localize('SelectOption'),
                                    listeners: {
                                        change: 'changeCurrency'
                                    }
                                },
                                {
                                    xtype: 'combobox',
                                    name: 'salesRepId',
                                    fieldLabel: app.localize('SalesRep'),
                                    width: '100%',
                                    ui: 'fieldLabelTop',
                                    displayField: 'salesRepName',
                                    valueField: 'salesRepId',
                                    emptyText: app.localize('SelectOption')

                                }
                            ]
                        }
                    ]

                }
            ]

        }
    ]
});