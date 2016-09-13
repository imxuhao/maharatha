Ext.define('Chaching.store.users.PermissionsTreeStore', {
    extend: 'Chaching.store.base.BaseTreeStore',
    model: 'Chaching.model.roles.RolePermissionsModel',
    clearOnLoad: false,
    selectedNodeCount: 0,
    remoteFilter: false,
    remoteSort: false,
    //statefulFilters: true,
    root: {
        //expanded: true
    },
    proxy: {
        type: 'chachingProxy',
        actionMethods: {read: 'POST', update: 'POST'},
        api: {
            read: abp.appPath + 'api/services/app/user/GetUserAllPermissionsForEdit',
            update: abp.appPath + 'api/services/app/user/UpdateUserPermissions'
        },
        reader: {
            type: 'json',
            rootProperty: 'result.permissions'
        }
    },
    listeners: {
        load: function (permissionStore, records, success) {
            var me = this;
            var pages = Ext.create(me.getModel().$className,{
                text: 'Pages',
                iconCls: null,
                expanded: true,
                name: null,
                url: null,
                checked: false,
                displayName: 'Pages',
                name : 'Pages',
                children: [],
                leaf: false
            });
            var length = records.length,
                parents = [];
            me.selectedNodeCount = 0;
            for (var i = 0; i < length; i++) {
                var item = records[i];
                if (item.get('name') === "Pages" && item.get('parentId') === "root") {
                    pages.set('checked', item.get('isPermissionGranted') ? true : false);
                }
                else if (item.get('parentName') === "Pages") {
                    parents.push(item);
                }
                if (item.get('isPermissionGranted')) {
                    me.selectedNodeCount = me.selectedNodeCount + 1;
                }
            }
            var root = me.getRoot();
            //root.set('expanded', true);
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
        parent.set('checked', parent.get('isPermissionGranted') ? true : false);
        var parentName = parent.get('name');
        if (!parent.data.children) parent.data.children = [];
        for (var i = 0; i < records.length; i++) {
            var record = records[i];
            if (record.get('parentName') === parentName) {
                record.set('checked', record.get('isPermissionGranted') ? true : false);
                parent.appendChild(record);
                record.set('loaded', true);
                record.set('expanded', true);
                me.buildChilds(record, records);
               
            }
        }
        parent.set('loaded', true);
        parent.set('expanded', true);
        return parent;
    },
    getSelected: function (grantedPermissions, name) {
        return grantedPermissions.indexOf(name)<0? false: true;

    }
});
