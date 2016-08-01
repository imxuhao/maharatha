Ext.define('Chaching.store.roles.RolesTreeViewStore', {
    extend: 'Chaching.store.base.BaseTreeStore',
    model: 'Chaching.model.roles.RolePermissionsModel',
    autoLoad: false,
    clearOnLoad: false,
    remoteFilter: false,
    remoteSort: false,
    root: {
        expanded: false
    },
    proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + 'api/services/app/user/GetPermissionsForSelectedRole'
        },
        reader: {
            type: 'json',
            rootProperty: 'result.permissions'
        }
    },
    listeners: {
        load: function (permissionStore, records, success) {
            var me = this;
            var pages = Ext.create(me.getModel().$className, {
                text: 'Pages',
                iconCls: null,
                expanded: true,
                name: null,
                url: null,
                displayName: 'Pages',
                name: 'Pages',
                children: [],
                leaf: false
            });

            var length=0;
            if (records) {
                length = records.length;
            }
            var parents = [];
            for (var i = 0; i < length; i++) {
                var item = records[i];
                if (item.get('parentName') === "Pages") {
                    parents.push(item);
                }
            }
            var root = me.getRoot();
            root.removeAll();
            for (var j = 0; j < parents.length; j++) {
                var parent = parents[j];
                pages.appendChild(me.buildChilds(parent, records));
            }
            if (!root.data.children) root.data.children = [];
            root.appendChild(pages);
            root.expand(true);
        }
    },
    buildChilds: function (parent, records) {
        var me = this;
        var parentName = parent.get('name');
        if (!parent.data.children) parent.data.children = [];
        for (var i = 0; i < records.length; i++) {
            var record = records[i];
            if (record.get('parentName') === parentName) {
                parent.appendChild(record);
                record.set('loaded', true);
                record.set('expanded', true);
                me.buildChilds(record, records);
            }
        }
        parent.set('loaded', true);
        parent.set('expanded', true);
        return parent;
    }
});