
Ext.define('Chaching.view.projects.projectmaintenance.ProjectsForm',{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.projects.projectmaintenance.projects.create', 'widget.projects.projectmaintenance.projects.edit'],
    requires: [
        'Chaching.view.projects.projectmaintenance.ProjectsFormController',
        'Chaching.view.projects.projectmaintenance.ProjectLocations'
    ],
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
        create: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete')
    },
    controller: 'projects-projectmaintenance-projectsform',
    name: 'Projects',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'fit',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CreateNewProject").initCap()
    },
    items:[
    {
        xtype: 'tabpanel',
        ui:'formTabPanels',
        items:[
        {////start of setup card
            title: app.localize('Setup'),
            iconCls:'fa fa-gear',
            layout:'column',
            items:[
            {
                xtype: 'hiddenfield',
                name: 'jobId',
                value:0
            }, {
                columnWidth: .5,
                padding: '20 10 0 20',
                defaults: {
                    labelAlign: 'top',
                    blankText: app.localize('MandatoryToolTipText')
                },
                items: [{
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
                }, {///TODO: Replace with combo once servie is ready
                    xtype: 'combobox',
                    name: 'typeofProjectId',
                    itemId: 'typeofProjectId',
                    allowBlank:false,
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
                }, {
                    xtype: 'combobox',
                    name: 'chartOfAccountId',
                    itemId: 'chartOfAccountId',
                    queryMode: 'local',
                    store: 'projects.projectmaintenance.ProjectCoaStore',
                    valueField: 'coaId',
                    displayField: 'caption',
                    width: '100%',
                    ui: 'fieldLabelTop',
                    allowBlank:false,
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
                }]
            }, {
                    columnWidth: .5,
                    padding: '20 10 0 20',
                    defaults: {
                        labelAlign: 'top',
                        blankText: app.localize('MandatoryToolTipText')
                    },
                    items: [
                        {
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
                        }, {///TODO: Replace with combo box once tax credit service is ready
                            xtype: 'textfield',
                            name: 'taxRecoveryId',
                            itemId: 'taxRecoveryId',
                            allowBlank: true,
                            fieldLabel: app.localize('TaxRecovery'),
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
        },/////End of setup card
            {///// Start of project details card
                title: app.localize('ProjectDetails'),
                autoScroll: true,
                iconCls: 'fa fa-gears',
                items:[
                {
                    xtype: 'fieldset',
                    ui:'transparentFieldSet',
                    layout: 'column',
                    title: app.localize('BiddingInfo'),
                    items:[
                    {
                        columnWidth: .5,
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
                        }, {
                            xtype: 'datefield',
                            name: 'awardDate',
                            itemId: 'awardDate',
                            format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                            emptyText: Chaching.utilities.ChachingGlobals.defaultDateFormat,
                            fieldLabel: app.localize('AwardDate')
                        }]
                    }, {
                        columnWidth: .5,
                        padding: '0 10 0 20',
                        defaults: {
                            labelAlign: 'top',
                            blankText: app.localize('MandatoryToolTipText')
                        },
                        items: [
                        {
                            xtype: 'textfield',
                            name: 'bidder',
                            itemId: 'bidder',
                            ui: 'fieldLabelTop',
                            width:'100%',
                            fieldLabel: app.localize('Bidder')
                        }, {
                            xtype: 'checkbox',
                            name: 'isOTon',
                            itemId: 'isOTon',
                            labelAlign: 'right',
                            inputValue: true,
                            boxLabelCls: 'checkboxLabel',
                            boxLabel: app.localize('OtBasedOn')
                        }]
                    }]
                  
                }, {
                    xtype: 'fieldset',
                    title: app.localize('AgencyInfo'),
                    ui: 'transparentFieldSet',
                    layout: 'column',
                    items:[
                    {
                        columnWidth: .5,
                        padding: '0 10 0 20',
                        defaults: {
                            width: '100%',
                            ui: 'fieldLabelTop',
                            labelAlign: 'top'
                        },
                        items:[
                        {///TODO:Replace with combo once agency service is ready
                            xtype: 'textfield',
                            name: 'agencyId',
                            itemId: 'agencyId',
                            fieldLabel: app.localize('Agency'),
                            emptyText: app.localize('SelectOption')
                        }, {
                            xtype: 'textfield',
                            name: 'productName',
                            itemId: 'productName',
                            fieldLabel: app.localize('ProductName'),
                            emptyText: app.localize('ToolTipProductName')
                        }, {///TODO:Replace with combo once thirdparty customer service is ready 
                            xtype: 'textfield',
                            name: 'thirdPartyCustomerId',
                            itemId: 'thirdPartyCustomerId',
                            fieldLabel: app.localize('ThirdPartyCustomer'),
                            emptyText: app.localize('SelectOption')
                        }, {
                            xtype: 'textfield',
                            name: 'agencyProducer',
                            itemId: 'agencyProducer',
                            fieldLabel: app.localize('AgencyProducer'),
                            emptyText: app.localize('ToolTipAgencyProducer')
                        }, {
                            xtype: 'textfield',
                            name: 'agencyBusinessManager',
                            itemId: 'agencyBusinessManager',
                            fieldLabel: app.localize('BizManager'),
                            emptyText: app.localize('ToolTipBizManager')
                        }, {
                            xtype: 'textfield',
                            name: 'agencyAddress',
                            itemId: 'agencyAddress',
                            fieldLabel: app.localize('Address'),
                            emptyText: app.localize('ToolTipAgencyAddress')
                        }, {
                            xtype: 'textfield',
                            name: 'agencyPhone',
                            itemId: 'agencyPhone',
                            fieldLabel: app.localize('AgencyTelephone'),
                            emptyText: app.localize('ToolTipAgencyTelephone')
                        }, {
                            xtype: 'textfield',
                            name: 'agencyJobNumber',
                            itemId: 'agencyJobNumber',
                            fieldLabel: app.localize('AgencyJobNumber'),
                            emptyText: app.localize('ToolTipAgencyJobNumber')
                        }, {///TODO: to be added
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
                    }, {
                        columnWidth: .5,
                        padding: '0 10 0 20',
                        defaults: {
                            width: '100%',
                            ui: 'fieldLabelTop',
                            
                            labelAlign: 'top'
                        },
                        items:[
                        {
                            xtype: 'numberfield',
                            minValue: 0,
                            maxValue: 100,
                            hideTrigger:true,
                            fieldLabel: app.localize('InvoiceSchedule1'),
                            emptyText: app.localize('ToolTipInvoiceSchedule1')
                        }, {
                            xtype: 'numberfield',
                            minValue: 0,
                            maxValue: 100,
                            hideTrigger: true,
                            fieldLabel: app.localize('InvoiceSchedule2'),
                            emptyText: app.localize('ToolTipInvoiceSchedule2')
                        }, {
                            xtype: 'numberfield',
                            minValue: 0,
                            maxValue: 100,
                            hideTrigger: true,
                            fieldLabel: app.localize('InvoiceSchedule3'),
                            emptyText: app.localize('ToolTipInvoiceSchedule3')
                        }, {///TODO: Rplace with combo once terms section is completed on contracts tab
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
                        }, {
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
                            width:200,
                            disabled:true
                        }
                        ////TODO :  uncomment if required to attach the file
                        //, {
                        //    xtype: 'filebutton',
                        //    scale: 'small',
                        //    itemId: 'AttachContract',
                        //    text: app.localize('AttachContract'),
                        //    ui: 'actionButton',
                        //    iconCls: 'fa fa-plus-circle',
                        //    width: 150
                        //}
                        //, {
                        //        xtype: 'filefield',
                        //        name: 'attachContract',
                        //        itemId: 'attachContract',
                        //        fieldLabel: app.localize('AttachContract'),
                        //        emptyText: app.localize('ToolTopAttachContract'),
                        //        buttonText: app.localize('Attach'),
                        //        ui: 'fieldLabelTopType2'
                        //    }
                        ]
                    }]
                    
                }, {
                    xtype: 'fieldset',
                    title: app.localize('ShootInfo'),
                    ui: 'transparentFieldSet',
                    layout: 'column',
                    items:[
                    {
                        columnWidth: .5,
                        padding: '0 10 0 20',
                        defaults: {
                            width: '100%',
                            ui: 'fieldLabelTop',
                            labelAlign: 'top'
                        },
                        items:[
                        {//TODO: Convert to combo when director service is ready
                            xtype: 'textfield',
                            name: 'directorEmployeeId',
                            itemId: 'directorEmployeeId',
                            fieldLabel: app.localize('Director'),
                            emptyText:app.localize('SelectOption')
                        }, {//TODO: Convert to combo when executive producer service is ready
                            xtype: 'textfield',
                            name: 'executiveProducerId',
                            itemId: 'executiveProducerId',
                            fieldLabel: app.localize('ExeProducer'),
                            emptyText: app.localize('SelectOption')
                        }, {//TODO: Convert to combo when director service is ready
                            xtype: 'textfield',
                            name: 'dirOfPhotoEmployeeId',
                            itemId: 'dirOfPhotoEmployeeId',
                            fieldLabel: app.localize('DirOfPhotography'),
                            emptyText: app.localize('SelectOption')
                        },{///TODO: to be added
                            xtype: 'datefield',
                            name: 'deliveryDate',
                            itemId: 'deliveryDate',
                            format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
                            emptyText: Chaching.utilities.ChachingGlobals.defaultDateFormat,
                            fieldLabel: app.localize('DeliveryDate')
                        }, {//TODO: Convert to combo when delivery format is finalize and service is ready. This field needs to be added in dto
                            xtype: 'textfield',
                            name: 'deliveryFormat',
                            itemId: 'deliveryFormat',
                            fieldLabel: app.localize('DeliveryFormat'),
                            emptyText: app.localize('SelectOption')
                        }, {
                            xtype: 'numberfield',
                            name: 'commercialNumber',
                            itemId: 'commercialNumber',
                            minValue: 0,
                            maxValue:100,
                            fieldLabel: app.localize('CommercialNumber'),
                            emptyText:app.localize('ToolTipCommercialNumber'),
                            hideTrigger:true
                        }, {
                            xtype: 'numberfield',
                            name: 'commercialLength',
                            itemId: 'commercialLength',
                            minValue: 0,
                            maxValue: 100,
                            fieldLabel: app.localize('CommercialLength'),
                            emptyText: app.localize('ToolTipCommercialLength'),
                            hideTrigger: true
                        }]
                    }, {
                        columnWidth: .5,
                        padding: '0 10 0 20',
                        defaults: {
                            width: '100%',
                            ui: 'fieldLabelTop',

                            labelAlign: 'top'
                        },
                        items:[
                       {
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
                       }, {
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
                            xtype: 'widget.projects.projectmaintenance.projectLocations'
                        }]
                    }]
                    
                }]
            },//end of project details card  
            {///// Start of cost manager card
                title: app.localize('CostManager'),
                iconCls: 'fa fa-bar-chart'
            },//end of cost manager card
            {///// Start of Petty Cash Accounts card
                title: app.localize('PcAccount'),
                iconCls: 'fa fa-book'
            },//end of Petty Cash Accounts card
            {///// Start of PO Log card
                title: app.localize('PoLog'),
                iconCls: 'fa fa-shopping-cart'
            }//end of Po Log card
        ]
    }],
    
});
