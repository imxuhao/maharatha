// Import form 
Ext.define('Chaching.view.import.ImportForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    requires: [
        'Chaching.view.import.ImportFormController'
    ],
    controller: 'ImportForm',
    name: 'importForm',
    openInPopupWindow: true,
    hideDefaultButtons: true,
    layout: 'vbox',
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'top',
        blankText: app.localize('MandatoryToolTipText')
    },
    defaultButton: 'uploadBtn',
    items: [
        {
            xtype: 'filefield',
            name: 'importFileField',
            clearOnSubmit: false,
            anchor: '100%',
            width: '100%',
            allowBlank: false,
            anchor: '100%',
            buttonText: app.localize('SelectFile').toUpperCase(),
            listeners : {
                change: 'onFileChange'
            }
            
        },
         {
             xtype: 'label',
             cls : 'helpText',
             text: app.localize('Template_File_Info'),
             width: '100%'
         }
    ],
    bbar: ['->',{
        text: app.localize('Upload').toUpperCase(),
        scale: 'small',
        iconCls: 'fa fa-save',
        iconAlign: 'left',
        ui: 'actionButton',
        itemId: 'uploadBtn',
        formBind : true,
        handler: 'uploadTemplateFile'
    },
    {
        text: app.localize('Cancel').toUpperCase(),
        ui: 'actionButton',
        scale: 'small',
        iconCls: 'fa fa-close',
        iconAlign: 'left',
        handler: 'onCancelClicked'
    }
    ]
});
