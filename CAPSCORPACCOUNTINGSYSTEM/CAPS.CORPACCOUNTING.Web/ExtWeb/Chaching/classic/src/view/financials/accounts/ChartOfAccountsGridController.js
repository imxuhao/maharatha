Ext.define('Chaching.view.financials.accounts.ChartOfAccountsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials-accounts-chartofaccountsgrid',
    onChartOfAccountClicked: function (tableView, td, cellIndex, record, tr, rowIndex, e, eOpts) {
        var me = this,
            view = me.getView(),
            tabPanel = view.up('tabpanel');
        if (e && e.target && tabPanel) {
            var horizontalTabPanel = tabPanel.up('tabpanel');
            var target = e.target,
                nodeName = target.nodeName;
            if (nodeName === "A" && horizontalTabPanel) {
                var accountsGrid = Ext.create({
                    xtype: 'financials.accounts.accounts',
                    hideMode: 'offsets',
                    closable: true,
                    title: abp.localization.localize("FinancialAccounts"),
                    routId: 'financials.accounts.accounts',
                    parentAccountId: record.get('coaId')
                    //iconCls: titleConfig.iconCls,
                    //titleConfig: titleConfig,
                    //isEdit: isEdit
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
    }
    
});
