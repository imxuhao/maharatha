Ext.define('Chaching.data.proxy.BaseProxy', {
    extend: 'Ext.data.proxy.Ajax',
    alias: 'proxy.chachingProxy',
    timeout: 6000000,
    limitParam: 'maxResultCount',
    startParam: 'skipCount',
    sortParam: 'sorting',
    filterParam: 'filters',
    pageParam: '',
    headers: {
        'Accept': 'application/json, text/plain, */*',
        'Content-Type': 'application/json;charset=UTF-8'
    },
    actionMethods: { create: 'POST', read: 'GET', update: 'POST', destroy: 'POST' },
    paramsAsJson: true,

    reader: {
        type: 'json',
        rootProperty: 'result.items',
        totalProperty: 'result.totalCount'
    },
    writer: {
        type: 'json'
    },
    encodeFilters: function(filters) {
        var filterArray = [];
        if (filters) {
            for (var i = 0; i < filters.length; i++) {
                var filterObject = {
                    Entity: filters[i].entity,
                    Property: filters[i].getProperty(),
                    SearchTerm: filters[i].searchTerm,
                    SearchTerm2: filters[i].searchTerm2,
                    Comparator: filters[i].comparator,
                    DataType: filters[i].dataType
                };
                filterArray.push(filterObject);
            }
            return filterArray;
        }
        return filters;
    },
    encodeSorters: function (sorters) {
        ///TODO Implement as per server requirements
        return sorters;
    },
    listeners:
    {
        exception: function(proxy, request, operation) {
            if (request.responseText != undefined) {
                // responseText was returned, decode it
                try {
                    var responseObj = Ext.decode(request.responseText, true);

                } catch (e) {
                    Ext.Msg.alert('Error', 'Unknown error: The server did not send any information about the error.');
                }

            } else {
                // no responseText sent
                Ext.Msg.alert('Error', 'Unknown error: Unable to understand the response from the server');
            }
        },
        metachange:function(proxy, meta, eOpts) {
            debugger;
        }
    }
});