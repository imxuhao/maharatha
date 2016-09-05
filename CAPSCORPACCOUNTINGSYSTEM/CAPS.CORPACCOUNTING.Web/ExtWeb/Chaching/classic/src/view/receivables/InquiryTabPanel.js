/**
 * The class is created as a container to hold inquiry items in Receivable
 * Author: Rajesh Sharma
 * Date: 02/09/2016
 */
/**
 * @class Chaching.view.financials.AccountsTabPanel
 * Host for accounts subMenuItems
 * @alias financials.accounts
 */
Ext.define('Chaching.view.receivables.InquiryTabPanel', {
    extend: 'Chaching.view.common.tab.ChachingTabPanel',
    xtype: 'receivables.inquiry',
    name: 'Receivables.Inquiry',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Receivables.Inquiry')
    },
    staticTabItems: null
});
