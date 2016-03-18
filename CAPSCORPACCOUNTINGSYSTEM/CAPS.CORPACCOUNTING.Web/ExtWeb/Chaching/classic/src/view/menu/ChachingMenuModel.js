Ext.define('Chaching.view.menu.ChachingMenuModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.menu-chachingmenu',
    data: {
        name: 'Chaching'
    },
    formulas: {
        selectionText: function (get) {
            var selection = get('treelist.selection'),
                path;
            if (selection) {
                path = selection.getPath('text');
                path = path.replace(/^\/Root/, '');
                return 'Selected: ' + path;
            } else {
                return 'No node selected';
            }
        }
    },
    stores: {
        navItems: {
            type: 'tree',
            root: {
                expanded: true
                //being populated from abp.nav.menus
            }
        }
    }

});
