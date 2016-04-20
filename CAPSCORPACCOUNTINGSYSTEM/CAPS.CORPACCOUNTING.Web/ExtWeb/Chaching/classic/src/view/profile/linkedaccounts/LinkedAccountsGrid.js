
Ext.define('Chaching.view.profile.linkedaccounts.LinkedAccountsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.profile.linkedaccounts.LinkedAccountsGridController'
    ],

    controller: 'linkedaccounts-linkedaccountsgrid',
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
    manageViewSetting:false,
    createWndTitleConfig: {
        title: app.localize('LinkNewAccount'),
        iconCls: 'fa fa-plus'
    },   
    columns: [
        {
            xtype: 'gridcolumn',           
            width: '20%',           
            text: app.localize('Actions'),        
            renderer: Chaching.utilities.ChachingRenderers.loginaccount
        },

         {
             xtype: 'gridcolumn',
             text: app.localize('UserName'),
             dataIndex: 'username',
             sortable: true,
             groupable: true,
             width: '60%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: app.localize('UUserName')
             }            
         },
         {
             xtype: 'gridcolumn',       
             text: app.localize('Delete'),
             width: '20%',
             renderer: Chaching.utilities.ChachingRenderers.unlinkedaccount
            
         }


    ]
});
