Ext.define('Chaching.store.base.TransactionDetailsStore', {
    extend: 'Chaching.store.base.BaseStore',
    pageSize: 50,//Number.MAX_SAFE_INTEGER, // items per page
    autoLoad: false,
    remoteSort: false,
    remoteFilter: false,
    idPropertyField:'accountingItemId',
    listeners: {
        beforeload: function (store, operation, eOpts) {
            if (store.isLoading()) return false;
            // do not hit the server if no param (transactionId) exists
            //if (operation.getProxy().getExtraParams() && operation.getProxy().getExtraParams().accountingDocumentId > 0)
            //    return true;
            //else
            //    return false;
        },
        load:function(me, records, successful, eOpts) {
            if (!successful) return;
            me.loadDefaultRecords();
        }
    },
    loadDefaultRecords: function (remainingCount) {
        var store = this;
        if (!remainingCount) {
            var idealCount = 15, actualCount = store.getCount();
            remainingCount = idealCount - actualCount;
        }
        var modelClass = store.getModel(),
            className = modelClass.$className;
        for (var i = 0; i < remainingCount; i++) {
            var record = Ext.create(className);
            record.set('organizationUnitId', Chaching.utilities.ChachingGlobals.loggedInUserInfo.userOrganizationId);
            record.commit();
            store.add(record);
            //store.add({ jobId: null, accountingItemId :0}); //AP, JE, PR, PC, CC, PO
        }
    }
});