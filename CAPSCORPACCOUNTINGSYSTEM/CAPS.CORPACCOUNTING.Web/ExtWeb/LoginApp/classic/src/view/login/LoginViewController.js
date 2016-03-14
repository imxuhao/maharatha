Ext.define('LoginApp.view.login.LoginViewController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.login-loginview',
    //event handlers
    onLoginClick:function() {
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
                    var result = Ext.decode(response.responseText);
                    if (result && result.success) {
                        document.location = result.targetUrl;
                    }
                },
                failure: function (response) {
                    Ext.toast('Unable to login. Please enter valid username or password.');
                }
            });
        }
    }
    
});
