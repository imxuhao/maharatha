Ext.define('Chaching.view.maintenance.MaintenanceFormPanelController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.maintenance-maintenanceFormPanelController',
    loadCacheAndWeblogs: function () {
        var me = this,
        view = me.getView();
        cacheView = view.down('dataview[itemId=cacheDataView]');
        if (cacheView) {
            cacheView.getStore().load();
        }
        weblogView = view.down('dataview[itemId=webLogView]');
        if (weblogView) {
            weblogView.getStore().load(function (records, operation, success) {
                Ext.each(records, function (record) {
                    record.data = '<span class="log-line">' + record.data
                    .replace('DEBUG', '<span class="label label-default">DEBUG</span>')
                    .replace('INFO', '<span class="label label-info">INFO</span>')
                    .replace('WARN', '<span class="label label-warning">WARN</span>')
                    .replace('ERROR', '<span class="label label-danger">ERROR</span>')
                    .replace('FATAL', '<span class="label label-danger">FATAL</span>') + '</span>'

                });
            });
        }
    },
    onMaintenanceResize: function (formPanel, newWidth, newHeight, oldWidth, oldHeight) {
        var me = this,
            view = me.getView(),
            cachesDataView = view.down('*[itemId=cacheDataView]'),
            webLogsDataView = view.down('*[itemId=webLogView]');
        var height = newHeight - 50;
        if (cachesDataView) cachesDataView.setHeight(height);
        if (webLogsDataView) webLogsDataView.setHeight(height);
    },
    clearCache: function (view, record, item, index, e, eOpts) {
            var clickTarget = e.getTarget(),
            button = clickTarget.nodeName;
        if (button == "BUTTON" && clickTarget.name === "clearSingle") {
            Ext.Ajax.request({
                url: '/api/services/app/caching/ClearCache',
                params: {
                    'input': record.get('name')
                },

                success: function () {
                    abp.notify.success(app.localize('CacheSuccessfullyCleared'));
                },

                failure: function () {
                    abp.notify.error(app.localize('Error'));
            }
            });
        }
        else if (button == "BUTTON" && clickTarget.name === "clearAllCache") {
            this.clearAllCaches();
        }
    },

    clearAllCaches: function () {      
        Ext.Ajax.request({
            url: '/api/services/app/caching/ClearAllCaches',
            method: 'POST',

            success: function () {
                abp.notify.success(app.localize('AllCachesSuccessfullyCleared'));
            },
            failure: function () {
                abp.notify.error(app.localize('Error'));
        }
        });
    },

    buttonClicked: function (view, record, item, index, e, eOpts) {        
        if (item.name == "downloadAllButton") {           
            Ext.Ajax.request({                
                    url: '/api/services/app/webLog/DownloadWebLogs',
                    method: 'POST',
                    success: function (result) {                       
                        var res = Ext.decode(result.responseText);
                        if (res.success && res.result) {
                            location.href = abp.appPath + 'File/DownloadTempFile?fileType=' + res.result.fileType + '&fileToken=' + res.result.fileToken + '&fileName=' + res.result.fileName;
                        }
                    }, failure: function () {
                        abp.notify.error(app.localize('Error'));
                    }
            });
        }
        else {         
            Ext.Ajax.request({
                url: '/api/services/app/webLog/GetLatestWebLogs',
                method: 'POST'
            });
        }
    }

});