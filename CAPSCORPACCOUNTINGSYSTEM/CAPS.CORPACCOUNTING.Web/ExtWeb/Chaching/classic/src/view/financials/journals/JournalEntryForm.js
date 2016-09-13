
Ext.define('Chaching.view.financials.journals.JournalEntryForm',
{
    extend: 'Chaching.view.common.form.ChachingTransactionFormPanel',
    alias: ['widget.financials.journals.entry.create', 'widget.financials.journals.entry.edit'],
    requires: [
        'Chaching.view.financials.journals.JournalEntryFormController',
        'Chaching.view.financials.journals.JournalTransactionDetailGrid'
    ],

    controller: 'financials-journals-journalentryform',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Journals.Entry'),
        create: abp.auth.isGranted('Pages.Financials.Journals.Entry.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Journals.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Journals.Entry.Delete'),
        attach: abp.auth.isGranted('Pages.Financials.Journals.Entry.Attach')
    },
    attachmentConfig: {
        objectType: 'AccountingHeaderTransactionsUnit',
        objectIdField: 'accountingDocumentId'
    },
    openInPopupWindow: false,
    layout: 'fit',
    autoScroll: false,
    border: false,
    frame: false,
    items: [
        {
            xtype: 'hiddenfield',
            name: 'accountingDocumentId',
            value: 0
        }, {
            xtype: 'hiddenfield',
            name: 'organizationUnitId',
            value: null
        }, {
            xtype: 'container',
            layout: {
                type: 'column',
                columns: 4
            },
            items: [
                {
                    columnWidth: .25,
                    padding: '10 5 0 10',
                    defaults: {
                        //labelAlign: 'top',
                        ui: 'fieldLabelTop',
                        width: '100%',
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items: [{
                        xtype: 'datefield',
                        name: 'transactionDate',
                        itemId: 'transactionDate',
                        formatText : 'Expected date format mm/dd/yy',
                        allowBlank: false,
                        format : 'm/d/y',
                        submitFormat: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                        emptyText: app.localize('MandatoryField'),
                        fieldLabel: app.localize('TransactionDate')
                    }, {
                        xtype: 'textfield',
                        name: 'description',
                        itemId: 'description',
                        allowBlank: false,
                        fieldLabel: app.localize('Description'),
                        emptyText: app.localize('MandatoryField')
                    }]
                }, {
                    columnWidth: .25,
                    padding: '10 5 0 10',
                    defaults: {
                        //labelAlign: 'top',
                        width: '100%',
                        ui: 'fieldLabelTop',
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items: [{
                        xtype: 'textfield',
                        name: 'documentReference',
                        itemId: 'documentReference',
                        allowBlank: false,
                        fieldLabel: app.localize('JournalNumber'),
                        emptyText: app.localize('MandatoryField')
                    }, {
                        xtype: 'amountfield',
                        name: 'controlTotal',
                        itemId: 'controlTotal',
                        fieldLabel: app.localize('ControlTotal'),
                        disabled: true
                    }]
                }, {
                    columnWidth: .25,
                    padding: '10 5 0 10',
                    defaults: {
                        width: '100%',
                        ui: 'fieldLabelTop',
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items:[
                    {
                        xtype: 'combobox',
                        name: 'journalTypeId',
                        fieldLabel: app.localize('JournalType'),
                        itemId: 'journalTypeId',
                        valueField: 'journalTypeId',
                        displayField: 'journalType',
                        queryMode: 'local',
                        store: 'utilities.JournalTypeListStore',
                        emptyText: app.localize('MandatoryField'),
                        allowBlank: false,
                        listeners: {
                            select : 'onJournalTypeSelect'
                        }

                    }, {////TODO: Replace with combo once batch is ready
                        xtype: 'combobox',
                        name: 'batchId',
                        itemId: 'batchId',
                        ui: 'fieldLabelTop',
                        forceSelection: true,
                        fieldLabel: app.localize('Batch'),
                        emptyText: app.localize('SelectOption'),
                        valueField: 'batchId',
                        displayField: 'description',
                        store: new Chaching.store.batchposting.batches.BatchesStore()

                    }]
                }, {
                    columnWidth: .25,
                    padding: '10 5 0 10',
                    items: [
                        {
                            xtype: 'checkbox',
                            name: 'is13Period',
                            itemId: 'is13Period',
                            boxLabel: app.localize('Is13Period'),
                            boxLabelCls: 'checkboxLabel',
                            ui: 'default'
                        }
                    ]
                }, {
                    columnWidth: 1,
                    padding: '20 0 0 0',
                    itemId: 'transactionDetails',
                    items:[
                    {
                        xtype: 'financials.journals.entry.transactionDetails',
                        isTransactionDetailGrid: true
                    }]
                }
            ]
        }
    ]
});
