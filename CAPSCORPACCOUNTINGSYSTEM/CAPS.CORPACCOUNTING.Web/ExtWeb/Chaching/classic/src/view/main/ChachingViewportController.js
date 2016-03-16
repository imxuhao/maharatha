Ext.define('Chaching.view.main.ChachingViewportController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.main-chachingviewport',
    onViewportResize:function(vprt, width, height, oldWidth, oldHeight, eOpts) {
        var me = this,
            view = me.getView();
        var westPanel = view.down('panel[region=west]');
        var northPanel = view.down('panel[region=north]');
        var treeList = undefined;
        var logo = undefined;
        if (width < 600 && westPanel && northPanel) {
            treeList = westPanel.down('chachingmenu');
            treeList.originalState = treeList.getMicro();
            treeList.setMicro(true);
            westPanel.setWidth(85);
            logo = northPanel.down('image[itemId=CapsLogo]');
            logo.setWidth(0);

        } else if (westPanel && northPanel) {
            treeList = westPanel.down('chachingmenu');
            var originalState = treeList.originalState === undefined ? false : treeList.originalState;
            treeList.setMicro(originalState);
            westPanel.setWidth(!originalState ? 300 : 85);
            logo = northPanel.down('image[itemId=CapsLogo]');
            logo.setWidth(!originalState ? 110 : 0);
        }
    }
    
});
