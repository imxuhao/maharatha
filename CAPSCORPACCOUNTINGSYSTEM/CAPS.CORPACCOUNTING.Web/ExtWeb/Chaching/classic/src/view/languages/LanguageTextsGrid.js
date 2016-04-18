
Ext.define('Chaching.view.languages.LanguageTextsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.languages.LanguageTextsGridController',
       'Chaching.view.languages.LanguageTextsGridModel'
    ],

    controller: 'languages-languagetextsgrid',
    viewModel: {
        type: 'languages-languagetextsgrid'
    },

   // xtype: 'Languagetexts',
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
            width: '32%',
            groupable: true,
            // simplest filter configuration
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('Key')               
            },           
            editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('BaseValue'),
            dataIndex: 'baseValue',
            sortable: true,
            groupable: true,
            width: '32%',           
            // equivalent to filterField:true
            // as textfield is created by default
            
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('BaseValue')
            },
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
            width: '33%',          
            filterField: {
                xtype: 'textfield'
            },
            editor: {
                xtype: 'textfield'
            }
        },        
       
       
    ]
});
