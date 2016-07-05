Ext.define('Chaching.view.roles.RolesGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.roles-rolesgrid',
    doAfterCreateAction: function (createNewMode, form, isEdit, record) {       
        var me = this,
         currentform = form;
        if (form.down('treepanel')) {
            var treeStore = form.down('treepanel').getStore();
            if (isEdit) {
                treeStore.getProxy().setExtraParams({ id: record.get('id') });
            } else {
                treeStore.getProxy().setExtraParams({});
            }
            treeStore.load();
        }

        //var data = {};
        //if (isEdit) {
        //    data.id = record.get('id');
        //}
        //Ext.Ajax.request({
        //    url: abp.appPath + 'api/services/app/role/GetRoleForEdit',
        //    jsonData: Ext.encode(data),
        //    success: function (response, opts) {
        //        var res = Ext.decode(response.responseText);
        //        if (res.success) {
        //            debugger;
        //            var data = res.result.permissions;
        //            if (basicForm.findField('permissions')) {
        //                var treeStore = basicForm.findField('permissions').getStore();
        //                treeStore.getProxy().setExtraParams();
        //            }
        //           // var treePanel = basicFormpermissions

        //        }
        //        else {
        //            Ext.toast(res.error.message);
        //        }
        //    },
        //    failure: function (response) {
        //        var res = Ext.decode(response.responseText);
        //        Ext.toast(res.error.message);
        //        console.log(response);
        //    }
        //})
    }

});
