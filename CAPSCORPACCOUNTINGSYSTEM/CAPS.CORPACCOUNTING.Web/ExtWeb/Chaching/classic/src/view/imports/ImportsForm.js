
Ext.define('Chaching.view.imports.ImportsForm',{
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['import.create', 'import.edit'],

    requires: [
        'Chaching.view.imports.ImportsFormController'
    ],
    controller: 'imports-importsform',
    name: 'importsForm',
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
            buttonText: app.localize('SelectFile').toUpperCase(),
            listeners: {
                change: 'onFileChange'
            }

        },
        {
            xtype: 'label',
            cls: 'helpText',
            text: app.localize('Template_File_Info'),
            width: '100%'
        }
    ],
    bbar: [
        '->', {
            text: app.localize('Upload').toUpperCase(),
            scale: 'small',
            iconCls: 'fa fa-save',
            iconAlign: 'left',
            ui: 'actionButton',
            itemId: 'uploadBtn',
            formBind: true,
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
