Ext.define('Chaching.view.projects.projectmaintenance.ProjectCOAsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.projects-projectmaintenance-projectcoasgrid',
    onChartOfAccountClicked: function (tableView, td, cellIndex, record, tr, rowIndex, e, eOpts) {       
        var me = this,
            view = me.getView(),
            tabPanel = view.up('tabpanel');
        if (e && e.target && tabPanel) {
            var horizontalTabPanel = tabPanel.up('tabpanel');
            var target = e.target,
                nodeName = target.nodeName;
            if (nodeName === "DIV" && target.attributes.isHyperLink) {
                nodeName = "A";
            }
            if (nodeName === "A" && horizontalTabPanel) {
                var accountsGrid = Ext.create({
                    xtype: 'projects.projectmaintenance.linenumbers',
                    hideMode: 'offsets',
                    closable: true,
                    title: abp.localization.localize("LineNumbers"),
                    routId: 'projects.projectmaintenance.linenumbers',
                    coaId: record.get('coaId')                 
                   
                });              
                var gridStore = accountsGrid.getStore(),                    
                    storeProxy = gridStore.getProxy();
                storeProxy.setExtraParam('coaId', record.get('coaId'));
                gridStore.load();
                var tabLayout = horizontalTabPanel.getLayout();
                if (tabLayout) {
                    tabLayout.setActiveItem(horizontalTabPanel.add(accountsGrid));
                }
            }
        }
    },
    doAfterCreateAction: function (createMode, formView, isEdit) {        
        var viewModel = formView.getViewModel();
        var form = formView.getForm();
        var rollupDivisionCombo = form.findField('rollupDivisionId');
        var rollupDivisionStore = rollupDivisionCombo.getStore();
        rollupDivisionStore.load();

        var rollupAccountCombo = form.findField('rollupAccountId');
        var rollupAccountStore = rollupAccountCombo.getStore();
        rollupAccountStore.load();
        if (isEdit) {
            //var rollupDivisionCombo = form.findField('rollupDivisionId');
            //var rollupDivisionStore = rollupDivisionCombo.getStore();
            //rollupDivisionStore.load();

            //var rollupAccountCombo = form.findField('rollupAccountId');
            //var rollupAccountStore = rollupAccountCombo.getStore();
            //rollupAccountStore.load();
        }
        
    }
    
});
