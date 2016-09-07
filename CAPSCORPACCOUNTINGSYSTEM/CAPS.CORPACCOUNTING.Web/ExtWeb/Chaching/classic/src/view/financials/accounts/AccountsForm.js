
Ext.define('Chaching.view.financials.accounts.AccountsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.financials.accounts.accounts.create', 'widget.financials.accounts.accounts.edit'],
    requires: [
        'Chaching.view.financials.accounts.AccountsFormController'
    ],

    controller: 'financials-accounts-accountsform',
    name: 'Financials.Accounts.Accounts',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
        create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete'),
        attach: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Attach'),
        imports: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Import')
    },
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'column',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    popupWndSize: {
        height: '70%',
        width: '70%'
    },
    //titleConfig: {
    //    title: abp.localization.localize("CreateNewFinancialAccount").initCap()
    //},
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
            //labelAlign: 'top',
            //blankText: app.localize('MandatoryToolTipText')
            labelWidth : 120
        },
        items: [{
            xtype: 'textfield',
            name: 'accountNumber',
            itemId: 'accountNumber',
            allowBlank: false,
            fieldLabel: app.localize('AccountNumber'),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'combobox',
            name: 'typeOfAccountId',
            fieldLabel: app.localize('Classification'),
            width: '100%',
            allowBlank: false,
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField'),
            displayField: 'typeOfAccount',
            valueField: 'typeOfAccountId',
            queryMode: 'local',
            bind: {
                store: '{typeOfAccountList}'
            }
        }
        , {
            xtype: 'combobox',
            name: 'typeOfCurrencyId',
            fieldLabel: app.localize('Currency'),
            width: '100%',
            ui: 'fieldLabelTop',
            displayField: 'typeOfCurrency',
            valueField: 'typeOfCurrencyId',
            queryMode: 'local',
            bind: {
                store: '{typeOfCurrencyList}'
            }
        },{
            xtype: 'chachingcombobox',
            store: new Chaching.store.utilities.autofill.MappingAccountStore(),
            width: '100%',
            valueField: 'linkAccountId',
            displayField: 'linkAccount',
            queryMode: 'remote',
            minChars: 2,
            name: 'linkAccountId',
            ui: 'fieldLabelTop',
            fieldLabel: app.localize('NewAccount'),
            modulePermissions: {
                read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
                create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
                edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
                destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete')
            },
            primaryEntityCrudApi: {
                read: abp.appPath + 'api/services/app/accountUnit/GetAccountsForMapping',
                create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
                update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
                destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
            },
            createEditEntityType: 'financials.accounts.accounts',
            createEditEntityGridController: 'financials-accounts-accountsgrid',
            entityType: 'Account',
            isTwoEntityPicker: false,
            listeners: {
                beforequery: 'beforeMappingAccountQuery'
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
        }
        ]
    }, {
        columnWidth: .5,
        padding: '20 10 0 20',
        defaults: {
            //labelAlign: 'top',
            //blankText: app.localize('MandatoryToolTipText')
            labelWidth: 130
        },
        items: [{
            xtype: 'textfield',
            name: 'caption',
            itemId: 'caption',
            allowBlank: false,
            fieldLabel: app.localize('Description'),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'combobox',
            name: 'typeofConsolidationId',
            fieldLabel: app.localize('Consolidation'),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField'),
            displayField: 'typeofConsolidation',
            valueField: 'typeofConsolidationId',
            queryMode : 'local',
            bind: {
                store: '{typeofConsolidationList}'
            }
        }, {
            xtype: 'combobox',
            name: 'typeOfCurrencyRateId',
            fieldLabel: app.localize('RateTypeOverride'),
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
            boxLabel: app.localize('EliminationAccount'),
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
    }]
});