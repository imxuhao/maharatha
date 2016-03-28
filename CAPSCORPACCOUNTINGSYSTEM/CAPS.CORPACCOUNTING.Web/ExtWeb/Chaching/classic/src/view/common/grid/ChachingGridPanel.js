
Ext.define('Chaching.view.common.grid.ChachingGridPanel',{
    extend: 'Ext.grid.Panel',

    requires: [
        'Chaching.view.common.grid.ChachingGridPanelController',
        'Chaching.view.common.grid.ChachingGridPanelModel'
    ],

    controller: 'common-grid-chachinggridpanel',
    viewModel: {
        type: 'common-grid-chachinggridpanel'
    },
    requireMultiSearch:true,
    requireMultisort: true,
    headerButtonsConfig: null,
    requireExport:false,
    selModelConfig: null,
    isEditable: false,
    columnLines:true,
    padding: 5,
    frame: false,
    layout: {
        type:'fit'
    },
    showPagingToolbar: true,
    ignoreCellDblClick: false,
    dockedItems:[],
    initComponent: function() {
        var me = this,
            headerTbButtons = [],
            plugins = [];

        if (me.headerButtonsConfig) {
            for (var i = 0; i < me.headerButtonsConfig.length; i++) {
                var btn = me.headerButtonsConfig[i];
                headerTbButtons.push(btn);
            }
        }
        if (me.requireExport) {
            var exportBtn = {
                xtype: 'splitbutton',
                ui:'actionButton',
                text: abp.localization.localize("Export").toUpperCase(),
                iconCls: 'fa fa-download',
                iconAlign:'left',
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
                    pack:'left'
                },
                //width: '100%',
                items: []
            };
            for (var j = 0; j < headerTbButtons.length; j++) {
                var item = headerTbButtons[j];
                topBar.items.push(item);
            }
            me.dockedItems.push(topBar);

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
                inputItemWidth: 50,
                dock: 'bottom',
                width: '100%',
                tabIndex: -1,
                ui: 'plainBottom'

            };
            me.dockedItems.push(pagingToolBar);
            //me.dockedItems = [

            //];
        }
        me.callParent(arguments);
    }


});
