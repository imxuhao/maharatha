
Ext.define('Chaching.view.financials.accounts.ChartOfAccountsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.financials.accounts.coa.create', 'widget.financials.accounts.coa.edit'],
    requires: [
        'Chaching.view.financials.accounts.ChartOfAccountsFormController'
    ],

    controller: 'financials-accounts-chartofaccountsform',
    //viewModel: {
    //    type: 'financials-accounts-chartofaccountsform'
    //},
    name: 'Financials.Accounts.ChartOfAccounts',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'column',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    //titleConfig: { 
    //    title: abp.localization.localize("CreatingNewCOA").initCap()
    //},
    //bodyStyle: { 'background-color': '#F3F5F9' },   
    items: [{
        xtype: 'hiddenfield',
        name: 'coaId',
        value: 0
    }, {
        columnWidth: .5,
        padding: '20 10 0 20',
        //bodyStyle: { 'background-color': '#F3F5F9' },
        defaults: {
           // labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [{
            xtype: 'textfield',
            name: 'caption',
            itemId: 'caption',              
            allowBlank: false,
            tabIndex: 1,
            fieldLabel: app.localize('Caption') ,
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'textfield',
            name: 'description',
            itemId: 'description',
            tabIndex: 3,
            //allowBlank: false,
            fieldLabel: app.localize('Description'),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: '' //app.localize('MandatoryField')
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('IsApproved'),
            name: 'isApproved',
            labelAlign: 'right',
            tabIndex: 5,
            inputValue: true,
            checked: true,
            boxLabelCls: 'checkboxLabel',
            hidden: false
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('IsPrivate'),
            name: 'isPrivate',
            labelAlign: 'right',
            inputValue: true,
            tabIndex: 7,
            checked: true,
            boxLabelCls: 'checkboxLabel',
            hidden: false
        }]
    }, {
        columnWidth: .5,
        padding: '20 10 0 20',
        //bodyStyle: { 'background-color': '#F3F5F9' },
        defaults: {
           // labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [{
            xtype: 'combobox',
            name: 'standardGroupTotalId',
            fieldLabel: app.localize('StdGroupTotal').initCap(),
            width: '100%',
            labelWidth: 140,
            ui: 'fieldLabelTop',
            tabIndex: 2,
            emptyText: app.localize('StdGroupTotal'),
            displayField: 'standardGroupTotal',
            valueField: 'standardGroupTotalId',
            queryMode:'local',
            bind: {
                store: '{StandardGroupTotalList}'
            }
        }, {
            xtype: 'combobox',
            name: 'linkChartOfAccountID',
            fieldLabel: app.localize('ConvertToNewCOA').initCap(),
            width: '100%',
            labelWidth: 140,
            ui: 'fieldLabelTop',
            tabIndex: 4,
            emptyText: app.localize('ConvertToNewCOA'),
            displayField: 'linkChartOfAccount',
            valueField: 'linkChartOfAccountID',
            queryMode: 'local',
            bind: {
                store: '{linkChartOfAccountList}'
            }
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('IsCorporate'),
            name: 'isCorporate',
            labelAlign: 'right',
            tabIndex: 6,
            inputValue: true,
            checked: true,
            readOnly:true,
            boxLabelCls: 'checkboxLabel'
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('IsNumeric'),
            name: 'isNumeric',
            labelAlign: 'right',
            tabIndex: 8,
            inputValue: true,
            checked: true,
            boxLabelCls: 'checkboxLabel'
        }]
    }]
});
