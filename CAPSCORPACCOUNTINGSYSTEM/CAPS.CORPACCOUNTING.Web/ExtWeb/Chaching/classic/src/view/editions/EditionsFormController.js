Ext.define('Chaching.view.editions.EditionsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.editions-editionsform',

    doPreSaveOperation: function (record, values) {
        var me = this,
            view = this.getView().ownerCt;

        var recordClicked = values;
        var displayName = recordClicked.displayName;

        var grid = me.getView().parentGrid,
            gridStore = grid.getStore();

        var treepanel = me.getView().down('treepanel');
        var featureGrid = treepanel.getColumns()[1];
        var selectionModel = featureGrid.getView().getSelectionModel();
        var featureStore = selectionModel.getStore(),
            featureItems = featureStore.getDataSource().items;
        
        if (parseInt(recordClicked.id) > 0) {
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
            edition: edition,
            featureValues: features
        };
        
        record.data.edition = input.edition;
        record.data.featureValues = input.featureValues;

        return record;
   },

    doPostSaveOperations: function (records, operation, success) {
        var me = this,
            view = this.getView().ownerCt;

            var grid = me.getView().parentGrid,
            gridStore = grid.getStore();

            Ext.destroy(view);
            grid.getStore().load();
    },

    onEditionsNameEnter: function (field, newValue, oldValue, eOpts) {
        field.setValue(newValue.toUpperCase());
    }
    
});
