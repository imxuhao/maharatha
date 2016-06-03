
Ext.define('Chaching.view.financials.fiscalperiod.FiscalPeriodForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.financials.fiscalperiod.create', 'widget.financials.fiscalperiod.edit'],
    requires: [
        'Chaching.view.financials.fiscalperiod.FiscalPeriodFormController'
    ],

    /**
    * @cfg {object}
    * permissions to access fiscal period.
    */
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.FiscalPeriod'),
        create: abp.auth.isGranted('Pages.Financials.FiscalPeriod.Create'),
        edit: abp.auth.isGranted('Pages.Financials.FiscalPeriod.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.FiscalPeriod.Delete')
    },
    controller: 'financials-fiscalperiod-fiscalperiodform',
    name: 'fiscalperiod',
    openInPopupWindow: false,
    hideDefaultButtons: false,
    //layout: 'column',
    autoScroll: false,
    border: false,
    showFormTitle: true,
    displayDefaultButtonsCenter: true,
    titleConfig: {
        title: abp.localization.localize("CreateNewFiscalPeriod").initCap()
    },
    //bodyStyle: { 'background-color': '#F3F5F9' }, 
    defaults : {
        labelWidth: 120,
        padding: '20 10 0 20',
        width: '20%',
        minWidth : 280
    },
    items: [
        {
            xtype : 'fieldcontainer',
            layout: 'hbox',
            defaults : {
                padding : '0 5 0 20'
            },
            items: [{
                xtype: 'hiddenfield',
                name: 'fiscalYearId',
                value: 0
            }, {
                xtype: 'datefield',
                name: 'yearStartDate',
                itemId: 'yearStartDate',
                labelWidth : 120,
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
        }
        , {
            xtype: 'financials.fiscalperiodchildgrid',
             anchor : '100% 80%',
            autoScroll : true
     }]
});
