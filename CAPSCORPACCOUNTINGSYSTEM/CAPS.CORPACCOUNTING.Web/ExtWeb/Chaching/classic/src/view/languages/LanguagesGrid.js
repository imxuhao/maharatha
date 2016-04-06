
Ext.define('Chaching.view.languages.LanguagesGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.languages.LanguagesGridController',
        'Chaching.view.languages.LanguagesGridModel'
    ],

    controller: 'languages-languagesgrid',
    viewModel: {
        type: 'languages-languagesgrid'
    },

    xtype: 'languages',
    store: 'languages.LanguagesStore',
    name: 'Administration.Languages',
    padding: 5,
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
        //listeners: {
        //    click: controller.editActionClicked
        //}
    }],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    createNewMode: 'popup',
    columnLines: true,
    multiColumnSort: true,
    columns: [
        {
            xtype: 'gridcolumn',
            text: app.localize('Name'),
            dataIndex: 'displayName',           
            sortable: true,
            width: '32%',
            groupable: true,
            // simplest filter configuration
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('Name')               
            },
            renderer: function (val, meta, record, rowIndex) {                
                //if (record.data.isdefault)
                //    return '<i class="' + record.get('icon') + '" style="display: inline-block;margin-right: 10px;"font-weight: bold;" !important" ></i><span>' + val + '<span style="font-weight: bold;">(Default)</span></span>';
                //else
                    return '<i class="' + record.get('icon') + '" style="display: inline-block;margin-right: 10px; !important" ></i><span>' + val + '</span>';
            },
            editor: {
                xtype: 'textfield'
            }
        }, {
            xtype: 'gridcolumn',
            text: app.localize('Code'),
            dataIndex: 'name',
            sortable: true,
            groupable: true,
            width: '32%',           
            // equivalent to filterField:true
            // as textfield is created by default
            
            filterField: {
                xtype: 'textfield',
                width: '100%',
                emptyText: app.localize('Code')
            },
            editor: {
                xtype: 'textfield'
            }
        },
        {
            xtype: 'gridcolumn',
            format: 'Y-m-d',
            text: app.localize('Creation Time'),
            dataIndex: 'creationTime',
            sortable: true,
            groupable: true,
            width: '30%',
            renderer: Ext.util.Format.dateRenderer('m-d-Y g:i A'),
            filterField: {
                xtype: 'datefield',
                width: '100%',
                emptyText: 'Enter creation time to search'
            },
        },
         
       
       
    ]
});
