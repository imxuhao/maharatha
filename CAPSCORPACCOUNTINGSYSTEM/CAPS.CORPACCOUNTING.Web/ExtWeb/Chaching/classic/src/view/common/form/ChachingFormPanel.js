
Ext.define('Chaching.view.common.form.ChachingFormPanel',{
    extend: 'Ext.form.Panel',

    requires: [
        'Chaching.view.common.form.ChachingFormPanelController',
        'Chaching.view.common.form.ChachingFormPanelModel'
    ],

    controller: 'common-form-chachingformpanel',
    viewModel: {
        type: 'common-form-chachingformpanel'
    },
    /**
   * @hide
   * @private
   * @cfg {string} name of the grid to do permission check
   */
    name: null,
    /**
  * @hide
  * @private
  * @cfg {object} parentGrid from which this formpanel is created
  */
    parentGrid: null,
    /**
  * @hide
  * @private
  * @cfg {boolean} hide default save and cancel buttons.
     * if set to true then create new buttons
  */
    hideDefaultButtons: false,
    /**
  * @hide
  * @private
  * @cfg {object} default values to be loaded when the form loads
     * use beforeCreateAction in parentGrid's controller class to set defaultValues if any.
  */
    defaultValuesToLoad: null,
    /**
 * @hide
 * @private
 * @cfg {boolean} open this form in window panel.
    * if set to true then popup window will be opened containing this form
 */
    openInPopupWindow: false,
    /**
* @hide
* @private
* @cfg {boolean} save button to act accordingly create/update.   
*/
    isEditing: false,
    referenceHolder: true,
    border: false,
    frame: false,
    initComponent: function () {
        //create buttons
        var me = this,
            controller = me.getController,
            buttons = [];
        if (!me.hideDefaultButtons) {
            buttons.push('->');
            var saveButton= {
                xtype: 'button',
                scale: 'small',
                iconCls: 'fa fa-save',
                iconAlign: 'left',
                text: app.localize('Save').toUpperCase(),
                ui: 'actionButton',
                name: 'Save',
                itemId: 'BtnSave',
                reference:'BtnSave',
                listeners: {
                    click:'onSaveClicked'
                }
            }
            buttons.push(saveButton);

            var cancelButton= {
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
            }
            buttons.push(cancelButton);
            me.bbar = buttons;
            //me.button = buttons;
            me.buttonAlign = 'right';
            me.defaultButton = 'BtnSave';
        }
        me.callParent(arguments);
    },
    findField: function (id) {
        return this.getFields().findBy(function (f) {
            return f.id === id || f.getName() === id || f.hiddenName === id;
        });
    }
});
