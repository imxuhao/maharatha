Ext.define('Chaching.view.common.tab.ChachingTabPanelController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-tab-chachingtabpanel',
    //do your custom operation before adding dynamicTabItem
    doBeforeAddDynamicTabItem: function (dynamicTabItem) { },
    onSubMenuItemTabChange:function(tabPanel, newCard, oldCard, eOpts) {
        if (newCard && typeof (newCard.getStore)==="function") {
            newCard.getStore().load();
        }
    }
});
