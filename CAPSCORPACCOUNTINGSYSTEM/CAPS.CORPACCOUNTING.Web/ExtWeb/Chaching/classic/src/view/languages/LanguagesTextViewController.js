Ext.define('Chaching.view.languages.LanguagesTextViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.languages-languagestextview',
    onWindowResize: function (wnd, width, height, oldWidth, oldHeight) {
        var me = this,
            view = me.getView(),
            form = view.down('form'),
            grid = form.down('Languagetexts');
        grid.setWidth(width);
        grid.setHeight(height - 120);
    }
    
});
