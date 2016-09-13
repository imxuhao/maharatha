/**
 * This file contains the custom amount field considering amount seperator and currency symbols.
 * Author: Krishna Garad
 * Date:11/07/2016
 */
/**
 * A basic amount field inherited from {@link Ext.form.field.Text}.  Can be used as a direct replacement for traditional text inputs,
 * or as the base class for more sophisticated input controls (like {@link Ext.form.field.TextArea}
 * and {@link Ext.form.field.ComboBox}). Has support for empty-field placeholder values (see {@link #emptyText}).
 *
 * # Validation
 *
 * The Text field has a useful set of validations built in:
 *
 * - {@link #allowBlank} for making the field required
 * - {@link #minLength} for requiring a minimum value length
 * - {@link #maxLength} for setting a maximum value length (with {@link #enforceMaxLength} to add it
 *   as the `maxlength` attribute on the input element)
 * - {@link #regex} to specify a custom regular expression for validation
 *
 * In addition, custom validations may be added:
 *
 * - {@link #vtype} specifies a virtual type implementation from {@link Ext.form.field.VTypes} which can contain
 *   custom validation logic
 * - {@link #validator} allows a custom arbitrary function to be called during validation
 *
 * The details around how and when each of these validation options get used are described in the
 * documentation for {@link #getErrors}.
 *
 * By default, the field value is checked for validity immediately while the user is typing in the
 * field. This can be controlled with the {@link #validateOnChange}, {@link #checkChangeEvents}, and
 * {@link #checkChangeBuffer} configurations. Also see the details on Form Validation in the
 * {@link Ext.form.Panel} class documentation.
 *
 * # Masking and Character Stripping
 *
 * Text fields can be configured with custom regular expressions to be applied to entered values before
 * validation: see {@link #maskRe} and {@link #stripCharsRe} for details.
 *
 * # Example usage
 *
 *     @example
 *     Ext.create('Ext.form.Panel', {
 *         title: 'Amount Field Demo',
 *         width: 300,
 *         bodyPadding: 10,
 *         renderTo: Ext.getBody(),
 *         items: [{
 *             xtype: 'amountfield',
 *             name: 'amount',
 *             fieldLabel: 'Balance',
 *             allowBlank: false ,
 *             currency:'USD',
 *             precision:2
 *         }]
 *     });
 *
 */
Ext.define('Chaching.components.form.field.Amount', {
    extend: 'Ext.form.field.Text',
    alias: ['widget.amountfield'],
    config: {
        /**
        * @cfg {string} currency
        * Any valid currency code.
        */
        currency: 'USD',
        /**
        * @cfg {int/Number} precision
        * Digits to be shown after decimal point.
        */
        precision:2
    },
    maskRe: /[0-9.]/,
    initComponent: function () {
        var me = this,
            currency = me.getCurrency();
        me.callParent(arguments);
        me.mon(me,{
            scope: me,
            blur: me.onAmountBlur,
            focus: me.onAmountFocus
        });
        me.setCurrency(currency);
    },
    /**
    * @private
    * Event handler when field is blured out.
    */
    onAmountBlur: function () {
        var me = this;
        me.setCurrency(me.getCurrency());
    },
    /**
    * @private
    * Event handler when field is focused.
    */
    onAmountFocus: function () {
        var me = this,
           value = me.getValue();
        me.setValue(value);
    },
    /**
    * @private
    * Internal function to change currency.
     * @param {String/Number/Float} value
     * @param {String} currencySign
     * @param {Number} precision
    */
    changeCurrency:function(value, currencySign, precision,currency) {
        var me = this;
        value = me.unFormatValue(value);
        me.setValue(Ext.util.Format.currency(value, currencySign, precision));
    },
    /**
    * Set given currency to the amount entered
    * @param {String} currency
    */
    setCurrency:function(currency) {
        var me = this,
            value = me.getValue(),
            currencySign = me.getCurrencySign(currency),
            precision = me.getPrecision();
        me.changeCurrency(value, currencySign, precision, currency);
        me.currency = currency;
    },
    /**
    * Returns the currency sign
    * @param {String} currency
    * @return {Currency Symbol}
    */
    getCurrencySign: function (currency) {
        var me = this,
            currencyCodes = Chaching.utilities.ChachingGlobals.currencyCodesAndSymbols,
            currencySymbol = '$';
        if (currency) {
            var result = currencyCodes.filter(function (rec) { return rec.code === currency; });

            currencySymbol = result && result.length > 0 ? result[0].symbol : '$'; // or default symbol
        }
        return currencySymbol;
    },
    /**
   * @private
   * Internal function to unformat the value.
   * @param {Number/Float/String} value.
   */
    unFormatValue:function(value) {
        var newValue = "0.00",
            me = this;
        if (value != undefined) {
            value = value.toString();
            if (value !== null && value !== '') {
                newValue = value.replace("(", '-');
                newValue = newValue.replace(")", '');
                newValue = newValue.replace(/,/g, "");
                newValue = newValue.replace(me.getCurrencySign(me.getCurrency()),'');
            }
        }
        return newValue;
    },
    getValue: function () {
        var me = this,
            val = me.rawToValue(me.processRawValue(me.getRawValue()));
        if (val)
            val = me.unFormatValue(val);
        me.value = val;
        return val;
    },
    getSubmitData: function () {
        var me = this,
            data = null,
            val;
        if (!me.disabled && me.submitValue) {
            val = me.getSubmitValue();
            if (val !== null) {
                val = me.unFormatValue(val);
                data = {};
                data[me.getName()] = val;
            }
        }
        return data;
    }
});