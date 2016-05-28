/**
 * The class is created to as host for preferences submenu items
 * Author: kamal shahu
 * Date: 26/05/2016
 */
/**
 * @class Chaching.view.financials.PreferencesTabPanel
 * Host for accounts subMenuItems
 * @alias financials.accounts
 */
Ext.define('Chaching.view.financials.PreferencesTabPanel', {
    extend: 'Chaching.view.common.tab.ChachingTabPanel',
    xtype: 'financials.preferences',
    name: 'Financials.Preferences',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Preferences')
    },
    staticTabItems: null
});
