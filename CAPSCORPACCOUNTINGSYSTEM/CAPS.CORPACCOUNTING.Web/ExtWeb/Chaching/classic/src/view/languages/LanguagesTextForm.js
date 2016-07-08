
Ext.define('Chaching.view.languages.LanguagesTextForm',{
    extend: 'Chaching.view.common.form.ChachingFormPanel',

    requires: [
        'Chaching.view.languages.LanguagesTextFormController'
    ],

    controller: 'languages-languagestextform',
    hideDefaultButtons: true,
    name: 'LanguageTextsForm',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.Languages'),
        create: abp.auth.isGranted('Pages.Administration.Languages.Create'),
        edit: abp.auth.isGranted('Pages.Administration.Languages.Edit'),
        destroy: abp.auth.isGranted('Pages.Administration.Languages.Delete')
    },
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
                            listConfig: {
                                getInnerTpl: function () {
                                    // here you place the images in your combo
                                    var div = '<div style="padding:8px 16px 0 0; display:block; line-height:18px;">' +
                                        '<span class="famfamfam-flag {icon}" style="height:11px !important; width:16px !important; display:inline-block; line-height:18px;"></span>&nbsp;&nbsp;' +
                                        '<span style="display:inline-block; line-height:18px;">{displayName}</span></div>';
                                    return div;
                                }
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
                            listConfig: {
                                getInnerTpl: function () {
                                    // here you place the images in your combo
                                    var div = '<div style="padding:8px 16px 0 0; display:block; line-height:18px;">' +
                                        '<span class="famfamfam-flag {icon}" style="height:11px !important; width:16px !important; display:inline-block; line-height:18px;"></span>&nbsp;&nbsp;' +
                                        '<span style="display:inline-block; line-height:18px;">{displayName}</span></div>';
                                    return div;
                                }
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
                    layout: 'fit',
                    //height: '100%',
                    //width: '100%'
                }
            ]
        }
    ]
});
