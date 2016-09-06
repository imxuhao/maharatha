
/**
 * The class is created to provide main UI to access Credit card Company Form .
 * Author: kamal
 * Date: 30/08/2016
 */
/**
 * @class Chaching.view.creditcard.entry.CreditCardCompanyForm
 * UI design for Credit Card->CreditCardCompanyForm.
 * @alias widget.creditcard.entry.ccdcompanies.create
 */
Ext.define('Chaching.view.creditcard.entry.CreditCardCompanyForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.creditcard.entry.ccdcompanies.create', 'widget.creditcard.entry.ccdcompanies.edit'],
    requires: [
        'Chaching.view.creditcard.entry.CreditCardCompanyFormController'
    ],

    controller: 'creditcard-entry-creditcardcompanyform',
    name: 'CreditCard.Entry.CreditCardCompanies.Create',
    modulePermissions: {
        read: abp.auth.isGranted('Pages.CreditCard.Entry.CreditCardCompanies'),
        create: abp.auth.isGranted('Pages.CreditCard.Entry.CreditCardCompanies.Create'),
        edit: abp.auth.isGranted('Pages.CreditCard.Entry.CreditCardCompanies.Edit'),
        destroy: abp.auth.isGranted('Pages.CreditCard.Entry.CreditCardCompanies.Delete')
    },
    openInPopupWindow: false,
    hideDefaultButtons: false,
    layout: 'column',
    autoScroll: true,
    border: false,
    showFormTitle: true,
    defaultFocus : 'textfield#creditCardCompany',
    items: [{
        columnWidth: .5,
        padding: '20 10 0 20',
        defaults: {
            labelWidth : 140,
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [{
            xtype: 'hiddenfield',
            name: 'bankAccountId',
            value: 0
        }, {
            xtype: 'textfield',
            name: 'description',
            itemId: 'creditCardCompany',
            allowBlank: false,
            tabIndex: 1,
            fieldLabel: app.localize('CreditCardCompany'),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'textfield',
            name: 'bankAccountName',
            itemId: 'accountName',
            tabIndex: 2,
            allowBlank: false,
            fieldLabel: app.localize('AccountName'),
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        }, {
            xtype: 'combobox',
            name: 'typeOfBankAccountId',
            fieldLabel: app.localize('CreditCardType'),
            width: '100%',
            ui: 'fieldLabelTop',
            tabIndex: 3,
            emptyText: app.localize('CreditCardType'),
            displayField: 'typeOfBankAccount',
            valueField: 'typeOfBankAccountId',
            queryMode: 'local',
            store : Ext.create('Chaching.store.utilities.CardTypeListStore')
        },{
            xtype: 'numberfield',
            name: 'bankAccountNumber',
            itemId: 'accountNumber',
            tabIndex: 4,
            allowBlank: false,
            fieldLabel: app.localize('CreditCardAccountNumber'),
            minValue: 0, //prevents negative numbers
            // Remove spinner buttons, and arrow key and mouse wheel listeners
            hideTrigger: true,
            keyNavEnabled: false,
            mouseWheelEnabled: false,
            width: '100%',
            ui: 'fieldLabelTop',
            emptyText: app.localize('MandatoryField')
        },
        {
            xtype: 'chachingcombobox',
            store: new Chaching.store.utilities.autofill.AccountListStore(),
            fieldLabel: app.localize('ClearingAccount'),
            ui: 'fieldLabelTop',
            tabIndex: 5,
            width: '100%',
            name: 'clearingAccountId',
            valueField: 'accountId',
            displayField: 'accountNumber',
            queryMode: 'remote',
            minChars: 2,
            modulePermissions: {
                read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
                create: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
                edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
                destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete')
            },
            primaryEntityCrudApi: {
                read: abp.appPath + 'api/services/app/list/GetAccountsListByClassification',
                create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
                update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
                destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
            },
            createEditEntityType: 'financials.accounts.accounts',
            createEditEntityGridController: 'financials-accounts-accountsgrid',
            entityType: 'Account'
        }]
    }, {
        columnWidth: .5,
        padding: '20 10 0 20',
        defaults: {
            labelWidth: 140,
            blankText: app.localize('MandatoryToolTipText')
        },
        items: [
            {
                xtype: 'chachingcombobox',
                name: 'jobId',
                ui: 'fieldLabelTop',
                width: '100%',
                store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                valueField: 'jobId',
                displayField: 'jobNumber',
                queryMode: 'remote',
                minChars: 2,
                tabIndex: 6,
                fieldLabel: app.localize('JobDivision'),
                useDisplayFieldToSearch: true,
                modulePermissions: {
                    read: abp.auth.isGranted('Pages.Financials.Accounts.Divisions'),
                    create: false,//abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Create'),
                    edit: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Edit'),
                    destroy: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Delete')
                },
                primaryEntityCrudApi: {
                    read: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
                    create: abp.appPath + 'api/services/app/divisionUnit/CreateDivisionUnit',
                    update: abp.appPath + 'api/services/app/divisionUnit/UpdateDivisionUnit',
                    destroy: abp.appPath + 'api/services/app/divisionUnit/DeleteDivisionUnit'
                },
                createEditEntityType: 'financials.accounts.divisions',
                createEditEntityGridController: 'financials-accounts-divisionsgrid',
                entityType: 'Division',
                isTwoEntityPicker: true,
                secondEntityDetails: {
                    editCreateModelClass: 'Chaching.model.projects.projectmaintenance.ProjectModel',
                    identificationKey: 'isDivision',
                    entityType: 'Job',
                    createEditEntityType: 'projects.projectmaintenance.projects',
                    createEditEntityGridController: 'Chaching.view.projects.projectmaintenance.ProjectsGridController',
                    modulePermissions: {
                        read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
                        create: false,//abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
                        edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
                        destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete')
                    },
                    secondoryEntityCrudApi: {
                        read: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
                        create: abp.appPath + 'api/services/app/jobUnit/CreateJobUnit',
                        update: abp.appPath + 'api/services/app/jobUnit/UpdateJobUnit',
                        destroy: abp.appPath + 'api/services/app/jobUnit/DeleteJobUnit'
                    }
                }
            },
            {
                xtype: 'chachingcombobox',
                store: new Chaching.store.utilities.autofill.VendorsStore(),
                fieldLabel: app.localize('CreditCardVendor'),
                ui: 'fieldLabelTop',
                width: '100%',
                tabIndex: 7,
                name: 'vendorId',
                valueField: 'vendorId',
                displayField: 'vendorName',
                queryMode: 'remote',
                minChars: 2,
                modulePermissions: {
                    read: abp.auth.isGranted('Pages.Payables.Vendors'),
                    create: abp.auth.isGranted('Pages.Payables.Vendors.Create'),
                    edit: abp.auth.isGranted('Pages.Payables.Vendors.Edit'),
                    destroy: abp.auth.isGranted('Pages.Payables.Vendors.Delete')
                },
                primaryEntityCrudApi: {
                    read: abp.appPath + 'api/services/app/list/GetVendorsListByClassification',
                    create: abp.appPath + 'api/services/app/vendorUnit/CreateVendorUnit',
                    update: abp.appPath + 'api/services/app/vendorUnit/UpdateVendorUnit',
                    destroy: abp.appPath + 'api/services/app/vendorUnit/DeleteVendorUnit'
                },
                createEditEntityType: 'payables.vendors',
                createEditEntityGridController: 'payables-vendors-vendorsgrid',
                entityType: 'Vendor'
            }, {
                xtype: 'combobox',
                name: 'typeOfUploadFileId',
                fieldLabel: app.localize('CreditCardSyncUploadMethod'),
                width: '100%',
                ui: 'fieldLabelTop',
                tabIndex: 8,
                emptyText: app.localize('CreditCardSyncUploadMethod'),
                displayField: 'uploadFileName',
                valueField: 'typeOfUploadFileId',
                queryMode: 'local',
                store: Ext.create('Chaching.store.utilities.UploadMethodListStore')
        }, {
            xtype: 'combobox',
            name: 'batchId',
            fieldLabel: app.localize('Batch'),
            width: '100%',
            labelWidth: 140,
            ui: 'fieldLabelTop',
            tabIndex: 9,
            emptyText: app.localize('Batch'),
            displayField: 'description',
            valueField: 'batchId',
            queryMode: 'local',
            store: Ext.create('Chaching.store.utilities.BatchListStore')
        }, {
            xtype: 'checkbox',
            boxLabel: app.localize('CreditCardClosedAccount'),
            name: 'isClosed',
            labelAlign: 'right',
            tabIndex: 10,
            inputValue: true,
            uncheckedValue : false,
            checked: true,
            boxLabelCls: 'checkboxLabel'
        }]
    }]
});
