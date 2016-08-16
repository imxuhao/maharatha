Ext.define('Chaching.view.imports.ImportsErrorFormController',{
    extend:'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.imports-errorform',
    onBtnClick: function (control, e, eOpts) {
        var me = this,
            view = me.getView();
        var wnd = view.up('window');
        if (wnd) {
            Ext.destroy(wnd);
        } else {
            Ext.destroy(view);
        }
    }
});