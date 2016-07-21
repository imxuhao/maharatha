
Ext.define('Chaching.view.profile.linkedaccounts.LinkedAccountsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.profile.linkedaccounts.LinkedAccountsGridController'
    ],

    controller: 'linkedaccounts-linkedaccountsgrid',
    modulePermissions: {
        read: true, //abp.auth.isGranted(''),
        create: true,//abp.auth.isGranted(''),
        edit: true, //abp.auth.isGranted(''),
        destroy: true //abp.auth.isGranted('')
    },
    xtype: 'manageaccounts',
    headerButtonsConfig: [
       '->', {
         xtype: 'button',
         scale: 'small',
         ui: 'actionButton',
         action: 'create',
         text: abp.localization.localize("LinkNewAccount").toUpperCase(),
         tooltip: app.localize('LinkNewAccount'),
         checkPermission: false,      
         iconCls: 'fa fa-plus',       
         iconAlign: 'left'
     }], 
    store: 'profile.linkedaccounts.LinkedAccountsStore',
    name: 'LinkedAccounts',
    padding: 5,
    gridId:8,
    requireMultiSearch: false,
    requireMultisort: false,
    isEditable: true,
    editingMode: 'row',
    createNewMode: 'popup',
    columnLines: true,
    multiColumnSort: true,
    manageViewSetting: false,
    requireActionColumn: false,
    createWndTitleConfig: {
        title: app.localize('LinkNewAccount'),
        iconCls: 'fa fa-plus'
    },   
    columns: [
        {
            xtype: 'gridcolumn',           
            width: '15%',
            maxWidth: 150,
            text: app.localize('Actions'),        
            renderer: Chaching.utilities.ChachingRenderers.loginaccount
        }, {
             xtype: 'gridcolumn',
             text: app.localize('UserName'),
             dataIndex: 'tenantUser',
             sortable: true,
             groupable: true,
             width: '60%',
             flex : 1      
         },
         {
             xtype: 'actioncolumn',       
             text: app.localize('Delete'),
             width: '15%',
             maxWidth: 70,
             cls : 'actionColumn',
             items: [{
                 iconCls: 'deleteCls',
                 tooltip: app.localize('Delete'),
                 handler: 'unlinkUser'
             }]
             
            // maxWidth: 50,
             //renderer: Chaching.utilities.ChachingRenderers.unlinkedaccount
            
         }


    ]
});
