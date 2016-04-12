
Ext.define('Chaching.view.linkedaccounts.LinkedAccountsGrid',{
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.linkedaccounts.LinkedAccountsGridController',
        'Chaching.view.linkedaccounts.LinkedAccountsGridModel'
    ],

    controller: 'linkedaccounts-linkedaccountsgrid',
    viewModel: {
        type: 'linkedaccounts-linkedaccountsgrid'
    },

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
    store: 'linkedaccounts.LinkedAccountsStore',
    name: 'LinkedAccounts',
    padding: 5, 
    requireMultiSearch: true,
    requireMultisort: true,
    isEditable: true,
    editingMode: 'row',
    createNewMode: 'popup',
    columnLines: true,
    multiColumnSort: true,   
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
             },
             editor: {
                 xtype: 'textfield'
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
