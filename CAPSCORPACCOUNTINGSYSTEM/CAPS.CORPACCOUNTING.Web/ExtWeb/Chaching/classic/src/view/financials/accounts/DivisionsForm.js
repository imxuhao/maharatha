
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
        //bodyStyle: { 'background-color': '#F3F5F9' },
        defaults: {
            labelAlign: 'top',
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [{
            xtype: 'textfield',
            name: 'jobNumber',
            itemId: 'jobNumber',
            allowBlank: false,
            fieldLabel: app.localize('DivisionNumber').initCap() + Chaching.utilities.ChachingGlobals.mandatoryFlag,
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
            emptyText: app.localize('Currency'),
            displayField: 'currencyId',
            valueField: 'currencyName',
            bind: {
                // store: '{StandardGroupTotalList}'
            }
        },
        {
            xtype: 'combobox',
            name: 'typeOfBidSoftwareId',// 'iCTCompanyAccount',
            fieldLabel: app.localize('Inter-CompanyAccount'),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('Inter-CompanyAccount'),
            displayField: 'organizationName',
            valueField: 'organizationId',
            bind: {
                 store: '{getOrganizations}'
            }
        },
        {
            xtype: 'checkbox',
            boxLabel: app.localize('UseCorpDivisionDefault'),
            name: 'isICTDivision',
            labelAlign: 'right',
            inputValue: true,
            checked: true,
            boxLabelCls: 'checkboxLabel',
            hidden: false
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
                     fieldLabel: app.localize('description').initCap() + Chaching.utilities.ChachingGlobals.mandatoryFlag,
                     width: '100%',
                     ui: 'fieldLabelTop',
                     emptyText: app.localize('MandatoryField')
                 },
                 {
                     xtype: 'combobox',
                     name: 'rollupAccountId',//'iCTCompanyId',
                     fieldLabel: app.localize('Inter-CompanyLink').initCap(),
                     width: '100%',
                     ui: 'fieldLabelTop',
                     emptyText: app.localize('Inter-CompanyLink'),
                     displayField: 'organizationName',
                     valueField: 'orgnizationId',
                     bind: {
                         store: '{getOrganizations}'
                     }
                 },
                 {
                     xtype: 'combobox',
                     name: 'typeofProjectId',// 'iCTCompanyAccount',
                     fieldLabel: app.localize('Inter-CompanySub').initCap(),
                     width: '100%',
                     ui: 'fieldLabelTop',
                     emptyText: app.localize('Inter-CompanySub'),
                     displayField: 'subAcccountCaption',
                     valueField: 'aubAccountId',
                     bind: {
                        
                     }
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
