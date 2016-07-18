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
                   '<div class="row margin-bottom-10" style=" padding-left: 35px !important; padding-right: 35px !important; padding-top: 10px; padding-bottom: 10px;">',
                          abp.localization.localize('CachesHeaderInfo'),
                          '<button name="clearAllCache" class="btn blue" style="float: right; color:#FFFFFF; background-color:#3598dc"><i class="fa fa-refresh"></i>',
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
                                        '<span>{name}</span>',
                                        '<button class="btn btn-xs blue pull-right" style="color:#FFFFFF; background-color:#3598dc" name="clearSingle">',
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
           scrollable: true,           
           itemId: 'webLogView',
           width: '100%',
           title: abp.localization.localize('WebSiteLogs'),
            iconCls: 'fa fa-outdent',                 
            store : 'maintenance.WebLogStore',           
            tpl: [
                   '<div class="row margin-bottom-10" style="padding-left: 20px !important; padding-right: 20px !important; padding-top: 8px;">',
                        '<div class="col-xs-6">',
                            abp.localization.localize('WebSiteLogsHeaderInfo'),
                        '</div>',
                        '<div class="col-xs-6 text-right">',
                            '<button name="downloadAllButton" style="color:#FFFFFF; background-color:#3598dc; margin-right: 10px; padding-left: 14px !important; padding-right: 14px !important; padding-top: 8px; padding-bottom: 7px;" ><i class="fa fa-download"></i>',
                              abp.localization.localize('DownloadAll'),
                            '</button>',
                            '<button name="refreshButton" style="color:#FFFFFF; background-color:#3598dc; padding-left: 14px !important; padding-right: 14px !important; padding-top: 8px; padding-bottom: 7px;" ><i class="fa fa-refresh"></i>',
                            abp.localization.localize('Refresh'), 
                            '</button>',
                        '</div>',
                    '</div>',

                    '<div class="row" style="padding-left: 20px !important; padding-right: 20px !important;">',
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