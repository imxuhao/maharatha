Ext.define('Chaching.view.maintenance.MaintenanceFormPanelController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.maintenance-maintenanceFormPanelController',
    loadCacheAndWeblogs: function () {
        var me = this,            
        view = me.getView();
        var cacheView = view.down('dataview[itemId=cacheDataView]');
        if (cacheView) {
            var cacheStore = cacheView.getStore();
            if (cacheStore && !cacheStore.isLoaded())
                cacheStore.load();
        }
        var weblogView = view.down('dataview[itemId=webLogView]');
        if (weblogView) {            
            this.loadWebLogView(weblogView, false);
        }
    },

    loadWebLogView: function (view, isRefreshBtnClick) {
        var webLogStore = view.getStore(),
            me = this;
        if (webLogStore && !webLogStore.isLoaded()) {
            me.loadWebLogStore(webLogStore, view);
        } else if (isRefreshBtnClick) {
            me.loadWebLogStore(webLogStore, view);
        }
    },

    loadWebLogStore: function (webLogStore, view) {
        webLogStore.load(function (records, operation, success) {
            Ext.each(records,
                function (record) {
                    record.data = '<span class="log-line">' +
                        record.data
                        .replace('DEBUG', '<span class="label" style="background-color:#777;">DEBUG</span>')
                        .replace('INFO', '<span class="label" style="background-color:#5bc0de;">INFO</span>')
                        .replace('WARN', '<span class="label" style="background-color:#f0ad4e;">WARN</span>')
                        .replace('ERROR', '<span class="label" style="background-color:#d9534f;">ERROR</span>')
                        .replace('FATAL',
                            '<span class="label" style="background-color:#1ef7b8;">FATAL</span>') +
                        '</span>';
                });
            if (view.getStore()) {
                view.getStore().removeAll();
                view.getStore().loadData(records);
            }

        });
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
           var parentNode = clickTarget.parentNode,
           cacheName = parentNode.getElementsByTagName('span')[0].textContent;

            var data = {
                id: cacheName
            };
            Ext.Ajax.request({
                url: abp.appPath + 'api/services/app/caching/ClearCache',
                jsonData : Ext.encode(data),      
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
            url: abp.appPath + 'api/services/app/caching/ClearAllCaches',
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
        var me = this;
        if (item.name == "downloadAllButton") {
            Ext.Ajax.request({                
                    url: abp.appPath + 'api/services/app/webLog/DownloadWebLogs',
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
           if (item.name == "refreshButton") {
               me.loadWebLogView(view, true);
            }
        }
    }

});