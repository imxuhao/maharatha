// change company logo view
Ext.define('Chaching.view.administration.companysetup.CompanyLogoForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    requires: [
        'Chaching.view.administration.companysetup.CompanyLogoFormController'
    ],

    controller: 'companylogoform',
    name: 'CompanyLogo',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'vbox',
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'top',
        blankText: app.localize('MandatoryToolTipText')
    },
    items: [
        {
            xtype: 'filefield',
            name: 'companyLogoField',
            clearOnSubmit: false,
            anchor: '100%',
            width: '100%',
            listeners: {
                change: 'fileChange'
            }
        },
         {
             xtype: 'label',
             text: app.localize('CompanyLogo_Change_Info').initCap(),
             width: '100%'
         }
    ]
});
