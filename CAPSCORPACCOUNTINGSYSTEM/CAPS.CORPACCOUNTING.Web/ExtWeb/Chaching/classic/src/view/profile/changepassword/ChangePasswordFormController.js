Ext.define('Chaching.view.profile.changepassword.ChangePasswordFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.changepassword-changepasswordform',
    passwordenter: function (form, e, opt) {       
        var me = this,
        labelPassword = me.lookupReference('labelPassword');
        labelPassword.show();
    },
    passwordleave: function (form, e, opt) {
        var me = this,
        password = me.lookupReference('password'),
        labelPassword = me.lookupReference('labelPassword');

        if (password.value)
            labelPassword.show();
        else
            labelPassword.hide();
    },
    onSaveClicked: function (btn) {        
        var me = this,       
        view = me.getView(),
        password = me.lookupReference('password'),
        newpassword = me.lookupReference('newpassword');
        var input = new Object();       
        input.CurrentPassword = password.value;
        input.NewPassword = newpassword.value;
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/profile/ChangePassword',
            jsonData: Ext.encode(input),
            success: function (response, opts) {
                debugger;
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    var wnd = view.up('window');
                    Ext.destroy(wnd);
                    abp.notify.info(app.localize('YourPasswordHasChangedSuccessfully'));
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
