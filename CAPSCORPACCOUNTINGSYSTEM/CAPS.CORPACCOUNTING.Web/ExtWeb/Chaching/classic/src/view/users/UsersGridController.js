Ext.define('Chaching.view.users.UsersGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.users-usersgrid',
    permissionsClicked: function (menu, item, e, eOpts, isView) {
        var me = this, id=0, userName ='',
            parentMenu = menu.parentMenu,
            widgetRec = parentMenu.widgetRecord;
        if (widgetRec){
            id = widgetRec.get('id');
            userName = widgetRec.get('userName');
        }

        var form = Ext.create('Chaching.view.users.UsersPermissionView', {
            iconCls: 'fa fa-pencil',
            name: 'Administration.Users'
        });
        form.setTitle(form.getTitle() + ' - ' + userName);
        form.show();
        var windowMask = new Ext.LoadMask({
            msg: 'Please wait...',
            target: form
        });
        windowMask.show();
        var treePanel = form.down('treepanel[itemId=usersPermissionsItemId]');
        var treeStore = treePanel.getStore();
        //proxy.api.read = abp.appPath + 'api/services/app/user/GetUserPermissionsForEdit';
        treeStore.getProxy().api.read = abp.appPath + 'api/services/app/user/GetUserAllPermissionsForEdit';
        treeStore.getProxy().setExtraParam('id', id);
        treeStore.load({
            callback: function (records, response, success) {
                windowMask.hide();
            }
        });
        
    },
    doRowSpecificEditDelete: function (button, grid) {
        var me = this,
            view = Ext.ComponentQuery.query('users')[0];
        if (button.menu) {
            var loginAsThisUserActionMenu = button.menu.down('menuitem#loginAsThisUserActionMenu');
            if (view && loginAsThisUserActionMenu && button.widgetRec && view.modulePermissions.impersonation && button.widgetRec.get('id') != abp.session.userId) {
                loginAsThisUserActionMenu.show();
            } else {
                loginAsThisUserActionMenu.hide();
            }
        }
        
    },
    loginAsThisUserClicked: function (menu, item, e, eOpts, isView) {
        var me = this,
           parentMenu = menu.parentMenu,
           widgetRec = parentMenu.widgetRecord;
        Ext.Ajax.request({
            url: abp.appPath + 'Account/ImpersonateUser',
            jsonData: Ext.encode({ tenantId: abp.session.tenantId, userId: widgetRec.get('id') }),
            success: function (response) {
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    window.location.href = res.targetUrl;
                } else {
                    abp.message.error(res.error.message, app.localize('Error'));
                }
            },
            failure: function (response,a,b) {
                abp.message.error(app.localize('CascadeImpersonationErrorMessage'), app.localize('Error'));
            }
        });
    },

    doAfterCreateAction: function (createMode, formView, isEdit, record) {
        var me = this,
         form = formView.getForm();
        //get company list tab
        var companyListTab = formView.down('*[itemId=companyListTab]');
        var rolesGrid = formView.down('gridpanel[itemId=rolesListGridItemId]');
        var tenantRolesGrid = formView.down('gridpanel[itemId=companyListGridItemId]');
        if (formView && isEdit) {
            form.findField('userName').setReadOnly(true);
            //disable tabs
            if (companyListTab) {
                if (abp.session.tenantId != null) {
                    companyListTab.setDisabled(false);
                }
                else {
                    companyListTab.setDisabled(true);
                }
            }
            Ext.Ajax.request({
                url: abp.appPath + 'api/services/app/user/GetUserUnitForEdit',
                jsonData: Ext.encode({ id: record.get('id') }),
                success: function (response, opts) {
                    var res = Ext.decode(response.responseText);
                    if (res.success) {
                        if (isEdit && res.result != undefined) {
                            var roles = res.result.roles;
                            Ext.apply(record.data, res.result.user);
                            if (roles && roles.length > 0 ) {
                                Ext.each(roles, function (role) {
                                    var roleModel = Ext.create('Chaching.model.roles.RolesModel');
                                    roleModel.set('id', role.roleId);
                                    roleModel.set('name', role.roleName);
                                    roleModel.set('displayName', role.roleDisplayName);
                                    roleModel.commit();
                                    if (role.isAssigned) {
                                        rolesGrid.getStore().add(roleModel);
                                        rolesGrid.getSelectionModel().select(roleModel, true);
                                       // rolesGrid.getSelectionModel().select(roleModel);
                                    } else {
                                        rolesGrid.getStore().add(roleModel);
                                    }
                                });
                            }
                            if (abp.session.tenantId != null) {
                                // bind Link Company Grid
                                tenantRolesGrid.getStore().removeAll();
                                var tenantwithRoles = res.result.tenantwithRoles;
                                Ext.each(tenantwithRoles, function (role) {
                                    var tenantModel = Ext.create('Chaching.model.users.CompanyRoleModel');
                                    tenantModel.set('tenantId', role.tenantId);
                                    tenantModel.set('roleId', role.roleId);
                                    tenantModel.set('roleName', role.roleName);
                                    tenantModel.set('roleDisplayName', role.roleDisplayName);
                                    tenantModel.set('tenantName', role.tenantName);
                                    tenantModel.commit();
                                    tenantRolesGrid.getStore().add(tenantModel);
                                   
                                    if (role.isRoleSelected) {
                                        tenantRolesGrid.getSelectionModel().select(tenantModel, true);
                                    }
                                    //else {
                                    //    tenantRolesGrid.getStore().add(tenantModel);
                                    //}
                                });
                                
                                // End bind grid
                            }
                        }
                    } else {
                        abp.message.error(res.error.message, app.localize('Error'));
                    }
                },
                failure: function (response, opts) {
                    var res = Ext.decode(response.responseText);
                    Ext.toast(res.exceptionMessage);
                    console.log(response);
                }
            });

        } else {
            //load roles list
            var rolesStore = rolesGrid.getStore();
            rolesStore.load({
                callback: function (response, records, success) {
                    if (success) {
                        Ext.each(response, function (rec) {
                            //var isDefault = rec.get('isDefault');
                            //var isStatic = rec.get('isStatic');

                            var roleModel = Ext.create('Chaching.model.roles.RolesModel');
                            roleModel.set('id', rec.get('id'));
                            roleModel.set('name', rec.get('name'));
                            roleModel.set('displayName', rec.get('displayName'));
                            roleModel.commit();
                            //if (isDefault) {
                            //   // rolesGrid.getSelectionModel().select(roleModel, true);
                            //    rolesGrid.getSelectionModel().select(roleModel);
                            //}

                        });
                    }
                }
            });
            //load company list
            if (abp.session.tenantId != null) {
                var companyListGrid = formView.down('gridpanel[itemId=companyListGridItemId]');
                var companyListStore = companyListGrid.getStore();
                var proxy = companyListStore.getProxy();
                proxy.url = abp.appPath + 'api/services/app/user/GetTenantListofOrganization',
                companyListStore.getProxy().setExtraParams({ id: abp.session.tenantId });
                companyListStore.load();
                //enable tabs
                if (companyListTab) {
                    companyListTab.setDisabled(false);
                }
            }
            else {
                if (companyListTab) {
                    companyListTab.setDisabled(true);
                }
            }
        }
       
    },
    onEditComplete: function (editor, e) {
        var me = this,
            view = this.getView();
        if (editor && editor.ptype === "chachingRowediting" && editor.context) {
            var context = editor.context,
                grid = context.grid,
                gridStore = grid.getStore(),
                record = context.record,
                idPropertyField = gridStore.idPropertyField;
            var operation;
            if (record.get(idPropertyField) > 0) {
                
                var AssignedRoleNames = [];
                Ext.each(e.record.data.roles, function (roles, index) {
                    AssignedRoleNames.push(roles.roleName);
                });

                var input = new Object();
                var user = {
                    id: e.record.data.id,
                    name: e.record.data.name,
                    surname: e.record.data.surname,
                    userName: e.record.data.userName,
                    emailAddress: e.record.data.emailAddress,
                    isActive: e.record.data.isActive
                };
                input.user = user;
                input.assignedRoleNames = AssignedRoleNames;
                //input.sendActivationEmail = e.record.data.isEmailConfirmed;

                Ext.Ajax.request({
                    url: abp.appPath + 'api/services/app/user/CreateOrUpdateUser',
                    jsonData: Ext.encode(input),
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json'
                    },
                    success: function (response) {
                        gridStore.reload();
                    },
                    failure: function (response) {
                    }
                });
            } else {
                record.id = 0;
                record.set('id', 0);
                operation = Ext.data.Operation({
                    params: record.data,
                    controller: me,
                    callback: me.onOperationCompleteCallBack
                });
                gridStore.create(record.data, operation);
            }

        }
    }
    
});
