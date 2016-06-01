/**
 * This class is created as a base formPanel class for all transactions forms.
 * Author: Krishna Garad
 * Date Created: 16/05/2016
 */
/**
 * FormPanel provides a standard container for forms. It is essentially a standard {@link Ext.panel.Panel} which
 * automatically creates a {@link Ext.form.Basic BasicForm} for managing any {@link Ext.form.field.Field}
 * objects that are added as descendants of the panel. It also includes conveniences for configuring and
 * working with the BasicForm and the collection of Fields.
 * 
 * # Layout
 * 
 * By default, FormPanel is configured with `{@link Ext.layout.container.Anchor layout:'anchor'}` for
 * the layout of its immediate child items. This can be changed to any of the supported container layouts.
 * The layout of sub-containers is configured in {@link Ext.container.Container#layout the standard way}.
 * 
 * # BasicForm
 * 
 * FormPanel class accepts all
 * of the config options supported by the {@link Ext.form.Basic} class, and will pass them along to
 * the internal BasicForm when it is created.
 * 
 * The following events fired by the BasicForm will be re-fired by the FormPanel and can therefore be
 * listened for on the FormPanel itself:
 * 
 * - {@link Ext.form.Basic#beforeaction beforeaction}
 * - {@link Ext.form.Basic#actionfailed actionfailed}
 * - {@link Ext.form.Basic#actioncomplete actioncomplete}
 * - {@link Ext.form.Basic#validitychange validitychange}
 * - {@link Ext.form.Basic#dirtychange dirtychange}
 * 
 * # Field Defaults
 * 
 * The {@link #fieldDefaults} config option conveniently allows centralized configuration of default values
 * for all fields added as descendants of the FormPanel. Any config option recognized by implementations
 * of {@link Ext.form.Labelable} may be included in this object. See the {@link #fieldDefaults} documentation
 * for details of how the defaults are applied.
 * 
 * # Form Validation
 * 
 * With the default configuration, form fields are validated on-the-fly while the user edits their values.
 * This can be controlled on a per-field basis (or via the {@link #fieldDefaults} config) with the field
 * config properties {@link Ext.form.field.Field#validateOnChange} and {@link Ext.form.field.Base#checkChangeEvents},
 * and the FormPanel's config properties {@link #pollForChanges} and {@link #pollInterval}.
 * 
 * Any component within the FormPanel can be configured with `formBind: true`. This will cause that
 * component to be automatically disabled when the form is invalid, and enabled when it is valid. This is most
 * commonly used for Button components to prevent submitting the form in an invalid state, but can be used on
 * any component type.
 * 
 * For more information on form validation see the following:
 * 
 * - {@link Ext.form.field.Field#validateOnChange}
 * - {@link #pollForChanges} and {@link #pollInterval}
 * - {@link Ext.form.field.VTypes}
 * - {@link Ext.form.Basic#doAction BasicForm.doAction clientValidation notes}
 * 
 * # Form Submission
 * 
 * By default, Ext Forms are submitted through Ajax, using {@link Ext.form.action.Action}. See the documentation for
 * {@link Ext.form.Basic} for details.
 *
 * # Example usage
 * 
 *     @example
 *     Ext.create('Chaching.common.form.ChachingFormPanel', {
 *         title: 'Simple Form',
 *         bodyPadding: 5,
 *         width: 350,
 * 
 *         // The form will submit an AJAX request to this URL when submitted
 *         url: 'save-form.php',
 * 
 *         // Fields will be arranged vertically, stretched to full width
 *         layout: 'anchor',
 *         defaults: {
 *             anchor: '100%'
 *         },
 * 
 *         // The fields
 *         defaultType: 'textfield',
 *         items: [{
 *             fieldLabel: 'First Name',
 *             name: 'first',
 *             allowBlank: false
 *         },{
 *             fieldLabel: 'Last Name',
 *             name: 'last',
 *             allowBlank: false
 *         }],
 * 
 *         // Reset and Submit buttons
 *         buttons: [{
 *             text: 'Reset',
 *             handler: function() {
 *                 this.up('form').getForm().reset();
 *             }
 *         }, {
 *             text: 'Submit',
 *             formBind: true, //only enabled once the form is valid
 *             disabled: true,
 *             handler: function() {
 *                 var form = this.up('form').getForm();
 *                 if (form.isValid()) {
 *                     form.submit({
 *                         success: function(form, action) {
 *                            Ext.Msg.alert('Success', action.result.msg);
 *                         },
 *                         failure: function(form, action) {
 *                             Ext.Msg.alert('Failed', action.result.msg);
 *                         }
 *                     });
 *                 }
 *             }
 *         }],
 *         renderTo: Ext.getBody()
 *     });
 *
 */
