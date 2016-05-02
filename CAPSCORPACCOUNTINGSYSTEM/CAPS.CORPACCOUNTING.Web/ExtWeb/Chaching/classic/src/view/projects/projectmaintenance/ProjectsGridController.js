Ext.define('Chaching.view.projects.projectmaintenance.ProjectsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.projects-projectmaintenance-projectsgrid',
    doAfterCreateAction: function(createMode, formPanel, isEdit,record) {
        var form = undefined;
        var viewModel = undefined;
        if (formPanel) {
            form = formPanel.getForm();
            viewModel = formPanel.getViewModel();
        }

        if (form && viewModel) {
            var budgetFormatFieldStore = form.findField('chartOfAccountId').getStore();
            budgetFormatFieldStore.load();
          
            var rollUpAccountsStore = viewModel.getStore('genericRollupAccountList');
            rollUpAccountsStore.load();

            var rollUpJobStore = form.findField('rollupJobId').getStore();
            rollUpJobStore.load();

            var currencyStore = viewModel.getStore('typeOfCurrencyList');
            currencyStore.load();

            var projectStatusStore = viewModel.getStore('projectStatusList');
            projectStatusStore.load();

            var projectTypeStore = viewModel.getStore('getProjectTypeList');
            projectTypeStore.load();
        }
    },
    
});
