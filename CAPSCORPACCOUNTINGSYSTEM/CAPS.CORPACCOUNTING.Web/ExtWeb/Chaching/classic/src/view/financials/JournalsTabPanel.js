/**
 * The class is created to as host for journals submenu items
 * Author: Krishna Garad
 * Date: 13/05/2016
 */
/**
 * @class Chaching.view.financials.JournalsTabPanel
 * Host for Journals subMenuItems
 * @alias financials.journals
 */
Ext.define('Chaching.view.financials.JournalsTabPanel',{
    extend: 'Chaching.view.common.tab.ChachingTabPanel',
    xtype: 'financials.journals',
    name: 'Financials.Journals',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Journals')
    },
    staticTabItems: null
});
