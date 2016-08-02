
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
            width: '19%',
            align: 'center',
            hideable : false,
            //maxWidth: 100,
            text: app.localize('Actions'),        
            renderer: Chaching.utilities.ChachingRenderers.loginaccount
        }, {
             xtype: 'gridcolumn',
             text: app.localize('UserName'),
             dataIndex: 'tenantUser',
             hideable: false,
             sortable: true,
             groupable: false,
             width: '60%'
             //flex : 1      
         },
         {
             xtype: 'actioncolumn',       
             text: app.localize('Delete'),
             width: '19%',
             //width: 70,
             align: 'center',
             hideable: false,
             items: [{
                 iconCls: 'unlinkUserCls',
                 tooltip: app.localize('UnlinkUser_Tooltip'),
                 handler: 'unlinkUser'
             }]
             
            // maxWidth: 50,
             //renderer: Chaching.utilities.ChachingRenderers.unlinkedaccount
            
         }


    ]
});
