/**
 * The main application class. An instance of this class is created by app.js when it
 * calls Ext.application(). This is the ideal place to handle application launch and
 * initialization details.
 */
Ext.require(
['Ext.*']);
Ext.define('Chaching.Application', {
    extend: 'Ext.app.Application',
    
    name: 'Chaching',

    stores: [
        // TODO: add global / shared stores here
    ],
    //requires: [
    //    'Chaching.view.main.ChachingViewport'
    //],
    launch: function () {
        var me = this;
        // TODO - Launch the application
        var vprt = Ext.create('Chaching.view.main.ChachingViewport');
        //create menu items first and bind to treelist/mobile menu based on profile
        
        var menuControl = undefined;
        var menu = undefined;
        var menuModel = undefined;
        if (Ext.os.deviceType === "Desktop") {
            menuControl = vprt.down('panel[reference=treelistContainer]');
            if (menuControl) {
                menu = menuControl.down('chachingmenu');
                if (menu) {
                    menuModel = menu.getViewModel();
                    if (menuModel) {
                        me.loadNavigationData(menuModel.getData().navItems);
                    }
                }
            }
        }
    },
    loadNavigationData: function (navStore) {
        var me = this;
        var originalMenu = abp.nav.menus.MainMenu;
        var root = navStore.getRoot();
        if (originalMenu && originalMenu.items.length>0) {
            for (var i = 0; i < originalMenu.items.length; i++) {
                var originalItem = originalMenu.items[i];
                root.appendChild(me.transformData(originalItem));
            }
        }
    },
    transformData:function(originalItem) {
        var transformedItem = [];
        var me = this;

        var newItem = {
            text: null,
            iconCls: null,
            expanded: false,
            name: null,
            url: null,
            children: [],
            leaf: true
        };
        for (var key in originalItem) {
            if (originalItem.hasOwnProperty(key)) {
                switch (key) {
                    case "items":
                        //recusively transform child items
                        if (originalItem[key].length > 0) {
                            newItem.leaf = false;
                            for (var i = 0; i < originalItem[key].length; i++) {
                                var subMenuItem = originalItem[key][i];
                                newItem.children.push(me.transformData(subMenuItem));
                            }
                        }
                    break;
                    case "displayName":
                        newItem.text = originalItem[key];
                        break;
                    case "icon":
                        newItem.iconCls = originalItem[key];
                        break;
                    case "name":
                        newItem.name = originalItem[key];
                        break;
                    case "url":
                        newItem.url = originalItem[key];
                        break;
                    default:
                        break;
                }
            }
        }
        transformedItem.push(newItem);
        return newItem;
    },

    onAppUpdate: function () {
        Ext.Msg.confirm('Application Update', 'This application has an update, reload?',
            function (choice) {
                if (choice === 'yes') {
                    window.location.reload();
                }
            }
        );
    }
});
