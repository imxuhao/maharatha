
Ext.define('Chaching.view.projects.projectmaintenance.LineNumbersForm',{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.projects.projectmaintenance.linenumbers.create', 'widget.projects.projectmaintenance.linenumbers.edit'],
    requires: [
        'Chaching.view.projects.projectmaintenance.LineNumbersFormController'       
    ],

    controller: 'projects-projectmaintenance-linenumbersform',   

    name: 'accounts',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'column',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CreateNewLine").initCap()
    },
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
        create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete')
    },
    items: [{
        xtype: 'hiddenfield',
        name: 'accountId',
        value: 0
    }, {
        xtype: 'hiddenfield',
        name: 'chartOfAccountId',
        value: 0
    }, {
        columnWidth: .5,
        padding: '20 10 0 20',
        defaults: {
            labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [{
            xtype: 'textfield',
            name: 'accountNumber',
            itemId: 'accountNumber',
            allowBlank: false,
            fieldLabel: app.localize('LineNumber').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'combobox',
            name: 'typeOfAccountId',
            fieldLabel: app.localize('Classification').initCap(),
            width: '100%',            
            ui: 'fieldLabelTop',          
            displayField: 'typeOfAccount',
            valueField: 'typeOfAccountId',
            bind: {
                store: '{typeOfAccountList}'
            }
        }
        , {
            xtype: 'combobox',
            name: 'rollupAccountId',
            fieldLabel: app.localize('RollUpAccount').initCap(),
            width: '100%',           
            ui: 'fieldLabelTop',           
            displayField: 'name',
            valueField: 'value',
            store: 'financials.accounts.RollupAccountStore',
            queryMode:'local'           
        }
        , {
            xtype: 'combobox',
            name: 'typeOfCurrencyId',
            fieldLabel: app.localize('Currency').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            displayField: 'typeOfCurrency',
            valueField: 'typeOfCurrencyId',
            bind: {
                store: '{typeOfCurrencyList}'
            }
        }        
        ]
    }, {
        columnWidth: .5,
        padding: '20 10 0 20',
        defaults: {
            labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [{
            xtype: 'textfield',
            name: 'caption',
            itemId: 'caption',
            allowBlank: false,
            fieldLabel: app.localize('Caption').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'combobox',
            name: 'typeofConsolidationId',
            fieldLabel: app.localize('Consolidation').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',            
            displayField: 'typeofConsolidation',
            valueField: 'typeofConsolidationId',
            bind: {
                store: '{typeofConsolidationList}'
            }
        }, {
            xtype: 'combobox',
            name: 'rollupJobId',
            fieldLabel: app.localize('RollUpDivision').initCap(),
            width: '100%',           
            ui: 'fieldLabelTop',           
            displayField: 'rollupDivision',
            valueField: 'rollupDivisionId',
            bind: {
                store: '{rollupDivisionList}'
            }
        }
        ,
        {
            xtype: 'checkbox',
            boxLabel: app.localize('JournalsAllowed'),
            name: 'isEnterable',
            labelAlign: 'right',
            inputValue: true,
            checked: false,
            boxLabelCls: 'checkboxLabel'
        }
        ]
    }]
});
