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
            var logo = view.down('image[itemId=CapsLogo]');
            logo.setWidth(micro ? 110 : 0);
        }
    },
    onBeforeLocalizationRender:function(btn) {
        var currentCulture = abp.localization.currentCulture;
        if (currentCulture) {
            var locale = abp.localization.languages;
            for (var i = 0; i < locale.length; i++) {
                var item = locale[i];
                if (item.name === currentCulture.name) {
                    btn.text = currentCulture.displayName+ ' &#xf107;';
                    btn.iconCls = item.icon;
                    break;
                }
            }
        }
    },
    onLocalizationHover:function(btn, e, eOpts) {
        var me = this,
            view = me.getView();
        var contextMenu = btn.contextMenu;
        var position = btn.getPosition();

        var notificationBtn = view.down('button[itemId=NotificationBtn]');
        if (notificationBtn) {
            me.hideContextMenu(notificationBtn);
        }
        if (contextMenu) {
            contextMenu.showAt(position[0]-50, position[1] + 30, true);
        } else {
            var items = [];
            var locale = abp.localization.languages;
            for (var i = 0; i < locale.length; i++) {
                var item = locale[i];
                var menuItem = {
                    text: item.displayName ,
                    iconCls: item.icon,
                    name: item.name,
                    isDefault: item.isDefault
                };
                items.push(menuItem);
            }
            contextMenu = Ext.create({
                xtype: 'menu',
                ui: 'countryMenu',
                width: 150,
                items: items,
                ownerElement:btn,
                listeners: {
                    click: me.onLocalizationItemClick
                }
            });
            btn.contextMenu = contextMenu;
            contextMenu.showAt(position[0]-50, position[1] + 30, true);
        }
    },
    hideContextMenu:function(overedBtn) {
        if (overedBtn && overedBtn.contextMenu) {
            overedBtn.contextMenu.hide();
        }
    },
    onLocalizationItemClick:function(menu, item, e, eOpts) {
        var ownerElement = menu.ownerElement;
        if (ownerElement) {
            ownerElement.setText(item.text + ' &#xf107;');
            ownerElement.setIconCls(item.iconCls);
            location.href = abp.appPath + 'AbpLocalization/ChangeCulture?cultureName=' + item.name + '&returnUrl=' + window.location.href;
        }
    },
    onNotificationReady: function (btn) {
        ///TODO: Populate with unread count of notification
        btn.btnIconEl.dom.textContent = 1;
    },
    onNotificationHover: function(btn) {
        var me = this,
            view = me.getView();
        var contextMenu = btn.contextMenu;
        var position = btn.getPosition();
        var localizationBtn = view.down('button[itemId=LocalizationBtn]');
        if (localizationBtn) {
            me.hideContextMenu(localizationBtn);
        }
        if (contextMenu) {
            contextMenu.showAt(position[0] - 250, position[1] + 30, true);
        } else {
            ///TODO: Store To be populated with real time data as service does not return data.
            var dataview = new Ext.view.View({
                ui: 'balck',
                store: {
                    fields: ['name'],
                    data: [
                        { name: 'Notification one' },
                        { name: 'This is long notification message wraps to second line' }
                    ]
                },
                tpl: [
                    '<div class="menuHeaderTextRight">Settings</div>',
                    '<tpl for=".">',
                    '<ul class="dropdown-menu">',
                    '<li>{name}</li>',
                    '</ul>',
                    '</tpl>', {
                        userRenderer: function(user) {
                            //.. return a name instead of id
                            return user;
                        }
                    }, {
                        timeRenderer: function(timeStamp) {
                            //.. return time in some format
                            return timeStamp;
                        }
                    }
                ],
                // Match the li, since each one maps to a record
                itemSelector: 'li'
            });
            contextMenu = Ext.create({
                xtype: 'menu',
                ui: 'balck',
                width: 300,
                items: [dataview],
                ownerElement: btn
            });
            btn.contextMenu = contextMenu;
            btn.menu = contextMenu;
            contextMenu.showAt(position[0] - 250, position[1] + 30, true);
        }
    }

});
