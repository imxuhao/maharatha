Ext.define('LoginApp.view.resetpwd.ResetPasswordController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.resetpwd-resetpassword',
    onSubmitClick: function () {
        var me = this,
            view = me.getView(),
            form = view.getForm();
        var values = form.getValues();
        if (values.TenancyName === "Tenance name") {
            values.TenancyName = null;
        }
        if (form.isValid()) {
            Ext.Ajax.request({
                url: form.url,
                jsonData: Ext.encode(values),
                method: 'POST',
                headers: {
                    'Accept': 'application/json'
                },
                success: function (response) {

                },
                failure: function (response) {
                    Ext.toast('Unable to reset password . Please enter valid email address.');
                }
            });
        }
    },
    onBackClick:function() {
        var me = this,
            view = me.getView(),
            tabPanel = view.up('tabpanel');
        if (tabPanel) tabPanel.setActiveTab(0);
    }
});
