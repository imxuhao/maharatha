
Ext.define('Chaching.view.financials.accounts.SubAccountsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.financials.accounts.subaccounts.create', 'widget.financials.accounts.subaccounts.edit'],
    requires: [
        'Chaching.view.financials.accounts.SubAccountsFormController'
    ],

    controller: 'financials-accounts-subaccountsform',
    name: 'subaccounts',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'column',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CreateNewSubAccounts").initCap()
    },
    items: [
        {
            xtype: 'hiddenfield',
            name: 'subAccountId',
            value: 0
        },
    {
        columnWidth: .5,
        padding: '20 10 0 20',
        defaults: {
            labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [
            {
                xtype: 'textfield',
                name: 'subAccountNumber',
                itemId: 'subAccountNumber',
                allowBlank: false,
                fieldLabel: app.localize('SubAccountNumber').initCap() + Chaching.utilities.ChachingGlobals.mandatoryFlag,
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('MandatoryField')
            }


        , {
            xtype: 'textfield',
            name: 'caption',
            itemId: 'caption',
            allowBlank: false,
            fieldLabel: app.localize('Caption').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            hidden: true,
            emptyText: app.localize('MandatoryField')
        }
        , {
            xtype: 'combobox',
            name: 'typeofSubAccountId',
            fieldLabel: app.localize('TypeofSubAccount').initCap() + Chaching.utilities.ChachingGlobals.mandatoryFlag,
            width: '100%',
            allowBlank: false,
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField'),
            displayField: 'typeofSubAccount',
            valueField: 'typeofSubAccountId',
            bind: {
                store: '{typeOfSubAccountList}'
            }
        }
        , {
            xtype: 'checkbox',
            boxLabel: app.localize('JournalsAllowed'),
            name: 'isActive',
            labelAlign: 'right',
            inputValue: true,
            checked: false,
            boxLabelCls: 'checkboxLabel'
        }



        ]
    }

    , {
        columnWidth: .5,
        padding: '20 10 0 20',
        //bodyStyle: { 'background-color': '#F3F5F9' },
        defaults: {
            labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [
             {
                 xtype: 'textfield',
                 name: 'description',
                 itemId: 'description',
                 allowBlank: false,
                 fieldLabel: app.localize('Description').initCap() + Chaching.utilities.ChachingGlobals.mandatoryFlag,
                 width: '100%',
                 ui: 'fieldLabelTop',
                 emptyText: app.localize('MandatoryField')
             }
                  , {
                      xtype: 'checkbox',
                      boxLabel: app.localize('CorpSubAccount'),
                      name: 'isCorporateSubAccount',
                      labelAlign: 'right',
                      inputValue: true,
                      checked: false,
                      boxLabelCls: 'checkboxLabel'
                  }
                  , {
                      xtype: 'checkbox',
                      boxLabel: app.localize('ProjectSubAccount'),
                      name: 'isProjectSubAccount',
                      labelAlign: 'right',
                      inputValue: true,
                      checked: false,
                      boxLabelCls: 'checkboxLabel'
                  }
             ,

           {
               xtype: 'checkbox',
               boxLabel: app.localize('AccountSpecific'),
               name: 'isAccountSpecific',
               labelAlign: 'right',
               inputValue: true,
               checked: false,
               boxLabelCls: 'checkboxLabel'
           }
        ]
    }
    ]
});
