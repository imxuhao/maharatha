
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
            width: '100%',
            allowBlank: false,
            regex: /(.)+((\.xls)|(\.xlsx)|(\.csv)(\w)?)$/i,
            regexText: 'Only excel files are accepted',
            buttonText: app.localize('SelectFile').toUpperCase(),
            emptyText: app.localize('Template_File_Info'),
            listeners: {
                change: 'onFileChange'
            }
        },{
            xtype: 'panel',
            itemId:'placeHolderPanel',
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
