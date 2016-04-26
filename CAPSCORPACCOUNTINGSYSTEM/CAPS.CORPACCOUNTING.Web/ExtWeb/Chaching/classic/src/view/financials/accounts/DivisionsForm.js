
Ext.define('Chaching.view.financials.accounts.DivisionsForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.financials.accounts.divisions.create', 'widget.financials.accounts.divisions.edit'],
    requires: [
        'Chaching.view.financials.accounts.DivisionsFormController'
    ],

    controller: 'financials-accounts-divisionsform',
    name: 'divisions',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'column',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CreateNewDivision").initCap()
    },
    items: [{
        xtype: 'hiddenfield',
        name: 'jobId',
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
            name: 'jobNumber',
            itemId: 'jobNumber',
            allowBlank: false,
            fieldLabel: app.localize('DivisionNumber').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        },
        {
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
        }]
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
                     name: 'caption',
                     itemId: 'caption',
                     allowBlank: false,
                     fieldLabel: app.localize('description').initCap(),
                     width: '100%',
                     ui: 'fieldLabelTop',
                     emptyText: app.localize('MandatoryField')
                 },                 
                 {
                     xtype: 'checkbox',
                     boxLabel: app.localize('Active'),
                     name: 'isActive',
                     labelAlign: 'right',
                     inputValue: true,
                     checked: true,
                     boxLabelCls: 'checkboxLabel',
                     hidden: false
                 }]
        }]
});
