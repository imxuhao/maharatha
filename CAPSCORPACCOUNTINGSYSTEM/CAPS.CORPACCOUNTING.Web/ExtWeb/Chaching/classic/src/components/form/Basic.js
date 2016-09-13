/**
 * This Class is created for Basic form for chaching.
 * Author: Krishna Garad
 * Date: 05/05/2016
 */
/**
 * Provides input field management, validation, submission, and form loading services for the collection
 * of {@link Ext.form.field.Field Field} instances within a {@link Ext.container.Container}. It is recommended
 * that you use a {@link Ext.form.Panel} as the form container, as that has logic to automatically
 * hook up an instance of {@link Ext.form.Basic} (plus other conveniences related to field configuration.)
 *
 * ## Form Actions
 *
 * The Basic class delegates the handling of form loads and submits to instances of {@link Ext.form.action.Action}.
 * See the various Action implementations for specific details of each one's functionality, as well as the
 * documentation for {@link #doAction} which details the configuration options that can be specified in
 * each action call.
 *
 * The default submit Action is {@link Ext.form.action.Submit}, which uses an Ajax request to submit the
 * form's values to a configured URL. To enable normal browser submission of an Ext form, use the
 * {@link #standardSubmit} config option.
 *
 * ## File uploads
 *
 * File uploads are not performed using normal 'Ajax' techniques; see the description for
 * {@link #hasUpload} for details. If you're using file uploads you should read the method description.
 *
 * ## Example usage:
 *
 *     @example
 *     Ext.create('Chaching.components.form.Panel', {
 *         title: 'Basic Form',
 *         renderTo: Ext.getBody(),
 *         bodyPadding: 5,
 *         width: 350,
 *
 *         // Any configuration items here will be automatically passed along to
 *         // the Ext.form.Basic instance when it gets created.
 *
 *         // The form will submit an AJAX request to this URL when submitted
 *         url: 'save-form.php',
 *
 *         items: [{
 *             xtype: 'textfield',
 *             fieldLabel: 'Field',
 *             name: 'theField'
 *         }],
 *
 *         buttons: [{
 *             text: 'Submit',
 *             handler: function() {
 *                 // The getForm() method returns the Ext.form.Basic instance:
 *                 var form = this.up('form').getForm();
 *                 if (form.isValid()) {
 *                     // Submit the Ajax request and handle the response
 *                     form.submit({
 *                         success: function(form, action) {
 *                            Ext.Msg.alert('Success', action.result.message);
 *                         },
 *                         failure: function(form, action) {
 *                             Ext.Msg.alert('Failed', action.result ? action.result.message : 'No response');
 *                         }
 *                     });
 *                 }
 *             }
 *         }]
 *     });
 */
Ext.define('Chaching.components.form.Basic', {
    extend: 'Ext.form.Basic',
    alternateClassName: 'Chaching.form.BasicForm',
    alias: 'form.chachingBasicForm',
    /**
    * Loads an {@link Ext.data.Model} into this form by calling {@link #setValues} with the
    * {@link Ext.data.Model#getData record data}. The fields in the model are mapped to 
    * fields in the form by matching either the {@link Ext.form.field.Base#name} or {@link Ext.Component#itemId}.  
    * See also {@link #trackResetOnLoad}. 
    * @param {Ext.data.Model} record The record to load
    * @return {Ext.form.Basic} this
    */
    loadRecord: function (record) {
        var me = this;
        me._record = record;
        if (record && record.associations) {
            var associationInfo = record.associations;
            for (var associationKey in associationInfo) {
                if (associationInfo.hasOwnProperty(associationKey)) {
                    var association = associationInfo[associationKey];
                    var associationRecord = record[association.instanceName];
                    if (associationRecord) {
                        for (var key in associationRecord.data) {
                            if (associationRecord.data.hasOwnProperty(key) && key !== "id") {
                                record.data[key] = associationRecord.data[key];
                            }
                        }
                    }
                }
            }
        }
        return this.setValues(record.getData());
    },
    /**
     * Set values for fields in this form in bulk.
     *
     * @param {Object/Object[]} values Either an array in the form:
     *
     *     [{id:'clientName', value:'Fred. Olsen Lines'},
     *      {id:'portOfLoading', value:'FXT'},
     *      {id:'portOfDischarge', value:'OSL'} ]
     *
     * or an object hash of the form:
     *
     *     {
     *         clientName: 'Fred. Olsen Lines',
     *         portOfLoading: 'FXT',
     *         portOfDischarge: 'OSL'
     *     }
     *
     * @return {Ext.form.Basic} this
     */
    setValues: function (values) {
        var me = this,
            v, vLen, val;

        function setVal(fieldId, val) {
            var field = me.findField(fieldId);
            if (field) {
                //isEditMode used to idenfiy the mode of form field during load record for event check like(change evnt on field)
                field.isEditMode = true;
                field.setValue(val);
                //Reseting the isEditMode after setting value in field using load record
                field.isEditMode = false;
                if (me.trackResetOnLoad) {
                    field.resetOriginalValue();
                }
            }
        }

        // Suspend here because setting the value on a field could trigger
        // a layout, for example if an error gets set, or it's a display field
        Ext.suspendLayouts();
        if (Ext.isArray(values)) {
            // array of objects
            vLen = values.length;

            for (v = 0; v < vLen; v++) {
                val = values[v];

                setVal(val.id, val.value);
            }
        } else {
            // object hash
            Ext.iterate(values, setVal);
        }
        Ext.resumeLayouts(true);
        return this;
    }
});