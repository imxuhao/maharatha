/**
 * The viewController class for projects/jobs list
 * Author: Krishna Garad
 * Date: 28/04/2016
 */
/**
 * @class Chaching.view.projects.projectmaintenance.ProjectsGridController
 * ViewController class for project/job.
 * @alias controller.projects-projectmaintenance-projectsgrid
 */
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

            var projectStatusStore = form.findField('typeOfJobStatusId').getStore();
            projectStatusStore.load();

            var projectTypeStore = form.findField('typeofProjectId').getStore();
            projectTypeStore.load();

            var taxCreditStore = viewModel.getStore('getTaxCreditList');
            taxCreditStore.load();

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

                var customerStore = form.findField('agencyId').getStore();//viewModel.getStore('getCustomersList');
                customerStore.load();
               
                var directorsStore = form.findField('directorEmployeeId').getStore();
                directorsStore.getProxy().setExtraParam('property', 'isDirector');
                
                directorsStore.load();

                var producersStore = form.findField('executiveProducerId').getStore();//viewModel.getStore('getProducersList');
                producersStore.getProxy().setExtraParam('property', 'isProducer');
                
                producersStore.load();

                var dirOfPhotStore = form.findField('dirOfPhotoEmployeeId').getStore();
                dirOfPhotStore.getProxy().setExtraParam('property', 'isDirPhoto');
                dirOfPhotStore.load();

                var jobAccountGridStore = formPanel.down('gridpanel[itemId=jobAccountsGridPanel]').getStore();
                var jobAccountProxy = jobAccountGridStore.getProxy();
                jobAccountProxy.setExtraParam('chartofAccountId', record.get('chartOfAccountId'));
                jobAccountProxy.setExtraParam('jobId', record.get('jobId'));
                jobAccountGridStore.load();

                var jobLocationsStore = formPanel.down('gridpanel[itemId=jobLocationsGridPanel]').getStore();
                jobLocationsStore.getProxy().setExtraParam('jobId', record.get('jobId'));
                jobLocationsStore.load();

                var poAllocationStore = formPanel.down('gridpanel[itemId=jobPurchaseOrderAllocation]').getStore();
                poAllocationStore.getProxy().setExtraParam('jobId', record.get('jobId'));
                poAllocationStore.load();
            }
        }
    },
    onProjectsCellClick: function(view, td, cellIndex, record, tr, rowIndex, e, eOpts) {
        var me = this,
            grid = me.getView(),
            columns = grid.getColumns(),
            tabPanel = view.up('tabpanel');
        if (e && e.target && tabPanel) {
            var horizontalTabPanel = tabPanel.up('tabpanel');
            var target = e.target,
                nodeName = target.nodeName;
            if (nodeName === "DIV" && target.attributes.isHyperLink) {
                nodeName = "A";
            }
            var column = columns[cellIndex];
            if (nodeName === "A" && horizontalTabPanel&&column) {
                var formView = undefined;
                switch (column.dataIndex) {
                    case "jobNumber":
                        formView = me.createNewRecord(grid.xtype, grid.createNewMode, true, grid.editWndTitleConfig, record);
                        break;
                    case "caption":
                        formView = me.createNewRecord(grid.xtype, grid.createNewMode, true, grid.editWndTitleConfig, record);
                        if (formView) {
                            formView.down('tabpanel').setActiveItem(3);
                        }
                        break;
                    case "detailTransactions":
                        abp.notify.info('Comming sooooooooooon.....', 'Not available');
                        break;
                    case "poLogCount":
                        formView = me.createNewRecord(grid.xtype, grid.createNewMode, true, grid.editWndTitleConfig, record);
                        if (formView) {
                            formView.down('tabpanel').setActiveItem(5);
                        }
                        break;
                    default:
                        break;
                }
                if (formView) {
                    formView.getForm().loadRecord(record);
                }
            }
        }
    },
    doPostSaveOperations: function(records, operation, success) {
        var deferred = new Ext.Deferred();
        if (records) {
            var record = records[0];
            if (record.get('agencyEmail') === "") record.data.agencyEmail = null;
            Ext.Ajax.request({
                url: abp.appPath + 'api/services/app/jobCommercial/UpdateJobDetailUnit',
                jsonData: Ext.encode(record.data),
                success: function(response, opts) {
                    var res = Ext.decode(response.responseText);
                    if (res.success) {
                        deferred.resolve(response.responseText);
                    } else {
                        deferred.reject(response.responseText);
                    }
                },
                failure: function(response, opts) {
                    deferred.reject(response.responseText);
                }
            });
        }
        return deferred.promise;
    }
});
