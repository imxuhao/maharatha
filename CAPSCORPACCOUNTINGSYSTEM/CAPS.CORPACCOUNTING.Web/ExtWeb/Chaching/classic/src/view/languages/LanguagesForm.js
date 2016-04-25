
Ext.define('Chaching.view.languages.LanguagesForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',

    requires: [
        'Chaching.view.languages.LanguagesFormController'
    ],
    controller: 'languages-languagesform',
    name: 'Languages',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'vbox',
    setDefferedValuesOnEdit: true,
    defferedValueSetDelay: 5,
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'top',
        blankText: app.localize('MandatoryToolTipText')
    },
    defaultFocus: 'textfield#tenancyName',
    items: [
        {
            xtype: 'hiddenfield',
            name: 'id',
            value: 0
        }, {
            xtype: 'combobox',
            name: 'name',
            fieldLabel: app.localize('Language').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('Language'),
            displayField: 'displayText',
            valueField: 'value',
            queryMode: 'local',
            //renderTpl: [
            //   // '<i class="{value}">',
            ////'<h1 class="title">{title}</h1>',
            //'<p>{value}-{displayText}</p>'//,
            // //'<p>{displayText}</p>'
            //],
            //store: 'languages.LanguagesDataStore'
            store: {
                model: 'Chaching.model.languages.LanguagesNamesModel',
                data: []
            }
        }, {
            xtype: 'combobox',
            name: 'icon',
            fieldLabel: app.localize('Flag').initCap(),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('Flag'),
            displayField: 'displayText',
            valueField: 'value',
            queryMode: 'local',
            //store: 'languages.LanguagesDataStore'

            listConfig: {
                getInnerTpl: function() {
                    // here you place the images in your combo
                    var div = '<div style="padding:8px 16px 0 0; display:block; line-height:18px;">' +
                        '<span class="famfamfam-flag {value}" style="height:11px !important; width:16px !important; display:inline-block; line-height:18px;"></span>&nbsp;&nbsp;' +
                        '<span style="display:inline-block; line-height:18px;">{displayText}</span></div>';
                    return div;
                }
            },
            store: {
                model: 'Chaching.model.languages.LanguagesFlagModel',
                data: []
            }
        }
    ]

});
