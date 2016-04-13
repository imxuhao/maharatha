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
                input.Edition = Edition;

                Ext.Ajax.request({
                    url: abp.appPath + 'api/services/app/edition/CreateOrUpdateEdition',
                    jsonData: Ext.encode(input)
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
