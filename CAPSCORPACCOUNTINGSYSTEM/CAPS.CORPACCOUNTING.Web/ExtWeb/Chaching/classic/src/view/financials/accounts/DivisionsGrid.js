
Ext.define('Chaching.view.financials.accounts.DivisionsGrid',{
    extend: 'Ext.panel.Panel',
    xtype: 'widget.financials.accounts.divisions',
    requires: [
        'Chaching.view.financials.accounts.DivisionsGridController'
    ],

    controller: 'financials-accounts-divisionsgrid',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts.Divisions'),
        create: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Delete')
    },

    html: 'Hello, World!!'
});
