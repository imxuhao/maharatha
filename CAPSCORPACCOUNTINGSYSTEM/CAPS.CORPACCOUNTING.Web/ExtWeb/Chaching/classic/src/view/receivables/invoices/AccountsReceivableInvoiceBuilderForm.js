/**
 * Accounts list to display all records of Accounts Receivable Invoice Builder form.
 */
Ext.define('Chaching.view.receivables.invoices.AccountsReceivableInvoiceBuilderForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    //alias: ['widget.receivables.invoices.create', 'widget.receivables.invoices.edit'],
    requires: [
        'Chaching.view.receivables.invoices.AccountsReceivableInvoiceBuilderFormController'
    ],
    xtype: 'accountreceivableinvoicebuilderform',
    //name: 'Receivables.Invoices.Entry',
    controller: 'receivables-invoices-accountsreceivableinvoicebuilderform',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Receivables.Invoices'),
        create: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Create'),
        edit: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Delete'),
        attach: abp.auth.isGranted('Pages.Receivables.Invoices.Entry.Attach')
    },
    openInPopupWindow: false,
    layout: 'fit',
    autoScroll: false,
    border: false,
    frame: false,
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
                xtype: 'hiddenfield',
                name: 'typeOfAccountingDocumentId',
                value: 4
            }, {
                xtype: 'container',
                layout: {
                    type: 'column',
                    columns: 2
                },
                items: [
                    {
                        columnWidth: .50,
                        padding: '0 5 0 10',
                        defaults: {
                            ui: 'fieldLabelTop',
                            width: '100%',
                            labelWidth: 125
                        },
                        items: [
                            {
                                xtype: 'chachingcombobox',
                                name: 'jobId',
                                ui: 'fieldLabelTop',
                                width: '100%',
                                store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                                valueField: 'jobId',
                                displayField: 'jobNumber',
                                minChars: 2,
                                fieldLabel: app.localize('Projects'),
                                useDisplayFieldToSearch: true,
                                modulePermissions: {
                                    read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
                                    create: false,//abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
                                    edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
                                    destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete')
                                },
                                primaryEntityCrudApi: {
                                    read: abp.appPath + 'api/services/app/list/GetJobListByStatus',
                                    create: abp.appPath + 'api/services/app/jobUnit/CreateJobUnit',
                                    update: abp.appPath + 'api/services/app/jobUnit/UpdateJobUnit',
                                    destroy: abp.appPath + 'api/services/app/jobUnit/DeleteJobUnit'
                                },
                                createEditEntityType: 'projects.projectmaintenance.projects',
                                createEditEntityGridController: 'projects-projectmaintenance-projectsgrid',
                                entityType: 'Job',
                                isTwoEntityPicker: false
                            },
                            {
                                xtype: 'chachingcombobox',
                                store: new Chaching.store.utilities.autofill.CustomerListStore(),
                                fieldLabel: app.localize('Agency/Customer'),
                                ui: 'fieldLabelTop',
                                width: '100%',
                                name: 'customerId',
                                valueField: 'customerId',
                                displayField: 'lastName',
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
                                    //change: 'onCustomerChange'
                                }
                            }
                        ]
                    },
                    {
                        columnWidth: .50,
                        padding: '0 5 0 10',
                        defaults: {
                            ui: 'fieldLabelTop',
                            width: '100%',
                            labelWidth: 125
                        },
                        items: [
                        {
                            columnWidth: .33,
                            padding: '0 10 0 20',
                            defaults: {
                                width: '100%',
                                ui: 'fieldLabelTop',
                                //labelAlign: 'top',
                                labelWidth: 150,
                                xtype: 'numberfield',
                                hideTrigger: true,
                                minValue: 0,
                                maxValue: 100
                            },
                            items: [
                                {
                                    fieldLabel: app.localize('InvoiceSchedule1'),
                                    emptyText: app.localize('ToolTipInvoiceSchedule1')
                                }, {
                                    fieldLabel: app.localize('InvoiceSchedule2'),
                                    emptyText: app.localize('ToolTipInvoiceSchedule2')
                                }, {
                                    fieldLabel: app.localize('InvoiceSchedule3'),
                                    emptyText: app.localize('ToolTipInvoiceSchedule3')
                                }
                            ]
                        }, {
                            columnWidth: .33,
                            padding: '0 10 0 20',
                            defaults: {
                                width: '100%',
                                ui: 'fieldLabelTop',
                                labelWidth: 150
                            },
                            items: [
                                {
                                    xtype: 'textfield',
                                    name: 'amount',
                                    itemId: 'amount',
                                    fieldLabel: app.localize('StartupAmount'),
                                    emptyText: app.localize('ToolTipAmount')
                                }
                            ]
                        }, {
                            columnWidth: .33,
                            padding: '0 10 0 20',
                            defaults: {
                                width: '100%',
                                ui: 'fieldLabelTop',
                                labelAlign: 'top'
                            },
                            items: [
                                 {
                                     xtype: 'button',
                                     scale: 'small',
                                     itemId: 'createInvoices',
                                     text: app.localize('GenerateInvoices'),
                                     ui: 'actionButton',
                                     iconCls: 'fa fa-plus-circle',
                                     width: 200,
                                     disabled: true
                                 }
                            ]
                        }
                        ]
                    }
                ]
            }
        ]
});