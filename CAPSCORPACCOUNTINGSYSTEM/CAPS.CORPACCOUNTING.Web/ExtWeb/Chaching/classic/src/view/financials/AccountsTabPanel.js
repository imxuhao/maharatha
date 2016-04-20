
Ext.define('Chaching.view.financials.AccountsTabPanel', {
    extend: 'Chaching.view.common.tab.ChachingTabPanel',

    requires: [
        'Chaching.view.financials.AccountsTabPanelController',
        'Chaching.view.financials.AccountsTabPanelModel'
    ],
    xtype:'financials.accounts',
    controller: 'financials-accountstabpanel',
    viewModel: {
        type: 'financials-accountstabpanel'
    },
    name: 'Financials.Accounts',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts')
    },
    staticTabItems: null
});
