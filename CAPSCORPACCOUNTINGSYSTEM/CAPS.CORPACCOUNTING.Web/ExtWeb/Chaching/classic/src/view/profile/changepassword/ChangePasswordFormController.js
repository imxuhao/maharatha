Ext.define('Chaching.view.profile.changepassword.ChangePasswordFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.changepassword-changepasswordform',
    passwordenter: function (form, e, opt) {       
        var me = this;
        lablepassword = me.lookupReference('lablepassword');
        lablepassword.show();
    },
    passwordleave: function (form, e, opt) {
        var me = this;
        password = me.lookupReference('password');
        lablepassword = me.lookupReference('lablepassword');

        if (password.value)
            lablepassword.show();
        else
            lablepassword.hide();
    },
    onSaveClicked: function (btn) {        
        var me = this;       
        view = me.getView();
        password = me.lookupReference('password');
        newpassword = me.lookupReference('newpassword');
        var input = new Object();       
        input.CurrentPassword = password.value;
        input.NewPassword = newpassword.value;
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/profile/ChangePassword',
            jsonData: Ext.encode(input),
            success: function (response,opts) {              
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    var wnd = view.up('window');
                    Ext.destroy(wnd);
                }
            },
            failure: function (response, opts) {
                var res = Ext.decode(response.responseText);
                Ext.toast(res.exceptionMessage);
                console.log(response);
            }



        })
    }

});
