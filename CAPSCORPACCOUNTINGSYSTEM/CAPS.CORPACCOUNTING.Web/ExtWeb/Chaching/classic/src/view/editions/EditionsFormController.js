Ext.define('Chaching.view.editions.EditionsFormController', {
    extend: 'Chaching.view.common.form.ChachingFormPanelController',
    alias: 'controller.editions-editionsform',

    onSaveClicked: function (btn, e) {
        var me = this,
            view = this.getView().ownerCt,
        displayName = view.down('textfield').value;
        var grid = me.getView().parentGrid,
            gridStore = grid.getStore();
        var editionId, widgetRec;
            
        var editionTitle = view.el.component.title;

        if (editionTitle === "Edit edition") {
            var records = gridStore.getData().items;
            for (i = 0; i < records.length; i++) {
                if (records[i].data.displayName === displayName) {
                    editionId = records[i].id;
                    widgetRec = records[i];
                    }
                }
                var edition = {
                    id: editionId,
                    displayName: displayName
                };
            
        }
        else {

            var edition = {
                id: null,
                displayName: displayName
            };
        }

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
                    edition:edition,
                    featureValues: features
                }

                Ext.Ajax.request({
                    url: abp.appPath + 'api/services/app/edition/CreateOrUpdateEdition',
                    jsonData: Ext.encode(input),

                    success: function(){
                        abp.notify.info(app.localize('Success'));
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
