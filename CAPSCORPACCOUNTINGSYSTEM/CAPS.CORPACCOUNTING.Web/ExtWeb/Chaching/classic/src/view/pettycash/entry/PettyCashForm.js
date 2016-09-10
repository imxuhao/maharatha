
Ext.define('Chaching.view.pettycash.entry.PettyCashForm',{
    extend: 'Chaching.view.common.form.ChachingTransactionFormPanel',
    alias: ['widget.pettycash.entry.create', 'widget.pettycash.entry.edit'],
    requires: [
        'Chaching.view.pettycash.entry.PettyCashFormController'
    ],

    controller: 'pettycash-entry-pettycashform',

    modulePermissions: {
        read: abp.auth.isGranted('Pages.PettyCash.Entry'),
        create: abp.auth.isGranted('Pages.PettyCash.Entry.Create'),
        edit: abp.auth.isGranted('Pages.PettyCash.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.PettyCash.Entry.Delete'),
        attach: abp.auth.isGranted('Pages.PettyCash.Entry.Attach')
    },
    attachmentConfig: {
        objectType: 'AccountingDocument',
        objectIdField: 'accountingDocumentId'
    },
    openInPopupWindow: false,
    layout: 'fit',
    autoScroll: false,
    border: false,
    frame: false,
    initComponent: function() {
        var me = this;
        me.tbar = [
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
                    { boxLabel: app.localize('Commercial'), name: 'typeOfInvoiceId', inputValue: '1', checked: true },
                    { boxLabel: app.localize('Project'), name: 'typeOfInvoiceId', inputValue: '2', padding: '0 0 0 20' },
                    { boxLabel: app.localize('Office'), name: 'typeOfInvoiceId', inputValue: '3', padding: '0 0 0 20' }
                ]
                //listeners: {
                //    change: 'onInvoiceTypeChange'
                //}
            }
        ];
        me.items = [
            {
                xtype: 'hiddenfield',
                name: 'accountingDocumentId',
                value: 0
            }, {
                xtype: 'hiddenfield',
                name: 'typeOfAccountingDocumentId',
                value: 5
            }, {
                xtype: 'container',
                layout: {
                    type: 'column',
                    columns: 4
                },
                items: [{
                    columnWidth: .25,
                    padding: '0 5 0 10',
                    defaults: {
                        ui: 'fieldLabelTop',
                        width: '100%'
                    },
                    items: [{
                        xtype: 'chachingcombobox',
                        store: new Chaching.store.projects.projectmaintenance.ProjectsStore(),
                        valueField: 'jobId',
                        displayField: 'jobNumber',
                        itemId: 'jobId',
                        name: 'jobId',
                        queryMode: 'remote',
                        fieldLabel: app.localize('Job#'),
                        minChars: 2,
                        modulePermissions: {
                            read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
                            create: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
                            edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
                            destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete')
                        },
                        primaryEntityCrudApi: {
                            read: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
                            create: abp.appPath + 'api/services/app/jobUnit/CreateJobUnit',
                            update: abp.appPath + 'api/services/app/jobUnit/UpdateJobUnit',
                            destroy: abp.appPath + 'api/services/app/jobUnit/DeleteJobUnit'
                        },
                        createEditEntityType: 'projects.projectmaintenance.projects',
                        createEditEntityGridController: 'projects-projectmaintenance-projectsgrid',
                        entityType: 'Job',
                        isTwoEntityPicker: false
                    }, {
                    xtype: 'chachingcombobox',
                    store: new Chaching.store.utilities.autofill.VendorsStore({
                        filters: [
                            {
                                entity: 'vendors',
                                searchTerm: 3,
                                comparator: 2,
                                dataType: 0,
                                property: 'typeofVendorId',
                                value: 3 //Only PC vendors
                            }
                        ]
                    }),
                    fieldLabel: app.localize('PCVendor'),
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
                    //listeners: {
                    //    change: 'onVendorChange'
                    //}
                    }, {
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
                                handler: function () {
                                    console.log('poRelief trigger clicked');
                                }
                            }
                        }
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
                    padding: '0 5 0 10',
                    defaults: {
                        ui: 'fieldLabelTop',
                        width: '100%'
                    },
                    items: [{
                        xtype: 'textfield',
                        name: 'documentReference',
                        itemId: 'documentReference',
                        allowBlank: false,
                        fieldLabel: app.localize('Envelope#'),
                        emptyText: app.localize('MandatoryField')
                    },{
                            xtype: 'amountfield',
                            name: 'controlTotal',
                            itemId: 'controlTotal',
                            fieldLabel: app.localize('EnvelopeTotal'),
                            allowBlank: false,
                            emptyText: app.localize('MandatoryField')
                        }, {
                            xtype: 'amountfield',
                            name: 'advanceAmount',
                            itemId: 'advanceAmount',
                            fieldLabel: app.localize('AdvanceAmount')
                        }, {
                            xtype: 'amountfield',
                            name: 'balance',
                            itemId: 'balance',
                            disabled:true,
                            fieldLabel: app.localize('Balance')
                        }
                    ]
                }, {
                    columnWidth: .25,
                    padding: '0 5 0 10',
                    defaults: {
                        ui: 'fieldLabelTop',
                        width: '100%'
                    },
                    items: [{
                        xtype: 'datefield',
                        name: 'datePosted',
                        itemId: 'datePosted',
                        allowBlank: false,
                        format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                        emptyText: app.localize('MandatoryField'),
                        fieldLabel: app.localize('PostingDate')
                    }, {
                        xtype: 'datefield',
                        name: 'transactionDate',
                        itemId: 'transactionDate',
                        allowBlank: false,
                        format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                        emptyText: app.localize('MandatoryField'),
                        fieldLabel: app.localize('Date')
                    },{
                            ////TODO: Replace with combo once batch is ready
                            xtype: 'textfield',
                            name: 'batchId',
                            itemId: 'batchId',
                            ui: 'fieldLabelTop',
                            fieldLabel: app.localize('Batch'),
                            emptyText: app.localize('SelectOption')
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
                                change: 'changeCurrency'
                            }
                        }
                    ]
                }, {
                    columnWidth: .25,
                    padding: '0 5 0 10',
                    defaults: {
                        ui: 'fieldLabelTop',
                        width: '100%',
                        labelWidth: 140
                    },
                    items: [
                    {
                        xtype: 'chachingcombobox',
                        store: new Chaching.store.utilities.autofill.VendorsStore({
                            filters: [
                                {
                                    entity: 'vendors',
                                    searchTerm: 3,
                                    comparator: 2,
                                    dataType: 0,
                                    property: 'typeofVendorId',
                                    value: 3 //Only PC vendors
                                }
                            ]
                        }),
                        fieldLabel: app.localize('CustodianVendor'),
                        ui: 'fieldLabelTop',
                        width: '100%',
                        name: 'custodianVendorId',
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
                        //listeners: {
                        //    change: 'onVendorChange'
                        //}
                    },{
                        xtype: 'checkbox',
                        name: 'printCheck',
                        itemId: 'printCheck',
                        labelAlign: 'right',
                        inputValue: true,
                        ui: 'default',
                        boxLabelCls: 'checkboxLabel',
                        boxLabel: app.localize('PrintAdvanceCheck')
                    }]
                }]
            }
        ];
        me.callParent(arguments);
    }
});
