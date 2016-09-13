
Ext.define('Chaching.view.languages.LanguageTextsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.languages.LanguageTextsGridController'
    ],

    controller: 'languages-languagetextsgrid',

    xtype: 'Languagetexts',
    store: 'languages.LanguageTextsStore',
    name: 'LanguageTexts',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.Languages'),
        create: abp.auth.isGranted('Pages.Administration.Languages.Create'),
        edit: abp.auth.isGranted('Pages.Administration.Languages.Edit'),
        destroy: abp.auth.isGranted('Pages.Administration.Languages.Delete')
    },
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
    createNewMode: 'tab',

    listeners: {
        cellclick: 'onLanguageCellClick'
    },
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('Key'),
            dataIndex: 'key',           
            sortable: true,
            width: '30%',
            groupable: true,
            filterField: {
                xtype: 'textfield',
                width: '100%'
            },
            editor: {
                xtype: 'textfield',
                disabled:true
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('BaseValue'),
            dataIndex: 'baseValue',
            sortable: true,
            groupable: true,
            width: '31%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            },
            editor: {
                xtype: 'textfield',
                disabled: true
            }
        },
        {
            xtype: 'gridcolumn',          
            text: app.localize('TargetValue'),
            dataIndex: 'targetValue',
            sortable: true,
            groupable: true,
            width: '32%',
            filterField: {
                xtype: 'textfield',
                width: '100%'
            },
            editor: {
                xtype: 'textfield'
            }
        },
        {
            xtype: 'gridcolumn',
            width: '0', //'8%',
            hidden:true,
            text: app.localize('Edit'),
            dataIndex: 'isEdit',
            renderer: Chaching.utilities.ChachingRenderers.languagesTextsEditIcon
        } ]
});
