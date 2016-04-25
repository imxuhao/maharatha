
Ext.define('Chaching.view.common.tab.ChachingTabPanel',{
    extend: 'Ext.tab.Panel',

    requires: [
        'Chaching.view.common.tab.ChachingTabPanelController',
        'Chaching.view.common.tab.ChachingTabPanelModel'
    ],

    controller: 'common-tab-chachingtabpanel',
    viewModel: {
        type: 'common-tab-chachingtabpanel'
    },
    ui: 'submenuTabs',
    tabPosition: 'left',
    tabStretchMax: true,
    titleAlign: 'left',
    //tabBarHeaderPosition: 1,
    titleRotation: 0,
    tabRotation: 2,//0
    bodyStyle: {
        'background-color': '#F3F5F9'
    },
    /**
  * @hide
  * @private
  * @cfg {object} modulePermissions
    * Override this config in child grid if has additional permissions
  */
    modulePermissions: undefined,
    //creates dynamic tabs apart from items config of tabPanel
    dynamicTabItems: null,
    staticTabItems:null,
    layout: {
        type: 'card',
        anchor: '100%'
    },
    initComponent: function() {
        var me = this,
            controller = me.getController(),
            items = [];

        //creates statics items
        var staticTabItems = me.getStaticTabItems();
        if (staticTabItems && staticTabItems.length > 0) {
            for (var i = 0; i < staticTabItems.length; i++) {
                items.push(staticTabItems[i]);
            }
            
        }
        //create items based on custom data config apart from items config of tabpanel
        var dynamicTabItems = me.getDynamicTabItems();
        if (dynamicTabItems) {
            for (var j = 0; j < dynamicTabItems.length; j++) {
                var dynamicItem = dynamicTabItems[j];
                controller.doBeforeAddDynamicTabItem(dynamicItem);
                items.push(dynamicItem);
            }
            
        }
        //load first tab list's store
        if (items.length>0) {
            var firstTabItem = items[0];
            if (typeof (firstTabItem.getStore) === "function") {
                firstTabItem.getStore().load();
            }
        }
        me.items = items;
        me.callParent(arguments);
        me.on('tabchange', controller.onSubMenuItemTabChange);
    },
    getDynamicTabItems:function() {
        var me = this,
           controller = me.getController(),
           items;
        var dynamicItems = me.dynamicTabItems;
        if (dynamicItems && dynamicItems.length > 0) {
            items = [];
            
            for (var i = 0; i < dynamicItems.length; i++) {
                var custom = dynamicItems[i];
                var dynamicItem = Ext.create({
                    xtype: custom.url,
                    hideMode: 'offsets',
                    closable: false,
                    title: app.localize(custom.displayName.name)
                });
                if (dynamicItem && dynamicItem.modulePermissions.read) {
                    items.push(dynamicItem);
                }
            }
        }
        return items;
    },
    getStaticTabItems:function() {
        var me = this,
            controller = me.getController(),
            items;
        var staticItems = me.staticTabItems;
        if (staticItems && staticItems.length > 0) {
            items = [];
            for (var i = 0; i < staticItems.length; i++) {
                var item = staticItems[i];
                items.push(item);
            }
        }
        return items;
    }
});
