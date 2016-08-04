Ext.define('Chaching.store.editions.EditionsTreeStore', {
    extend: 'Chaching.store.base.BaseTreeStore',
    model: 'Chaching.model.editions.EditionsTreeModel',

    clearOnLoad: false,
    remoteFilter: false,
    remoteSort: false,
    statefulFilters: true,
    root: {
        expanded: false
    },

    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        url: abp.appPath + 'api/services/app/edition/GetEditionForEdit',
       
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
    },
    idPropertyField: 'id',

    listeners: {
        load: function (store, record, success) {
            var me = this;
            var recordExists = record[0].data.edition;

            if (parseInt(recordExists.id) > 0) {
                for (var i = 0; i < record[0].data.featureValues.length; i++) {
                    var featureValue = record[0].data.featureValues[i];
                    for (var j = 0; j < record[0].data.features.length; j++) {
                        var feature = record[0].data.features[j];
                        if (feature.name == featureValue.name) {
                            feature.defaultValue = featureValue.value;
                        }
                    }
                }
            }
            var recordsList = [];
            for (var j = 0; j < record[0].data.features.length; j++) {
                var treeRecord = Ext.create('Chaching.model.editions.EditionsTreeModel');
                Ext.apply(treeRecord.data, record[0].data.features[j]);
                recordsList.push(treeRecord);
            }
            record = recordsList;
            

            var length = record.length;
            var root = me.getRoot();
            root.removeAll();
            root.set('expanded', true);
            for (var i = 0; i < length; i++) {
                var item = record[i];
                if (item.data.parentName == null) {
                    if (item.data.inputType.name === "CHECKBOX") {
                        root.appendChild({
                            displayName: item.data.displayName,
                            inputType: item.data.inputType,
                            defaultValue: item.data.defaultValue,
                            name: item.data.name,
                            description:item.data.description,
                            expanded: true
                        });
                    }

                    else if (item.data.inputType.name === "SINGLE_LINE_STRING") {
                        root.appendChild({
                            displayName: item.data.displayName,
                            inputType: item.data.inputType,
                            defaultValue: item.data.defaultValue,
                            name: item.data.name,
                            description: item.data.description,
                            expanded: true
                        });
                    }

                    else {
                        root.appendChild({
                            displayName: item.data.displayName,
                            inputType: item.data.inputType,
                            defaultValue: item.data.defaultValue,
                            name: item.data.name,
                            description: item.data.description,
                            expanded: true
                        });
                    }
                }
                else {
                    this.buildParent(record, item, root);
                }
            }
        }
        
    },

    buildParent: function (records, child,root) {
        var parentRoot;
        for (i = 0; i < records.length; i++) {
            if (records[i].data.name == child.get('parentName')) {
                parentRoot = records[i];
            }
        }
        if (parentRoot) {
            parentRoot.set('expanded', true);
            parentRoot.insertChild(0,
            {
                displayName: child.data.displayName,
                defaultValue: child.data.defaultValue,
                inputType: child.data.inputType,
                name: child.data.name,
                description: child.data.description,
                expanded: true,
                leaf: true
            });

            root.removeAll();
            root.set('expanded', true);
            root.appendChild(parentRoot);
        }
    }

});