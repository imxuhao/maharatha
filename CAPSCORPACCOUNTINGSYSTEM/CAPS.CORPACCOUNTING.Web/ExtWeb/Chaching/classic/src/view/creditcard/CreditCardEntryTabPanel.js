/**
 * The class is created to as host for credit card entry submenu items
 * Author: Kamal
 * Date: 28/04/2016
 */
/**
 * @class Chaching.view.creditcard.CreditCardEntryTabPanel
 * Host for CreditCardEntry subMenuItems
 * @alias creditcard.entry
 */
Ext.define('Chaching.view.creditcard.CreditCardEntryTabPanel', {
    extend: 'Chaching.view.common.tab.ChachingTabPanel',
    xtype: 'creditcard.entry',
    name: 'CreditCard.Entry',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.CreditCard.Entry')
    },
    staticTabItems: null
});
