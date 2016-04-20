
Ext.define('Chaching.view.financials.accounts.DivisionsForm',{
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
        title: abp.localization.localize("CreatingNewCOA").initCap()
    },
    html: 'Hello, World!!'
});
