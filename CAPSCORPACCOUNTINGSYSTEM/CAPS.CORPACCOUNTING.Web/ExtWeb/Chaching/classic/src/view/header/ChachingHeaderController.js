Ext.define('Chaching.view.header.ChachingHeaderController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.header-chachingheader',
    onToggleClick:function(btn) {
        var me = this,
            view = me.getView();
        var westPanel = view.up('viewport').down('panel[region=west]');
        if (westPanel) {
            var treeList = westPanel.down('chachingmenu');
            var micro = treeList.getMicro();
            treeList.setMicro(!micro);
            westPanel.setWidth(micro ? 300 : 85);
        }
    }
    
});
