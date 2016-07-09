Ext.define('Chaching.view.users.UsersGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.users-usersgrid',
    doAfterCreateAction: function (createMode, formView, isEdit, record) {

        var me = this,
         form = formView.down('form').getForm();
        //load roles list
        var rolesGrid = formView.down('gridpanel[itemId=rolesListGridItemId]');
        var rolesStore = rolesGrid.getStore();
        rolesStore.load();

        //load company list
        var companyListGrid = formView.down('gridpanel[itemId=companyListGridItemId]');
        var companyListStore = companyListGrid.getStore();
        var proxy = companyListStore.getProxy();
        proxy.url = abp.appPath + 'api/services/app/user/GetTenantListofOrganization',
        companyListStore.getProxy().setExtraParams({ id: abp.session.tenantId});
        companyListStore.load();

        var companyListTab = formView.down('*[itemId=companyListTab]');
        if (formView && isEdit) {
            form.findField('userName').setReadOnly(true);
            //disable tabs
            if (companyListTab) {
                companyListTab.setDisabled(true);
            }
        } else {
            //enable tabs
            if (companyListTab) {
                companyListTab.setDisabled(false);
            }
        }
        //var organizationStore = form.findField('organizationId').getStore();
        //organizationStore.load();
        //var viewModel = formView.down('form').getViewModel();
        //var editionStore = viewModel.getStore('editionsForComboBox');
        //editionStore.load();
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
                var User = {
                    Id: e.record.data.id,
                    Name: e.record.data.name,
                    Surname: e.record.data.surname,
                    UserName: e.record.data.userName,
                    EmailAddress: e.record.data.emailAddress,
                    IsActive: e.record.data.isActive
                };
                input.User = User;
                input.AssignedRoleNames = AssignedRoleNames;
                //input.sendActivationEmail = e.record.data.isEmailConfirmed;

                Ext.Ajax.request({
                    url: abp.appPath + 'api/services/app/user/CreateOrUpdateUser',
                    jsonData: Ext.encode(input),
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json'
                    },
                    success: function (response) {
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
