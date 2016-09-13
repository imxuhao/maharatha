
Ext.define('Chaching.view.auditlogs.AuditLogDetail', {
    extend: 'Ext.view.View',
    requires : ['Chaching.model.auditlogs.AuditLogsModel'],
    store: {
        model: 'Chaching.model.auditlogs.AuditLogsModel'
    },
    scrollable: true,
    tpl: [
        '<tpl for=".">',
            '<div class="modal-body">'+
            '<form class="form-horizontal audit-log-detail-view" role="form">'+
            '<div class="form-body">'+
                '<h3 class="form-section">' + app.localize('UserInformations') + '</h3>' +
                '<div class="row">'+
                    '<div class="col-md-12">'+
                        '<div>'+
                            '<label class="control-label col-sm-3">' + app.localize('UserName') + ':</label>' +
                            '<div class="col-sm-9">'+
                                '<p class="form-control-static">{userName}</p>'+
                            '</div>'+
                        '</div>'+
                        '<div>'+
                            '<label class="control-label col-sm-3">' + app.localize('IpAddress') + ':</label>' +
                            '<div class="col-sm-9">'+
                                '<p class="form-control-static">{clientIpAddress}</p>'+
                            '</div>'+
                        '</div>'+
                        '<div >'+
                            '<label class="control-label col-sm-3">' + app.localize('Client') + ':</label>' +
                            '<div class="col-sm-9">'+
                                '<p class="form-control-static">{clientName}</p>'+
                            '</div>'+
                        '</div>'+
                        '<div>'+
                            '<label class="control-label col-sm-3">' + app.localize('Browser') + ':</label>' +
                            '<div class="col-sm-9">'+
                                '<p class="form-control-static">{browserInfo}</p>'+
                           ' </div>'+
                        '</div>'+
                        '<tpl if="this.isImpersonatorUserId(impersonatorUserId)">' +
                        '<div>'+
                            '<label class="control-label col-sm-3"></label>'+
                            '<div class="col-sm-9">'+
                                '<p class="form-control-static text-warning">'+app.localize('AuditLogImpersonatedOperationInfo')+'</p>'+
                            '</div>'+
                        '</div>'+
                         '</tpl>'+
                    '</div>'+
                '</div>'+

                '<h3 class="form-section">'+app.localize('ActionInformations')+'</h3>'+
                '<div class="row">'+
                    '<div class="col-md-12">'+
                        '<div>'+
                            '<label class="control-label col-sm-3">' + app.localize('Service') + ':</label>' +
                            '<div class="col-sm-9">'+
                                '<p class="form-control-static"> {serviceName}</p>'+
                            '</div>'+
                        '</div>'+
                    '</div>'+
                    '<div class="col-md-12">'+
                        '<div>'+
                            '<label class="control-label col-sm-3">' + app.localize('Action') + ':</label>' +
                            '<div class="col-sm-9">'+
                                '<p class="form-control-static">{methodName}</p>'+
                            '</div>'+
                        '</div>'+
                    '</div>'+
                    '<div class="col-md-12">'+
                        '<div>'+
                            '<label class="control-label col-sm-3">' + app.localize('Time') + ':</label>' +
                            '<div class="col-sm-9">'+
                                '<p class="form-control-static">{[this.getExecutionTime(values.executionTime)]}</p>' +
                            '</div>'+
                        '</div>'+
                    '</div>'+
                    '<div class="col-md-12">'+
                        '<div>'+
                            '<label class="control-label col-sm-3">' + app.localize('Duration') + ':</label>' +
                            '<div class="col-sm-9">'+
                                '<p class="form-control-static">{[this.getDurationAsMs(values.executionDuration)]}</p>' +
                            '</div>'+
                        '</div>'+
                        '<div>'+
                            '<label class="control-label col-sm-3">' + app.localize('Parameters') + ':</label>' +
                            '<div class="col-sm-9">' +
                               '<pre>{[this.formatParameters(values.parameters)]}</pre>' +
                            '</div>'+
                        '</div>'+
                    '</div>'+
                '</div>'+

                '<h3 class="form-section">'+app.localize('CustomData')+'</h3>'+
                '<div>'+
                    '<div class="col-md-12">'+
                        '<div>'+
                           '<div class="col-sm-12">'+
                           '<tpl if="!this.isCustomData(customData)">' +
                                '<p class="form-control-static" >'+app.localize('None')+'</p>'+
                                 '</tpl>'+
                                 '<tpl if="this.isCustomData(customData)">' +
                                '<p>{customData}</p>'+
                                '</tpl>'+
                            '</div>'+
                        '</div>'+
                    '</div>'+
                '</div>'+

                '<h3 class="form-section">'+app.localize('ErrorState')+'</h3>'+
                '<div >'+
                    '<div class="col-md-12">'+
                        '<div>'+
                            '<div class="col-sm-12">'+
                             '<tpl if="!this.isException(exception)">'+
                                '<p class="form-control-static" >'+
                                    '<i class="fa fa-check-circle font-green"></i>' +' ' + app.localize('Success')+
                                '</p>'+
                                 '</tpl>'+
                                  '<tpl if="this.isException(exception)">'+
                                '<pre>{exception}</pre>'+
                                '</tpl>'+
                            '</div>'+
                        '</div>'+
                    '</div>'+
                '</div>'+

            '</div>'+
        '</form>'+
   ' </div>'+
                    '</tpl>',
                    {
                        // XTemplate configuration:
                       // disableFormats: true,
                        // member functions:
                        isImpersonatorUserId: function (impersonatorUserId) {
                            if (impersonatorUserId) {
                                return true;
                            } else {
                                return false;
                            }
                        },
                        isCustomData: function(customData){
                            if(customData) {
                                return true;
                            } else {
                                return false;
                            }
                        },
                        isException: function(exception){
                            if(exception) {
                                return true;
                            } else {
                                return false;
                            }
                        },
                        formatParameters: function (parameters) {
                            var json = JSON.parse(parameters);
                            return JSON.stringify(json, null, 4);
                        },
                        getDurationAsMs: function (executionDuration) {
                            return app.localize('Xms', executionDuration);
                        },
                        getExecutionTime: function (executionTime) {
                            return moment(executionTime).fromNow() + ' (' + moment(executionTime).format('YYYY-MM-DD hh:mm:ss') + ')';
                        }
                    }
    ],
    itemSelector: 'div.modal-body',
    height: '100%',
    width: '100%'
});
