
Ext.define('Chaching.view.financials.preferences.FiscalPeriodForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.financials.preferences.fiscalperiod.create', 'widget.financials.preferences.fiscalperiod.edit'],
    requires: [
        'Chaching.view.financials.preferences.FiscalPeriodFormController'
    ],

    /**
    * @cfg {object}
    * permissions to access fiscal period.
    */
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Preferences.FiscalPeriod'),
        create: abp.auth.isGranted('Pages.Financials.Preferences.FiscalPeriod.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Preferences.FiscalPeriod.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Preferences.FiscalPeriod.Delete')
    },
    controller: 'financials.preferences.fiscalperiodform',
    name: 'fiscalperiod',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    //layout: 'column',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CreateNewFiscalPeriod").initCap()
    },
    //bodyStyle: { 'background-color': '#F3F5F9' }, 
    defaults : {
        labelWidth: 120,
        padding: '20 10 0 100',
        width: '20%',
        minWidth : 280
    },
    items: [{
        xtype: 'hiddenfield',
        name: 'fiscalYearId',
        value: 0
    }, {
         xtype: 'datefield',
         name: 'yearStartDate',
         itemId: 'yearStartDate',
         allowBlank: false,
         fieldLabel: app.localize('FiscalStartDate'),
         format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
         emptyText: Chaching.utilities.ChachingGlobals.defaultDateFormat,
        // width: '100%',
         ui: 'fieldLabelTop'
     }, {
         xtype: 'datefield',
         name: 'yearEndDate',
         itemId: 'yearEndDate',
         allowBlank: false,
         fieldLabel: app.localize('FiscalEndDate'),
         format: Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat,
         emptyText: Chaching.utilities.ChachingGlobals.defaultDateFormat,
        // width: '100%',
         ui: 'fieldLabelTop'
     }, {
         xtype: 'checkbox',
         boxLabel: app.localize('FiscalYearOpen'),
         name: 'isYearOpen',
         labelAlign: 'right',
         inputValue: true,
         checked: true,
         boxLabelCls: 'checkboxLabel',
         hidden: false
     }]
});
