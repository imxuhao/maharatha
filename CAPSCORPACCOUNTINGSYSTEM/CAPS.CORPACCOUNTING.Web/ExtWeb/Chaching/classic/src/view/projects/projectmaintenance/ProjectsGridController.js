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

            if (isEdit) {
                //enable all tabs
                var projectDetails = formPanel.down('*[itemId=ProjectDetailsTab]');
                if (projectDetails) {
                    projectDetails.setDisabled(false);
                }
                var cmTab = formPanel.down('*[itemId=CostManagerTab]');
                if (cmTab) cmTab.setDisabled(false);
                var pcAccountTab = formPanel.down('*[itemId=PCAccountTab]');
                if (pcAccountTab) pcAccountTab.setDisabled(false);
                var poLogTab = formPanel.down('*[itemId=POLogTab]');
                if (poLogTab) poLogTab.setDisabled(false);
                var lineNumberTab = formPanel.down('*[itemId=LineNumbersTab]');
                if (lineNumberTab) lineNumberTab.setDisabled(false);

                var customerStore = viewModel.getStore('getCustomersList');
                customerStore.load();

                var directorsStore = viewModel.getStore('getDirectorsList');
                directorsStore.load();

                var producersStore = viewModel.getStore('getProducersList');
                producersStore.load();

                var dirOfPhotStore = viewModel.getStore('getDirofPhotoList');
                dirOfPhotStore.load();
            }
        }
    },
    
});
