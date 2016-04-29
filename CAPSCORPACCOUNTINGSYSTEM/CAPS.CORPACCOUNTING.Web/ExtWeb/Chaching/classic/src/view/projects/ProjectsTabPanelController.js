Ext.define('Chaching.view.projects.ProjectsTabPanelController', {
    extend: 'Chaching.view.common.tab.ChachingTabPanelController',
    alias: 'controller.projects-projectstabpanel',
    onSubMenuItemTabChange: function (tabPanel, newCard, oldCard, eOpts) {      
        if (newCard && typeof (newCard.getStore) === "function") {
            var store = newCard.getStore();
            if (newCard.config.xtype == "projects.projectmaintenance.projectcoas") {
                var filters = [];
                var filter = new Ext.util.Filter({
                    entity: 'Coa',
                    searchTerm: false,
                    comparator: 1,
                    dataType: 3,
                    property: 'IsCorporate',
                    value: false
                });
                filters.push(filter);             
                store.filter(filters);
            }
            store.load();
        }
    }
    
});