Ext.define('Chaching.view.common.form.ChachingTransactionFormPanel',{
    extend: 'Chaching.components.form.Panel',

    requires: [
        'Chaching.view.common.form.ChachingTransactionFormPanelController',
        'Chaching.view.common.form.ChachingTransactionFormPanelModel'
    ],

    controller: 'common-form-chachingtransactionformpanel',
    viewModel: {
        type: 'common-form-chachingtransactionformpanel'
    },

    /**
    * @cfg {object} set up module permissions for the form.
    */
    modulePermissions: {
        read: true,
        create: true,
        edit: true,
        destroy:true
    },
    /**
    * @cfg {object} parentGrid from which this formpanel is created
    */
    parentGrid: null,
    /**
    * @cfg {object} form specific buttons object.
    */
    formButtons: null,
    /**
    * @cfg {true} default values to be loaded when the form loads
    * Returns the function which can be handled in respective controller class to load default values.
    */
    defaultValuesToLoad: true,
    /**
    * @cfg {boolean} open this form in window panel.
    * if set to true then popup window will be opened containing this form
    */
    openInPopupWindow: false,
    /**
    * @cfg {boolean} save button to act accordingly create/update.   
    */
    isEditing: false,
    referenceHolder: true,
    border: false,
    frame: false,
    showFormTitle: false,
    titleConfig: null,
    isTransactionForm:true,
    initComponent: function() {
        var me = this,
            controller = me.getController();
        var defaultActionToolBar = me.getDefaultActionToolbar(),
            formSpecificActionToolBar = me.getFormSpecificActionToolbar(me.formButtons);
       
        me.dockedItems = [
            {
                xtype: 'panel',
                dock: 'bottom',
                items: [defaultActionToolBar, formSpecificActionToolBar]
            }
        ];
       
        me.callParent(arguments);
        me.defaultActionGroup = me.dockedItems[0];
        me.on('resize', controller.onFormResize, this);
        if (me.defaultValuesToLoad) {
            controller.setDefaultValues();
        }
    },
    getDefaultActionToolbar: function() {
        var me = this;
        var buttons = [];
        buttons.push('->');
        if (me.modulePermissions.create || me.modulePermissions.edit) {
            //save current
            buttons.push({
                xtype: 'button',
                scale: 'small',
                iconCls: 'fa fa-check',
                iconAlign: 'left',
                text: app.localize('Save').toUpperCase(),
                ui: 'actionButton',
                name: 'SaveCurrent',
                itemId: 'BtnSaveCurrent',
                reference: 'BtnSaveCurrent',
                listeners: {
                    click: 'onSaveClicked'
                }
            });
            //save and new
            buttons.push({
                xtype: 'button',
                scale: 'small',
                iconCls: 'fa fa-check-square',
                iconAlign: 'left',
                text: app.localize('SaveContinue').toUpperCase(),
                ui: 'actionButton',
                name: 'SaveContinue',
                itemId: 'BtnSaveContinue',
                reference: 'BtnSaveContinue',
                listeners: {
                    click: 'onSaveContinueClicked'
                }
            });
            //save and clone
            buttons.push({
                xtype: 'button',
                scale: 'small',
                iconCls: 'fa fa-clone',
                iconAlign: 'left',
                text: app.localize('SaveClone').toUpperCase(),
                ui: 'actionButton',
                name: 'SaveClone',
                itemId: 'BtnSaveClone',
                reference: 'BtnSaveClone',
                listeners: {
                    click: 'onSaveCloneClicked'
                }
            });
            //attachments
            buttons.push({
                xtype: 'button',
                scale: 'small',
                iconCls: 'fa fa-paperclip',
                iconAlign: 'left',
                text: app.localize('Attachment').toUpperCase(),
                ui: 'actionButton',
                name: 'Attachment',
                itemId: 'BtnAttachment',
                reference: 'BtnAttachment',
                //listeners: {
                //    click: 'onCancelClicked'
                //}
            });
        }
        //close/exist button
        buttons.push({
            xtype: 'button',
            scale: 'small',
            iconCls: 'fa fa-close',
            iconAlign: 'left',
            text: app.localize('Cancel').toUpperCase(),
            ui: 'actionButton',
            name: 'Cancel',
            itemId: 'BtnCancel',
            reference: 'BtnCancel',
            listeners: {
                click: 'onCancelClicked'
            }

        });
        buttons.push('->');
        buttons.push({
            xtype: 'button',
            scale: 'small',
            iconCls: 'fa fa-history',
            iconAlign: 'left',
            ui: 'actionButton',
            name: 'History',
            itemId: 'BtnHistory',
            reference: 'BtnHistory',
            handler: function() {
                abp.message.info('Transaction # : 100', 'Transaction History');
            }
        });
        var defaultButtons = {
            xtype: 'toolbar',
            border: false,
            layout: {
                type: 'hbox',
                pack:'center'
            },
            items: buttons
        };
        
        return defaultButtons;
    },
    getFormSpecificActionToolbar: function (formButtons) {
        var me = this;
        if (formButtons) {
            var userDefinedButtons = {
                xtype: 'toolbar',
                border: false,
                layout: {
                    type: 'hbox',
                    pack: 'center'
                },
                items: formButtons
            };
            return userDefinedButtons;
        }
        return null;
    }
});
