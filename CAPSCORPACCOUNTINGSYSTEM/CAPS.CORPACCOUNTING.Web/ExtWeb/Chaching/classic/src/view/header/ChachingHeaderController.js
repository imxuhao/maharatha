Ext.define('Chaching.view.header.ChachingHeaderController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.header-chachingheader',
    onToggleClick: function (btn) {
        var me = this,
            view = me.getView();
        var westPanel = view.up('viewport').down('panel[region=west]');
        if (westPanel) {
            var treeList = westPanel.down('treelist[itemId=navigationTreeList]');
            var micro = treeList.getMicro();
            treeList.setMicro(!micro);
            westPanel.setWidth(micro ? 250 : 85);
            var logo = view.down('image[itemId=CapsLogo]');
            logo.setWidth(micro ? 110 : 0);
        }
    },
    onBeforeLocalizationRender: function (btn) {
        var currentCulture = abp.localization.currentCulture;
        if (currentCulture) {
            var locale = abp.localization.languages;
            for (var i = 0; i < locale.length; i++) {
                var item = locale[i];
                if (item.name === currentCulture.name) {
                    btn.text = currentCulture.displayName + ' &#xf107;';
                    btn.iconCls = item.icon;
                    break;
                }
            }
        }
    },
    onLocalizationHover: function (btn, e, eOpts) {
        var me = this,
            view = me.getView();
        var contextMenu = btn.contextMenu;
        var position = btn.getPosition();

        //var notificationBtn = view.down('button[itemId=NotificationBtn]');
        //if (notificationBtn) {
        //    me.hideContextMenu(notificationBtn);
        //}
        //var accountsBtn = view.down('button[itemId=AccountBtn]');
        //if (accountsBtn) {
        //    me.hideContextMenu(accountsBtn);
        //}
        if (contextMenu) {
            contextMenu.showAt(position[0] - 50, position[1] + 30, true);
        } else {
            var items = [];
            var locale = abp.localization.languages;
            for (var i = 0; i < locale.length; i++) {
                var item = locale[i];
                var menuItem = {
                    text: item.displayName,
                    iconCls: 'famfamfam-flag '+item.icon,
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
                ownerElement: btn,
                listeners: {
                    click: me.onLocalizationItemClick
                }
            });
            btn.contextMenu = contextMenu;
            contextMenu.showAt(position[0] - 50, position[1] + 30, true);
        }
    },
    hideContextMenu: function (overedBtn) {
        if (overedBtn && overedBtn.contextMenu) {
            overedBtn.contextMenu.hide();
        }
    },
    onLocalizationItemClick: function (menu, item, e, eOpts) {
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
    onNotificationHover: function (btn) {
        var me = this,
            view = me.getView();
        var contextMenu = btn.contextMenu;
        var position = btn.getPosition();
        //var localizationBtn = view.down('button[itemId=LocalizationBtn]');
        //if (localizationBtn) {
        //    me.hideContextMenu(localizationBtn);
        //}
        //var accountsBtn = view.down('button[itemId=AccountBtn]');
        //if (accountsBtn) {
        //    me.hideContextMenu(accountsBtn);
        //}
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
                    '<ul>',
                    '<li>{name}</li>',
                    '</ul>',
                    '</tpl>', {
                        userRenderer: function (user) {
                            //.. return a name instead of id
                            return user;
                        }
                    }, {
                        timeRenderer: function (timeStamp) {
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
    },
    onAccountsReady: function (btn) {
        var me = this,
            view = me.getView();
        var userName = '';
        //get user's login information
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
                    if (result.tenant) {
                        userName = result.tenant.tenancyName + '\\' + result.user.userName;
                    } else userName = '.\\' + result.user.userName;

                    if (userName && abp.session.impersonatorUserId !== abp.session.userId && abp.session.impersonatorUserId !== null) {
                        userName = '&#xf112 ' + userName;
                        btn.gotoMyAccount = true;//to get go to my account menu item
                        btn.setTooltip(abp.localization.localize("YouCanBackToYourAccount"));
                    }
                    var image = view.down('image[itemId=AccountPic]');
                    var ticks = new Date().getTime();
                    var profilePic = abp.appPath + 'Profile/GetProfilePicture?t=' + ticks;
                    if (result.user.profilePictureId) {
                        btn.gotoMyAccount ? btn.setWidth(180) : btn.setWidth(130);
                        btn.setIcon(profilePic);
                        image.hide();
                    } else {
                        image.show();
                        image.setSrc(profilePic);
                    }
                    btn.setText(userName);

                    var loggedInUserInfo = {
                        userName: userName,
                        defaultOrganizationId: result.user.defaultOrganizationId,
                        emailAddress: result.user.emailAddress,
                        userId: result.user.id,
                        name: result.user.name,
                        profilePictureId: result.user.profilePictureId,
                        surname: result.user.surname,
                        userOrganizationId: result.userOrganizationId,
                        gotoMyAccount: btn.gotoMyAccount
                    }
                    Chaching.utilities.ChachingGlobals.loggedInUserInfo = loggedInUserInfo;
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
    },
    manageActionClicked: function (menu, item, e, eOpts) {      
        var manageAction = Ext.create('Chaching.view.profile.linkedaccounts.LinkedAccountsView');
        var grid = manageAction.down('grid'),
        gridStore = grid.getStore();
        gridStore.load();
        manageAction.show();
    },
    changePasswordClick: function (menu, item, e, eOpts) {
        var changepasswordAction = Ext.create('Chaching.view.profile.changepassword.ChangePasswordView');       
        changepasswordAction.show();
    },
    mySettings: function (menu, item, e, eOpts) {
        var changepasswordAction = Ext.create('Chaching.view.profile.settings.SettingsView');

        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/profile/GetCurrentUserProfileForEdit',
            jsonData: {},
            success: function (response, opts) {
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    var form = changepasswordAction.down('form');
                    form.getForm().setValues(res.result);
                    changepasswordAction.show();
                }
            },
            failure: function (response, opts) {
                var res = Ext.decode(response.responseText);
                Ext.toast(res.exceptionMessage);
                console.log(response);
            }
        });

    },
    changeProfilePicture: function (menu, item, e, eOpts) {
        var changeProfilePicture = Ext.create('Chaching.view.profile.changeprofilepicture.ChangeProfilePictureView');
        changeProfilePicture.show();
    },
    onAccountsHover: function (btn) {
        var me = this,
           view = me.getView();
        var contextMenu = btn.contextMenu;
        var position = btn.getPosition();

        //var notificationBtn = view.down('button[itemId=NotificationBtn]');
        //if (notificationBtn) {
        //    me.hideContextMenu(notificationBtn);
        //}
        //var localizationBtn = view.down('button[itemId=LocalizationBtn]');
        //if (localizationBtn) {
        //    me.hideContextMenu(localizationBtn);
        //}
        if (contextMenu) {
            contextMenu.showAt(position[0] - 50, position[1] + btn.gotoMyAccount ? 60 : 30, true);
        } else {
            var items = [
                { text: abp.localization.localize("BackToMyAccount"), hidden: !btn.gotoMyAccount, name: 'BackToAccount', iconCls: 'icon-action-undo' },
                {
                    text: abp.localization.localize("LinkedAccounts"),
                    iconCls: 'icon-link',
                    menu: {
                        ui: 'accounts',
                        width: 170,
                        items: [
                            {
                                text: abp.localization.localize("ManageAccounts"),
                                iconCls: 'icon-settings',
                                name: 'ManageAccount',                              
                                listeners: {
                                    click: me.manageActionClicked
                                }
                            }
                        ]
                    }
                }, {
                    text: abp.localization.localize("LoginAttempts"),
                    iconCls: 'icon-shield',
                    name: 'LoginAttempts',
                    leaf: true,
                    listeners: {
                        click: me.loginAttemptsClicked
                    }
                }, {
                    text: abp.localization.localize("ChangePassword"),
                    iconCls: 'icon-key',
                    name: 'ChangePassword',
                    listeners: {
                        click: me.changePasswordClick
                    }
                }, {
                    text: abp.localization.localize("ChangeProfilePicture"),
                    iconCls: 'icon-user',
                    name: 'ChangeProfilePicture',
                    listeners: {
                        click: me.changeProfilePicture
                    }
                }, {
                    text: abp.localization.localize("MySettings"),
                    iconCls: 'icon-settings',
                    name: 'MySettings',
                    listeners: {
                        click: me.mySettings
                    }
                }, '-',
                {
                    text: abp.localization.localize("Logout"),
                    iconCls: 'icon-logout',
                    name: 'Logout'
                }
            ];
            if (btn.gotoMyAccount) {
                items.splice(1, 0, "-");
            }
            contextMenu = Ext.create({
                xtype: 'menu',
                ui: 'accounts',
                width: 200,
                items: items,
                ownerElement: btn,
                //listeners: {
                //    click: me.onLocalizationItemClick
                //}
            });
            btn.contextMenu = contextMenu;
            contextMenu.showAt(position[0] - 50, position[1] + btn.gotoMyAccount ? 60 : 30, true);
        }
    },
    loginAttemptsClicked: function (menu, item, e, eOpts) {
        var loginAttemptView = Ext.create('Chaching.view.profile.loginAttempts.LoginAttemptView');
        var listStore = loginAttemptView.down('dataview').getStore();
        listStore.load();
        loginAttemptView.show();
    }

});
