Ext.define('Chaching.store.editions.EditionsTreeStore', {
    extend: 'Chaching.store.base.BaseTreeStore',
    model: 'Chaching.model.editions.EditionsTreeModel',

    clearOnLoad: false,
    remoteFilter: false,
    remoteSort: false,
    statefulFilters: true,
    root: {
        expanded: true
    },

    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        url: abp.appPath + 'api/services/app/edition/GetEditionForEdit',
       
        reader: {
            type: 'json',
            rootProperty: 'result.features'
        }
    },
    idPropertyField: 'id',

    listeners: {
        load: function (store, record, success) {
            var me = this;
            var length = record.length;
            var root = me.getRoot();
            root.removeAll();

            for (var i = 0; i < length; i++) {
                var item = record[i];
                if (item.data.parentName == null) {
                    if (item.data.inputType.name === "CHECKBOX") {
                        root.appendChild({
                            displayName: item.data.displayName,
                            expanded: true,
                            checked: true
                        });
                    }

                    else if (item.data.inputType.name === "SINGLE_LINE_STRING") {
                        root.appendChild({
                            displayName: item.data.displayName,
                            expanded: true
                        });
                    }

                    else {
                        root.appendChild({
                            displayName: item.data.displayName,
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
        parentRoot.set('expanded', true);
        parentRoot.insertChild(0,{
            displayName: child.data.displayName,
            expanded: true
        });

        root.removeAll();
        root.appendChild(parentRoot);
    }

});