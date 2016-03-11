Ext.define('LoginApp.view.email.EmailActivationController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.email-emailactivation',
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
                    Ext.toast('Unable to activate email address. Please enter valid email address.');
                }
            });
        }
    },
    onBackClick: function () {
        var me = this,
            view = me.getView(),
            tabPanel = view.up('tabpanel');
        if (tabPanel) tabPanel.setActiveTab(0);
    }
    
});
