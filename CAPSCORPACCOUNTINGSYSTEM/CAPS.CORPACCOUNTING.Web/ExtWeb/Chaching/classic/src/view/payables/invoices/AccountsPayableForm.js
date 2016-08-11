
Ext.define('Chaching.view.payables.invoices.AccountsPayableForm',{
    extend: 'Chaching.view.common.form.ChachingTransactionFormPanel',
    alias: ['widget.payables.invoices.create', 'widget.payables.invoices.edit'],
    requires: [
        'Chaching.view.payables.invoices.AccountsPayableFormController',
        'Chaching.view.payables.invoices.AccountsPayableDetailGrid'
    ],

    controller: 'payables-invoices-accountspayableform',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Payables.Invoices'),
        create: abp.auth.isGranted('Pages.Payables.Invoices.Create'),
        edit: abp.auth.isGranted('Pages.Payables.Invoices.Edit'),
        destroy: abp.auth.isGranted('Pages.Payables.Invoices.Delete')
    },
    openInPopupWindow: false,
    layout: 'fit',
    autoScroll: false,
    border: false,
    frame: false,
    rbar: [
    {
        xtype: 'panel',
        ui: 'summaryPanel',
        layout: { type: 'vbox'},
        title: app.localize('VendorSnapshot'),
        collapsed: true,
        collapsible: true,
        collapseDirection: 'right',
        headerPosition: 'top',
        flex: 1,
        width: 350,
        border: true,
        items:[
        {
            xtype: 'gridpanel',
            title: app.localize('RecentPayables'),
            width: '100%',
            ui: 'summaryPanel',
            cls: 'chaching-transactiongrid',
            flex: 1,
            padding:'5 0 0 0',
            store: {
                ///TODO:Replace with live data
                fields: [{ name: 'referenceNumber' }, { name: 'transactionDate' }, { name: 'amount' }],
                data: [
                    { referenceNumber: '123', transactionDate: '08-01-2016', amount: '100' },
                    { referenceNumber: '456', transactionDate: '08-01-2016', amount: '102' },
                    { referenceNumber: '789', transactionDate: '08-01-2016', amount: '1000' },
                    { referenceNumber: '101', transactionDate: '08-01-2016', amount: '2054' },
                    { referenceNumber: '102', transactionDate: '08-01-2016', amount: '4520' }
                ]
            },
            columns:[
            {
                xtype: 'gridcolumn',
                text: app.localize('Invoice#'),
                dataIndex: 'referenceNumber',
                width:'24%'
            }, {
                xtype: 'gridcolumn',
                text: app.localize('TransDate'),
                dataIndex: 'transactionDate',
                renderer:ChachingRenderers.renderDateOnly,
                width: '35%'
            }, {
                xtype: 'gridcolumn',
                text: app.localize('Amount'),
                dataIndex: 'amount',
                width: '40%'
            }]
        }, {
            xtype: 'gridpanel',
            title: app.localize('Payments'),
            width: '100%',
            ui: 'summaryPanel',
            cls: 'chaching-transactiongrid',
            flex: 1,
            padding: '0 0 0 0',
            store: {
                fields: [{ name: 'checkNumber' }, { name: 'checkDate' }, { name: 'isPaid' }, { name: 'amount' }],
                data: [
                    { checkNumber: '123', checkDate: '08-01-2016', amount: '100',isPaid:false },
                    { checkNumber: '456', checkDate: '08-01-2016', amount: '102', isPaid: false },
                    { checkNumber: '789', checkDate: '08-01-2016', amount: '1000', isPaid: true },
                    { checkNumber: '101', checkDate: '08-01-2016', amount: '2054', isPaid: false },
                    { checkNumber: '102', checkDate: '08-01-2016', amount: '4520', isPaid: true }
                ]
            },
            columns: [
            {
                xtype: 'gridcolumn',
                text: app.localize('Check#'),
                dataIndex: 'checkNumber',
                width: '25%'
            }, {
                xtype: 'gridcolumn',
                text: app.localize('CheckDate'),
                renderer: ChachingRenderers.renderDateOnly,
                dataIndex: 'checkDate',
                width: '30%'
            }, {
                xtype: 'gridcolumn',
                text: app.localize('IsPaid'),
                renderer: ChachingRenderers.rightWrongMarkRenderer,
                dataIndex: 'isPaid',
                width:'19%'
            }, {
                xtype: 'gridcolumn',
                text: app.localize('Amount'),
                dataIndex: 'amount',
                width: '25%'
            }]
        }, {
            xtype: 'gridpanel',
            title: app.localize('RecentPO'),
            width: '100%',
            itemId:'recentPos',
            ui: 'summaryPanel',
            cls: 'chaching-transactiongrid',
            flex: 1,
            padding: '0 0 0 0',
            tools: [
            {
                type: 'save',
                tooltip: app.localize('ProcessSelected'),
                handler:'onProcessPoClicked'
            }],
            store: {
                fields: [{ name: 'referenceNumber' }, { name: 'description' }, { name: 'transactionDate' }, { name: 'remainingAmount' }, { name: 'process' }],
                data: [
                   { referenceNumber: '123', transactionDate: '08-01-2016', remainingAmount: '100', description:'ABC' },
                   { referenceNumber: '456', transactionDate: '08-01-2016', remainingAmount: '102', description: 'ABC' },
                   { referenceNumber: '789', transactionDate: '08-01-2016', remainingAmount: '1000', description: 'ABC' },
                   { referenceNumber: '101', transactionDate: '08-01-2016', remainingAmount: '2054', description: 'ABC' },
                   { referenceNumber: '102', transactionDate: '08-01-2016', remainingAmount: '4520', description: 'ABC' }
                ]
            },
            columns: [
            {
                xtype: 'gridcolumn',
                text: app.localize('PO#'),
                dataIndex: 'referenceNumber',
                width: '14%'
            }, {
                xtype: 'gridcolumn',
                text: app.localize('Desciption'),
                dataIndex: 'description',
                width: '26%'
            }, {
                xtype: 'gridcolumn',
                text: app.localize('TransDate'),
                dataIndex: 'transactionDate',
                renderer: ChachingRenderers.renderDateOnly,
                width: '26%'
            },  {
                xtype: 'gridcolumn',
                text: app.localize('RemainingAmount'),
                dataIndex: 'remainingAmount',
                width: '25%'
            }, {
                xtype: 'checkcolumn',
                dataIndex: 'process',
                width: '8%'
            }]
        }]
    }],
    initComponent: function () {
        var me = this;
        me.tbar=[
            {
                xtype: 'radiogroup',
                columns: 3,
                layout: {
                    type: 'hbox'
                },
                vertical: false,
                defaults: {
                    boxLabelCls: 'radioGroupboxLabel'
                },
                items: [
                    { boxLabel: app.localize('Invoice'), name: 'typeOfInvoiceId', inputValue: '1', checked: true },
                    { boxLabel: app.localize('CreditMemo'), name: 'typeOfInvoiceId', inputValue: '2', padding: '0 0 0 20' },
                    { boxLabel: app.localize('QuickPay'), name: 'typeOfInvoiceId', inputValue: '3', padding: '0 0 0 20' }
                ],
                listeners: {
                    change: 'onInvoiceTypeChange'
                }
            }, '->', {
                xtype: 'button',
                scale: 'small',
                ui: 'actionButton',
                itemId: 'PostBtn',
                hidden:true,
                text: abp.localization.localize("Post").toUpperCase(),
                iconCls: 'fa  fa-files-o',
                iconAlign: 'left'
            }, {
                xtype: 'button',
                ui: 'actionButton',
                iconCls: 'fa fa-download',
                iconAlign: 'left',
                itemId: 'PrintBtn',
                hidden:true,
                menu: new Ext.menu.Menu({
                    ui: 'accounts',
                    items: [
                        { text: abp.localization.localize("PrintPDF").toUpperCase(), iconCls: 'fa fa-file-pdf-o', itemId: 'PrintPdf' },
                        { text: abp.localization.localize("PrintWord").toUpperCase(), iconCls: 'fa fa-file-word-o', itemId: 'PrintWord' },
                        { text: abp.localization.localize("Email").toUpperCase(), iconCls: 'fa fa-envelope-square', itemId: 'Email' }
                    ]
                })
            }
        ];
        me.items=[
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
                value: 3
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
                        items: [
                            {
                                xtype: 'textfield',
                                name: 'purchaseOrderReference',
                                itemId: 'purchaseOrderReference',
                                ui: 'fieldLabelTop',
                                width: '100%',
                                fieldLabel: app.localize('PO#'),
                                emptyText: app.localize('SearchText'),
                                triggers: {
                                    poRelief: {
                                        cls: 'purchaseOrderTriggerClsInactive',
                                        handler: function() {
                                            console.log('poRelief trigger clicked');
                                        }
                                    }
                                },
                                listeners: {
                                    change: function(field, newValue, oldValue) {
                                        ///TODO: Change to enter key press once poo flow work starts
                                        if (field.getValue().length > 3) {
                                            var poRel = field.getTrigger('poRelief');
                                            poRel.getEl().removeCls('x-form-trigger x-form-trigger-fieldLabelTop purchaseOrderTriggerClsInactive purchaseOrderTriggerClsInactive-fieldLabelTop');
                                            poRel.getEl().setCls('x-form-trigger x-form-trigger-fieldLabelTop purchaseOrderTriggerClsActive purchaseOrderTriggerClsActive-fieldLabelTop');
                                            //poRel.getEl().setCls('x-form-trigger-fieldLabelTop purchaseOrderTriggerClsActive');
                                            field.updateLayout();
                                        }
                                    }
                                }
                            }, {
                                xtype: 'chachingcombobox',
                                store: new Chaching.store.utilities.autofill.VendorsStore(),
                                fieldLabel: app.localize('Vendor'),
                                ui: 'fieldLabelTop',
                                width: '100%',
                                name: 'vendorId',
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
                                xtype: 'amountfield',
                                name: 'controlTotal',
                                itemId: 'controlTotal',
                                fieldLabel: app.localize('InvoiceTotal'),
                                allowBlank: false,
                                emptyText: app.localize('MandatoryField')
                            }
                        ]
                    }, {
                        columnWidth: .25,
                        padding: '0 5 0 10',
                        defaults: {
                            ui: 'fieldLabelTop',
                            width: '100%',
                            labelWidth: 90
                        },
                        items: [
                            {
                                xtype: 'textfield',
                                name: 'documentReference',
                                itemId: 'documentReference',
                                allowBlank: false,
                                fieldLabel: app.localize('Invoice#'),
                                emptyText: app.localize('MandatoryField')
                            }, {
                                xtype: 'datefield',
                                name: 'transactionDate',
                                itemId: 'transactionDate',
                                allowBlank: false,
                                format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                                emptyText: app.localize('MandatoryField'),
                                fieldLabel: app.localize('InvoiceDate')
                            }, {
                                xtype: 'datefield',
                                name: 'datePosted',
                                itemId: 'datePosted',
                                allowBlank: true,
                                format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                                emptyText: Chaching.utilities.ChachingGlobals.defaultDateFormat,
                                fieldLabel: app.localize('PostingDate')
                            }, {
                                xtype: 'datefield',
                                name: 'dueDate',
                                itemId: 'dueDate',
                                allowBlank: true,
                                format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                                emptyText: Chaching.utilities.ChachingGlobals.defaultDateFormat,
                                fieldLabel: app.localize('DueDate')
                            }
                        ]
                    }, {
                        columnWidth: .25,
                        padding: '0 5 0 10',
                        defaults: {
                            ui: 'fieldLabelTop',
                            width: '100%',
                            labelWidth: 90
                        },
                        items: [
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
                            }, {
                                xtype: 'combobox',
                                name: 'typeOfCheckGroupId',
                                itemId: 'typeOfCheckGroupId',
                                queryMode: 'local',
                                bind: {
                                    store: '{typeOfCheckGroup}'
                                },
                                valueField: 'typeOfCheckGroupId',
                                displayField: 'typeOfCheckGroup',
                                width: '100%',
                                ui: 'fieldLabelTop',
                                fieldLabel: app.localize('CheckGroup'),
                                emptyText: app.localize('SelectOption')
                            }, {
////TODO: Replace with combo once batch is ready
                                xtype: 'textfield',
                                name: 'batchId',
                                itemId: 'batchId',
                                ui: 'fieldLabelTop',
                                fieldLabel: app.localize('Batch'),
                                emptyText: app.localize('SelectOption')
                            }, {
                                xtype: 'textfield',
                                name: 'memoLine',
                                itemId: 'memoLine',
                                allowBlank: true,
                                fieldLabel: app.localize('MemoLine')
                            }
                        ]
                    }, {
                        columnWidth: .25,
                        padding: '0 5 0 10',
                        itemId: 'quickPaySection',
                        defaults: {
                            ui: 'fieldLabelTop',
                            width: '100%',
                            labelWidth: 80,
                            listeners: {
                                change: 'onQuickPayFieldChanged'
                            }
                        },
                        items: [
                            {
                                xtype: 'textfield',
                                name: 'adjustInvoice',
                                itemId: 'adjustInvoice',
                                allowBlank: true,
                                hidden: true,
                                fieldLabel: app.localize('AdjustInvoice')
                            }, {
                                xtype: 'chachingcombobox',
                                store: new Chaching.store.utilities.autofill.BankAccountListStore(),
                                fieldLabel: app.localize('Bank'),
                                ui: 'fieldLabelTop',
                                width: '100%',
                                name: 'bankAccountId',
                                valueField: 'bankAccountId',
                                displayField: 'description',
                                queryMode: 'remote',
                                minChars: 2,
                                hidden: true,
                                modulePermissions: {
                                    read: abp.auth.isGranted('Pages.Banking.BankSetup'),
                                    create: abp.auth.isGranted('Pages.Banking.BankSetup.Create'),
                                    edit: abp.auth.isGranted('Pages.Banking.BankSetup.Edit'),
                                    destroy: abp.auth.isGranted('Pages.Banking.BankSetup.Delete')
                                },
                                primaryEntityCrudApi: null,
                                createEditEntityType: 'banking.banksetup',
                                createEditEntityGridController: 'banking.banksetupgrid',
                                entityType: 'Bank'
                            }, {
                                xtype: 'datefield',
                                name: 'paymentDate',
                                itemId: 'paymentDate',
                                format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                                emptyText: app.localize('MandatoryField'),
                                hidden: true,
                                fieldLabel: app.localize('CheckDate')
                            }, {
////TODO: Replace with combo once payType is clarified
                                xtype: 'textfield',
                                name: 'payType',
                                itemId: 'payType',
                                ui: 'fieldLabelTop',
                                fieldLabel: app.localize('PayType'),
                                emptyText: app.localize('SelectOption'),
                                hidden: true
                            }, {
                                xtype: 'textfield',
                                name: 'paymentNumber',
                                itemId: 'paymentNumber',
                                ui: 'fieldLabelTop',
                                fieldLabel: app.localize('Check#'),
                                emptyText: app.localize('MandatoryField'),
                                hidden: true,
                                triggers: {
                                    printCheck: {
                                        cls: 'printTriggerClsInactive',
                                        handler: function(tri) {
                                            if (tri.allowAction) {
                                                abp.notify.success('Check Printing Comming Sooooooon...');
                                            }
                                        }
                                    }
                                }
                            }
                        ]
                    }, {
                        columnWidth: 1,
                        itemId: 'transactionDetails',
                        items: [
                            {
                                xtype: 'payables.invoices.transactionDetails',
                                isTransactionDetailGrid: true
                            }
                        ]
                    }
                ]
            }
        ];
        me.callParent(arguments);
    }
    
});
