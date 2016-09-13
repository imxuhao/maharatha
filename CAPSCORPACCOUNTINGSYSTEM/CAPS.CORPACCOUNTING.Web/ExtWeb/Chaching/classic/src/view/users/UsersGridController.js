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
            iconCls: 'fa fa-ticket',
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
            var editButton = button.menu.down('menuitem#editActionMenu');
            var viewButton = button.menu.down('menuitem#viewActionMenu');
            if ((editButton && button.widgetRec) || (viewButton && button.widgetRec)) {
                ChachingGlobals.SelectedUserId = button.widgetRec.data.id;
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
        // Start horizontal tab bar disable while create
        var userSecuritySettings = formView.down('tabpanel[itemId=userSecuritySettingsItemId]');
        if (abp.session.tenantId != null && isEdit) {
            userSecuritySettings.setDisabled(false);
            //Population of combo and grid
            var me = this,
            view = me.getView(),
            coaCombo = formView.down('combobox[reference=coaCombo]');
            coaCombo.getStore().load({
                callback: function (records, operation, success) {
                    if (success && records.length > 0) {
                        coaCombo.setValue(records[0].data.coaId);
                    }
                }
            });
            //populate project coa combo
            var projectCoaCombo = formView.down('combobox[reference=projectCoaCombo]');
            projectCoaCombo.getStore().load();
            //load project security accesss
            me.loadProjectSecurity(formView);
            //load division security accesss
            me.loadDivisionSecurity(formView);
            // load credit card security access
            me.loadCreditCardSecurity(formView);
            // load bank security access
            me.loadBankSecurity(formView);
        }
        else {
            userSecuritySettings.setDisabled(true);
            ChachingGlobals.SelectedUserId = 0;
        }
        
        if (formView && isEdit) {
            // Edit user screen
            //form.findField('userName').setReadOnly(true);
            //form.findField('setRandomPassword').setValue(false);
            //form.findField('sendActivationEmail').setValue(false);
            form.findField('setRandomPassword').setHidden(true);
            form.findField('sendActivationEmail').setHidden(true);
            form.findField('shouldChangePasswordOnNextLogin').setHidden(true);
            // disable email address in User Edit
            form.findField('emailAddress').setReadOnly(true);
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
                            //Ext.apply(record.data, res.result.user);
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
            // create new user screen
            //load roles list
            var rolesStore = rolesGrid.getStore();
            rolesStore.load({
                callback: function (response, records, success) {
                    if (success) {
                        var rolesSelectionModel = rolesGrid.getSelectionModel();
                        var defaultRoles = [];
                        Ext.each(response, function (rec) {
                            var isDefault = rec.get('isDefault');
                            //var isStatic = rec.get('isStatic');

                            var roleModel = Ext.create('Chaching.model.roles.RolesModel');
                            
                            roleModel.set('id', rec.get('id'));
                            roleModel.set('name', rec.get('name'));
                            roleModel.set('displayName', rec.get('displayName'));
                            if (isDefault) {
                                defaultRoles.push(rec);
                            }
                            roleModel.commit();
                            //if (isDefault) {
                            //   // rolesGrid.getSelectionModel().select(roleModel, true);
                            //    rolesGrid.getSelectionModel().select(roleModel);
                            //}

                        });
                        rolesSelectionModel.select(defaultRoles);
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
   
    loadProjectSecurity: function (formView) {
        var projectSecurityControl = formView.down('chachingGridDragDrop[itemId=projectSecurityGridItemId]'),
        leftStore = projectSecurityControl.getLeftStore(),
        rightStore = projectSecurityControl.getRightStore(),
        filter = new Ext.util.Filter({
            entity: '',
            searchTerm: 'false',
            comparator: 0,
            dataType: 3,
            property: 'IsDivision'
        });
        leftStore.filter(filter);
        leftStore.getProxy().setExtraParams({
            //'chartOfAccountId': chartOfAccountId,
            'userId': ChachingGlobals.SelectedUserId,
            'entityClassificationId': ChachingGlobals.entityClassification.Project
        });
        leftStore.load();
        rightStore.getProxy().setExtraParams({
           // 'chartOfAccountId': chartOfAccountId,
            'userId': ChachingGlobals.SelectedUserId,
            'entityClassificationId': ChachingGlobals.entityClassification.Project
        });
        rightStore.load();
    },
    loadDivisionSecurity: function (formView) {
        var divisionSecurityControl = formView.down('chachingGridDragDrop[itemId=divisionSecurityGridItemId]'),
        leftStore = divisionSecurityControl.getLeftStore(),
        rightStore = divisionSecurityControl.getRightStore(),
        filter = new Ext.util.Filter({
            entity: '',
            searchTerm: 'true',
            comparator: 0,
            dataType: 3,
            property: 'IsDivision'
        });
        leftStore.filter(filter);
        leftStore.getProxy().setExtraParams({
            'userId': ChachingGlobals.SelectedUserId,
            'entityClassificationId': ChachingGlobals.entityClassification.Division
        });
        leftStore.load();
        rightStore.filter(filter);
        rightStore.getProxy().setExtraParams({
            'userId': ChachingGlobals.SelectedUserId,
            'entityClassificationId': ChachingGlobals.entityClassification.Division
        });
        rightStore.load();
    },

    loadCreditCardSecurity: function (formView) {
        var creditCardSecurityControl = formView.down('chachingGridDragDrop[itemId=creditCardSecurityGridItemId]'),
        leftStore = creditCardSecurityControl.getLeftStore(),
        rightStore = creditCardSecurityControl.getRightStore();
        leftStore.getProxy().setExtraParams({
            'userId': ChachingGlobals.SelectedUserId,
            'entityClassificationId': ChachingGlobals.entityClassification.CreditCard
        });
        leftStore.load();
        rightStore.getProxy().setExtraParams({
            'userId': ChachingGlobals.SelectedUserId,
            'entityClassificationId': ChachingGlobals.entityClassification.CreditCard
        });
        rightStore.load();
    },
    loadBankSecurity: function (formView) {
        var bankSecurityControl = formView.down('chachingGridDragDrop[itemId=bankSecurityGridItemId]'),
        leftStore = bankSecurityControl.getLeftStore(),
        rightStore = bankSecurityControl.getRightStore();
        leftStore.getProxy().setExtraParams({
            //'chartOfAccountId': chartOfAccountId,
            'userId': ChachingGlobals.SelectedUserId,
            'entityClassificationId': ChachingGlobals.entityClassification.BankAccount
        });
        leftStore.load();
        rightStore.getProxy().setExtraParams({
            // 'chartOfAccountId': chartOfAccountId,
            'userId': ChachingGlobals.SelectedUserId,
            'entityClassificationId': ChachingGlobals.entityClassification.BankAccount
        });
        rightStore.load();
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
                    url: abp.appPath + 'api/services/app/user/CreateOrUpdateUserUnit',
                    jsonData: Ext.encode(input),
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json'
                    },
                    success: function (response) {
                        abp.notify.success(app.localize('SuccessMessage'), app.localize('Success'));
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
