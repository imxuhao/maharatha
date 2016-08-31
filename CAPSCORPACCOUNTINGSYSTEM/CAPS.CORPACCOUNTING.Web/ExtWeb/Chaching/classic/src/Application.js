/**
 * The main application class. An instance of this class is created by app.js when it
 * calls Ext.application(). This is the ideal place to handle application launch and
 * initialization details.
 */
Ext.require(
['Ext.*']);
Ext.ariaWarn = Ext.emptyFn;
Ext.define('Chaching.Application', {
    extend: 'Ext.app.Application',
    name: 'Chaching',
    requires: [
        'Chaching.utilities.ChachingGlobals',
        'Chaching.view.common.grid.ChachingGridPanel',
        'Ext.saki.grid.MultiSearch',
        'Ext.ux.grid.MultiSort',
        'Ext.saki.form.field.Icon',
        'Gearbox.*',
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
        'roles.RolesTreeViewStore',
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
        'utilities.CheckStockListStore',
        'utilities.UploadMethodListStore',
        'utilities.PositivePayFileListStore',
        'utilities.TypeOf1099BoxListStore',
        'utilities.TypeOfTaxListStore',
        'utilities.JournalTypeListStore',
        'utilities.EmployeeListStore',
        'utilities.AccountTypeListStore',
        'utilities.autofill.AccountListStore',
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
        'banking.banksetup.BankSetupStore',
        'banking.banksetup.BankCheckNumberStore',
        'utilities.autofill.DivisionListStore',
        'financials.accounts.AccountRestrictionLeftStore',
        'financials.accounts.AccountRestrictionRightStore',
        'payables.invoices.AccountsPayableStore',
        'utilities.autofill.RollupAccountListStore',
        'utilities.autofill.LinesListStore',
        'utilities.autofill.GLAccountListStore',
        'administration.organization.CompanyStore',
        'utilities.autofill.BankAccountListStore',
        'administration.organization.UserOrganizationListStore',
        'administration.organization.CompanyUsersStore',
        'administration.organization.CompanyUserRoleListStore',
        'payables.invoices.AccountsPayableDetailsStore',
        'purchaseorders.entry.PurchaseOrderStore',
        'administration.organization.OrganizationsStore',
        'administration.organization.TenantListStore',
        'purchaseorders.entry.PurchaseOrderDetailsStore',
        'administration.organization.ConnectionStringListStore',
        'purchaseorders.entry.PurchaseOrderDetailHistoryStore',
        'tenants.TenantUserListStore',
        'users.CompanyRoleStore',
        'Chaching.store.ItemsPerPageStore',
        'maintenance.CacheStore',
        'maintenance.WebLogStore',
        'Chaching.store.TimezoneStore',
        'users.PermissionsTreeStore',
        'Chaching.store.ARPaymentTermsListStore',
        'Chaching.store.APPaymentTermsListStore',
        'editions.EditionsTreeStore',
        'users.ProjectSecurityLeftStore',
        'users.ProjectSecurityRightStore',
        'users.BankSecurityRightStore',
        'users.BankSecurityLeftStore',
        'users.CreditCardSecurityRightStore',
        'users.CreditCardSecurityLeftStore',
        'users.AccountSecurityRightStore',
        'users.AccountSecurityLeftStore',
        'Chaching.store.imports.ErrorStore',
        'receivables.invoices.AccountsReceivableStore',
        'imports.AccountsImportStore',
        'imports.LinesImportStore',
        'imports.DivisionsImportStore',
        'imports.ProjectsImportStore',
        'pettycash.entry.PettyCashStore',
        'creditcard.entry.OpenStatementStore',
        'creditcard.entry.StatementDetailStore',
        'creditcard.entry.CreditCardHistoryStore',
        'creditcard.entry.CreditCardCompanyStore',
        'financials.accounts.ClassificationsStore'
    ],
    //mainView: 'Chaching.view.main.ChachingViewport',
    launch: function () {
        
        var me = this;
        var promise = me.loadInitialSetup();
        var runner = new Ext.util.TaskRunner(),
           task = undefined;

        task = runner.newTask({
            run: function () {
                if (promise && promise.owner.completed) {
                    task.stop();

                    var docBody = document.body;
                    if (docBody) {
                        var loadingMask = document.getElementById('intialLoadinMask');
                        if (loadingMask) docBody.removeChild(loadingMask);
                    }
                    //create viewPort once all dependencies are loaded
                    var mainView = Ext.create('Chaching.view.main.ChachingViewport');
                    if (me && window.location.hash && window.location.hash !== "#dashboard" && mainView) {
                        var mainViewController = mainView.getController(),
                            refs = mainViewController.getReferences(),
                            mainCardPanel = refs.mainCardPanel,
                            hashTag = window.location.hash.replace('#', ''),
                            hasComponent = mainCardPanel.child('component[routeId=' + hashTag + ']');
                        if (!hasComponent) {
                            mainViewController.setCurrentView(hashTag);
                            hasComponent = mainCardPanel.child('component[routeId=' + hashTag + ']');
                        }
                        if (hasComponent && typeof (hasComponent.applyGridViewSetting) === 'function') {
                            var cols = hasComponent.getColumns();
                            hasComponent.applyGridViewSetting(cols, true);
                        }
                    }
                }
            },
            interval: 1000
        });

        task.start();
        //load company level settings
        if (abp.session.tenantId != null) {
            Ext.Ajax.request({
                url: abp.appPath + 'api/services/app/tenantSettings/GetAllTenantSettings',
                method: 'POST',
                success: function (response, opts) {
                    var result = Ext.decode(response.responseText);
                    if (result.success) {
                        //update Comnpany settings after override
                        Ext.apply(ChachingGlobals.companySettings, result.result);
                    } else {
                        //function to show error details (Chaching.utilities.ChachingGlobals)
                        ChachingGlobals.showPageSpecificErrors(response);
                    }
                },
                failure: function (response, opts) {
                    //function to show error details (Chaching.utilities.ChachingGlobals)
                    ChachingGlobals.showPageSpecificErrors(response);
                }
            });
        }
        me.loadRecentlyLinkedAccounts();

    },
    loadRecentlyLinkedAccounts:function() {
        Ext.Ajax.request({
            url: abp.appPath + 'api/services/app/userLink/GetRecentlyUsedLinkedUsers',
            method: 'POST',
            success: function (response, opts) {
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    ChachingGlobals.userLinkedAccounts = res.result.items;
                }
            },
            failure: function (response, opts) {
                var res = Ext.decode(response.responseText);
                if (!Ext.isEmpty(res.exceptionMessage)) {
                    abp.message.error(res.exceptionMessage);
                } else {
                    abp.message.error(res.error.message);
                }
                console.log(response);
            }
        });
    },
    loadInitialSetup: function () {
        var me = this;
        var deferred = new Ext.Deferred();
        Ext.Ajax.request({
            method: 'POST',
            headers: {
                'Accept': 'application/json'
            },
            url: abp.appPath + 'api/services/app/session/GetCurrentLoginInformations',

            success: function (response, opts) {
                var obj = Ext.decode(response.responseText),
                    userName = '',
                    gotoMyAccount = false;
                if (obj && obj.success) {
                    var result = obj.result;
                    if (result.tenant) {
                        userName = result.tenant.tenancyName + '\\' + result.user.userName;
                    } else userName = '.\\' + result.user.userName;
                    if (userName && abp.session.impersonatorUserId !== null) {
                        userName = '&#xf112 ' + userName;
                        gotoMyAccount = true;
                    }
                    var loggedInUserInfo = {
                        userName: userName,
                        defaultOrganizationId: result.user.defaultOrganizationId,
                        emailAddress: result.user.emailAddress,
                        userId: result.user.id,
                        name: result.user.name,
                        profilePictureId: result.user.profilePictureId,
                        surname: result.user.surname,
                        userOrganizationId: result.userOrganizationId,
                        gotoMyAccount: gotoMyAccount
                    }

                    Chaching.utilities.ChachingGlobals.loggedInUserInfo = loggedInUserInfo;

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
                        callback: function (records, operation, success) {
                            if (success && records && records.length > 0) {
                                var usersDefaultGridViewSettings = [];
                                var rec;
                                Ext.each(records, function (record) {
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
                                
                            }
                            deferred.resolve('{success:true}');
                        }
                    });
                } else {
                    abp.message.error(obj.error.message);
                    deferred.resolve('{success:true}');
                }
            },

            failure: function (response, opts) {
                var res = Ext.decode(response.responseText);
                Ext.toast(res.exceptionMessage);
                console.log(response);
                deferred.resolve('{success:true}');
            }
        });
        return deferred.promise;
    },
    onAppUpdate: function() {
        //abp.message.confirm(app.localize('AppUpdate'),
        //    app.localize('AppUpdateMessage'),
        //    function(isConfirmed) {
        //        if (isConfirmed) {
        //            window.location.reload();
        //        }
        //    }
        //);
    }
});
var nullHandler = function (val) {
    if (val) return val;
    else return null;
};