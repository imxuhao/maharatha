
Ext.define('Chaching.view.financials.accounts.SubAccountsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.financials.accounts.subaccounts.create', 'widget.financials.accounts.subaccounts.edit'],
    requires: [
        'Chaching.view.financials.accounts.SubAccountsFormController',
        'Chaching.components.dragdrop.GridToGrid'
    ],

    controller: 'financials-accounts-subaccountsform',
    name: 'Financials.Accounts.SubAccounts',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'column',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    //titleConfig: {
    //    title: abp.localization.localize("CreateNewSubAccounts").initCap()
    //},
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
            //labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [
            {
                xtype: 'textfield',
                name: 'subAccountNumber',
                itemId: 'subAccountNumber',
                allowBlank: false,
                tabIndex: 1,
                labelWidth : 150,
                fieldLabel: app.localize('SubAccountNumber').initCap() ,
                width: '100%',
                ui: 'fieldLabelTop',
                emptyText: app.localize('MandatoryField')
            }


        , {
            xtype: 'textfield',
            name: 'caption',
            itemId: 'caption',
            allowBlank: false,
            labelWidth: 150,
            fieldLabel: app.localize('Caption').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            hidden: true,
            emptyText: app.localize('MandatoryField')
        }
        , {
            xtype: 'combobox',
            name: 'typeofSubAccountId',
            fieldLabel: app.localize('TypeofSubAccount').initCap() ,
            width: '100%',
            allowBlank: false,
            tabIndex: 3,
            labelWidth: 150,
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField'),
            displayField: 'typeofSubAccount',
            valueField: 'typeofSubAccountId',
            queryMode : 'local',
            bind: {
                store: '{typeOfSubAccountList}'
            }
        }
        , {
            xtype: 'checkbox',
            boxLabel: app.localize('JournalsAllowed'),
            name: 'isActive',
            labelAlign: 'right',
            tabIndex: 5,
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
            //labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [
             {
                 xtype: 'textfield',
                 name: 'description',
                 itemId: 'description',
                 tabIndex: 2,
                 allowBlank: false,
                 fieldLabel: app.localize('Description').initCap(),
                 width: '100%',
                 ui: 'fieldLabelTop',
                 emptyText: app.localize('MandatoryField')
             }
                  , {
                      xtype: 'checkbox',
                      boxLabel: app.localize('CorpSubAccount'),
                      tabIndex: 4,
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
                      tabIndex: 6,
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
               tabIndex: 7,
               inputValue: true,
               checked: false,
               boxLabelCls: 'checkboxLabel',
               listeners: {
                   change:'onAccountSpecificChange'
               }
           }
        ]
    },
    {
        columnWidth: 1.0,
        items:[
        {
            xtype: 'chachingGridDragDrop',
            leftTitle: '',
            rightTitle: '',
            hidden:true,
            columns: [
                {
                    xtype: 'gridcolumn',
                    text: app.localize('Number'),
                    dataIndex: 'accountNumber',
                    sortable: true,
                    groupable: true,
                    width: '47%',
                    filterField: {
                        xtype: 'textfield',
                        width: '100%'
                    }
                },{
                    xtype: 'gridcolumn',
                    text:app.localize('Caption'),
                    dataIndex: 'caption',
                    sortable: true,
                    groupable: true,
                    width: '47%',
                    filterField: {
                        xtype: 'textfield',
                        width: '15%'
                    }
                }
            ],
           // store: 'Chaching.store.financials.accounts.ChartOfAccountStore',
            loadStoreOnCreate: false,
            leftStore: 'Chaching.store.financials.accounts.AccountRestrictionLeftStore',
            rightStore: 'Chaching.store.financials.accounts.AccountRestrictionRightStore',
            requireMultiSearch:true,
            selModelConfig: {
                selType: 'chachingCheckboxSelectionModel',
                injectCheckbox: "first",
                headerWidth: '5%',
                mode: 'MULTI',
                showHeaderCheckbox: false
            },
            doSaveOperation: function(direction, records) {
                var isActive = direction === "leftToRight" ? true : false,
                    wasActive = direction === "rightToLeft" ? true : false;
                for (var i = 0; i < records.length; i++) {
                    var rec = records[i];
                    rec.set('isActive', isActive);
                    rec.set('wasActive', wasActive);
                }
            }
        }]
     }
    ]
});
