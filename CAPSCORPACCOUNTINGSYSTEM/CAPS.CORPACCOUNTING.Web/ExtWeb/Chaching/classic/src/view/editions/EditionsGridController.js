Ext.define('Chaching.view.editions.EditionsGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.editions-editionsgrid',
   
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
            //if record.get(id)>0 then update else add
            if (record.get(idPropertyField) > 0) {               
                var input = new Object();
                var Edition = {
                    Id: e.record.data.id,
                    DisplayName: e.record.data.displayName                 
                };

                var features = [
                    //{
                    //    name: a.down('treepanel').items.features,
                    //    value: a.down('treepanel').items.features
                    //},
                      {
                          name: "MyApplication.SampleBooleanFeature",
                          value: false
                      },
                      {
                          name: "MyApplication.SampleSelectionFeature",
                          value: "B"
                      },
                      {
                          name: "MyApplication.SampleNumericFeature",
                          value: 10
                      }
                ];

                var input = {
                    edition: Edition,
                    featureValues: features
                };

                Ext.Ajax.request({
                    url: abp.appPath + 'api/services/app/edition/CreateOrUpdateEdition',
                    jsonData: Ext.encode(input),
                    success: function(){
                        abp.notify.info(app.localize('Success'));
                        me.doPostSaveOperations();
                        
                    },
                failure: function(){
                    abp.notify.error(app.localize('Error'));
                }
                });


                //operation = Ext.data.Operation({
                //    params: record.data,
                //    records: [record],
                //    callback: me.onOperationCompleteCallBack
                //});
                //gridStore.update(operation);
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
    },


    doAfterCreateAction: function (createNewMode, form, isEdit, record) {
        var me = this;
        if (form.down('treepanel')) {
            var treeStore = form.down('treepanel').getStore();
            if (isEdit) {
                treeStore.getProxy().setExtraParams({ id: record.get('id') });
            } else {
                treeStore.getProxy().setExtraParams({id: null});
            }
            treeStore.load();
        }
    },

    doPostSaveOperations: function () {
        var me = this,
            view = this.getView();
        var grid = view.ownerGrid,
        gridStore = grid.getStore();
        grid.getStore().load();
    }

    //TODO convert this function in component(editing) so for every combo we need not to write
    //onEditionChange:function(combo, newValue, oldValue, e) {
    //    var grid = combo.up();
    //    if (grid) {
    //        var context = grid.context,
    //            record = context.record;
    //        record.set('editionId', newValue);
    //    }
    //}
});
