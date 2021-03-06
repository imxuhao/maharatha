﻿Ext.define('Chaching.store.NavigationTree', {
    extend: 'Ext.data.TreeStore',
    autoload: true,
    storeId: 'NavigationTree',
    fields: [{ name: 'text' },
    { name: 'iconCls' }, { name: 'name' }, { name: 'url' }, { name: 'viewType' },{name:'customData',type:'auto'}],

    root: {
        expanded: true

    },
    listeners: {
        load: function (treeStore, items, e) {
            treeStore.loadNavigationData(treeStore);
        }
    },
    loadNavigationData: function (navStore) {
        var me = this;
        var originalMenu = abp.nav.menus.MainMenu;
        var root = navStore.getRoot();
        if (originalMenu && originalMenu.items.length > 0) {
            for (var i = 0; i < originalMenu.items.length; i++) {
                var originalItem = originalMenu.items[i];
                root.appendChild(me.transformData(originalItem));
            }
        }
    },
    transformData: function (originalItem) {
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
                        newItem.viewType = newItem.url;
                        break;
                    case "customData":
                        newItem.customData = originalItem[key];
                        break;
                    default:
                        break;
                }
            }
        }
        transformedItem.push(newItem);
        return newItem;
    }
});
