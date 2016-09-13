Ext.define('Chaching.view.profile.linkedaccounts.LinkedAccountsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.linkedaccounts-linkedaccountsform',
    doPostSaveOperations: function (records, operation, success) {
        var deferred = new Ext.Deferred();
        
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/userLink/GetRecentlyUsedLinkedUsers',
            method: 'POST',
            success: function (response, opts) {
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    ChachingGlobals.userLinkedAccounts = res.result.items;
                }
                deferred.resolve('{success:true}');
            },
            failure: function (response, opts) {
                var res = Ext.decode(response.responseText);
                if (!Ext.isEmpty(res.exceptionMessage)) {
                    abp.message.error(res.exceptionMessage);
                } else {
                    abp.message.error(res.error.message);
                }
                console.log(response);
                deferred.resolve('{success:false}');
            }
        });
        return deferred.promise;
    }
});
