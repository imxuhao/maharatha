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
            height: '80%',
            width: '30%',
            iconCls: 'fa fa-pencil',
            name: 'Administration.Users'
        });
        var treePanel = form.down('treepanel[itemId=usersPermissionsItemId]');
        var treeStore = treePanel.getStore();
        //proxy.api.read = abp.appPath + 'api/services/app/user/GetUserPermissionsForEdit';
        treeStore.getProxy().api.read = abp.appPath + 'api/services/app/user/GetUserAllPermissionsForEdit';
        treeStore.getProxy().setExtraParam('id', id);
        treeStore.reload();
        form.setTitle(form.getTitle() + ' - ' + userName);
        form.show();
    },
    doRowSpecificEditDelete: function (button, grid) {
        //var menu = button.menu;
        //var permissionButton = menu.items.get('permissions');
        //var separatorButton = menu.items.get('actionMenuSeparator');
        
        //if (permissionButton && abp.session.tenantId == null) {
        //    permissionButton.show();
        //    separatorButton.show();
        //}
        //else if(permissionButton) {
        //    permissionButton.hide();
        //    separatorButton.hide();
        //}
        
    },
    doAfterCreateAction: function (createMode, formView, isEdit, record) {
        var me = this,
         form = formView.getForm();
        //get company list tab
        var companyListTab = formView.down('*[itemId=companyListTab]');
        var rolesGrid = formView.down('gridpanel[itemId=rolesListGridItemId]');
        if (formView && isEdit) {
            form.findField('userName').setReadOnly(true);
            //disable tabs
            if (companyListTab) {
                companyListTab.setDisabled(true);
            }
            Ext.Ajax.request({
                url: abp.appPath + 'api/services/app/user/GetUserForEdit',
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
                                        rolesGrid.getSelectionModel().select(roleModel,true);
                                    } else {
                                        rolesGrid.getStore().add(roleModel);
                                    }
                                });
                            }
                        }
                    } else {
                        abp.message.error(res.error.message, 'Error');
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
            rolesStore.load();
            //load company list
            if (abp.session.tenantId) {
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
