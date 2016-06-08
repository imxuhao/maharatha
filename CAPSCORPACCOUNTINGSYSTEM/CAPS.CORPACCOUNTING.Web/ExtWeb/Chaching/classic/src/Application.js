/**
 * The main application class. An instance of this class is created by app.js when it
 * calls Ext.application(). This is the ideal place to handle application launch and
 * initialization details.
 */
Ext.require(
['Ext.*']);
Ext.define('Chaching.Application', {
    extend: 'Ext.app.Application',

    name: 'Chaching',
    requires: [
        'Chaching.view.common.grid.ChachingGridPanel',
        'Ext.saki.grid.MultiSearch',
        'Ext.ux.grid.MultiSort',
        'Ext.saki.form.field.Icon',
        'Chaching.*'
    ],
    stores: [
        // TODO: add global / shared stores here
        'NavigationTree',
        'tenants.TenantsStore',
        'users.UsersStore',
        'roles.RolesStore',
        'editions.EditionsStore',
        'languages.LanguagesStore',
        'auditlogs.AuditLogsStore',
        'languages.LanguageTextsStore',
        'profile.linkedaccounts.LinkedAccountsStore',
        'financials.accounts.ChartOfAccountStore',
        'profile.loginAttempts.LoginAttemptStore',
        'manageView.ManageViewStore',
        'financials.accounts.SubAccountsStore',
        'financials.accounts.DivisionsStore',
        'manageView.ManageViewStore',
        'roles.RolesTreeStore',
        'financials.accounts.AccountsStore',
        'languages.LanguagesDataStore',
        'projects.projectmaintenance.LinesStore',
        'financials.accounts.RollupAccountStore',
        'payables.vendors.VendorsStore',
        'languages.LanguagesDataStore',
        'projects.projectmaintenance.ProjectsStore',
        'projects.projectmaintenance.ProjectCoaStore',
        'address.AddressStore',
        'projects.projectmaintenance.ProjectCoaStore',
        'projects.projectmaintenance.JobAccountsStore',
        'projects.projectmaintenance.ProjectLocationsStore',
        'utilities.LocationListStore',
        'projects.projectmaintenance.PoRangeAllocationStore',
        'employee.EmployeeStore',
        'customers.CustomersStore',
        'financials.journals.JournalStore',
        'utilities.ProjectTypeStore',
        'utilities.ProjectStatusStore',
        'utilities.CountryListStore',
        'utilities.StateOrRegionListStore',
        'utilities.TypeOfAddressListStore',
        'utilities.PaymentTermsListStore',
        'utilities.TypeOf1099BoxListStore',
        'utilities.TypeOfTaxListStore',
        'utilities.JournalTypeListStore',
        'utilities.EmployeeListStore',
        'utilities.autofill.CustomerListStore',
        'financials.journals.JournalDetailsStore',
        'batchposting.batches.BatchesStore',
        'utilities.autofill.JobDivisionStore',
        'utilities.autofill.AccountsStore',
        'utilities.autofill.SubAccountsStore',
        'utilities.autofill.TaxRebateStore',
        'utilities.autofill.VendorsStore',
        'utilities.autofill.T41099Store',
        'financials.fiscalperiod.FiscalPeriodStore',
        'banking.banksetup.BankSetupStore'
    ],
    mainView: 'Chaching.view.main.ChachingViewport',
    launch: function() {
        var me = this;
        ///****Load usersDefaultView Settings
        var defaultViewSettingStore = Ext.create('Chaching.store.manageView.ManageViewStore');
        var filters = [];
        var filter = new Ext.util.Filter({
            entity: '',
            searchTerm: true,
            comparator: 1,
            dataType: 3,
            property: 'isDefault',
            value: true
        });
        filters.push(filter);
        filter = new Ext.util.Filter({
            entity: '',
            searchTerm: Chaching.utilities.ChachingGlobals.loggedInUserInfo.userId,
            comparator: 2,
            dataType: 0,
            property: 'userId',
            value: Chaching.utilities.ChachingGlobals.loggedInUserInfo.userId
        });
        filters.push(filter);
        defaultViewSettingStore.filter(filters);
        defaultViewSettingStore.load({
            callback: function(records, operation, success) {
                if (success && records && records.length > 0) {
                    var usersDefaultGridViewSettings = [];
                    var rec;
                    Ext.each(records, function(record) {
                        if (record.get('isDefault')) {
                            rec = {
                                gridId: record.get('viewId'),
                                userViewId: record.get('userViewId'),
                                viewSettingName: record.get('viewName'),
                                viewSettings: record.get('viewSettings'),
                                isDefault: record.get('isDefault')
                            }
                            usersDefaultGridViewSettings.push(rec);
                        }
                    });
                    Chaching.utilities.ChachingGlobals.usersDefaultGridViewSettings = usersDefaultGridViewSettings;

                    if (me && window.location.hash && window.location.hash !== "#dashboard") {
                        var mainView = me.getMainView();
                        if (mainView) {
                            var mainViewController = mainView.getController(),
                                refs = mainViewController.getReferences(),
                                mainCardPanel = refs.mainCardPanel,
                                hasComponent = mainCardPanel.child('component[routeId=' + window.location.hash.replace('#', '') + ']');
                            if (hasComponent && typeof (hasComponent.applyGridViewSetting) === 'function') {
                                var cols = hasComponent.getColumns();
                                hasComponent.applyGridViewSetting(cols, true);
                            }
                        }
                    }
                }
            }
        });
    },
    onAppUpdate: function() {
        Ext.Msg.confirm('Application Update', 'This application has an update, reload?',
            function(choice) {
                if (choice === 'yes') {
                    window.location.reload();
                }
            }
        );
    }
});
var nullHandler = function (val) {
    if (val) return val;
    else return null;
};