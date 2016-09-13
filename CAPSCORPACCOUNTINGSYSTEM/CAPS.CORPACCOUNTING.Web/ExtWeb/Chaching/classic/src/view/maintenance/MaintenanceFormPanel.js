Ext.define('Chaching.view.maintenance.MaintenanceFormPanel', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.host.maintenance'],
    requires: [
        'Chaching.view.maintenance.MaintenanceFormPanelController'
    ], 

    controller: 'maintenance-maintenanceFormPanelController',    
    store: 'maintenance.MaintenanceStore',
    name: 'Administration.Host.Maintenance',
    hideDefaultButtons: true,
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Administration.Host.Maintenance')
    },
    items: [{
        xtype: 'tabpanel',
        ui: 'formTabPanels',
        items: [{
            xtype: 'dataview',
            scrollable: 'y',
            width: '100%',
            itemId : 'cacheDataView',
            title: abp.localization.localize('Caches'),            
            iconCls: 'fa fa-database',
            store: 'maintenance.CacheStore',            
            tpl: [                     
                   '<div class="row margin-bottom-10" style=" padding-left: 35px !important; padding-right: 35px !important; padding-top: 10px; padding-bottom: 10px; color: #9eacb4;">',
                          abp.localization.localize('CachesHeaderInfo'),
                          '<button name="clearAllCache" class="button-icon" style="float: right; height: 30px !important;"><i class="fa fa-refresh" style="margin-right: 10px; color:#FFFFFF"></i>',
                          abp.localization.localize('ClearAll'),
                          '</button>',
                 '</div>',

                  '<div class="row">',
                        '<div class="col-xs-12" id="clearCache" style="padding-left: 35px !important; padding-right: 35px !important">',
                            '<table class="table table-striped table-hover table-bordered">',
                                '<tbody>',
                                '<tpl for=".">',
                                '<tr>',
                                    '<td>',
                                        '<p>',                                        
                                        '<span name="clear">{name}</span>',
                                        '<button class="button-div" style="float:right; width: 50px; height: 25px;" name="clearSingle">',
                                        abp.localization.localize('Clear'),
                                        '</button>',
                                        '</p>',
                                    '</td>',
                                '</tr>',
                                '</tpl>',
                                '</tbody>',
                            '</table>',
                        '</div>',
                    '</div>'

           ],                       
            itemSelector: 'div',
            listeners: {
                'itemclick': 'clearCache'
                    }
        },
       {
           xtype: 'dataview',
           scrollable: 'vertical',           
           itemId: 'webLogView',
           width: '100%',
           title: abp.localization.localize('WebSiteLogs'),
            iconCls: 'fa fa-outdent',                 
            store : 'maintenance.WebLogStore',           
            tpl: [
                   '<div class="row margin-bottom-10" style="padding-left: 20px !important; padding-right: 20px !important; padding-top: 8px;">',
                        '<div class="col-xs-6" style="color: #9eacb4;">',
                            abp.localization.localize('WebSiteLogsHeaderInfo'),
                        '</div>',
                        '<div class="col-xs-6 text-right">',
                            '<button class="button-icon" name="downloadAllButton" style="height:30px; margin-right: 10px;"><i class="fa fa-download" style="margin-right: 10px;"></i>',
                              abp.localization.localize('DownloadAll'),
                            '</button>',
                            '<button class="button-icon" name="refreshButton" style="height: 30px; width: 100px;"><i class="fa fa-refresh" style="margin-right: 10px;"></i>',
                            abp.localization.localize('Refresh'),
                            '</button>',
                        '</div>',
                    '</div>',                    

                    '<div class="row" style="padding-left: 20px !important; padding-right: 20px !important; margin-top: 10px;">',
                      '<tpl for=".">',
                        '<div class="col-xs-12">', 
                            '<div class="web-log-view full-height"> {.} </div>', '<br>',
                         '</div>',                         
                       '</tpl>',
                    '</div>' 
            ],

            itemSelector: 'button',
            listeners: {
                'itemclick': 'buttonClicked'
            }
            
            }
        ]        
    }
    ],
    listeners: {
        'beforeshow': 'loadCacheAndWeblogs',
        'resize':'onMaintenanceResize'
    }
});