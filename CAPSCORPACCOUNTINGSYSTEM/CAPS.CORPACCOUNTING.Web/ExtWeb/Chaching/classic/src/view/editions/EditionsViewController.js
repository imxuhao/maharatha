Ext.define('Chaching.view.editions.EditionsViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.editions-editionsview',
    onSaveClicked: function (btn) {       
        var me = this,
            view = me.getView(),
            parentGrid = view.parentGrid,
            values = view.getValues();

        if (parentGrid) {
            var gridStore = parentGrid.getStore(),
                idPropertyField = gridStore.idPropertyField,
                operation;
            var record = Ext.create(gridStore.model.$className);
            Ext.apply(record.data, values);
            var myMask = new Ext.LoadMask({
                msg: 'Please wait...',
                target: view
            });

            myMask.show();
            if (values && parseInt(values[idPropertyField]) > 0) {                
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
            } else if (values && parseInt(values[idPropertyField]) === 0) {
                operation = Ext.data.Operation({
                    params: record.data,
                    parentGrid: parentGrid,
                    controller: me,
                    callback: me.onOperationCompleteCallBack
                });
                gridStore.create(values, operation);
            } else {
                myMask.hide();
            }
        }
    }
    
});
