
Ext.define('Chaching.view.languages.LanguageTextsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.languages.LanguageTextsGridController',
     //  'Chaching.view.languages.LanguageTextsGridModel'
    ],

    controller: 'languages-languagetextsgrid',
    //viewModel: {
    //    type: 'languages-languagetextsgrid'
    //},

    xtype: 'Languagetexts',
    store: 'languages.LanguageTextsStore',
    name: 'LanguageTexts',
    padding: 5,
    gridId:4,
    headerButtonsConfig: [
    {
        xtype: 'displayfield',
        value: abp.localization.localize("BaseLanguage"),
        ui: 'headerTitle'
    }, {
        xtype: 'displayfield',
        value: abp.localization.localize("TargetLanguage"),
        ui: 'headerSubTitle'
    }, '->'],
    manageViewSetting: false,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',   
    columnLines: true,   
    multiColumnSort: true,
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('Key'),
            dataIndex: 'key',           
            sortable: true,
            width: '30%',
            groupable: true,                 
            editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('BaseValue'),
            dataIndex: 'baseValue',
            sortable: true,
            groupable: true,
            width: '30%',       
            editor: {
                xtype: 'textfield'
            }
        },
        {
            xtype: 'gridcolumn',          
            text: app.localize('TargetValue'),
            dataIndex: 'targetValue',
            sortable: true,
            groupable: true,
            width: '30%',     
            editor: {
                xtype: 'textfield'
            }
        },
        {
            xtype: 'gridcolumn',
            width: '10%',
            text: app.localize('Edit'),
            renderer: Chaching.utilities.ChachingRenderers.languagesTextsEditIcon
        }, ]
});
