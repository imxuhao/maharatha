Ext.define('Chaching.view.linkedaccounts.LinkedAccountsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.linkedaccounts-linkedaccountsgrid',
   
    unlinkUser: function (editor, e, rowIndex) {
        var me = this,
            controller = me.controller,
            view = me.gridControl;

        var rec = me.widgetRec;      
            if (rec.id > 0) {                    
               var input = new Object();               
                input.UserId = rec.get('id');               

                Ext.Ajax.request({
                    url: abp.appPath + 'api/services/app/userLink/unlinkUser',
                    jsonData: Ext.encode(input),
                    success: function (response) {
                        view.getStore().reload();
                    },
                    failure: function (response, opts) {
                        var res = Ext.decode(response.responseText);
                        Ext.toast(res.exceptionMessage);
                        console.log(response);
                    }

                });
            }          
    }
});
