Ext.define('Chaching.view.users.UsersFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.users-usersform',
    onProjectCoaSelect: function (combo, newValue, oldValue) {
        var me = this;
        if (combo.getValue() != undefined) {
            me.getLinesByProjectCoa(combo.getValue());
        }
        
    },
    getLinesByProjectCoa: function (chartOfAccountId) {
        var me = this,
            view = me.getView(),
        projectCoaDragDropControl = view.down('chachingGridDragDrop[itemId=projectCOASecurityGridItemId]'),
        leftStore = projectCoaDragDropControl.getLeftStore(),
        rightStore = projectCoaDragDropControl.getRightStore();
        leftStore.getProxy().setExtraParams({
            'chartOfAccountId': chartOfAccountId,
            'userId': ChachingGlobals.SelectedUserId,
            'entityClassificationId': ChachingGlobals.entityClassification.Line
        });
        leftStore.load();
        rightStore.getProxy().setExtraParams({
            'chartOfAccountId': chartOfAccountId,
            'userId': ChachingGlobals.SelectedUserId,
            'entityClassificationId': ChachingGlobals.entityClassification.Line
        });
        rightStore.load();
    },
    onCorporateCoaSelect: function (combo, newValue, oldValue) {
        var me = this;
        if (combo.getValue() != undefined) {
            //load corporate chart of accounts security accesss
            me.loadCorporateAccountsByCoaId(combo.getValue());
        }

    },
    loadCorporateAccountsByCoaId: function (chartOfAccountId) {
        var me = this,
         view = me.getView(),
         corporateCoaControl = view.down('chachingGridDragDrop[itemId=corporateCOASecurityGridItemId]'),
       leftStore = corporateCoaControl.getLeftStore(),
       rightStore = corporateCoaControl.getRightStore();
        leftStore.getProxy().setExtraParams({
            'chartOfAccountId': chartOfAccountId,
            'userId': ChachingGlobals.SelectedUserId,
            'entityClassificationId': ChachingGlobals.entityClassification.Account
        });
        leftStore.load();
        rightStore.getProxy().setExtraParams({
            'chartOfAccountId': chartOfAccountId,
            'userId': ChachingGlobals.SelectedUserId,
            'entityClassificationId': ChachingGlobals.entityClassification.Account
        });
        rightStore.load();
    },
    onFormAfterRender:function(){
        //var me = this,
        //    view = me.getView(),
        //    coaCombo = me.lookupReference('coaCombo'),
        //    selectedCoa = 0;
        //coaCombo.getStore().load({
        //    callback: function (records, operation, success) {
        //        if (success) {
        //            coaCombo.setValue(records[0].data.coaId);
        //            selectedCoa = records[0].data.coaId;
        //        }
        //    }
        //});
        ///// fill grid
        //var dragDropControl = view.down('chachingGridDragDrop'),
        //    leftStore = dragDropControl.getLeftStore(),
        //    rightStore = dragDropControl.getRightStore(),
        //    values = view.getForm().getValues();
        //    leftStore.getProxy().setExtraParam('chartOfAccountId', selectedCoa);
        //    leftStore.getProxy().setExtraParam('userId', ChachingGlobals.SelectedUserId);
        //    leftStore.getProxy().setExtraParam('entityClassificationId', ChachingGlobals.CorporateCoa);
        //    leftStore.load();
        //    rightStore.getProxy().setExtraParam('chartOfAccountId', selectedCoa);
        //    rightStore.getProxy().setExtraParam('userId', ChachingGlobals.SelectedUserId);
        //    rightStore.getProxy().setExtraParam('entityClassificationId', ChachingGlobals.CorporateCoa);
        //    rightStore.load();
        //    //dragDropControl.show();
    },
    reloadPermissionsTree: function (btn, event, e) {
        var view = btn.currentView,
            currentController = view.getController(),
            record = btn.widgetRec;
        var treePanel = view.down('treepanel[itemId=permissionsListItemId]');
        var rolesMessageLabel = view.down('label[itemId=rolesMessageItemId]');
        currentController.populateTreePanel(treePanel, rolesMessageLabel, record);
    },
    reloadPermissionsTreeLinkCompany: function (btn, event, e) {
        var me = this,
            view = btn.currentView,
            record = btn.widgetRec;
        var treePanel = view.down('treepanel[itemId=permissionsCompanyListItemId]');
        var linkCompanyMessageLabel = view.down('label[itemId=linkCompanyMessageItemId]');
        me.populateTreePanel(treePanel, linkCompanyMessageLabel, record);
    },
    populateTreePanel: function (treePanel, messageLabel, record) {
        if (treePanel) {
            if (messageLabel)
                messageLabel.setHidden(true);
            treePanel.setHidden(false);
            var treeStore = treePanel.getStore();
            var proxy = treeStore.getProxy();
            proxy.api.read = abp.appPath + 'api/services/app/user/GetPermissionsForSelectedRole';
            if (treePanel.itemId === 'permissionsListItemId') {
                treeStore.getProxy().setExtraParam('tenantId', abp.session.tenantId);
                treeStore.getProxy().setExtraParam('roleId', record.get('id'));
            }
            else {
                treeStore.getProxy().setExtraParam('tenantId', record.get('tenantId'));
                treeStore.getProxy().setExtraParam('roleId', record.get('roleId'));
            }
            treeStore.reload();
        }
    },
    onUserFormResize: function (form, newWidth, newHeight, oldWidth, oldHeight) {
        var me = this,
            view = me.getView(),
            treePanel = view.down('treepanel[itemId=permissionsListItemId]'),
            rolesListGrid = view.down('grid[itemId=rolesListGridItemId]'),
            companyListGrid = view.down('grid[itemId=companyListGridItemId]'),
            treePanelLinkCompany = view.down('treepanel[itemId=permissionsCompanyListItemId]'),
            corporateCOASecurityGrid = view.down('chachingGridDragDrop[itemId=corporateCOASecurityGridItemId]'),
            projectCoaDragDropControl = view.down('chachingGridDragDrop[itemId=projectCOASecurityGridItemId]'),
            projectDragDropControl = view.down('chachingGridDragDrop[itemId=projectSecurityGridItemId]'),
            divisionDragDropControl = view.down('chachingGridDragDrop[itemId=divisionSecurityGridItemId]'),
            creditCardDragDropControl = view.down('chachingGridDragDrop[itemId=creditCardSecurityGridItemId]'),
            bankDragDropControl = view.down('chachingGridDragDrop[itemId=bankSecurityGridItemId]');
                
        if (treePanel) {
            treePanel.setHeight(newHeight - 100);
        }
        if (rolesListGrid) {
            rolesListGrid.setHeight(newHeight - 100);
        }
        if (companyListGrid) {
            companyListGrid.setHeight(newHeight - 100);
        }
        if (treePanelLinkCompany) {
            treePanelLinkCompany.setHeight(newHeight - 100);
        }
        if (corporateCOASecurityGrid) {
            corporateCOASecurityGrid.setHeight(newHeight - 130);
        }
        if (projectCoaDragDropControl) {
            projectCoaDragDropControl.setHeight(newHeight - 130);
        }
        if (projectDragDropControl) {
            projectDragDropControl.setHeight(newHeight - 130);
        }
        if (creditCardDragDropControl) {
            creditCardDragDropControl.setHeight(newHeight - 130);
        }
        if (bankDragDropControl) {
            bankDragDropControl.setHeight(newHeight - 130);
        }
        if (divisionDragDropControl) {
            divisionDragDropControl.setHeight(newHeight - 130);
        }
        
    },
    loadCompanyRoles: function (view, record, item, index, e, eOpts) {
        var me = this,
            view = me.getView();
        var rolesGrid = view.down('gridpanel[itemId=companyRolesListGridItemId]');
        var rolesStore = rolesGrid.getStore();
        var proxy = rolesStore.getProxy();
        proxy.api.read = abp.appPath + 'api/services/app/user/GetRolesByTenant',
        rolesStore.removeAll();
        rolesStore.getProxy().setExtraParams({ id: record.get('tenantId') });
        rolesStore.load();
    },
    doPreSaveOperation: function (record, values, idPropertyField) {
        var me = this,
             view = me.getView();
        if (ChachingGlobals.SelectedUserId > 0) {
            // Saving User Security Settings
            var corporateDragDropControl = view.down('chachingGridDragDrop[itemId=corporateCOASecurityGridItemId]'),
                    projectCoaDragDropControl = view.down('chachingGridDragDrop[itemId=projectCOASecurityGridItemId]'),
                    projectDragDropControl = view.down('chachingGridDragDrop[itemId=projectSecurityGridItemId]'),
                    corporateLeftStore = corporateDragDropControl.getLeftStore(),
                    corporateRightStore = corporateDragDropControl.getRightStore();
            var request = {
                userIdList: [], accountAccessList: [], bankAccountAccessList: [], creditCardAccessList: [], projectAcessList: []
            }
            request.userIdList.push(ChachingGlobals.SelectedUserId);
            if (corporateRightStore.getUpdatedRecords().length > 0) {
                var result = corporateRightStore.getUpdatedRecords();
                Ext.each(result, function (records) {
                    request.accountAccessList.push({ accountId: records.get('accountId'), accountNumber: records.get('accountNumber'), userId: ChachingGlobals.SelectedUserId });
                })
                me.callAjaxService('api/services/app/userSecuritySettings/CreateorUpdateAccountAccessList', request);
            }
            
            // END User Security Settings
        }
        // Saving User create/edit information
        record.data.user = values;
        Ext.apply(record.data, values);
        //get roles information
        var rolesListRecords = view.down('gridpanel[itemId=rolesListGridItemId]').getSelection();
        //get company information
        var companyListRecords = view.down('gridpanel[itemId=companyListGridItemId]').getSelection();
        var rolesListArray = [];
        Ext.each(rolesListRecords, function (rec) {
            //rolesListArray.push(rec.get('displayName'));
            rolesListArray.push(rec.get('name'));
        });
        record.data.assignedRoleNames = rolesListArray;
       
        var tenantListArray=[],
            CompanyRolesArray = [],
            gridPanel = view.down('gridpanel[itemId=companyListGridItemId]'),
            gridStore = gridPanel.getStore(),
            data = gridStore.data,
            companyListGrid = view.down('gridpanel[itemId=companyListGridItemId]'),
            companyListGridStore = companyListGrid.getStore(),
            groupedCompanyList = companyListGridStore.getGroups();
        Ext.each(data.items, function (rec) {
            var tenantId = rec.get('tenantId');
            if (me.isTenantIdExists(companyListRecords, rec)) {
                CompanyRolesArray = me.getCompanyRoles(companyListRecords);
            }

            if (!me.isTenantIdExists(companyListRecords, rec)) {
                tenantListArray.push({ tenantId: rec.get('tenantId'), isEmptyRoles: true, roleIds: [], roleNames: [] });
            }

        });
            
        record.data.tenantList = (tenantListArray.length > 0 ? CompanyRolesArray.concat(tenantListArray) : CompanyRolesArray);

        return record;
    },

    isTenantIdExists: function (arr, record) {
        var i = arr.length;
        while (i--) {
            if (arr[i].data.tenantId === record.get('tenantId')) {
                return true;
            }
        }
        return false;
    },
    showRandomPassword: function () {
        var me = this;
        password = me.lookupReference('password');
        passwordRepeat = me.lookupReference('passwordRepeat');
        password.reset();
        passwordRepeat.reset();
    },
    getCompanyRoles: function(companyListRecords){
        var tenantListArray = [],
            tempList = [],
            roleId = [],
            roleName = [];

            var isNewItem = false;
            Ext.each(companyListRecords, function (rec) {
                roleId.push(rec.get('roleId'));
                roleName.push(rec.get('roleName'));
                tempList.push({ tenantId: rec.get('tenantId'), tenantName: rec.get('tenantName'), roleIds: roleId, roleNames: roleName });
                if (tenantListArray.length > 0) {
                    for (var i = 0; i < tenantListArray.length; i++) {
                        if (tempList[0].tenantId == tenantListArray[i].tenantId) {
                            tenantListArray[i].roleIds.push(tempList[0].roleIds[0]);
                            tenantListArray[i].roleNames.push(tempList[0].roleNames[0]);
                            isNewItem = false;
                            break;
                        }
                        else {
                            isNewItem = true;
                        }
                    }
                }
                else {
                    tenantListArray = tempList;
                }
                if (isNewItem) {
                    tenantListArray.push({ tenantId: tempList[0].tenantId, tenantName: tempList[0].tenantName, roleIds: tempList[0].roleIds, roleNames: tempList[0].roleNames });
                }
                tempList = []; roleId = []; roleName = [];
            });
            return tenantListArray;

    },
    callAjaxService: function (url, request) {
        Ext.Ajax.request({
            url: abp.appPath + url,
            jsonData: Ext.encode(request),
            success: function (response) { },
            failure: function (response, a, b) { }
        });
    }

});
