Ext.define('Chaching.view.users.UsersFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.users-usersform',
    //reloadPermissionsTree: function (grid , record , tr , rowIndex , e , eOpts )  {
    //    debugger;
    //    var me = this,
    //        view = me.getView();
    //    var treePanel = view.down('treepanel[itemId=permissionsListItemId]');
    //    var rolesMessageLabel = view.down('label[itemId=RolesMessageItemId]')
    //    if (treePanel) {
    //        if (rolesMessageLabel)
    //            rolesMessageLabel.setHidden(true);
    //        treePanel.setHidden(false);
    //        var treeStore = treePanel.getStore();
    //        var proxy = treeStore.getProxy();
    //        proxy.api.read = abp.appPath + 'api/services/app/user/GetPermissionsForSelectedRole';
    //        treeStore.getProxy().setExtraParam('tenantId', abp.session.tenantId);
    //        treeStore.getProxy().setExtraParam('roleId', record.get('id'));
    //        treeStore.reload();
    //    }
    //},
    reloadPermissionsTree: function (btn, event, e) {
        var view = btn.currentView,
            record = btn.widgetRec;
            var treePanel = view.down('treepanel[itemId=permissionsListItemId]');
            var rolesMessageLabel = view.down('label[itemId=RolesMessageItemId]')
            if (treePanel) {
                if (rolesMessageLabel)
                    rolesMessageLabel.setHidden(true);
                treePanel.setHidden(false);
                var treeStore = treePanel.getStore();
                var proxy = treeStore.getProxy();
                proxy.api.read = abp.appPath + 'api/services/app/user/GetPermissionsForSelectedRole';
                treeStore.getProxy().setExtraParam('tenantId', abp.session.tenantId);
                treeStore.getProxy().setExtraParam('roleId', record.get('id'));
                treeStore.reload();
            }

    },
    reloadPermissionsTreeLinkCompany: function (btn, event, e) {
        var view = btn.currentView,
            record = btn.widgetRec;
        var treePanel = view.down('treepanel[itemId=permissionsCompanyListItemId]');
        var linkCompanyMessageLabel = view.down('label[itemId=LinkCompanyMessageItemId]');
        if (treePanel) {
            if (linkCompanyMessageLabel)
                linkCompanyMessageLabel.setHidden(true);
            treePanel.setHidden(false);
            var treeStore = treePanel.getStore();
            var proxy = treeStore.getProxy();
            proxy.api.read = abp.appPath + 'api/services/app/user/GetPermissionsForSelectedRole';
            treeStore.getProxy().setExtraParam('tenantId', record.get('tenantId'));
            treeStore.getProxy().setExtraParam('roleId', record.get('roleId'));
            treeStore.reload();
        }
    },
    onUserFormResize: function (form, newWidth, newHeight, oldWidth, oldHeight) {
        var me = this,
           view = me.getView();
        var treePanel = view.down('treepanel[itemId=permissionsListItemId]');
        var rolesListGrid = view.down('grid[itemId=rolesListGridItemId]');
        var companyListGrid = view.down('grid[itemId=companyListGridItemId]');
        var treePanelLinkCompany = view.down('treepanel[itemId=permissionsCompanyListItemId]');
        
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
       
        var tenantListArray=[];
        //if (companyListRecords && companyListRecords.length > 0) {
        //    var tempList = [],
        //        roleId = [],
        //        roleName = [];
        //        //tenantListArray=[];
        //    var isNewItem = false;
        //    Ext.each(companyListRecords, function (rec) {
        //        roleId.push(rec.get('roleId'));
        //        roleName.push(rec.get('roleName'));
        //        tempList.push({ tenantId: rec.get('tenantId'), tenantName: rec.get('tenantName'), roleIds: roleId, roleNames: roleName });
        //        if (tenantListArray.length > 0) {
        //            for (var i = 0; i < tenantListArray.length; i++) {
        //                if (tempList[0].tenantId == tenantListArray[i].tenantId) {
        //                    tenantListArray[i].roleIds.push(tempList[0].roleIds[0]);
        //                    tenantListArray[i].roleNames.push(tempList[0].roleNames[0]);
        //                    isNewItem = false;
        //                    break;
        //                }
        //                else {
        //                    isNewItem = true;
        //                }
        //            }
        //        }
        //        else {
        //            tenantListArray = tempList;
        //        }
        //        if (isNewItem) {
        //            tenantListArray.push({ tenantId: tempList[0].tenantId, tenantName: tempList[0].tenantName, roleIds: tempList[0].roleIds, roleNames: tempList[0].roleNames });
        //        }
        //        tempList = []; roleId = []; roleName = [];
        //    });
        //}
        //// Add tenantId when no selection happen
        //if (tenantListArray.length <= 0) {
        //    record.data.isEmptyRoles = true;
        //    var gridPanel = view.down('gridpanel[itemId=companyListGridItemId]');
        //    var gridStore = gridPanel.getStore();
        //    var data = gridStore.data;
        //    Ext.each(data.items, function (rec) {
        //        if (!me.checkObjectExistsOrNot(tenantListArray, rec)) {
        //            tenantListArray.push({ tenantId: rec.get('tenantId'), isEmptyRoles: true });
        //        }
        //    });
        //}

       // if (tenantListArray.length <= 0) {
            //record.data.isEmptyRoles = true;
        var CompanyRolesArray = [];    
        var gridPanel = view.down('gridpanel[itemId=companyListGridItemId]');
            var gridStore = gridPanel.getStore();
            var data = gridStore.data;
            //Ext.each(data.items, function (rec) {
            //    if (!me.checkObjectExistsOrNot(tenantListArray, rec)) {
            //        tenantListArray.push({ tenantId: rec.get('tenantId'), isEmptyRoles: true });
            //    }

            //});
        // }

            var companyListGrid = view.down('gridpanel[itemId=companyListGridItemId]');
            var companyListGridStore = companyListGrid.getStore();
            var groupedCompanyList = companyListGridStore.getGroups();
            Ext.each(data.items, function (rec) {
                var tenantId = rec.get('tenantId');
                if (me.isTenantIdExists(companyListRecords, rec)) {
                    CompanyRolesArray = me.getCompanyRoles(companyListRecords);
                }

                if (!me.isTenantIdExists(companyListRecords, rec)) {
                    tenantListArray.push({ tenantId: rec.get('tenantId'), isEmptyRoles: true, roleIds: [], roleNames: [] });
                }

            });
            //Ext.each(groupedCompanyList, function (rec) {
            //    CompanyRolesArray = me.getCompanyRoles(companyListRecords);
            //});

            record.data.tenantList = (tenantListArray.length > 0 ? CompanyRolesArray.concat(tenantListArray) : CompanyRolesArray);

        return record;
    },

    checkObjectExistsOrNot: function (arr, record) {
        var i = arr.length;
        while (i--) {
            if (arr[i].tenantId === record.get('tenantId')) {
                return true;
            }
        }
        return false;
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

    }

});
