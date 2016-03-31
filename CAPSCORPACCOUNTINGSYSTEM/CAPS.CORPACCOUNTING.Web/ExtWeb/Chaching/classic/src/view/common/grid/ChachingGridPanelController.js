Ext.define('Chaching.view.common.grid.ChachingGridPanelController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.common-grid-chachinggridpanel',
    //Event Listeners
    editActionClicked: function (menu, item, e, eOpts) {
        //do edit based on editMode of grid
        var parentMenu = menu.parentMenu,
            widgetRec = parentMenu.widgetRecord,
            widgetCol = parentMenu.widgetColumn,
            grid = widgetCol.up('grid');

        //TODO start edit by checking row allowEdit property
        if (widgetRec && grid) {
            var editingPlugin = grid.getPlugin('editingPlugin');
            if (editingPlugin) {
                editingPlugin.startEdit(widgetRec);
            }
        }

    },
    deleteActionClicked:function(menu,item,e,eOpts) {
    },

    //editing plugin listeners
    onBeforeGridEdit:function(editor, context, eOpts) {
        ///TODO cancel edit if restricted
        //cancel edit if is actioncolumn editing
        if (context.column.name === "ActionColumn") return false;
    }
    
});
