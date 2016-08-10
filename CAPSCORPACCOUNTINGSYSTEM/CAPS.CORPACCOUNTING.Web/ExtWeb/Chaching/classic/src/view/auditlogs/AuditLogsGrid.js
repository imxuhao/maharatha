
Ext.define('Chaching.view.auditlogs.AuditLogsGrid', {
    extend: 'Chaching.view.common.grid.ChachingGridPanel',

    requires: [
        'Chaching.view.auditlogs.AuditLogsGridController'
    ],

    controller: 'auditlogs-auditlogsgrid',
    xtype: 'auditLogs',
    store: 'auditlogs.AuditLogsStore',
    name: 'Administration.AuditLogs',
    padding: 5,
    gridId: 5,
    headerButtonsConfig: [
      {
          xtype: 'displayfield',
          value: abp.localization.localize("AuditLogs"),
          ui: 'headerTitle'
      }, '->', {
          xtype: 'button',
          scale: 'small',
          ui: 'actionButton',
          text: abp.localization.localize("Refresh").toUpperCase(),
          tooltip: app.localize('Refresh'),
          iconCls: 'fa fa-refresh',
          //routeName: 'coa.create',
          iconAlign: 'left',
          listeners: {
              click : 'onRefreshClick'
          }
      }],
    requireActionColumn : false,
    requireExport: true,
    requireMultiSearch: true,
    requireMultisort: true,
    columnLines: true,
    multiColumnSort: true,
    columns: [
          {
              xtype: 'actioncolumn',
              width: '5%',
              maxWidth: 30,
              dataIndex: 'search',
              items: [{
                  iconCls: 'searchCls',
                  tooltip: app.localize('ShowAuditLogDetailView_Tooltip'),
                  handler: 'showAuditLogDetailView'
              }]//,
             

              //renderer: Chaching.utilities.ChachingRenderers.auditLogView
          },
        {
            xtype: 'gridcolumn',
            dataIndex: 'exception',
            width: '5%',
            maxWidth: 30,
            renderer: Chaching.utilities.ChachingRenderers.auditLogExceptionIcon
        },
        {
            xtype: 'gridcolumn',
            text: app.localize('Time'),
            dataIndex: 'executionTime',
            //stateId: 'executionTime',
            sortable: true,
            width: '15%',
            groupable: true,
            renderer: Chaching.utilities.ChachingRenderers.renderDateTimeSecondsWithoutAmPm,
            filterField: {
                xtype: 'dateSearchField',
                dataIndex: 'executionTime',
                width: '100%'
            }

        },
         {
             xtype: 'gridcolumn',
             text: app.localize('UserName'),
             dataIndex: 'userName',
             sortable: true,
             groupable: true,
             sorter: {
                 property: 'userName',
                 sortOnEntity: 'User'
             },
             width: '10%',
             filterField: {
                 xtype: 'textfield',
                 width: '100%',
                 entityName: 'User',
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
             flex : 1,
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
             flex: 1,
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
             renderer: Chaching.utilities.ChachingRenderers.renderInMiliSeconds,
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
    ],
    listeners: {
        cellclick : 'auditLogCellClick'
    }
});

