/**
 * The class is created to design project/job UI
 * Author: Krishna Garad
 * Date: 29/04/2016
 */
/**
 * @class Chaching.view.projects.projectmaintenance.ProjectsForm
 * UI design for project/job
 * @alias widget.projects.projectmaintenance.projects.create, widget.projects.projectmaintenance.projects.edit
 */
Ext.define('Chaching.view.projects.projectmaintenance.ProjectsForm',{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.projects.projectmaintenance.projects.create', 'widget.projects.projectmaintenance.projects.edit'],
    requires: [
        'Chaching.view.projects.projectmaintenance.ProjectsFormController',
        'Chaching.view.projects.projectmaintenance.ProjectLocations',
        'Chaching.view.projects.projectmaintenance.PoRangeAllocationGrid'
    ],
    /**
     * @cfg {object}
     * permissions to access project/job.
     */
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
        create: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete')
    },
    controller: 'projects-projectmaintenance-projectsform',
    name: 'Projects',
    openInPopupWindow: false,
    hideDefaultButtons: true,
    layout: 'fit',
    autoScroll: true,
    border: false,
    showFormTitle: false,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CreateNewProject").initCap()
    },
    items:[
    {
        xtype: 'tabpanel',
        ui:'formTabPanels',
        items:[
            {
////start of setup card
                title: app.localize('Setup'),
                iconCls: 'fa fa-gear',
                itemId:'ProjectSetupTab',
                items: [
                    {
                        xtype: 'fieldset',
                        title: app.localize('ProjectSetup'),
                        layout: 'column',
                        ui: 'transparentFieldSet',
                        collapsible:true,
                        items: [
                            {
                                xtype: 'hiddenfield',
                                name: 'jobId',
                                value: 0
                            }, {
                                xtype: 'hiddenfield',
                                name: 'organizationUnitId',
                                value: null
                            }, {
                                columnWidth: .33,
                                padding: '20 10 0 20',
                                defaults: {
                                    labelAlign: 'top',
                                    blankText: app.localize('MandatoryToolTipText')
                                },
                                items: [
                                    {
                                        xtype: 'textfield',
                                        name: 'jobNumber',
                                        itemId: 'jobNumber',
                                        allowBlank: false,
                                        fieldLabel: app.localize('JobNumber').initCap() + Chaching.utilities.ChachingGlobals.mandatoryFlag,
                                        width: '100%',
                                        ui: 'fieldLabelTop',
                                        emptyText: app.localize('MandatoryField')
                                    }, {
                                        xtype: 'textfield',
                                        name: 'caption',
                                        itemId: 'caption',
                                        allowBlank: false,
                                        fieldLabel: app.localize('JobName') + Chaching.utilities.ChachingGlobals.mandatoryFlag,
                                        width: '100%',
                                        ui: 'fieldLabelTop',
                                        emptyText: app.localize('MandatoryField')
                                    }, {
                                        xtype: 'combobox',
                                        name: 'typeofProjectId',
                                        itemId: 'typeofProjectId',
                                        allowBlank: false,
                                        queryMode: 'local',
                                        bind: {
                                            store: '{getProjectTypeList}'
                                        },
                                        valueField: 'value',
                                        displayField: 'name',
                                        width: '100%',
                                        ui: 'fieldLabelTop',
                                        fieldLabel: app.localize('ProjectType') + Chaching.utilities.ChachingGlobals.mandatoryFlag,
                                        emptyText: app.localize('MandatoryField')
                                    }
                                ]
                            }, {
                                columnWidth: .33,
                                padding: '20 10 0 20',
                                defaults: {
                                    labelAlign: 'top',
                                    blankText: app.localize('MandatoryToolTipText')
                                },
                                items: [
                                    {
                                        xtype: 'combobox',
                                        name: 'chartOfAccountId',
                                        itemId: 'chartOfAccountId',
                                        queryMode: 'local',
                                        store: 'projects.projectmaintenance.ProjectCoaStore',
                                        valueField: 'coaId',
                                        displayField: 'caption',
                                        width: '100%',
                                        ui: 'fieldLabelTop',
                                        allowBlank: false,
                                        fieldLabel: app.localize('BudgetFormat') + Chaching.utilities.ChachingGlobals.mandatoryFlag,
                                        emptyText: app.localize('MandatoryField')
                                    }, {
                                        xtype: 'combobox',
                                        name: 'rollupAccountId',
                                        itemId: 'rollupAccountId',
                                        queryMode: 'local',
                                        bind: {
                                            store: '{genericRollupAccountList}'
                                        },
                                        valueField: 'value',
                                        displayField: 'name',
                                        width: '100%',
                                        ui: 'fieldLabelTop',
                                        fieldLabel: app.localize('RollupAccount'),
                                        emptyText: app.localize('SelectOption')
                                    }, {
                                        xtype: 'combobox',
                                        name: 'rollupJobId',
                                        itemId: 'rollupJobId',
                                        queryMode: 'local',
                                        store: 'financials.accounts.DivisionsStore',
                                        valueField: 'jobId',
                                        displayField: 'caption',
                                        width: '100%',
                                        ui: 'fieldLabelTop',
                                        fieldLabel: app.localize('RollupDivision'),
                                        emptyText: app.localize('SelectOption')
                                    }]
                            }, {
                                columnWidth: .33,
                                padding: '20 10 0 20',
                                defaults: {
                                    labelAlign: 'top',
                                    blankText: app.localize('MandatoryToolTipText')
                                },
                                items: [
                                    {
///TODO: Replace with combo box once tax credit service is ready
                                        xtype: 'combobox',
                                        name: 'taxCreditId',
                                        itemId: 'taxCreditId',
                                        allowBlank: true,
                                        queryMode: 'local',
                                        bind: {
                                            store: '{getTaxCreditList}'
                                        },
                                        valueField: 'value',
                                        displayField: 'name',
                                        fieldLabel: app.localize('TaxCredit'),
                                        width: '100%',
                                        ui: 'fieldLabelTop',
                                        emptyText: app.localize('SelectOption')
                                    },
                                    {
                                        xtype: 'combobox',
                                        name: 'typeOfCurrencyId',
                                        itemId: 'typeOfCurrencyId',
                                        queryMode: 'local',
                                        bind: {
                                            store: '{typeOfCurrencyList}'
                                        },
                                        valueField: 'value',
                                        displayField: 'name',
                                        width: '100%',
                                        ui: 'fieldLabelTop',
                                        fieldLabel: app.localize('Currency'),
                                        emptyText: app.localize('SelectOption')
                                    }, {
                                        xtype: 'combobox',
                                        name: 'typeOfJobStatusId',
                                        itemId: 'typeOfJobStatusId',
                                        queryMode: 'local',
                                        bind: {
                                            store: '{projectStatusList}'
                                        },
                                        valueField: 'value',
                                        displayField: 'name',
                                        width: '100%',
                                        ui: 'fieldLabelTop',
                                        fieldLabel: app.localize('Status'),
                                        emptyText: app.localize('SelectOption')
                                    }
                                ]
                            }
                        ]
                    }
                ],
                dockedItems:[
                {
                    xtype: 'toolbar',
                    dock: 'bottom',
                    layout: {
                        type: 'hbox',
                        pack:'center'
                    },
                    items:[
                    {
                        xtype: 'button',
                        itemId: 'btnSaveSetup',
                        ui: 'actionButton',
                        text: app.localize('SaveProject').toUpperCase(),
                        iconCls: 'fa fa-save',
                        listeners: {
                            click: 'onSaveClicked'
                        }
                    }, {
                        xtype: 'button',
                        itemId: 'btnCancelSetup',
                        ui: 'actionButton',
                        text: app.localize('Cancel').toUpperCase(),
                        iconCls: 'fa fa-close',
                        listeners: {
                            click: 'onCancelClicked'
                        }
                    }]
                }]

            },/////End of setup card
            {///// Start of project details card
                title: app.localize('ProjectDetails').initCap(),
                autoScroll: true,
                itemId: 'ProjectDetailsTab',
                disabled:true,
                iconCls: 'fa fa-gears',
                items:[
                {
                    xtype: 'fieldset',
                    ui:'transparentFieldSet',
                    layout: 'column',
                    title: app.localize('BiddingInfo'),
                    collapsible :true,
                    items:[
                    {
                        columnWidth: .25,
                        padding: '0 10 0 20',
                        defaults: {
                            width: '100%',
                            ui: 'fieldLabelTop',
                            labelAlign: 'top'
                        },
                        items:[
                        {
                            xtype: 'datefield',
                            name: 'bidDate',
                            itemId: 'bidDate',
                            format:Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                            emptyText: Chaching.utilities.ChachingGlobals.defaultDateFormat,
                            fieldLabel: app.localize('BidDate')
                        }]
                    }, {
                        columnWidth: .25,
                        padding: '0 10 0 20',
                        defaults: {
                            width: '100%',
                            ui: 'fieldLabelTop',
                            labelAlign: 'top'
                        },
                        items: [
                            {
                                xtype: 'datefield',
                                name: 'awardDate',
                                itemId: 'awardDate',
                                format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                                emptyText: Chaching.utilities.ChachingGlobals.defaultDateFormat,
                                fieldLabel: app.localize('AwardDate')
                            }
                        ]
                    }, {
                        columnWidth: .25,
                        padding: '0 10 0 20',
                        defaults: {
                            labelAlign: 'top',
                            blankText: app.localize('MandatoryToolTipText')
                        },
                        items: [
                        {
                            xtype: 'textfield',
                            name: 'productOwner',
                            itemId: 'productOwner',
                            ui: 'fieldLabelTop',
                            width:'100%',
                            fieldLabel: app.localize('Bidder'),
                            emptyText: app.localize('ToolTipBidder')
                        }]
                    }, {
                        columnWidth: .25,
                        padding: '0 10 0 20',
                        defaults: {
                            width: '100%'
                        },
                        items: [{
                            xtype: 'checkbox',
                            name: 'isOTon',
                            itemId: 'isOTon',
                            labelAlign: 'right',
                            inputValue: true,
                            ui: 'default',
                            boxLabelCls: 'checkboxLabel',
                            boxLabel: app.localize('OtBasedOn')
                        }]
                    }]
                  
                }, {
                    xtype: 'fieldset',
                    title: app.localize('AgencyInfo'),
                    ui: 'transparentFieldSet',
                    layout: 'column',
                    collapsible: true,
                    items:[
                    {
                        columnWidth: .33,
                        padding: '0 10 0 20',
                        defaults: {
                            width: '100%',
                            ui: 'fieldLabelTop',
                            labelAlign: 'top'
                        },
                        items:[
                        {
                            xtype: 'combobox',
                            name: 'agencyId',
                            itemId: 'agencyId',
                            fieldLabel: app.localize('Agency'),
                            bind: {
                                store: '{getCustomersList}'
                            },
                            valueField: 'customerId',
                            displayField: 'firstName',
                            queryMode: 'local',
                            listConfig: {
                                getInnerTpl: Chaching.utilities.ChachingRenderers.renderCustomerInnerTpl
                            },
                            anyMatch: true,
                            displayTpl: Chaching.utilities.ChachingRenderers.renderCustomerDispalyTpl(),//using XTemplate
                            emptyText: app.localize('SelectOption'),
                            listeners: {
                                change:'onAgencyChange'
                            }
                        }, {
                            xtype: 'textfield',
                            name: 'productName',
                            itemId: 'productName',
                            fieldLabel: app.localize('ProductName'),
                            emptyText: app.localize('ToolTipProductName')
                        }, {
                            xtype: 'combobox',
                            name: 'thirdPartyCustomerId',
                            itemId: 'thirdPartyCustomerId',
                            bind: {
                                store: '{getCustomersList}'
                            },
                            valueField: 'customerId',
                            displayField: 'firstName',
                            queryMode: 'local',
                            listConfig: {
                                getInnerTpl: Chaching.utilities.ChachingRenderers.renderCustomerInnerTpl
                            },
                            anyMatch: true,
                            displayTpl: Chaching.utilities.ChachingRenderers.renderCustomerDispalyTpl(),
                            fieldLabel: app.localize('ThirdPartyCustomer'),
                            emptyText: app.localize('SelectOption')
                        }, {
                            xtype: 'textfield',
                            name: 'agencyProducer',
                            itemId: 'agencyProducer',
                            fieldLabel: app.localize('AgencyProducer'),
                            emptyText: app.localize('ToolTipAgencyProducer')
                        }]
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
                                xtype: 'textfield',
                                name: 'agencyBusinessManager',
                                itemId: 'agencyBusinessManager',
                                fieldLabel: app.localize('BizManager'),
                                emptyText: app.localize('ToolTipBizManager')
                            }, {
                                xtype: 'textareafield',
                                name: 'agencyAddress',
                                itemId: 'agencyAddress',
                                resizable: false,
                                height:134,
                                fieldLabel: app.localize('Address'),
                                emptyText: app.localize('ToolTipAgencyAddress')
                            }, {
                                xtype: 'textfield',
                                name: 'agencyPhone',
                                itemId: 'agencyPhone',
                                fieldLabel: app.localize('AgencyTelephone'),
                                emptyText: app.localize('ToolTipAgencyTelephone')
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
                        items: [{
                            xtype: 'hiddenfield',
                            name: 'agencyEmail',
                            value: null
                        }, {
                            xtype: 'displayfield',
                            name: 'agencyEmailDisplay',
                            itemId: 'agencyEmailDisplay',
                            tabIndex: -1,
                            fieldLabel: app.localize('AgencyEmail'),
                            value: Chaching.utilities.ChachingRenderers.renderMailToTag('sgarad@capspayroll.com')
                        }, {
                            xtype: 'textfield',
                            name: 'agencyJobNumber',
                            itemId: 'agencyJobNumber',
                            padding: '23 10 0 0',
                            fieldLabel: app.localize('AgencyJobNumber'),
                            emptyText: app.localize('ToolTipAgencyJobNumber')
                        },{///TODO: to be added
                            xtype: 'datefield',
                            name: 'contractDate',
                            itemId: 'contractDate',
                            format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                            emptyText: Chaching.utilities.ChachingGlobals.defaultDateFormat,
                            fieldLabel: app.localize('ContractExecDate')
                        }, {
                            xtype: 'checkbox',
                            name: 'isWrapUpInsurance',
                            itemId: 'isWrapUpInsurance',
                            labelAlign: 'right',
                            inputValue: true,
                            ui: 'default',
                            boxLabelCls: 'checkboxLabel',
                            boxLabel: app.localize('WrapUpInsurance')
                        }]
                    }]
                    
                }, {
                    xtype: 'fieldset',
                    title: app.localize('AgencyContractInfo'),
                    ui: 'transparentFieldSet',
                    layout: 'column',
                    collapsible: true,
                    items: [
                        {
                            columnWidth: .33,
                            padding: '0 10 0 20',
                            defaults: {
                                width: '100%',
                                ui: 'fieldLabelTop',
                                labelAlign: 'top',
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
                                labelAlign: 'top'
                            },
                            items: [
                                {///TODO: Rplace with combo once terms section is completed on contracts tab
                                    xtype: 'textfield',
                                    name: 'termId',
                                    itemId: 'termId',
                                    fieldLabel: app.localize('Terms'),
                                    emptyText: app.localize('SelectOption')
                                }, {
                                    xtype: 'textfield',
                                    name: 'comments',
                                    itemId: 'comments',
                                    fieldLabel: app.localize('Comments'),
                                    emptyText: app.localize('ToolTipComments')
                                }, {
                                    xtype: 'textfield',
                                    name: 'amount',
                                    itemId: 'amount',
                                    fieldLabel: app.localize('Amount'),
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
                                     xtype: 'checkbox',
                                     name: 'isCostPlus',
                                     itemId: 'isCostPlus',
                                     labelAlign: 'right',
                                     inputValue: true,
                                     ui: 'default',
                                     boxLabelCls: 'checkboxLabel',
                                     boxLabel: app.localize('IsCostPlus')
                                 }, {
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
                }, {
                    xtype: 'fieldset',
                    title: app.localize('ShootInfo'),
                    ui: 'transparentFieldSet',
                    layout: 'column',
                    collapsible: true,
                    items:[
                    {
                        columnWidth: .33,
                        padding: '0 10 0 20',
                        defaults: {
                            width: '100%',
                            ui: 'fieldLabelTop',
                            labelAlign: 'top'
                        },
                        items:[
                        {
                            xtype: 'combobox',
                            name: 'directorEmployeeId',
                            itemId: 'directorEmployeeId',
                            bind: {
                                store: '{getDirectorsList}'
                            },
                            valueField: 'employeeId',
                            displayField: 'firstName',
                            queryMode:'local',
                            fieldLabel: app.localize('Director'),
                            listConfig: {
                                getInnerTpl: Chaching.utilities.ChachingRenderers.renderEmployeeInnerTpl
                            },
                            displayTpl: Chaching.utilities.ChachingRenderers.renderEmployeeDispalyTpl(),
                            emptyText:app.localize('SelectOption')
                        }, {
                            xtype: 'combobox',
                            name: 'executiveProducerId',
                            itemId: 'executiveProducerId',
                            bind: {
                                store: '{getProducersList}'
                            },
                            valueField: 'employeeId',
                            displayField: 'firstName',
                            queryMode: 'local',
                            listConfig: {
                                getInnerTpl: Chaching.utilities.ChachingRenderers.renderEmployeeInnerTpl
                            },
                            displayTpl: Chaching.utilities.ChachingRenderers.renderEmployeeDispalyTpl(),
                            anyMatch: true,
                            fieldLabel: app.localize('ExeProducer'),
                            emptyText: app.localize('SelectOption')
                        }, {
                            xtype: 'combobox',
                            name: 'dirOfPhotoEmployeeId',
                            itemId: 'dirOfPhotoEmployeeId',
                            bind: {
                                store: '{getDirofPhotoList}'
                            },
                            valueField: 'employeeId',
                            displayField: 'firstName',
                            queryMode: 'local',
                            listConfig: {
                                getInnerTpl: Chaching.utilities.ChachingRenderers.renderEmployeeInnerTpl
                            },
                            displayTpl: Chaching.utilities.ChachingRenderers.renderEmployeeDispalyTpl(),
                            anyMatch: true,
                            fieldLabel: app.localize('DirOfPhotography'),
                            emptyText: app.localize('SelectOption')
                        },{///TODO: to be added
                            xtype: 'datefield',
                            name: 'deliveryDate',
                            itemId: 'deliveryDate',
                            format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                            emptyText: Chaching.utilities.ChachingGlobals.defaultDateFormat,
                            fieldLabel: app.localize('DeliveryDate')
                        }]
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
                                xtype: 'numberfield',
                                name: 'commercialNumber',
                                itemId: 'commercialNumber',
                                minValue: 0,
                                maxValue: 100,
                                fieldLabel: app.localize('CommercialNumber'),
                                emptyText: app.localize('ToolTipCommercialNumber'),
                                hideTrigger: true
                            }, {
                                xtype: 'numberfield',
                                name: 'commercialLength',
                                itemId: 'commercialLength',
                                minValue: 0,
                                maxValue: 100,
                                fieldLabel: app.localize('CommercialLength'),
                                emptyText: app.localize('ToolTipCommercialLength'),
                                hideTrigger: true
                            }, {
                                xtype: 'textfield',
                                name: 'commercialTitle1',
                                itemId: 'commercialTitle1',
                                fieldLabel: app.localize('CommercialTitle1'),
                                emptyText: app.localize('ToolTipCommercialTitle1')
                            }, {
                                xtype: 'textfield',
                                name: 'commercialTitle2',
                                itemId: 'commercialTitle2',
                                fieldLabel: app.localize('CommercialTitle2'),
                                emptyText: app.localize('ToolTipCommercialTitle2')
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
                        items:[
                       {
                           xtype: 'textfield',
                           name: 'commercialTitle3',
                           itemId: 'commercialTitle3',
                           fieldLabel: app.localize('CommercialTitle3'),
                           emptyText: app.localize('ToolTipCommercialTitle3')
                       }, {
                           xtype: 'textfield',
                           name: 'commercialTitle4',
                           itemId: 'commercialTitle4',
                           fieldLabel: app.localize('CommercialTitle4'),
                           emptyText: app.localize('ToolTipCommercialTitle4')
                       }, {
                           xtype: 'textfield',
                           name: 'commercialTitle5',
                           itemId: 'commercialTitle5',
                           fieldLabel: app.localize('CommercialTitle5'),
                           emptyText: app.localize('ToolTipCommercialTitle5')
                       }, {
                           xtype: 'textfield',
                           name: 'commercialTitle6',
                           itemId: 'commercialTitle6',
                           fieldLabel: app.localize('CommercialTitle6'),
                           emptyText: app.localize('ToolTipCommercialTitle6')
                       }]
                    }, {
                        columnWidth: 1,
                        padding: '0 10 0 20',
                        defaults: {
                            width: '100%',
                            ui: 'dragDropPanel'
                        },
                        items:[
                        {
                            xtype: 'widget.projects.projectmaintenance.projectLocations',
                            itemId: 'jobLocationsGridPanel'
                        }]
                    }]
                    
                }, {
                    xtype: 'fieldset',
                    title: app.localize('OtherInfo'),
                    ui: 'transparentFieldSet',
                    layout: 'column',
                    collapsible: true,
                    items:[
                    {
                        columnWidth: .33,
                        padding: '0 10 0 20',
                        defaults: {
                            width: '100%',
                            ui: 'fieldLabelTop',
                            labelAlign: 'top'
                        },
                        items:[
                        {
                            xtype: 'textfield',
                            name: 'salesRepId',
                            itemId: 'salesRepId',
                            fieldLabel: app.localize('SalesRep'),
                            emptyText:app.localize('SelectOption')
                        }]
                    }, {
                        columnWidth: .66,
                        padding: '0 10 0 20',
                        defaults: {
                            width: '100%',
                            ui: 'fieldLabelTop',
                            labelAlign: 'top'
                        },
                        items:[
                        {
                            xtype: 'widget.projects.projectmaintenance.porangeallocation',
                            itemId: 'jobPurchaseOrderAllocation'
                        }]
                    }]
                }],
                dockedItems: [
                {
                    xtype: 'toolbar',
                    dock: 'bottom',
                    layout: {
                        type: 'hbox',
                        pack:'center'
                    },
                    items:[
                    {
                        xtype: 'button',
                        itemId: 'btnSaveProjectDetails',
                        ui: 'actionButton',
                        text: app.localize('SaveProjectDetails').toUpperCase(),
                        iconCls: 'fa fa-save',
                        listeners: {
                            click: 'onProjectDetailsSave'
                        }
                    }, {
                        xtype: 'button',
                        itemId: 'btnCancelProjectDetails',
                        ui: 'actionButton',
                        text: app.localize('Cancel').toUpperCase(),
                        iconCls: 'fa fa-close',
                        listeners: {
                            click: 'onCancelClicked'
                        }
                    }]
                }]
            },//end of project details card
            {///// Start of Line numbers card
                title: app.localize('LineNumbersTab').initCap(),
                iconCls: 'fa fa-list-ol',
                disabled: true,
                itemId: 'LineNumbersTab',
                items:[
                {
                    
                    xtype: 'projects.projectmaintenance.linenumbers',
                    store: 'projects.projectmaintenance.JobAccountsStore',
                    headerButtonsConfig: null,
                    requireExport: false,
                    requireMultiSearch: false,
                    requireMultisort: false,
                    editingMode: 'cell',
                    createNewMode: 'inline',
                    isSubMenuItemTab: false,
                    showPagingToolbar: false,
                    itemId: 'jobAccountsGridPanel',
                    modulePermissions: {
                        read: true,
                        create: false,
                        edit: true,
                        destroy: false,
                    },
                    columns:[
                    {
                        xtype: 'gridcolumn',
                        text: app.localize('LineNumber'),
                        dataIndex: 'accountNumber',
                        sortable: true,
                        groupable: true,
                        width: '20%'
                    }, {
                        xtype: 'gridcolumn',
                        text: app.localize('Description'),
                        dataIndex: 'description',
                        sortable: true,
                        groupable: true,
                        width: '70%',
                        editor: {
                            xtype: 'textfield'
                        }
                    }]
                }]
            },//end of Line numbers card
            {///// Start of cost manager card
                title: app.localize('CostManager').initCap(),
                iconCls: 'fa fa-bar-chart',
                disabled: true,
                itemId:'CostManagerTab'
            },//end of cost manager card
            {///// Start of Petty Cash Accounts card
                title: app.localize('PcAccount').initCap(),
                iconCls: 'fa fa-book',
                disabled: true,
                itemId: 'PCAccountTab'
            },//end of Petty Cash Accounts card
            {///// Start of PO Log card
                title: app.localize('PoLog').initCap(),
                iconCls: 'fa fa-shopping-cart',
                disabled: true,
                itemId: 'POLogTab'
            }//end of Po Log card
        ]
    }]
    
});
