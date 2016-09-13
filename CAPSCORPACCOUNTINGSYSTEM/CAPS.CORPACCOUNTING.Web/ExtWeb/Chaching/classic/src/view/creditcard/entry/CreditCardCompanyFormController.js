/**
 * The viewController class for Credit Card Company Form .
 * Author: kamal
 * Date: 28/04/2016
 */
/**
 * @class Chaching.view.creditcard.entry.CreditCardCompanyFormController
 * ViewController class for Credit card company form.
 * @alias controller.creditcard-entry-creditcardcompanyform
 */
Ext.define('Chaching.view.creditcard.entry.CreditCardCompanyFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.creditcard-entry-creditcardcompanyform',
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
        view = me.getView();
        return record;
    }
});
