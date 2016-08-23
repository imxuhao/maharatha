Ext.define('Chaching.view.imports.ImportsErrorFormController',{
    extend:'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.imports-errorform',
    onCancelBtnClick: function (control, e, eOpts) {
        var me = this,
            view = me.getView();
        var wnd = view.up('window');
        if (wnd) {
            Ext.destroy(wnd);
        } else {
            Ext.destroy(view);
        }
    },
    onSaveBtnClick: function (control, e, eOpts) {
        var me = this,
            view = me.getView(),
            grid = view.down('gridpanel[itemId=errorgridPanelItemId]'),
            records = grid.getStore().data.items;
            result.AccountsList = records;
            Ext.Ajax.request({
                url: abp.appPath + '/api/services/app/accountUnit/BulkAccountUploads',
                jsonData: Ext.encode(result),
                success: function (response) { },
                failure: function (response, a, b) {  }
            });
    }
});