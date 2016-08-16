Ext.define('Chaching.view.imports.ImportsErrorForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['import.error.create', 'import.error.edit'],
    requires: ['Chaching.view.imports.ImportsErrorFormController'],
    controller: 'imports-errorform',
    name: 'importsErrorForm',
    openInPopupWindow: true,
    hideDefaultButtons: true,
    layout: 'vbox',
    defaults: {
        bodyStyle: { 'background-color': 'transparent' },
        labelAlign: 'top'
    },
    defaultButton: 'OkBtn',
    listeners:{
        //afterrender: 'onFormShow'
    },
    popupWndSize: {
        height: '90%',
        width: '90%'
    },
    items: [{
        xtype: 'panel',
        itemId: 'errorPanelItemId',
        padding: '0 10 0 10',
        width: '100%',
        //height:'50px',
        items:[]
    }],
    bbar: ['->', {
        xtype: 'button',
        scale: 'small',
        iconAlign: 'left',
        text: app.localize('Close').toUpperCase(),
        ui: 'actionButton',
        name: 'Ok',
        handler:'onBtnClick'
    }
    ]
});