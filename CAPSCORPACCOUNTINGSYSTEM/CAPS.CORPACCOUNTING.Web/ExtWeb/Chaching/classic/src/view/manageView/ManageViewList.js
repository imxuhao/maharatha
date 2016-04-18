
Ext.define('Chaching.view.manageView.ManageViewList',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.manageView.ManageViewListController'
    ],

    controller: 'manageview-manageviewlist',
    xtype: 'host.manageUserViews',
    store: 'manageView.ManageViewStore',
    name:'ManageView',
    modulePermissions: {
        read: true,
        create: true,
        edit: true,
        destroy: true
    },
    padding: 5,
    gridId: null,////*******Important to apply grid's userView setting
    requireActionColumn:true,
    headerButtonsConfig: [
    {
        xtype: 'displayfield',
        value: abp.localization.localize("ManageUsersViewSetting"),
        ui: 'headerTitle'
    }, {
        xtype: 'displayfield',
        value: abp.localization.localize("ManageUsersViewSettingHeaderInfo"),
        ui: 'headerSubTitle'
    }, '->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        action: 'create',
        text: abp.localization.localize("ManageUsersViewSettingCreateNew").toUpperCase(),
        checkPermission: false,
        iconCls: 'fa fa-plus',
        iconAlign: 'left'
    }],
    requireExport: false,
    requireMultiSearch: true,
    requireMultisort: false,
    isEditable: true,
    editingMode: 'row',
    createNewMode: 'inline',
    columnLines: true,
    multiColumnSort: false,
    manageViewSetting: false,
    showPagingToolbar:false,
    selModelConfig: {
        selType: 'chachingCheckboxSelectionModel',
        injectCheckbox: 'last',
        headerText: app.localize('Apply'),
        headerWidth:'15%',
        mode:'SINGLE',
        showHeaderCheckbox: false,
        listeners: {
            beforeselect: 'onBeforeApplySelectedView',
            select: 'onApplySelectedView'
        }
    },
    listeners: {
        beforecellclick: 'onBeforeRowCellClick'
    },
    columns:[
    {
        text: abp.localization.localize("SettingName"),
        dataIndex: 'viewSettingName',
        xtype: 'gridcolumn',
        sortable: false,
        groupable: false,
        //flex:1,
        width: '45%',
        filterField: {
            xtype: 'textfield',
            width: '100%',
            emptyText: app.localize('TSettingName')
        },editor: {
            xtype: 'textfield',
            name:'viewSettingName'
        }
    },{
        text: abp.localization.localize("Default"),
        dataIndex: 'isDefault',
        xtype: 'gridcolumn',
        sortable: false,
        groupable: false,
        renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
        width: '15%',
        editor: {
            xtype: 'checkboxfield',
            inputValue: true,
            name: 'isDefault'
        }
    }, {
        text: abp.localization.localize("CurrentView"),
        dataIndex: 'isActive',
        xtype: 'gridcolumn',
        sortable: false,
        groupable: false,
        renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
        width: '15%'
    }]
    
});
