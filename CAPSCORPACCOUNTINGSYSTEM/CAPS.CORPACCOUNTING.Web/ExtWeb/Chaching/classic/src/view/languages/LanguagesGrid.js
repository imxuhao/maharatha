
Ext.define('Chaching.view.languages.LanguagesGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.languages.LanguagesGridController'
    ],

    controller: 'languages-languagesgrid',

    xtype: 'languages',
    store: 'languages.LanguagesStore',
    name: 'Administration.Languages',
    padding: 5,
    gridId:3,
    headerButtonsConfig: [
    {
        xtype: 'displayfield',
        value: abp.localization.localize("Languages"),
        ui: 'headerTitle'
    }, {
        xtype: 'displayfield',
        value: abp.localization.localize("LanguagesHeaderInfo"),
        ui: 'headerSubTitle'
    }, '->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        action: 'create',
        text: abp.localization.localize("CreateNewLanguage").toUpperCase(),
        tooltip: app.localize('CreateNewLanguage'),
        checkPermission: true,
        iconCls: 'fa fa-plus',
        iconAlign: 'left'
    }],
    actionColumnMenuItemsConfig: [{
        text: app.localize('ChangeTexts'),
        iconCls: 'fa fa-pencil',
        clickActionName: 'changeLanguageClick'
    }],
    requireExport: false,
    requireMultiSearch: false,
    requireMultisort: false,
    isEditable: false,
    editingMode: '',
    createNewMode: 'popup',
    columnLines: true,
    columndata:null,
    multiColumnSort: true,
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('Name'),
            dataIndex: 'displayName',           
            sortable: true,
            width: '32%',
            groupable: true,
            renderer: function (val, meta, record, rowIndex) { 
                    return '<i class="' + record.get('icon') + '" style="display: inline-block;margin-right: 10px; !important" ></i><span>' + val + '</span>';
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Code'),
            dataIndex: 'name',
            sortable: true,
            groupable: true,
            width: '32%'
        },
        {
            xtype: 'gridcolumn',
            format: 'Y-m-d',
            text: app.localize('Creation Time'),
            dataIndex: 'creationTime',
            sortable: true,
            groupable: true,
            width: '30%',
            renderer: Ext.util.Format.dateRenderer('m-d-Y g:i A')
        }
    ]
});
