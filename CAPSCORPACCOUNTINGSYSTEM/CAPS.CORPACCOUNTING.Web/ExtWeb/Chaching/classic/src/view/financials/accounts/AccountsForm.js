
Ext.define('Chaching.view.financials.accounts.AccountsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.financials.accounts.accounts.create', 'widget.financials.accounts.accounts.edit'],
    requires: [
        'Chaching.view.financials.accounts.AccountsFormController'
    ],

    controller: 'financials-accounts-accountsform',
    name: 'accounts',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'column',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    titleConfig: {
        title: abp.localization.localize("CreateNewFinancialAccount").initCap()
    },
    items: [{
        xtype: 'hiddenfield',
        name: 'accountId',
        value: 0
    },{
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
            fieldLabel: app.localize('Account').initCap(),
            width: '100%',
            ui: 'fieldLabelTop'
        }, {
            xtype: 'combobox',
            name: 'typeOfAccountId',
            fieldLabel: app.localize('Classification').initCap() + Chaching.utilities.ChachingGlobals.mandatoryFlag,
            width: '100%',
            allowBlank: false,
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField'),
            displayField: 'typeOfAccount',
            valueField: 'typeOfAccountId',
            bind: {
                store: '{typeOfAccountList}'
            }
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
        },
        {
            xtype: 'checkbox',
            boxLabel: app.localize('Multi-CurrencyReval'),
            name: 'isAccountRevalued',
            labelAlign: 'right',
            inputValue: true,
            checked: false,
            boxLabelCls: 'checkboxLabel'
        },
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
            fieldLabel: app.localize('Description').initCap() + Chaching.utilities.ChachingGlobals.mandatoryFlag,
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'combobox',
            name: 'typeofConsolidationId',
            fieldLabel: app.localize('Consolidation').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField'),
            displayField: 'typeofConsolidation',
            valueField: 'typeofConsolidationId',
            bind: {
                store: '{typeofConsolidationList}'
            }
        }, {
            xtype: 'combobox',
            name: 'typeOfCurrencyRateId',
            fieldLabel: app.localize('RateTypeOverride').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            displayField: 'typeOfCurrencyRate',
            valueField: 'typeOfCurrencyRateId',
            bind: {
                store: '{typeOfCurrencyRateList}'
            }
        },
        {
            xtype: 'checkbox',
            boxLabel: app.localize('RateTypeOverride'),
            name: 'isElimination',
            labelAlign: 'right',
            inputValue: true,
            checked: false,
            boxLabelCls: 'checkboxLabel'
        }
        ,
        {
            xtype: 'checkbox',
            boxLabel: app.localize('RollUpAccount'),
            name: 'isRollupAccount',
            labelAlign: 'right',
            inputValue: true,
            checked: false,
            boxLabelCls: 'checkboxLabel'
        }
        ]
    }]
});