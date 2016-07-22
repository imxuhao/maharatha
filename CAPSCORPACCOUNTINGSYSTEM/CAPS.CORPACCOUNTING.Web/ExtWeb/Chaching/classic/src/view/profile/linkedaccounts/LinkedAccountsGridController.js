Ext.define('Chaching.view.profile.linkedaccounts.LinkedAccountsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.linkedaccounts-linkedaccountsgrid',

    //unlinkUser: function (editor, e, rowIndex) {
    //    var me = this,
    //        controller = me.controller,
    //        view = me.gridControl;

    //    var rec = me.widgetRec;      
    //        if (rec.id > 0) {                    
    //           var input = new Object();               
    //           input.UserId = rec.get('id');
    //           input.tenantId = rec.get('tenantId');

    //            Ext.Ajax.request({
    //                url: abp.appPath + 'api/services/app/userLink/unlinkUser',
    //                jsonData: Ext.encode(input),
    //                success: function (response) {
    //                    view.getStore().reload();
    //                },
    //                failure: function (response, opts) {
    //                    var res = Ext.decode(response.responseText);
    //                    Ext.toast(res.exceptionMessage);
    //                    console.log(response);
    //                }

    //            });
    //        }          
    //},

    unlinkUser: function (grid, rowIndex, colIndex) {
        var me = this,
         rec = grid.getStore().getAt(rowIndex);
        if (rec.get('id') > 0) {
            var input = new Object();
            input.UserId = rec.get('id');
            input.tenantId = rec.get('tenantId') == 0 ? null : rec.get('tenantId');
            abp.message.confirm(
              app.localize('LinkedUserDeleteWarningMessage', rec.get('username')),
              function (isConfirmed) {
                  if (isConfirmed) {
                      //Delete record
                      Ext.Ajax.request({
                          url: abp.appPath + 'api/services/app/userLink/unlinkUser',
                          jsonData: Ext.encode(input),
                          success: function (response) {
                              var result = Ext.decode(response.responseText);
                              if (result.success) {
                                  grid.getStore().reload();
                                  //reload profile account menu
                                  me.reloadProfileAccountMenu();
                              } else {
                                  abp.message.error(result.error.message);
                              }
                             
                          },
                          failure: function (response, opts) {
                              var res = Ext.decode(response.responseText);
                              abp.message.error(res.error.message);
                              console.log(response);
                          }
                      });
                  }
              }
          );
        }
    },

    reloadProfileAccountMenu: function () {
        var headerView = Ext.ComponentQuery.query('chachingheader')[0];
        if (headerView) {
            var accountButton = headerView.down('#AccountBtn');
            if (accountButton) {
                accountButton.contextMenu = null;
            }
           
        }
    },

    login: function (editor, e, rowIndex) {
        var me = this,
            controller = me.controller,
            view = me.gridControl;
        var rec = me.widgetRec;
        if (rec.get('id') > 0) {
            var model = new Object();
            model.TargetUserId = rec.get('id');
            model.TargetTenantId = rec.get('tenantId') == 0 ? null : rec.get('tenantId');

            Ext.Ajax.request({
                url: abp.appPath + 'Account/SwitchToLinkedAccount',
                jsonData: Ext.encode(model),
                success: function (response) {                  
                    var res = Ext.decode(response.responseText);
                    if (res.success) {
                        document.location = res.targetUrl;
                    } else {
                        abp.message.error(res.error.message);
                    }
                },
                failure: function (response, opts) {
                    var res = Ext.decode(response.responseText);
                    abp.message.error(res.error.message);
                    console.log(response);
                }

            });
        }
    },
});
