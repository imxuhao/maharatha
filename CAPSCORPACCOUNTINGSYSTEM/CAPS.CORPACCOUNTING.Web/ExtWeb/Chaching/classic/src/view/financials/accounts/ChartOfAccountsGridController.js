Ext.define('Chaching.view.financials.accounts.ChartOfAccountsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.financials-accounts-chartofaccountsgrid',
    requires : ['Chaching.view.financials.accounts.AccountsGrid'],
    onChartOfAccountClicked: function (tableView, td, cellIndex, record, tr, rowIndex, e, eOpts) {
        var me = this,
            view = me.getView(),
            tabPanel = view.up('tabpanel'),
            accountsGrid = undefined;

        if (e && e.target && tabPanel) {
            var horizontalTabPanel = tabPanel.up('tabpanel');
            var target = e.target,
                nodeName = target.nodeName;
            if (nodeName === "DIV" && target.attributes.isHyperLink) {
                nodeName = "A";
            }
            if (nodeName === "A" && horizontalTabPanel) {
                accountsGrid = horizontalTabPanel.child('component[routId=financials.accounts.accounts]');
                if (!accountsGrid) {
                    accountsGrid = Ext.create({
                        xtype: 'financials.accounts.accounts',
                        hideMode: 'offsets',
                        closable: true,
                        //requireGrouping: false,
                        title: abp.localization.localize("FinancialAccounts"),
                        routId: 'financials.accounts.accounts',
                        coaId: record.get('coaId'),
                        linkChartOfAccountID: record.get('linkChartOfAccountID'),
                        iconCls: 'fa fa-book'
                    });
                }
                accountsGrid.coaId = record.get('coaId');
                var chartType = record.get('typeOfChartId');
                var visibleColumns = accountsGrid.getColumns();
                if (chartType === 1) {
                    accountsGrid.allowAccountMapping = true;
                    for (var i = 0; i < visibleColumns.length; i++) {
                        var col = visibleColumns[i];
                        if (col.dataIndex === "linkAccount") {
                            col.setVisible(true);
                            break;
                        }
                    }
                } else {
                    accountsGrid.allowAccountMapping = false;
                    for (var i = 0; i < visibleColumns.length; i++) {
                        var col = visibleColumns[i];
                        if (col.dataIndex === "linkAccount") {
                            col.setVisible(false);
                            break;
                        }
                    }
                }
                var toolBar = accountsGrid.child("component[dock=top]");
                
                if (toolBar) {
                    var displayTitle = toolBar.child('component[ui=headerTitle]');
                    if (displayTitle)
                        displayTitle.setValue(record.get('caption'));
                }
                
                var gridStore = accountsGrid.getStore(),
                    storeProxy = gridStore.getProxy();
                storeProxy.setExtraParam('coaId', record.get('coaId'));
                gridStore.load();
                var tabLayout = horizontalTabPanel.getLayout();
                if (tabLayout) {
                    tabLayout.setActiveItem(accountsGrid);
                }
            }
        }
    },
    doAfterCreateAction: function (ccreateMode, formView, isEdit, record) {
        var viewModel = formView.getViewModel();
        var standardGroupTotal = viewModel.getStore('StandardGroupTotalList');
        standardGroupTotal.load();
        var linkChartOfAccount = viewModel.getStore('linkChartOfAccountList');
        if (isEdit && record) {
            linkChartOfAccount.getProxy().setExtraParam('coaId', record.get('coaId'));
        }
        linkChartOfAccount.getProxy().setExtraParam('isProjectCoa', false);
        linkChartOfAccount.load();

        var typeOfChart = viewModel.getStore('TypeOfChartList');
        typeOfChart.load(function(records) {
            Ext.each(records,
                function(rec) {
                    if (rec.get('value') === "5" || rec.get('value') === "3") {
                        typeOfChart.remove(rec);
                    }
                });
        });
    },
    doRowSpecificEditDelete: function (button, grid) {
        var record = button.widgetRec;
        if (record) {
            var typeOfChartId = record.get('typeOfChartId');
            if (button.menu && (typeOfChartId === 1 || typeOfChartId === 4)) {
                    var mapCoa = button.menu.down('menuitem#mapCoa'),
                        seperatorBar = button.menu.down('menuitem#actionMenuSeparator');
                    if ((mapCoa && button.widgetRec)) {
                        mapCoa.hide(); seperatorBar.hide();
                    }
                }
            }
    },
    mapCoaClicked: function (menu, item, e, eOpts, isView) {
        var me = this,
            parentMenu = menu.parentMenu,
            widgetRec = parentMenu.widgetRecord;
        if(widgetRec){
            var CoaCaption = widgetRec.get('caption'),
                coaId = widgetRec.get('coaId');
            var window = Ext.create('Chaching.view.financials.accounts.AccountsMapCoaWindow', {
                title: app.localize('MapCOA') + ' - ' + CoaCaption
            });
            var gridPanel = window.down('gridpanel');
            var gridStore = gridPanel.getStore();
            //gridStore.getProxy().api.read = abp.appPath + 'api/services/app/accountUnit/Servicename';
            gridStore.getProxy().setExtraParam('coaId', coaId);
            gridStore.load();
        }
    }
    
});
