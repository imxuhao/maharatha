Ext.define('Chaching.view.main.ChachingViewportController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.main-chachingviewport',

    listen: {
        controller: {
            '#': {
                unmatchedroute: 'onRouteChange'
            }
        }
    },

    routes: {
        ':node': 'onRouteChange'
    },

    lastView: null,
    onRouteChange: function (id) {
        if (Chaching.utilities.RoutesNames.routesNames.indexOf(id) !== -1) {
            //if route is in ignore list then find route in ChachingGridPanelController
            var gridController = new Chaching.view.common.grid.ChachingGridPanelController;
            if (gridController) {
                gridController.currentRedirectedRoute = id;
                gridController.redirectTo(id, true);
            }
        }
        else {
            this.setCurrentView(id);
        }
    },
    setCurrentView: function (hashTag) {
        //try {


            hashTag = (hashTag || '').toLowerCase();

            var me = this,
                refs = me.getReferences(),
                mainCard = refs.mainCardPanel,
                mainLayout = mainCard.getLayout(),
                navigationList = refs.navigationTreeList,
                store = navigationList.getStore(),
                node = store.findNode('url', hashTag) ||
                       store.findNode('viewType', hashTag),
                view = (node && node.get('viewType')) || 'page404',
                lastView = me.lastView,
                iconCls = (node && node.get('iconCls')) || '',
                text = (node && node.get('text')) || 'Dashboard',
                existingItem = mainCard.child('component[routeId=' + hashTag + ']'),
                newView;

            // Kill any previously routed window
            if (lastView && lastView.isWindow) {
                lastView.destroy();
            }

            lastView = mainLayout.getActiveItem();

            if (!existingItem) {
                var customData = node.get('customData');
                if (customData && customData.length > 0) {
                }
                newView = Ext.create({
                    xtype: view,
                    routeId: hashTag,  // for existingItem search later
                    hideMode: 'offsets',
                    iconCls: iconCls,
                    closable: true,
                    dynamicTabItems:customData,
                    title: text
                });
                var modulePermission = abp.auth.hasPermission('Pages.' + newView.name);
                if (!modulePermission) {
                    Ext.toast('Requested resource by you is restricted due to security reason. Please contact support or clear #' + hashTag + ' from your browser url');
                    return;
                }
                if (typeof(newView.getStore) === 'function') {
                    var gridStore = newView.getStore();
                    if (gridStore)gridStore.load();
                }
            }

            if (!newView || !newView.isWindow) {
                // !newView means we have an existing view, but if the newView isWindow
                // we don't add it to the card layout.
                if (existingItem) {
                    // We don't have a newView, so activate the existing view.
                    if (existingItem !== lastView) {
                        mainLayout.setActiveItem(existingItem);
                    }
                    newView = existingItem;
                }
                else {
                    // newView is set (did not exist already), so add it and make it the
                    // activeItem.
                    Ext.suspendLayouts();
                    mainLayout.setActiveItem(mainCard.add(newView));
                    Ext.resumeLayouts(true);
                }
            }

            navigationList.setSelection(node);

            if (newView.isFocusable(true)) {
                newView.focus();
            }

            me.lastView = newView;
        //} catch (e) {
           // debugger;
            //Ext.toast('Please create view for the menuitem you clicked');
        //}
    },

    onNavigationTreeSelectionChange: function (tree, node) {
        var to = node && (node.get('url') || node.get('viewType'));

        if (to) {
            this.redirectTo(to);
        }
    },
    onMainViewRender: function () {
        if (!window.location.hash) {
            this.redirectTo("dashboard");
        }
    },
    onViewportResize: function (vprt, width, height, oldWidth, oldHeight, eOpts) {
        var me = this,
            view = me.getView();
        var westPanel = view.down('panel[region=west]');
        var northPanel = view.down('panel[region=north]');
        var treeList = undefined;
        var logo = undefined;
        if (width < 600 && westPanel && northPanel) {
            treeList = westPanel.down('treelist[itemId=navigationTreeList]');
            treeList.originalState = treeList.getMicro();
            treeList.setMicro(true);
            westPanel.setWidth(80);
            logo = northPanel.down('image[itemId=CapsLogo]');
            logo.setWidth(0);

        } else if (westPanel && northPanel) {
            treeList = westPanel.down('treelist[itemId=navigationTreeList]');
            var originalState = treeList.originalState === undefined ? false : treeList.originalState;
            treeList.setMicro(originalState);
            westPanel.setWidth(!originalState ? 250 : 80);
            logo = northPanel.down('image[itemId=CapsLogo]');
            logo.setWidth(!originalState ? 110 : 0);
        }
    },
    onTabItemChange: function (tabPanel, newCard, oldCard, eOpts) {
        var to = newCard && (newCard.routeId);

        if (to && window.location.hash !== '#' + to) {
            this.redirectTo(to);
        }
    }

});
