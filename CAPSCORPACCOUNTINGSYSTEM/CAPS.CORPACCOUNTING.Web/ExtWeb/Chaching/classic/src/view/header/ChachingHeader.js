
Ext.define('Chaching.view.header.ChachingHeader', {
    extend: 'Ext.toolbar.Toolbar',

    requires: [
        'Chaching.view.header.ChachingHeaderController'
    ],
    alias: 'widget.chachingheader',
    controller: 'header-chachingheader',

    bodyStyle: {
        'background-color': '#F3F5F9'
    },

    layout: {
        type: 'hbox'
    },
    defaults: {
        bodyStyle: {
            'background-color': 'transparent',
            'border-color': 'transparent',
            'border-style': 'transparent'
        }
    },
    items: [
        {
            xtype: 'image',
            height: 30,
            alt : '',
            itemId: 'companyLogoImage',
            width: 110,
            src: ChachingGlobals.defaultCompanyLogoImage,
            margin: '2px'
        }, {
            xtype: 'button',
            text: '',
            scale: 'medium',
            iconCls: 'x-fa fa-list',
            iconAlign: 'right',
            width: 120,
            baseCls: '',
            listeners: {
                click: 'onToggleClick'
            }
        }, '->',
        //{
        //    xtype: 'combobox',
        //    itemId: 'userOrganizationListItemId',
        //    fieldLabel: app.localize('UserOrganizations'),
        //    queryMode: 'local',
        //    displayField: 'name',
        //    valueField: 'value',
        //    labelWidth : 150,
        //    ui: 'fieldLabelTop',
        //    store: Ext.create('Chaching.store.administration.organization.UserOrganizationListStore'),
        //    listeners: {
        //        select: 'onUserOrganizationChange'
        //    }
        //},
        {
            xtype: 'button',
            scale: 'small',
            width: 120,
            iconCls: 'famfamfam-flag-gb',
            iconAlign: 'left',
            text: 'English', 
            textAlign:'right',
            baseCls: '',
            itemId:'LocalizationBtn',
            contextMenu: undefined,
            ui: 'countryMenu',
            listeners: {
                click: 'onLocalizationHover',
                beforerender:'onBeforeLocalizationRender'
            }
        },
         {
             scale: 'small',
             ui: 'badgeBtn',
             width: 50,
             iconCls: 'badge',
             iconAlign: 'right',
             text: '&#xf0a2;',
             textAlign: 'right',
             baseCls: '',
             itemId: 'NotificationBtn',
             contextMenu: undefined,
             menu: undefined,
             listeners: {
                 click: 'onNotificationHover',
                 boxready: 'onNotificationReady'
             }
         },
        {
            scale: 'small',
            text: '',
            ui: 'badgeBtnBack',
            gotoMyAccount: false,
            textAlign: 'right',
            baseCls: '',
            itemId: 'AccountBtn',
            contextMenu: undefined,
            menu: undefined,
            style: {
                'top': '3px !important'
            },
            listeners: {
                click: 'onAccountsHover',
                boxready: 'onAccountsReady'
            }
        }, {
            xtype: 'image',
            alt: '',
            userCls:'img-circle',
            height: 40,
            width: 40,
            itemId: 'AccountPic',
            hidden: true,
            style : {
                cursor: 'pointer !important;'
            },
            listeners: {
                el: {
                    click: 'onAccountsHover'
                }
            }
            
        }
    ]
});
