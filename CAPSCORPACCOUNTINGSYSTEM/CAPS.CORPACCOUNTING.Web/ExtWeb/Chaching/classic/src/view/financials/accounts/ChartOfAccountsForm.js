
Ext.define('Chaching.view.financials.accounts.ChartOfAccountsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.financials.accounts.coa.create', 'widget.financials.accounts.coa.edit',
        'widget.projects.projectmaintenance.projectcoas.create', 'widget.projects.projectmaintenance.projectcoas.edit'],
    requires: [
        'Chaching.view.financials.accounts.ChartOfAccountsFormController'
    ],

    controller: 'financials-accounts-chartofaccountsform',
    //viewModel: {
    //    type: 'financials-accounts-chartofaccountsform'
    //},
    name: 'coa',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'column',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    displayDefaultButtonsCenter:true,
    titleConfig: { 
        title: abp.localization.localize("CreatingNewCOA").initCap()
    },
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
            labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [{
            xtype: 'textfield',
            name: 'caption',
            itemId: 'caption',
            allowBlank: false,
            fieldLabel: app.localize('Caption').initCap() + Chaching.utilities.ChachingGlobals.mandatoryFlag,
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'textfield',
            name: 'description',
            itemId: 'description',
            allowBlank: false,
            fieldLabel: app.localize('description').initCap() + Chaching.utilities.ChachingGlobals.mandatoryFlag,
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('IsApproved'),
            name: 'isApproved',
            labelAlign: 'right',
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
            checked: true,
            boxLabelCls: 'checkboxLabel',
            hidden: false
        }]
    }, {
        columnWidth: .5,
        padding: '20 10 0 20',
        //bodyStyle: { 'background-color': '#F3F5F9' },
        defaults: {
            labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [{
            xtype: 'combobox',
            name: 'standardGroupTotalId',
            fieldLabel: app.localize('StdGroupTotal').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('StdGroupTotal'),
            displayField: 'standardGroupTotal',
            valueField: 'standardGroupTotalId',
            bind: {
                store: '{StandardGroupTotalList}'
            }
        }, {
            xtype: 'combobox',
            name: 'linkChartOfAccountID',
            fieldLabel: app.localize('ConvertToNewCOA').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('ConvertToNewCOA'),
            displayField: 'linkChartOfAccount',
            valueField: 'linkChartOfAccountID',
            bind: {
                store: '{linkChartOfAccountList}'
            }
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('IsCorporate'),
            name: 'isCorporate',
            labelAlign: 'right',
            inputValue: true,
            checked: true,
            readOnly:true,
            boxLabelCls: 'checkboxLabel'
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('IsNumeric'),
            name: 'isNumeric',
            labelAlign: 'right',
            inputValue: true,
            checked: true,
            boxLabelCls: 'checkboxLabel'
        }]
    }]
});
