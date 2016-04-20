
Ext.define('Chaching.view.languages.LanguagesForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',

    requires: [
        'Chaching.view.languages.LanguagesFormController'       
    ],
    layout: 'vbox',
    controller: 'languages-languagesform',
   
    hideDefaultButtons: true,
    name: 'LanguageTextsForm',
    openInPopupWindow: true,   
    layout: 'vbox',
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'top',
        blankText: app.localize('MandatoryToolTipText')
    },
    items: [{
        xtype: 'container',
        height: 50,
        width: '100%',
        layout: 'hbox',
        items: [
            {
                xtype: 'combobox',
                name: 'baseLanguage',
                fieldLabel: app.localize('BaseLanguage').initCap(),
                labelWidth: '5%',
                width: '50%',
                ui: 'fieldLabelTop',
                displayField: 'displayName',
                valueField: 'name',
                listeners: {
                    change: 'getLanguageTextsonChange'
                },
                store: {
                    fields: [{ name: 'displayName' }, { name: 'name' }, { name: 'icon' }],
                    data: abp.localization.languages,

                }
            },
         {
             xtype: 'combobox',
             name: 'targetLanguage',
             fieldLabel: app.localize('TargetLanguage').initCap(),
             width: '50%',
             labelWidth: '5%',
             ui: 'fieldLabelTop',
             displayField: 'displayName',
             valueField: 'name',            
             listeners: {
                 change: 'getLanguageTextsonChange'
             },
             bind: {
                 store: {
                     fields: [{ name: 'displayName' }, { name: 'name' }, { name: 'icon' }],
                     data: abp.localization.languages,
                    
                     }
             },
         },
         ]
    },
    {
        xtype: 'container',
        height: 50,
        width: '100%',
        layout: 'hbox',
        items: [
            {
                xtype: 'combobox',
                name: 'source',
                fieldLabel: app.localize('Source').initCap(),
                labelWidth: '5%',
                width: '50%',
                ui: 'fieldLabelTop',
                displayField: 'name',
                valueField: 'name',
                listeners: {
                    change: 'getLanguageTextsonChange'
                },
                bind: {
                    store: {
                        fields: [{ name: 'name' }, { name: 'name' }],
                        data: abp.localization.sources

                    }
                }
            },
         {
             xtype: 'combobox',
             name: 'targetValue',
             fieldLabel: app.localize('TargetValue').initCap(),
             labelWidth: '5%',
             width: '50%',
             ui: 'fieldLabelTop',
             displayField: 'text',
             valueField: 'value',
             listeners: {
                 change: 'getLanguageTextsonChange'
             },
             bind: {
                 store: {
                     fields: [{ name: 'text' }, { name: 'value' }],
                     data: [{ text: 'All', value: 'ALL' }, { text: 'EmptyOnes', value: 'EMPTY' }]

                 }
             }
         }]
    }
    , {
        xtype: 'container',
        layout: 'fit',
        width: '100%',        
        items: [{
            xtype: 'Languagetexts',
            width: '100%',
            height: '100%',
        }]
    }]
});
