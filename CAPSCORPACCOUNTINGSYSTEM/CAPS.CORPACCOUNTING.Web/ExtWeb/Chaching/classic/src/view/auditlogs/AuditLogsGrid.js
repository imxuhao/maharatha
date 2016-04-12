
Ext.define('Chaching.view.auditlogs.AuditLogsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.auditlogs.AuditLogsGridController',
        'Chaching.view.auditlogs.AuditLogsGridModel'
    ],

    controller: 'auditlogs-auditlogsgrid',
    viewModel: {
        type: 'auditlogs-auditlogsgrid'
    },
    xtype: 'auditLogs',
    store: 'auditlogs.AuditLogsStore',
    name: 'Administration.AuditLogs',
    padding: 5,
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("AuditLogs"),
          ui: 'headerTitle'
      }, '->'],
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    columnLines: true,
    multiColumnSort: true,
    columns: [
          {
              xtype: 'gridcolumn',
              width: '8%',
              text: app.localize('Actions'),
              renderer: Chaching.utilities.ChachingRenderers.auditLogView
          },
        {
            xtype: 'gridcolumn',
            dataIndex: 'exception',
            width: '5%',
            renderer: Chaching.utilities.ChachingRenderers.auditLogExceptionIcon
        },
        {
            xtype: 'gridcolumn',
            text: app.localize('Time'),
            dataIndex: 'executionTime',
            stateId: 'executionTime',
            sortable: true,
            width: '15%',
            groupable: true,
            renderer: Ext.util.Format.dateRenderer('m-d-Y g:i A'),
            filterField: {
                xtype: 'datefield',
                width: '100%',
                emptyText: ' Selec Date to search'
            }

        },
         {
             xtype: 'gridcolumn',
             text: app.localize('UserName'),
             dataIndex: 'userName',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter User Name to search'
             }
         }
         ,
         {
             xtype: 'gridcolumn',
             text: app.localize('Service'),
             dataIndex: 'serviceName',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter Service Name to search'
             }
         }
          ,
         {
             xtype: 'gridcolumn',
             text: app.localize('Action'),
             dataIndex: 'methodName',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter Action Name to search'
             }
         }
           ,
         {
             xtype: 'gridcolumn',
             text: app.localize('Duration'),
             dataIndex: 'executionDuration',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter Duration to search'
             }
         }
           ,
         {
             xtype: 'gridcolumn',
             text: app.localize('IpAddress'),
             dataIndex: 'clientIpAddress',
             sortable: true,
             groupable: true,
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter Ip Address to search'
             }
         }
            ,
         {
             xtype: 'gridcolumn',
             text: app.localize('Client'),
             dataIndex: 'clientName',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter Client to search'
             }
         }
            ,
         {
             xtype: 'gridcolumn',
             text: app.localize('Browser'),
             dataIndex: 'browserInfo',
             sortable: true,
             groupable: true,
             width: '15%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 emptyText: 'Enter Browser to search'
             }
         }
    ]
});

