
Ext.define('Chaching.view.purchaseorders.entry.PurchaseOrderForm',{
    extend: 'Chaching.view.common.form.ChachingTransactionFormPanel',
    alias: ['widget.purchaseorders.entry.create', 'widget.purchaseorders.entry.edit'],

    requires: [
        'Chaching.view.purchaseorders.entry.PurchaseOrderFormController',
        'Chaching.view.purchaseorders.entry.PurchaseOrderDetailGrid',
        'Chaching.view.purchaseorders.entry.PurchaseOrderDetailHistoryGrid'
    ],
    modulePermissions: {
        read: abp.auth.isGranted('Pages.PurchaseOrders.Entry'),
        create: abp.auth.isGranted('Pages.PurchaseOrders.Entry.Create'),
        edit: abp.auth.isGranted('Pages.PurchaseOrders.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.PurchaseOrders.Entry.Delete')
    },
    openInPopupWindow: false,
    layout: 'fit',
    autoScroll: false,
    border: false,
    frame: false,
    controller: 'purchaseorders-entry-purchaseorderform',
    initComponent:function() {
        var me = this;
        me.items = [
            {
                xtype: 'hiddenfield',
                name: 'accountingDocumentId',
                value: 0
            }, {
                xtype: 'hiddenfield',
                name: 'organizationUnitId',
                value: null
            }, {
                xtype: 'hiddenfield',
                name: 'typeOfAccountingDocumentId',
                value: 6
            }, {
                xtype: 'container',
                layout: {
                    type: 'column',
                    columns: 4
                },
                items: [
                {
                    columnWidth: .25,
                    padding: '0 5 0 10',
                    defaults: {
                        ui: 'fieldLabelTop',
                        width: '100%',
                        labelWidth: 90
                    },
                    items:[
                    {
                        xtype: 'textfield',
                        name: 'documentReference',
                        itemId: 'documentReference',
                        fieldLabel: app.localize('PO#'),
                        allowBlank: false,
                        emptyText: app.localize('MandatoryField')
                    }, {
                        xtype: 'chachingcombobox',
                        store: new Chaching.store.utilities.autofill.VendorsStore(),
                        fieldLabel: app.localize('Vendor'),
                        ui: 'fieldLabelTop',
                        width: '100%',
                        name: 'vendorId',
                        allowBlank:false,
                        valueField: 'vendorId',
                        displayField: 'vendorName',
                        queryMode: 'remote',
                        minChars: 2,
                        modulePermissions: {
                            read: abp.auth.isGranted('Pages.Payables.Vendors'),
                            create: abp.auth.isGranted('Pages.Payables.Vendors.Create'),
                            edit: abp.auth.isGranted('Pages.Payables.Vendors.Edit'),
                            destroy: abp.auth.isGranted('Pages.Payables.Vendors.Delete')
                        },
                        primaryEntityCrudApi: null,
                        createEditEntityType: 'payables.vendors',
                        createEditEntityGridController: 'payables-vendors-vendorsgrid',
                        entityType: 'Vendor'
                    }, {
                        xtype: 'textfield',
                        name: 'description',
                        itemId: 'description',
                        allowBlank: false,
                        fieldLabel: app.localize('Description'),
                        emptyText: app.localize('MandatoryField')
                    }, {
                        xtype: 'datefield',
                        name: 'transactionDate',
                        itemId: 'transactionDate',
                        allowBlank: false,
                        format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                        emptyText: app.localize('MandatoryField'),
                        fieldLabel: app.localize('InvoiceDate')
                    }]

                }, {
                    columnWidth: .25,
                    padding: '0 5 0 10',
                    defaults: {
                        ui: 'fieldLabelTop',
                        width: '100%',
                        labelWidth: 90
                    },
                    items: [{
                            xtype: 'amountfield',
                            name: 'poOriginalAmount',
                            itemId: 'poOriginalAmount',
                            fieldLabel: app.localize('OrigAmount'),
                            disabled:true,
                            allowBlank: true
                        },
                    {
                        xtype: 'amountfield',
                        name: 'controlTotal',
                        itemId: 'controlTotal',
                        fieldLabel: app.localize('InvoiceTotal'),
                        allowBlank: false,
                        emptyText: app.localize('MandatoryField')
                    }, {
                        xtype: 'amountfield',
                        name: 'remainingBalance',
                        itemId: 'remainingBalance',
                        fieldLabel: app.localize('RemainingBalance'),
                        disabled:true
                    }, {
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
                            change:'changeCurrency'
                        }
                    }]
                }, {
                    columnWidth: .25,
                    padding: '0 5 0 10',
                    defaults: {
                        ui: 'fieldLabelTop',
                        width: '100%',
                        labelWidth: 90
                    },
                    items: [{
                        xtype: 'datefield',
                        name: 'dateNeededBy',
                        itemId: 'dateNeededBy',
                        allowBlank: true,
                        format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                        emptyText: Chaching.utilities.ChachingGlobals.defaultDateFormat,
                        fieldLabel: app.localize('DateNeeded')
                    }, {
                        xtype: 'textfield',
                        name: 'timeNeededBy',
                        itemId: 'timeNeededBy',
                        fieldLabel:app.localize('TimeNeeded')
                    }, {///TODO:replace with autofill
                        xtype: 'textfield',
                        name: 'cardId',
                        itemId: 'cardId',
                        fieldLabel: app.localize('CardInfo')
                    }]
                }, {
                    columnWidth: .25,
                    padding: '0 5 0 10',
                    defaults: {
                        ui: 'fieldLabelTop',
                        width: '100%',
                        labelWidth: 90
                    },
                    items:[
                    {
                        xtype: 'checkboxgroup',
                        columns: 2,
                        vertical: true,
                        defaults: {
                            boxLabelCls: 'checkboxLabel'
                        },
                        
                        items: [
                            { boxLabel: app.localize('PettyCash'), name: 'isPettyCash', inputValue: 'true' },
                            { boxLabel: app.localize('CrDrCard'), name: 'isCreditCard', inputValue: 'true' },
                            { boxLabel: app.localize('WillBill'), name: 'isWillBill', inputValue: 'true',padding:'0 0 0 10' },
                            {
                                boxLabel: app.localize('Close'), name: 'isRetired', inputValue: 'true', padding: '0 0 0 10',
                                listeners: {
                                    change: 'onCloseCheckChange'
                                }
                            }
                        ]
                    }, {
                        xtype: 'datefield',
                        name: 'closeDate',
                        itemId: 'closeDate',
                        allowBlank: true,
                        hidden:true,
                        format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                        emptyText: Chaching.utilities.ChachingGlobals.defaultDateFormat,
                        fieldLabel: app.localize('DateClose')
                    }]
                }, {
                    columnWidth: 1,
                    itemId: 'transactionDetails',
                    items:[
                    {
                        xtype: 'tabpanel',
                        ui: 'formTabPanels',
                        items: [
                            {
                                xtype: 'purchaseorders.entry.transactionDetails',
                                title: app.localize('DistributionDetails').initCap(),
                                iconCls: 'fa fa-list',
                                isTransactionDetailGrid: true
                            }, {
                                title: app.localize('DetailHistory').initCap(),
                                iconCls: 'fa fa-history',
                                xtype: 'purchaseorders.entry.transactionDetailsHistory',
                                isHistoryGrid: true,
                                hidden:true
                            }
                        ]
                    }]
                    
                }]
            }
        ];
        me.callParent(arguments);
    }
});
