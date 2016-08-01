
Ext.define('Chaching.view.languages.LanguagesGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.languages.LanguagesGridController'
    ],

    controller: 'languages-languagesgrid',

    xtype: 'languages',
    store: 'languages.LanguagesStore',
    name: 'Administration.Languages',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.Languages'),
        create: abp.auth.isGranted('Pages.Administration.Languages.Create'),
        edit: abp.auth.isGranted('Pages.Administration.Languages.Edit'),
        destroy: abp.auth.isGranted('Pages.Administration.Languages.Delete')
    },
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
    forceFit:true,
    editWndTitleConfig: {
        title: app.localize('EditLanguage'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewLanguage'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewLanguage'),
        iconCls: 'fa fa-plus'
    },
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('Name'),
            dataIndex: 'displayName',           
            sortable: true,
            width: '33%',
            groupable: true,
            renderer: Chaching.utilities.ChachingRenderers.languageIconRenderer
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Code'),
            dataIndex: 'name',
            sortable: true,
            groupable: true,
            width: '33%'
        },
        {
            xtype: 'gridcolumn',
            format: 'Y-m-d',
            text: app.localize('Creation Time'),
            dataIndex: 'creationTime',
            sortable: true,
            groupable: true,
            width: '33%',
            renderer: Chaching.utilities.ChachingRenderers.renderDateOnly
        }
    ]
});
