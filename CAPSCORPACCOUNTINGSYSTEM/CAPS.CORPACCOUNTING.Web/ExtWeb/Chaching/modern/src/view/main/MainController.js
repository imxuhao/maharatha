Ext.define('Chaching.view.main.MainController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.main',

    //listen : {
    //    controller : {
    //        '#' : {
    //            unmatchedroute : 'onRouteChange'
    //        }
    //    }
    //},

    //routes: {
    //    ':node': 'onRouteChange'
    //},

    config: {
        showNavigation: true
    },

    collapsedCls: 'main-nav-collapsed',

    init: function (view) {
        var me = this,
            refs = me.getReferences();

        me.callParent([ view ]);

        me.nav = refs.navigation;
        me.navigationTree = refs.navigationTree;
    },

    onNavigationItemClick: function () {
        // The phone profile's controller uses this event to slide out the navigation
        // tree. We don't need to do anything but must be present since we always have
        // the listener on the view...
    },

    onNavigationTreeSelectionChange: function (tree, node) {
        var to = node && (node.get('routeId') || node.get('viewType'));

        if (to) {
            this.redirectTo(to);
        }
    },

    onRouteChange: function (id) {
        this.setCurrentView(id);
    },

    onSwitchToClassic: function () {
        Ext.Msg.confirm('Switch to Classic', 'Are you sure you want to switch toolkits?',
                        this.onSwitchToClassicConfirmed, this);
    },

    onSwitchToClassicConfirmed: function (choice) {
        if (choice === 'yes') {
            var s = location.search;

            // Strip "?modern" or "&modern" with optionally more "&foo" tokens following
            // and ensure we don't start with "?".
            s = s.replace(/(^\?|&)modern($|&)/, '').replace(/^\?/, '');

            // Add "?classic&" before the remaining tokens and strip & if there are none.
            location.search = ('?classic&' + s).replace(/&$/, '');
        }
    },

    onToggleNavigationSize: function () {
        this.setShowNavigation(!this.getShowNavigation());
    },

    setCurrentView: function (hashTag) {
        hashTag = (hashTag || '').toLowerCase();

        var me = this,
            refs = me.getReferences(),
            mainCard = refs.mainCard,
            navigationTree = me.navigationTree,
            store = navigationTree.getStore(),
            node = store.findNode('routeId', hashTag) ||
                   store.findNode('viewType', hashTag),
            item = mainCard.child('component[routeId=' + hashTag + ']');

        if (!item) {
            item = mainCard.add({
                xtype: node.get('viewType'),
                routeId: hashTag
            });
        }
        
        mainCard.setActiveItem(item);
        
        navigationTree.setSelection(node);

        //if (newView.isFocusable(true)) {
        //    newView.focus();
        //}
    },

    updateShowNavigation: function (showNavigation, oldValue) {
        // Ignore the first update since our initial state is managed specially. This
        // logic depends on view state that must be fully setup before we can toggle
        // things.
        //
        if (oldValue !== undefined) {
            var me = this,
                cls = me.collapsedCls,
                refs = me.getReferences(),
                logo = refs.logo,
                navigation = me.nav,
                navigationTree = refs.navigationTree,
                rootEl = navigationTree.rootItem.el;

            navigation.toggleCls(cls);
            logo.toggleCls(cls);

            if (showNavigation) {
                // Restore the text and other decorations before we expand so that they
                // will be revealed properly. The forced width is still in force from
                // the collapse so the items won't wrap.
                navigationTree.setMicro(false);
            } else {
                // Ensure the right-side decorations (they get munged by the animation)
                // get clipped by propping up the width of the tree's root item while we
                // are collapsed.
                rootEl.setWidth(rootEl.getWidth());
            }

            logo.element.on({
                transitionend: function () {
                    if (showNavigation) {
                        // after expanding, we should remove the forced width
                        rootEl.setWidth('');
                    } else {
                        navigationTree.setMicro(true);
                    }
                },
                single: true
            });
        }
    },

    toolbarButtonClick: function(btn,e) {
        var href = btn.config.href;
        this.redirectTo(href);
        switch (btn.getItemId()) {
            case "Localization":
                this.showLocalization(btn);
                break;
            case "Notifiaction":
                this.showNotification(btn);
                break;
            default:
                break;
        }
    },
    onClearGlobalSearch:function(text, input, e, eOpts) {
        var me = this,
           refs = me.getReferences(),
           globalSearchBox = refs.globalSearch;
        globalSearchBox.hide();
    },
    showProfileList:function(btn) {
        var me = this,
           view = me.getView(),
           contextMenu = btn.contextMenu,
           refs = me.getReferences(),
           mainCard = refs.mainCard;
        if (contextMenu) {
            //
            var itemInMainCard = mainCard.child('component[id=' + contextMenu.id + ']');
            if (itemInMainCard)
                mainCard.setActiveItem(itemInMainCard);
            else {
                mainCard.add(btn.contextMenu);
                mainCard.setActiveItem(contextMenu);
            }
        } else {
            Ext.Ajax.request({
                method: 'POST',
                headers: {
                    'Accept': 'application/json'
                },
                url: abp.appPath + 'api/services/app/session/GetCurrentLoginInformations',

                success: function (response, opts) {
                    var obj = Ext.decode(response.responseText);
                    if (obj && obj.success) {
                        var result = obj.result;
                        
                        var treeStore = me.getProfileStore();
                        if (abp.session.impersonatorUserId === abp.session.userId || abp.session.impersonatorUserId === null) {
                            var root = treeStore.getRoot();
                            root.removeChild(root.childNodes[0]);
                        }
                       
                        var treeList = Ext.create('Ext.list.Tree', {
                            fullscreen: true,
                            store: treeStore,
                            ui: 'navigation',
                            expanderFirst: false,
                            expanderOnly: false
                        });
                        var container = Ext.create('Ext.Container', {
                            fullscreen: true,
                            items: [treeList]
                        });
                        btn.contextMenu = container;
                        mainCard.add(container);
                        mainCard.setActiveItem(container);

                    } else {
                        Ext.toast(obj.error.message);
                    }
                },

                failure: function (response, opts) {
                    var res = Ext.decode(response.responseText);
                    Ext.toast(res.exceptionMessage);
                    console.log(response);
                }
            });
        }

    },
    getProfileStore:function() {
        var treeStore = Ext.create('Ext.data.TreeStore', {
            fields: [
                {
                    name: 'text'
                }, { name: 'iconCls' },{name:'href'}
            ],
            root: {
                expanded: true,
                children: [
                    {
                        text: abp.localization.localize("BackToMyAccount"),
                        iconCls: 'icon-action-undo',
                        name: 'BackToAccount',
                        leaf: true
                    },
                    {
                        text: abp.localization.localize("LinkedAccounts"),
                        iconCls: 'icon-link',
                        children:[
                        {
                            text: abp.localization.localize("ManageAccounts"),
                            name: 'ManageAccount',
                            iconCls: 'icon-settings',
                            leaf: true
                        }]
                    },
                     {
                         text: abp.localization.localize("LoginAttempts"),
                         iconCls: 'icon-shield',
                         name: 'LoginAttempts',
                         leaf: true
                     },
                    {
                        text: abp.localization.localize("ChangePassword"),
                        iconCls: 'icon-key',
                        name: 'ChangePassword',
                        leaf: true
                    },
                    {
                        text: abp.localization.localize("ChangeProfilePicture"),
                        iconCls: 'icon-user',
                        name: 'ChangeProfilePicture',
                        leaf: true
                    },
                    {
                        text: abp.localization.localize("MySettings"),
                        iconCls: 'icon-settings',
                        name: 'MySettings',
                        leaf: true
                    },
                    {
                        text: abp.localization.localize("Logout"),
                        iconCls: 'icon-logout',
                        name: 'Logout',
                        leaf: true
                    }
                ]
            }
        });
        return treeStore;
    },
    showNotification:function(btn) {
        var me = this,
           view = me.getView(),
           contextMenu = btn.contextMenu,
           refs = me.getReferences(),
           mainCard = refs.mainCard;
        if (contextMenu) {
            //
            var itemInMainCard = mainCard.child('component[id=' + contextMenu.id + ']');
            if(itemInMainCard)
                mainCard.setActiveItem(itemInMainCard);
            else {
                mainCard.add(btn.contextMenu);
                mainCard.setActiveItem(contextMenu);
            }
        } else {
            ///TODO:to be populated from abp
            var items = [];
            items.push({ text: 'Notification One' });
            items.push({text: 'Long line notification to test line wrapping' });
            var notificationList = Ext.create('Ext.List', {
                fullscreen: true,
                ownerElement: btn,
                itemTpl: '{text}',
                data: items
                //listeners: {
                //    itemtap: me.changeLocale,
                //    scope: me
                //}
            });
            var notificationView = Ext.create('Ext.Panel', {
                fullscreen: true,
                items:[
                {
                    xtype: 'toolbar',
                    userCls: 'main-notification-bar',
                    width:'100%',
                    dock: 'top',
                    items:[
                    {
                        text: abp.localization.localize("SetAllAsRead"),
                        ui: 'header'
                    }, '->', { text: abp.localization.localize("Settings"), ui: 'header' }]
                }, notificationList]
            });
            btn.contextMenu = notificationView;
            mainCard.add(notificationView);
            mainCard.setActiveItem(notificationView);
        }
    },
    onNotificationPainted:function(btn) {
        ///TODO: Populate from abp
        btn.component.setBadgeText('1');
        btn.component.setBadgeCls('badge');
    },
    showLocalization: function(btn) {
        var me = this,
            view = me.getView(),
            contextMenu = btn.contextMenu,
            refs = me.getReferences(),
            mainCard = refs.mainCard;

        if (contextMenu) {
            //
            var itemInMainCard = mainCard.child('component[id=' + contextMenu.id + ']');
            if (itemInMainCard)
                mainCard.setActiveItem(itemInMainCard);
            else {
                mainCard.add(btn.contextMenu);
                mainCard.setActiveItem(contextMenu);
            }

        } else {
            var items = [];
            var locale = abp.localization.languages;
            for (var i = 0; i < locale.length; i++) {
                var item = locale[i];
                var menuItem = {
                    text: item.displayName,
                    iconCls: item.icon,
                    name: item.name,
                    isDefault: item.isDefault
                };
                items.push(menuItem);
            }
            var localizationList = Ext.create('Ext.List', {
                fullscreen: true,
                ownerElement: btn,
                itemTpl: '<table>' +
                    '<tr><td><img  class="{iconCls}"/></td><td style="padding-left:10px;">{text}</td></tr></table>',
                data: items,
                listeners: {
                    itemtap: me.changeLocale,
                    scope: me
                }
            });

            btn.contextMenu = localizationList;
            mainCard.add(localizationList);
            mainCard.setActiveItem(localizationList);
        }
    },
    changeLocale:function(list, index, target, record, e, eOpts) {
        var ownerElement = list.ownerElement;
        if (ownerElement) {
            ownerElement.setIconCls(record.get('iconCls'));
            location.href = abp.appPath + 'AbpLocalization/ChangeCulture?cultureName=' + record.get('name') + '&returnUrl=' + window.location.href;
        }
    },
    onBeforeLocalizationRender: function (btn) {
        var currentCulture = abp.localization.currentCulture;
        if (currentCulture) {
            var locale = abp.localization.languages;
            for (var i = 0; i < locale.length; i++) {
                var item = locale[i];
                if (item.name === currentCulture.name) {
                    btn.component.setIconCls(item.icon);
                    break;
                }
            }
        }
    },
});
