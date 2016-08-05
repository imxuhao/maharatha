Ext.define('Chaching.view.editions.EditionsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.editions-editionsform',

   onSaveClicked: function (btn, e) {
        var me = this,
            view = this.getView().ownerCt,
        displayName = view.down('textfield').value;
        var grid = me.getView().parentGrid,
            gridStore = grid.getStore();
        var treepanel = me.getView().down('treepanel');
        var featureGrid = treepanel.getColumns()[1];
        var selectionModel = featureGrid.getView().getSelectionModel();
        var featureStore = selectionModel.getStore(),
              featureItems = featureStore.getDataSource().items;

        var editionTitle = view.el.component.title;
        var recordClicked = this.getView().getValues();
        if (parseInt(recordClicked.id) > 0) {
            var recordClicked = this.getView().getValues();
                var edition = {
                    id: recordClicked.id,
                    displayName: displayName
                };
        }
        else {

            var edition = {
                id: null,
                displayName: displayName
            };
        }

        var features = [];

        for (var i = 0; i < featureItems.length; i++) {
            features.push({
                name: featureItems[i].data.name,
                value: featureItems[i].data.defaultValue
            });
        }
                var input = {
                    edition:edition,
                    featureValues: features
                }

                Ext.Ajax.request({
                    url: abp.appPath + 'api/services/app/edition/CreateOrUpdateEdition',
                    jsonData: Ext.encode(input),

                    success: function(){
                        abp.notify.success(app.localize('Success'));
                        me.doPostSaveOperations();
                        Ext.destroy(view);
                        
                    },
                    failure: function(){
                        abp.notify.error(app.localize('Error'));
                    }
                });
   },

    doPostSaveOperations: function (records, operation, success) {
        var me = this,
            view = this.getView().ownerCt;
            var grid = me.getView().parentGrid,
            gridStore = grid.getStore();
            grid.getStore().load();
    }
    
});
