
Ext.define('Chaching.view.financials.accounts.SubAccountsForm',{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.financials.accounts.subaccounts.create', 'widget.financials.accounts.subaccounts.edit'],
    requires: [
        'Chaching.view.financials.accounts.SubAccountsFormController'
    ],

    controller: 'financials-accounts-subaccountsform',
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
