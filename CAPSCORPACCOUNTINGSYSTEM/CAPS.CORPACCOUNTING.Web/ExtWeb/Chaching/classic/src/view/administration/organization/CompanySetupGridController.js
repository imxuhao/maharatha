Ext.define('Chaching.view.administration.organization.CompanySetupGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.administration-organization-companysetupgrid',
    doAfterCreateAction: function (createMode, formPanel, isEdit, record) {
        var form = formPanel.getForm();
        var membersTab = formPanel.down('*[itemId=membersTab]');
        if (isEdit) {
            if (record._address)
            form.findField('addressId').setValue(record._address.get('addressId'));
            //enable tabs
            var companyPreferencesTab = formPanel.down('*[itemId=companyPreferencesTab]');
            if (companyPreferencesTab) {
                companyPreferencesTab.setDisabled(false);
            }
            if (membersTab) {
                membersTab.setDisabled(false);
            }
          
            //default preference settings
            if(abp.setting.values) {
                var isAllowDuplicateAPInvoiceNos = abp.setting.get("Sumit.Org.AllowDuplicateAPinvoiceNos");
                var isAllowDuplicateARInvoiceNos = abp.setting.get("Sumit.Org.AllowDuplicateARinvoiceNos");
                var isAllowAccountnumbersStartingwithZero = abp.setting.get("Sumit.Org.AllowAccountnumbersStartingwithZero");
                var allowTransactionsJobWithGL = abp.setting.get("Sumit.Org.AllowtransactionsactionsJobWithGL");
                var isImportPOlogsfromProducersActualUploads = abp.setting.get("Sumit.Org.ImportPOlogsfromProducersActualuploads");
                var buildAPuponCCstatementPosting = abp.setting.get("Sumit.Org.BuildAPuponCCstatementPosting");
                var buildAPuponPayrollPosting = abp.setting.get("Sumit.Org.BuildAPuponPayrollPosting");
                var setDefaultAPTerms = abp.setting.get("Sumit.Org.SetDefaultAPTerms");
                var setDefaultARTerms = abp.setting.get("Sumit.Org.SetDefaultARTerms");
                var depositGracePeriods = abp.setting.get("Sumit.Org.DepositGracePeriods");
                var paymentsGracePeriods = abp.setting.get("Sumit.Org.PaymentGracePeriods");
                var defaultBank = abp.setting.get("Sumit.Org.DefaultBank");
                var arAgingDate = abp.setting.get("Sumit.Org.ARAgingDate");
                var apAgingDate = abp.setting.get("Sumit.Org.APAgingDate");
                var defaultAPPostingDate = abp.setting.get("Sumit.Org.APpostingDateDefault");
                var pOAutoNumberingforProjects = abp.setting.get("Sumit.Org.POAutoNumberingforProjects");
                var pOAutoNumberingforDivisions = abp.setting.get("Sumit.Org.POAutoNumberingforDivisions");


                if (form.findField('pOAutoNumberingforProjects') && pOAutoNumberingforProjects) {
                    form.findField('pOAutoNumberingforProjects').setValue(pOAutoNumberingforProjects == "False" ? false : true);
                }
                if (form.findField('pOAutoNumberingforDivisions') && pOAutoNumberingforDivisions) {
                    form.findField('pOAutoNumberingforDivisions').setValue(pOAutoNumberingforDivisions == "False" ? false : true);
                }

                if (form.findField('isAllowDuplicateAPInvoiceNos') && isAllowDuplicateAPInvoiceNos) {
                    form.findField('isAllowDuplicateAPInvoiceNos').setValue(isAllowDuplicateAPInvoiceNos == "False" ? false : true);
                }
                if (form.findField('isAllowDuplicateARInvoiceNos') && isAllowDuplicateARInvoiceNos) {
                    form.findField('isAllowDuplicateARInvoiceNos').setValue(isAllowDuplicateARInvoiceNos == "False" ? false : true);
                }
                if (form.findField('isAllowAccountnumbersStartingwithZero') && isAllowAccountnumbersStartingwithZero) {
                    form.findField('isAllowAccountnumbersStartingwithZero').setValue(isAllowAccountnumbersStartingwithZero == "False" ? false : true);
                }
                if (form.findField('isImportPOlogsfromProducersActualUploads') && isImportPOlogsfromProducersActualUploads) {
                    form.findField('isImportPOlogsfromProducersActualUploads').setValue(isImportPOlogsfromProducersActualUploads == "False" ? false : true);
                }
                if (form.findField('buildAPuponCCstatementPosting') && buildAPuponCCstatementPosting) {
                    form.findField('buildAPuponCCstatementPosting').setValue(buildAPuponCCstatementPosting == "False" ? false : true);
                }
                if (form.findField('buildAPuponPayrollPosting') && buildAPuponPayrollPosting) {
                    form.findField('buildAPuponPayrollPosting').setValue(buildAPuponPayrollPosting == "False" ? false : true);
                }
                if (form.findField('setDefaultAPTerms') && setDefaultAPTerms) {
                    form.findField('setDefaultAPTerms').setValue(dSetDefaultAPTerms == null ? '' : SetDefaultAPTerms);
                }
                if (form.findField('setDefaultARTerms') && setDefaultARTerms) {
                    form.findField('setDefaultARTerms').setValue(setDefaultARTerms == null ? '' : setDefaultARTerms);
                }
                if (form.findField('depositGracePeriods') && depositGracePeriods) {
                    form.findField('depositGracePeriods').setValue(depositGracePeriods == null ? '' : depositGracePeriods);
                }
                if (form.findField('paymentsGracePeriods') && paymentsGracePeriods) {
                    form.findField('paymentsGracePeriods').setValue(paymentsGracePeriods == null ? '' : paymentsGracePeriods);
                }
                if (form.findField('defaultBank') && defaultBank) {
                    form.findField('defaultBank').setValue(defaultBank == null ? '' : defaultBank);
                }
                if (form.findField('allowTransactionsJobWithGL') && allowTransactionsJobWithGL) {
                    form.findField('allowTransactionsJobWithGL').setValue(allowTransactionsJobWithGL == "False" ? false : true);
                }

                var arAgingDateRadioGroup = formPanel.down('*[itemId=arAgingDateItemId]');
                if (arAgingDateRadioGroup) {
                    arAgingDateRadioGroup.setValue({arAgingDate: arAgingDate});
                }
                

                var aPAgingDateRadioGroup = formPanel.down('*[itemId=apAgingDateItemId]');
                if (aPAgingDateRadioGroup) {
                    aPAgingDateRadioGroup.setValue({
                        apAgingDate: apAgingDate
                    });
                }
                var defaultAPPostingDateRadioGroup = formPanel.down('*[itemId=defaultAPPostingDateItemId]');
                if (defaultAPPostingDateRadioGroup) {
                    defaultAPPostingDateRadioGroup.setValue({
                        defaultAPPostingDate: defaultAPPostingDate
                    });
                }
                
            }

            //load users of current organization
            var usersGridStore = formPanel.down('gridpanel[itemId=membersTab]').getStore();
            var usersGridProxy = usersGridStore.getProxy();
            usersGridProxy.setExtraParams({'id': Chaching.utilities.ChachingGlobals.loggedInUserInfo.userOrganizationId,
                'sorting': 'name'
            });
            usersGridStore.load();

        } else {

        }
        //load default combo
        var defaultBankCombo = form.findField('defaultBank');
        defaultBankCombo.getStore().load();
    }
});
