/**
 * The class is created to as host for accounts submenu items
 * Author: Krishna Garad
 * Date: 28/04/2016
 */
/**
 * @class Chaching.view.financials.AccountsTabPanel
 * Host for accounts subMenuItems
 * @alias financials.accounts
 */
Ext.define('Chaching.view.financials.AccountsTabPanel', {
    extend: 'Chaching.view.common.tab.ChachingTabPanel',
    xtype:'financials.accounts',
    name: 'Financials.Accounts',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Accounts')
    },
    staticTabItems: null
});
