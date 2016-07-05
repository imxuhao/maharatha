Ext.define('Chaching.view.roles.RolesGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.roles-rolesgrid',
    doAfterCreateAction: function (createNewMode, form, isEdit, record) {       
        var me = this;
        var currentform = form;
        var data = {};
        if (isEdit) {
            data.id = record.get('id');
        }
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/role/GetRoleForEdit',
            jsonData: Ext.encode(data),
            success: function (response, opts) {
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    var data = res.result.permissions;
                  

                }
                else {
                    Ext.toast(res.error.message);
                }
            },
            failure: function (response) {
                var res = Ext.decode(response.responseText);
                Ext.toast(res.error.message);
                console.log(response);
            }
        })
    },

});
