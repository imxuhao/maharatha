
Ext.define('Chaching.view.editions.EditionsGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.editions.EditionsGridController'
    ],

    controller: 'editions-editionsgrid',

    xtype: 'host.editions',
    store: 'editions.EditionsStore',
    name: 'Editions',
    padding: 5,
    gridId: 2,
    //forceFit:true,
    headerButtonsConfig: [
    {
        xtype: 'displayfield',
        value: abp.localization.localize("Editions"),
        ui: 'headerTitle'
    }, {
        xtype: 'displayfield',
        value: abp.localization.localize("EditionsHeaderInfo"),
        ui: 'headerSubTitle'
    }, '->', {
        xtype: 'button',
        scale: 'small',
        ui: 'actionButton',
        action: 'create',
        text: abp.localization.localize("CreateNewEdition").toUpperCase(),
        tooltip: app.localize('CreateNewEdition'),
        checkPermission: true,
        iconCls: 'fa fa-plus',
        iconAlign: 'left'
    }],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    createNewMode: 'popup',
    columnLines: true,
    multiColumnSort: true,
    editWndTitleConfig: {
        title: app.localize('EditEdition'),
        iconCls: 'fa fa-pencil'
    },
    createWndTitleConfig: {
        title: app.localize('CreateNewEdition'),
        iconCls: 'fa fa-plus'
    },
    viewWndTitleConfig: {
        title: app.localize('ViewEdition'),
        iconCls: 'fa fa-th'
    },
    columns: [             
         {
             xtype: 'gridcolumn',
             text: app.localize('Name'),
             dataIndex: 'displayName',
             sortable: true,
             groupable: true,
             //width: '46%',
             flex: 1,
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter DisplayName to search'
             },
             editor: {
                 xtype: 'textfield'
             }
         },
          {
              xtype: 'gridcolumn',
              format:'Y-m-d',
             text: app.localize('Creation Time'),
             dataIndex: 'creationTime',
             sortable: true,
             groupable: true,
              //width: '46%',
             flex: 1,
             renderer: Chaching.utilities.ChachingRenderers.dateSearchFieldRenderer,
             filterField: {
                 xtype: 'dateSearchField',
                 dataIndex: 'creationTime',
                 width: '100%'
             }
          }        
    ]
    
});
