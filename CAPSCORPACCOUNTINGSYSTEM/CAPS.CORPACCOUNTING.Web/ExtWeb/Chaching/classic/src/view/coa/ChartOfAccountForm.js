
Ext.define('Chaching.view.coa.ChartOfAccountForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['coa.create', 'coa.edit'],
    requires: [
        'Chaching.view.coa.ChartOfAccountFormController',
        'Chaching.view.coa.ChartOfAccountFormModel'
    ],

    controller: 'coa-chartofaccountform',
    viewModel: {
        type: 'coa-chartofaccountform'
    },
    name: 'coa',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'vbox',
    autoScroll: true,
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'top',
        blankText: app.localize('MandatoryToolTipText')
    },
    defaultFocus: 'textfield#caption',
    items: [{
        xtype: 'hiddenfield',
        name: 'coaId',
        value: 0
    }
    ,
    {
        xtype: 'textfield',
        name: 'caption',
        itemId: 'caption',
        allowBlank: false,
        fieldLabel: app.localize('Caption').initCap(),
        width: '100%',
        ui: 'fieldLabelTop',
        emptyText: app.localize('Caption')
    },
    {
        xtype: 'textfield',
        name: 'description',
        itemId: 'description',
        allowBlank: false,
        fieldLabel: app.localize('description').initCap(),
        width: '100%',
        ui: 'fieldLabelTop',
        emptyText: app.localize('Description')
    },
    {
        xtype: 'checkbox',
        boxLabel: app.localize('IsApproved'),
        name: 'isApproved',
        labelAlign: 'right',
        inputValue: true,
        checked: true,
        boxLabelCls: 'checkboxLabel',
        hidden:true
    },
    {
        xtype: 'checkbox',
        boxLabel: app.localize('IsPrivate'),
        name: 'isPrivate',
        labelAlign: 'right',
        inputValue: true,
        checked: true,
        boxLabelCls: 'checkboxLabel',
        hidden: true
    },
    {
        xtype: 'checkbox',
        boxLabel: app.localize('IsCorporate'),
        name: 'isCorporate',
        labelAlign: 'right',
        inputValue: true,
        checked: false,
        boxLabelCls: 'checkboxLabel'
    },
    {
        xtype: 'checkbox',
        boxLabel: app.localize('IsNumeric'),
        name: 'isNumeric',
        labelAlign: 'right',
        inputValue: true,
        checked: true,
        boxLabelCls: 'checkboxLabel'
    },
    {
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
    }
    ,
    {
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
    }
    ]

});
