Ext.define('Chaching.view.menu.ChachingMenuController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.menu-chachingmenu',
    onNavigationTreeSelectionChange:function(tree, node) {
        var me = this,
            view = me.getView();
        //get name from node and create new tab into centerPort panel
    }
    
});
