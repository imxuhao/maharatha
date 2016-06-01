
Ext.define('Chaching.view.financials.journals.JournalEntryForm', {
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
        destroy: abp.auth.isGranted('Pages.Financials.Journals.Entry.Delete')
    },
    openInPopupWindow: false,
    layout: 'fit',
    autoScroll: false,
    border: false,
    frame: false,
    items: [{
        xtype: 'container',
        flex: 1,
        items: [{
            xtype: 'fieldset',
            title: app.localize('JournalDetails'),
            layout: 'column',
            ui: 'transparentFieldSet',
            width: '100%',
            collapsible: true,
            listeners: {
                beforecollapse: 'onHeaderCollapse',
                beforeexpand:'onHeaderExpand'
            },
            items: [{
                xtype: 'hiddenfield',
                name: 'accountingDocumentId',
                value: 0
            }, {
                xtype: 'hiddenfield',
                name: 'organizationUnitId',
                value: null
            }, {
                columnWidth: .33,
                padding: '0 10 0 20',
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
                    allowBlank: false,
                    format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
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
                columnWidth: .33,
                padding: '0 10 0 20',
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
                },{
                    xtype: 'numberfield',
                    name: 'controlTotal',
                    itemId: 'controlTotal',
                    fieldLabel: app.localize('ControlTotal'),
                    disabled: true,
                    hideTrigger: true
                }]
            }, {
                columnWidth: .33,
                padding: '0 10 0 20',
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
                    valueField: 'value',
                    displayField:'name',
                    store: {
                        fields: [{ name: 'name' }, { name: 'value' }],
                        data: [{ name: app.localize('Standard'), value: 1 },
                            { name: app.localize('Intercompany'), value: 2 },
                            { name: app.localize('Reversing'), value: 3 },
                            { name: app.localize('Recurring'), value: 4 }
                        ]
                    }
                }, {////TODO: Replace with combo once batch is ready
                    xtype: 'textfield',
                    name: 'batchId',
                    itemId: 'batchId',
                    fieldLabel: app.localize('Batch'),
                    emptyText: app.localize('SelectOption')
                }, {
                    xtype: 'checkbox',
                    name: 'is13Period',
                    itemId: 'is13Period',
                    boxLabel: app.localize('Is13Period'),
                    boxLabelCls: 'checkboxLabel',
                    ui: 'default'
                }]
                /*items: [{
                    xtype: 'radiogroup',
                    fieldLabel: app.localize('JournalType'),
                    columns: 2,
                    vertical: true,
                    items: [
                        { boxLabel: app.localize('Standard'), name: 'journalTypeId', inputValue: '1', checked: true },
                        { boxLabel: app.localize('Intercompany'), name: 'journalTypeId', inputValue: '2' },
                        { boxLabel: app.localize('Reversing'), name: 'journalTypeId', inputValue: '3', padding: '0 0 0 10' },
                        { boxLabel: app.localize('Recurring'), name: 'journalTypeId', inputValue: '4', padding: '0 0 0 10' }
                    ]
                }]*/
            }]
        }, {
            xtype: 'fieldset',
            title: app.localize('DistributionDetails'),
            layout: 'column',
            ui: 'transparentFieldSet',
            collapsible: true,
            isTransactionDetailContainer:true,//set true to autoheight details grid
            items: [{
                columnWidth: 1,
                items: [{
                    xtype: 'financials.journals.entry.transactionDetails',
                    isTransactionDetailGrid:true
                }]
            }]
        }]
    }]

});
