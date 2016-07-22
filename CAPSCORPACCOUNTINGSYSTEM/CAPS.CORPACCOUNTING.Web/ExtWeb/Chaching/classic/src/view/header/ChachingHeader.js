
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
            itemId: 'CapsLogo',
            width: 110,
            src: abp.appPath + 'Content/images/capslogo.png',
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
            ui: 'countryMenu',
            width: 120,
            iconCls: 'famfamfam-flag-gb',
            iconAlign: 'left',
            text: 'English' + ' &#xf107;',
            textAlign:'right',
            baseCls: '',
            itemId:'LocalizationBtn',
            contextMenu:undefined,
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
            ui: 'badgeBtnBack',
            width: 100,
            height:50,
            iconAlign: 'right',
            text:'',//set dynamically based on login info of user
            gotoMyAccount: false,
            textAlign: 'right',
            baseCls: '',
            itemId: 'AccountBtn',
            contextMenu: undefined,
            menu: undefined,
            style: {
                'top':'3px !important'
            },
            listeners: {
                click: 'onAccountsHover',
                boxready: 'onAccountsReady'
            }
        }, {
            xtype: 'image',
            userCls:'img-circle',
            height: 50,
            width: 50,
            itemId: 'AccountPic',
            hidden:true
        }
    ]
});
