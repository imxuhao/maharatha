
Ext.define('Chaching.view.languages.LanguagesForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',

    requires: [
        'Chaching.view.languages.LanguagesFormController'
    ],
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
    items: [
        {
            xtype: 'container',
            height: 70,
            width: '100%',
            layout: 'column',
            items: [
                {
                    columnWidth: .25,
                    border: false,
                    frame: false,
                    padding: '0 2px 1px 0px',
                    items: [
                        {
                            xtype: 'combobox',
                            name: 'baseLanguage',
                            fieldLabel: app.localize('BaseLanguage').initCap(),
                            width: '99%',
                            ui: 'fieldLabelTop',
                            displayField: 'displayName',
                            valueField: 'name',
                            labelAlign: 'top',
                            listeners: {
                                change: 'getLanguageTextsonChange'
                            },
                            store: {
                                fields: [{ name: 'displayName' }, { name: 'name' }, { name: 'icon' }],
                                data: abp.localization.languages
                            }
                        }
                    ]
                }, {
                    columnWidth: .25,
                    border: false,
                    frame: false,
                    padding: '0 2px 1px 0px',
                    items: [
                        {
                            xtype: 'combobox',
                            name: 'targetLanguage',
                            fieldLabel: app.localize('TargetLanguage').initCap(),
                            width: '99%',
                            ui: 'fieldLabelTop',
                            displayField: 'displayName',
                            valueField: 'name',
                            labelAlign: 'top',
                            listeners: {
                                change: 'getLanguageTextsonChange'
                            },
                            bind: {
                                store: {
                                    fields: [{ name: 'displayName' }, { name: 'name' }, { name: 'icon' }],
                                    data: abp.localization.languages
                                }
                            }
                        }
                    ]
                }, {
                    columnWidth: .25,
                    border: false,
                    frame: false,
                    padding: '0 2px 1px 0px',
                    items: [
                        {
                            xtype: 'combobox',
                            name: 'source',
                            fieldLabel: app.localize('Source').initCap(),
                            width: '99%',
                            ui: 'fieldLabelTop',
                            displayField: 'name',
                            valueField: 'name',
                            labelAlign: 'top',
                            listeners: {
                                change: 'getLanguageTextsonChange'
                            },
                            bind: {
                                store: {
                                    fields: [{ name: 'name' }, { name: 'name' }],
                                    data: abp.localization.sources

                                }
                            }
                        }
                    ]
                }, {
                    columnWidth: .25,
                    border: false,
                    frame: false,
                    padding: '0 2px 1px 0px',
                    items: [
                        {
                            xtype: 'combobox',
                            name: 'targetValue',
                            fieldLabel: app.localize('TargetValue').initCap(),
                            width: '99%',
                            ui: 'fieldLabelTop',
                            displayField: 'text',
                            valueField: 'value',
                            labelAlign: 'top',
                            listeners: {
                                change: 'getLanguageTextsonChange'
                            },
                            bind: {
                                store: {
                                    fields: [{ name: 'text' }, { name: 'value' }],
                                    data: [{ text: 'All', value: 'ALL' }, { text: 'EmptyOnes', value: 'EMPTY' }]

                                }
                            }
                        }
                    ]
                }
            ]
        }, {
            xtype: 'container',
            layout: 'fit',
            width: '100%',
            items: [
                {
                    xtype: 'Languagetexts',
                    layout: 'fit'
                }
            ]
        }
    ]
});
