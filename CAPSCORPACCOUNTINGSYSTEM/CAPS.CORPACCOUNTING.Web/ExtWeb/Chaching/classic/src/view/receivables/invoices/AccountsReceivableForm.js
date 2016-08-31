/**
 * Accounts list to display all records of Accounts Receivable Invoices entry form.
 */
Ext.define('Chaching.view.receivables.invoices.AccountsReceivableForm', {
    extend: 'Chaching.view.common.form.ChachingTransactionFormPanel',
    alias: ['widget.receivables.invoices.create', 'widget.receivables.invoices.edit'],
    requires: [
        'Chaching.view.receivables.invoices.AccountsReceivableFormController',
        'Chaching.view.receivables.invoices.AccountsReceivableDetailGrid'
    ],

    controller: 'receivables-invoices-accountsreceivableform',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Receivables.Invoices'),
        create: abp.auth.isGranted('Pages.Receivables.Invoices.Create'),
        edit: abp.auth.isGranted('Pages.Receivables.Invoices.Edit'),
        destroy: abp.auth.isGranted('Pages.Receivables.Invoices.Delete')
    },
    openInPopupWindow: false,
    layout: 'fit',
    autoScroll: false,
    border: false,
    frame: false,
    bbar: [
    {
        xtype: 'panel',
        ui: 'summaryPanel',
        itemId: 'summaryPanel',
        layout: { type: 'hbox' },
        title: app.localize('InvoiceBuilder'),
        collapsed: true,
        collapsible: true,
        collapseMode: 'header',
        collapseDirection: 'bottom',//right
        animCollapse: 0,
        headerPosition: 'top',
        //flex: 1,
        width: '100%',
        border: true,
        autoScroll: true,
        titleCollapse: true,
        items: [
        {
            xtype: 'gridpanel',
            title: app.localize('RecentPayables'),
            width: '100%',
            ui: 'summaryPanel',
            cls: 'chaching-transactiongrid',
            flex: 1,
            padding: '5 5 0 0',
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
            columns: [
            {
                xtype: 'gridcolumn',
                text: app.localize('Invoice#'),
                dataIndex: 'referenceNumber',
                width: '24%'
            }, {
                xtype: 'gridcolumn',
                text: app.localize('TransDate'),
                dataIndex: 'transactionDate',
                renderer: ChachingRenderers.renderDateOnly,
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
            padding: '5 5 0 0',
            store: {
                fields: [{ name: 'checkNumber' }, { name: 'checkDate' }, { name: 'isPaid' }, { name: 'amount' }],
                data: [
                    { checkNumber: '123', checkDate: '08-01-2016', amount: '100', isPaid: false },
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
                width: '19%'
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
            itemId: 'recentPos',
            ui: 'summaryPanel',
            cls: 'chaching-transactiongrid',
            flex: 1,
            padding: '5 0 0 0',
            viewConfig: {
                plugins: {
                    ddGroup: 'grid-to-form',
                    ptype: 'gridviewdragdrop',
                    enableDrop: false
                }
            },
            tools: [
            {
                type: 'save',
                tooltip: app.localize('ProcessSelected'),
                handler: 'onProcessPoClicked'
            }],
            store: new Chaching.store.purchaseorders.entry.PurchaseOrderStore(),
            /*store: {
                fields: [{ name: 'purchaseOrderReference' }, { name: 'description' }, { name: 'transactionDate' }, { name: 'remainingAmount' }, { name: 'process' }, { name: 'controlTotal', mapping: 'remainingAmount' }],
                data: [
                   { purchaseOrderReference: '123', transactionDate: '08-01-2016', remainingAmount: '100', description: 'ABC' },
                   { purchaseOrderReference: '456', transactionDate: '08-01-2016', remainingAmount: '102', description: 'ABC' },
                   { purchaseOrderReference: '789', transactionDate: '08-01-2016', remainingAmount: '1000', description: 'ABC' },
                   { purchaseOrderReference: '101', transactionDate: '08-01-2016', remainingAmount: '2054', description: 'ABC' },
                   { purchaseOrderReference: '102', transactionDate: '08-01-2016', remainingAmount: '4520', description: 'ABC' }
                ]
            },*/
            columns: [
            {
                xtype: 'gridcolumn',
                text: app.localize('Agency/Customer'),
                dataIndex: 'purchaseOrderReference',
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
            }, {
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
    onBoxReady: function () {
        this.callParent(arguments);
        var form = this,
            body = form.body;

        this.formPanelDropTarget = new Ext.dd.DropTarget(body, {
            ddGroup: 'grid-to-form',
            notifyEnter: function (ddSource, e, data) {
                //Add some flare to invite drop.
                body.stopAnimation();
                body.highlight();
            },
            notifyDrop: function (ddSource, e, data) {
                // Reference the record (single selection) for readability
                var selectedRecord = ddSource.dragData.records[0];

                // Load the record into the form
                form.getForm().loadRecord(selectedRecord);

                // Delete record from the source store.  not really required.
                //ddSource.view.store.remove(selectedRecord);
                return true;
            }
        });
    },
    beforeDestroy: function () {
        var target = this.formPanelDropTarget;
        if (target) {
            target.unreg();
            this.formPanelDropTarget = null;
        }
        this.callParent();
    },
    initComponent: function () {
        var me = this;
        me.tbar = [
            {
                xtype: 'radiogroup',
                columns: 2,
                layout: {
                    type: 'hbox'
                },
                vertical: false,
                defaults: {
                    boxLabelCls: 'radioGroupboxLabel'
                },
                items: [
                    { boxLabel: app.localize('Invoice'), name: 'typeOfInvoiceId', inputValue: '1', checked: true },
                    { boxLabel: app.localize('CreditMemo'), name: 'typeOfInvoiceId', inputValue: '2', padding: '0 0 0 20' }
                ],
                listeners: {
                    change: 'onInvoiceTypeChange'
                }
            }, '->', {
                xtype: 'button',
                scale: 'small',
                ui: 'actionButton',
                itemId: 'PostBtn',
                hidden: true,
                text: abp.localization.localize("Post").toUpperCase(),
                iconCls: 'fa  fa-files-o',
                iconAlign: 'left'
            }, {
                xtype: 'button',
                ui: 'actionButton',
                iconCls: 'fa fa-download',
                iconAlign: 'left',
                itemId: 'PrintBtn',
                hidden: true,
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
                value: 3
            }, {
                xtype: 'container',
                layout: {
                    type: 'column',
                    columns: 4
                },
                items: [
                    {
                        columnWidth: .33,
                        padding: '0 5 0 10',
                        defaults: {
                            ui: 'fieldLabelTop',
                            width: '100%',
                            labelWidth: 125
                        },
                        items: [
                            {
                                xtype: 'textfield',
                                name: 'receivableOrderReference',
                                itemId: 'receivableOrderReference',
                                ui: 'fieldLabelTop',
                                width: '100%',
                                fieldLabel: app.localize('AgencyPO#'),
                                emptyText: app.localize('SearchText'),
                                triggers: {
                                    poRelief: {
                                        cls: 'purchaseOrderTriggerClsInactive',
                                        handler: function () {
                                            console.log('poRelief trigger clicked');
                                        }
                                    }
                                },
                                listeners: {
                                    change: function (field, newValue, oldValue) {
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
                                fieldLabel: app.localize('Agency/Customer'),
                                ui: 'fieldLabelTop',
                                width: '100%',
                                name: 'customerId',
                                valueField: 'customerId',
                                displayField: 'customerName',
                                queryMode: 'remote',
                                minChars: 2,
                                modulePermissions: {
                                    read: abp.auth.isGranted('Pages.Receivables.Invoices'),
                                    create: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Create'),
                                    edit: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Edit'),
                                    destroy: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Delete')
                                },
                                primaryEntityCrudApi: null,
                                createEditEntityType: 'receivables.customers',
                                createEditEntityGridController: 'receivables-customers-customersgrid',
                                entityType: 'Customer',
                                listeners: {
                                    change: 'onCustomerChange'
                                }
                            }, {
                                xtype: 'textfield',
                                name: 'description',
                                itemId: 'description',
                                allowBlank: false,
                                fieldLabel: app.localize('Description'),
                                emptyText: app.localize('MandatoryField')
                            }, {
                                xtype: 'amountfield',
                                name: 'invoiceAmount',
                                itemId: 'invoiceAmount',
                                fieldLabel: app.localize('InvoiceAmount')
                            }, {
                                xtype: 'combobox',
                                name: 'paymentTerms',
                                itemId: 'paymentTerms',
                                emptyText: app.localize('SelectOption'),
                                width: '100%',
                                ui: 'fieldLabelTop',
                                displayField: 'description',
                                valueField: 'customerPaymentTermId',
                                queryMode: 'local',
                                fieldLabel: app.localize('PaymentTerms'),
                                store: Ext.create('Chaching.store.ARPaymentTermsListStore')
                            }
                        ]
                    }, {
                        columnWidth: .34,
                        padding: '0 5 0 10',
                        defaults: {
                            ui: 'fieldLabelTop',
                            width: '100%',
                            labelWidth: 140
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
                                fieldLabel: app.localize('InvoiceDate'),
                                value: new Date()
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
                                fieldLabel: app.localize('EmailReminderDate')
                            }, {
                                xtype: 'textfield',
                                name: 'adjustInvoice',
                                itemId: 'adjustInvoice',
                                allowBlank: true,
                                hidden: true,
                                fieldLabel: app.localize('AdjustInvoice')
                            }
                        ]
                    }, {
                        columnWidth: .33,
                        padding: '0 5 0 10',
                        defaults: {
                            ui: 'fieldLabelTop',
                            width: '100%',
                            labelWidth: 130
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
                                fieldLabel: app.localize('SalesRep'),
                                emptyText: app.localize('SelectOption')
                            }, {
                                xtype: 'textfield',
                                name: 'memoLine',
                                itemId: 'memoLine',
                                allowBlank: true,
                                fieldLabel: app.localize('SpecialInstructions')
                            }, {
                                xtype: 'checkbox',
                                ui: 'default',
                                labelAlign: 'right',
                                inputValue: false,
                                uncheckedValue: false,
                                boxLabelCls: 'checkboxLabel',
                                name: 'startup',
                                boxLabel: app.localize('Startup')
                            }

                        ]
                    }, {
                        columnWidth: 1,
                        itemId: 'transactionDetails',
                        items: [
                            {
                                xtype: 'receivables.invoices.transactionDetails',
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
