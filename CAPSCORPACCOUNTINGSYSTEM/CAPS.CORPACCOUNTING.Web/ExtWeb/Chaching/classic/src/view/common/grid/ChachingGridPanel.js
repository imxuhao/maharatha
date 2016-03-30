
Ext.define('Chaching.view.common.grid.ChachingGridPanel', {
    extend: 'Ext.grid.Panel',

    requires: [
        'Chaching.view.common.grid.ChachingGridPanelController',
        'Chaching.view.common.grid.ChachingGridPanelModel'
    ],

    controller: 'common-grid-chachinggridpanel',
    viewModel: {
        type: 'common-grid-chachinggridpanel'
    },
    /**
    * @hide
    * @private
    * @cfg {boolean} requireMultiSearch
    */
    requireMultiSearch: true,
    /**
    * @hide
    * @private
    * @cfg {boolean} requireMultisort
    */
    requireMultisort: true,
    /**
    * @hide
    * @private
    * @cfg {object[]} headerButtonsConfig
    */
    headerButtonsConfig: null,
    /**
    * @hide
    * @private
    * @cfg {boolean} requireExport
    */
    requireExport: false,
    /**
    * @hide
    * @private
    * @cfg {object} selModelConfig
    */
    selModelConfig: null,
    /**
    * @hide
    * @private
    * @cfg {boolean} isEditable
    */
    isEditable: false,
    /**
    * @hide
    * @private
    * @cfg {string} editingMode
    */
    editingMode:null,
    columnLines: true,
    padding: 5,
    frame: false,
    layout: {
        type: 'fit'
    },
    showPagingToolbar: true,
    ignoreCellDblClick: false,
    cls: 'chaching-grid',
    /**
    * @hide
    * @private
    * @cfg {object[]} actionColumnMenuItemsConfig
    */
    actionColumnMenuItemsConfig: null,//grid related action columns menu items
    /**
    * @hide
    * @private
    * @cfg {boolean} requireActionColumn
    */
    requireActionColumn: true,
    name: '',
    enableLocking:false,
    syncRowHeight: false,
    initComponent: function () {
        var me = this,
            controller = me.getController(),
            headerTbButtons = [],
            plugins = [],
            dockedItems = [];

        if (me.headerButtonsConfig) {
            for (var i = 0; i < me.headerButtonsConfig.length; i++) {
                var btn = me.headerButtonsConfig[i];
                if (btn.checkPermission) {
                    if (abp.auth.hasPermission("Pages." + me.name + ".Create"))
                        headerTbButtons.push(btn);
                } else headerTbButtons.push(btn);
            }
        }
        if (me.requireExport) {
            var exportBtn = {
                xtype: 'splitbutton',
                ui: 'actionButton',
                text: abp.localization.localize("Export").toUpperCase(),
                iconCls: 'fa fa-download',
                iconAlign: 'left',
                menu: new Ext.menu.Menu({
                    ui: 'accounts',
                    items: [
                        { text: abp.localization.localize("ExportToExcel").toUpperCase(), iconCls: 'fa fa-file-excel-o', itemId: 'ExportExcel' },
                        { text: abp.localization.localize("ExportToPDF").toUpperCase(), iconCls: 'fa fa-file-pdf-o', itemId: 'ExportPDF' }
                    ]
                })
            };
            headerTbButtons.push(exportBtn);
        }

        if (headerTbButtons.length > 0) {
            var topBar =
            {
                xtype: 'toolbar',
                ui: 'plain',
                dock: 'top',
                layout: {
                    type: 'hbox',
                    pack: 'left'
                },
                //width: '100%',
                items: []
            };
            for (var j = 0; j < headerTbButtons.length; j++) {
                var item = headerTbButtons[j];
                topBar.items.push(item);
            }
            dockedItems.push(topBar);

        }

        //add requireMultisort plugin if required requireMultisort functionality
        if (me.requireMultisort) {
            me.features = [
                {
                    ftype: 'ux-gmsrt',
                    displaySortOrder: true
                }
            ];
        }
        if (me.requireMultiSearch) {
            var mutisearch = {
                ptype: 'saki-gms',
                iconColumn: true,
                clearItemIconCls: 'icon-settings',
                pluginId: 'gms',
                height: 32,
                filterOnEnter: false
            };
            plugins.push(mutisearch);
        }
        me.plugins = plugins;

        if (me.showPagingToolbar) {
            var pagingToolBar = {
                xtype: 'pagingtoolbar',
                store: me.store,
                displayInfo: true,
                //inputItemWidth: 50,
                dock: 'bottom',
                width: '100%',
                tabIndex: -1,
                ui: 'plainBottom'

            };
            dockedItems.push(pagingToolBar);
        }
        if (dockedItems.length > 0) {
            me.dockedItems = dockedItems;
        }

        //add actionColumn if required
        if (me.requireActionColumn) {
            var menuItems = [];
            var defaultMenuItems = me.getDefaultActionColumnMenuItems();
            var userMenuItems = me.actionColumnMenuItemsConfig;

            if (defaultMenuItems && defaultMenuItems.length > 0) {
                for (var k = 0; k < defaultMenuItems.length; k++) {
                    menuItems.push(defaultMenuItems[k]);
                }
            }

            if (userMenuItems && userMenuItems.length > 0) {
                if (menuItems.length > 0) {
                    menuItems.push('-');
                }
                for (var l = 0; l < userMenuItems.length; l++) {
                    menuItems.push(userMenuItems[l]);
                }
            }
            var actionCol= {
                text: app.localize('Actions'),
                width:150,
                hidden: false,
                sortable: false,
                name: 'ActionColumn',
                hideable: false,
                menuDisabled: true,
                renderer: function (value, cell) {
                    var id = Ext.id();
                    var widgetRec = cell.record;
                    var widgetCol = cell.column;
                    Ext.Function.defer(function () {
                        var button = Ext.create('Ext.button.Button', {
                            ui: 'actionMenuButton',
                            scale: 'small',
                            height: 25,
                            width: 100,
                            text: app.localize('Actions'),
                            iconCls: 'icon-settings',
                            iconAlign: 'left',
                            widgetRec: widgetRec,
                            widgetCol: widgetCol,
                            menu: new Ext.menu.Menu({
                                ui: 'accounts',
                                items: menuItems
                            }),
                            listeners: {
                                menushow: function (btn) {
                                    btn.menu.widgetRecord = btn.widgetRec;
                                    btn.menu.widgetColumn = btn.widgetCol;
                                }
                            }
                        });
                        if (Ext.get(id)) {
                            button.render(Ext.get(id));
                        }
                    }, 1);
                    return '<div id="' + id + '"></div>';

                }
            }
            me.columns.splice(0, 0, actionCol);
        }
        //add editing plugin
        if (me.isEditable) {
            
        }
        me.callParent(arguments);
    },
    getDefaultActionColumnMenuItems: function () {
        var me = this, controller = me.getController();
        var defaultMenuItems = [];
        //check for permission from abp
        if (abp.auth.hasPermission("Pages." + me.name + ".Edit")) {
            var editMenuItem = {
                text: app.localize('Edit'), iconCls: 'fa fa-pencil',
                listeners: {
                    click: controller.editActionClicked
                }
            };
            defaultMenuItems.push(editMenuItem);
        }
        if (abp.auth.hasPermission("Pages." + me.name + ".Delete")) {
            var deleteMenuItem = {
                text: app.localize('Delete'), iconCls: 'fa fa-recycle',
                listeners: {
                    click: controller.deleteActionClicked
                }
            };
            defaultMenuItems.push(deleteMenuItem);
        }
        return defaultMenuItems;
    }


});
