﻿Ext.define('Chaching.store.base.BaseTreeStore', {
    extend: 'Ext.data.TreeStore',
    requires: ['Chaching.data.proxy.BaseProxy'],
    pageSize: 10, // items per page
    autoLoad: false,
    remoteSort: true,
    remoteFilter: true
});