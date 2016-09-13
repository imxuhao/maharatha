/**
 * The ViewController class Project/Job form
 * Author: Krishna Garad
 * Date: 29/04/2016
 */
/**
 * @class Chaching.view.projects.projectmaintenance.ProjectsFormController
 * ViewController class for project/job
 * @alias controller.projects-projectmaintenance-projectsform
 */
Ext.define('Chaching.view.projects.projectmaintenance.ProjectsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.projects-projectmaintenance-projectsform',
    onAgencyChange:function(combo, newValue, oldValue, eOpts) {
        var me = this,
            view = me.getView();
        var comboStore = combo.getStore();
        if (comboStore) {
            var record = comboStore.findRecord('customerId', newValue);
            if (record && record._addresses) {
                var addresses = record.getAddresses();
                if (addresses) {
                    var form = view.getForm(),
                        agencyEmailField = form.findField('agencyEmail'),
                        agencyEmailDisplay = form.findField('agencyEmailDisplay'),
                        agencyAddressField = form.findField('agencyAddress'),
                        agencyPhoneField = form.findField('agencyPhone');
                    agencyEmailField.setValue(addresses.get('email'));
                    agencyEmailDisplay.setValue(Chaching.utilities.ChachingRenderers.renderMailToTag(addresses.get('email')));
                    agencyAddressField.setValue(me.getAgencyAddress(addresses));
                    agencyPhoneField.setValue(addresses.get('phone1Extension') + addresses.get('phone1'));

                }
            }
        }
    },
    getAgencyAddress:function(addresses) {
        var fullAddress = '';
        if (addresses.get('line1')) {
            fullAddress = addresses.get('line1') + ' ';
        }
        if (addresses.get('line2')) fullAddress += addresses.get('line2') + ' ';
        if (addresses.get('line3')) fullAddress += addresses.get('line3') + ' ';
        if (addresses.get('line4')) fullAddress += addresses.get('line4') + ' ';
        if (addresses.get('city')) fullAddress += ','+addresses.get('city') + ' ';
        if (addresses.get('state')) fullAddress +=','+ addresses.get('state') + ' ';
        if (addresses.get('postalCode')) fullAddress += ','+addresses.get('postalCode');
        return fullAddress;
    },
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
            view = me.getView(),
            lineNumbersGrid = view.down('gridpanel[itemId=jobAccountsGridPanel]'),
            lineNumberStore = lineNumbersGrid.getStore();

        var modifiedRecord = lineNumberStore.getModifiedRecords();
        var modifiedJobAccounts = [];
        if (modifiedRecord&&modifiedRecord.length>0) {
            Ext.each(modifiedRecord, function(rec) {
                var record = {
                    description: rec.get('description'),
                    accountId: rec.get('accountId'),
                    jobAccountId: rec.get('jobAccountId'),
                    jobId: rec.get('jobId'),
                    rollupAccountId: rec.get('rollupAccountId'),
                    rollupJobId: rec.get('rollupJobId'),
                    rollupAccountDescription: rec.get('rollupAccountDescription'),
                    rollupJobDescription:rec.get('rollupJobDescription')
                };
                modifiedJobAccounts.push(record);
            });
            record.set('JobAccountList', modifiedJobAccounts);
        }
        return record;
    },
    onProjectDetailsSave:function() {
        var me = this,
            view = me.getView(),
            parentGrid = view.parentGrid,
            values = view.getValues();
        var jobLocations = [],
            poRanges = [];
        
        if (parentGrid) {
            var gridStore = parentGrid.getStore(),
               idPropertyField = gridStore.idPropertyField,
               operation;
            var target;
            if (view.openInPopupWindow) {
                target = view.up('window');
            } else {
                target = view;
            }
            var myMask = new Ext.LoadMask({
                msg: 'Please wait...',
                target: target
            });

            
            myMask.show();
            if (values.agencyEmail === "")values.agencyEmail = null;
            //get updated joblocation
            var jobLocationGridStore = view.down('gridpanel[itemId=jobLocationsGridPanel]').getStore();
            var modifiedRecords = jobLocationGridStore.getModifiedRecords();
            if (modifiedRecords && modifiedRecords.length>0) {
                Ext.each(modifiedRecords, function(rec) {
                    var record = {
                        jobLocationId: rec.get('jobLocationId'),
                        jobId: values.jobId,
                        locationId: rec.get('locationId'),
                        locationSiteDate: rec.get('locationSiteDate'),
                        locationName: rec.get('locationName')
                    };
                    jobLocations.push(record);
                });
                values.jobLocations = jobLocations;
            }

            var poAllocationStore = view.down('gridpanel[itemId=jobPurchaseOrderAllocation]').getStore();
            var modifiedPoRangeRecords = poAllocationStore.getModifiedRecords();
            if (modifiedPoRangeRecords && modifiedPoRangeRecords.length>0) {
                Ext.each(modifiedPoRangeRecords, function (rec) {
                    var record = {
                        poRangeId: rec.get('poRangeId'),
                        jobId: values.jobId,
                        poRangeStartNumber: rec.get('poRangeStartNumber'),
                        poRangeEndNumber: rec.get('poRangeEndNumber'),
                        organizationUnitId: rec.get('organizationUnitId')
                    };
                    poRanges.push(record);
                });
                values.poAllocations = poRanges;
            }
            //fire save request
            Ext.Ajax.request({
                url: abp.appPath + 'api/services/app/jobCommercial/UpdateJobDetailUnit',
                jsonData: Ext.encode(values),
                success: function (response, opts) {
                    myMask.hide();
                    var res = Ext.decode(response.responseText);
                    if (res.success) {
                        var gridController = parentGrid.getController();
                        gridController.doReloadGrid();

                        if (view && view.openInPopupWindow) {
                            var wnd = view.up('window');
                            Ext.destroy(wnd);
                        } else if (view) {
                            Ext.destroy(view);
                        }
                        abp.notify.success('Operation completed successfully.', 'Success');
                    } else {
                        var message = '',
                            title = 'Error';
                        if (res && res.error) {
                            if (res.error.message && res.error.details) {
                                title = res.error.message;
                                message = res.error.details;
                                abp.message.warn(message, title);
                                return;
                            }
                            title = res.error.message;
                            message = res.error.details ? res.error.details : title;
                        }
                        abp.message.error(message, title);
                    }
                },
                failure: function (response, opts) {
                    myMask.hide();
                    var res = Ext.decode(response.responseText);
                    Ext.toast(res.exceptionMessage);
                    console.log(response);
                }
            });
        }
        
    },
    doModuleSpecificViewMode:function(formPanel) {
        var view = formPanel,
            controller = view.getController();
        var defaultActionButtons = view.query('button[actionButton=true]');
        if (defaultActionButtons && defaultActionButtons.length > 0) {
            Ext.each(defaultActionButtons, function(button) {
                if (button.name !== 'Cancel' && button.name !== "Edit" && typeof (button.hide) === "function") {
                    button.hide();
                }
                if (button.name === "Edit") button.show();
            });
        }
    },
    doModuleSpecificEditAction:function(view) {
        var actionButtons = view.query('button[actionButton=true]');
        Ext.each(actionButtons, function (button) {
            if (button.name !== 'Cancel' && button.name !== "Edit" && typeof (button.hide) === "function") {
                button.show();
            }
            if (button.name === "Edit") button.hide();
        });
    }
});
