/**
 * The class is created to as host for organizations submenu items
 * Author: Krishna Garad
 * Date: 28/04/2016
 */
/**
 * @class Chaching.view.financials.AccountsTabPanel
 * Host for accounts subMenuItems
 * @alias financials.accounts
 */
Ext.define('Chaching.view.administration.OrganizationTabPanel', {
    extend: 'Chaching.view.common.tab.ChachingTabPanel',
   // xtype: 'organizationUnits',
   // name: 'Administration.OrganizationUnits',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.OrganizationUnits')
    },
    staticTabItems: null
});
