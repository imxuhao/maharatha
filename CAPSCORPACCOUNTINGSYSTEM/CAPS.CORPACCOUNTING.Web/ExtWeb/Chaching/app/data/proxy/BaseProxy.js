/**
 * This Class is created as a base class for all Ext.data.Operations.
 * Author: Krishna Garad
 * Date: 31/03/2016
 */
/**
 * @class Chaching.data.proxy.BaseProxy
 * The based class for all Ext.data.Operation.
 * @alias proxy.chachingProxy
 *
 *     @example usage 
 *     Ext.define('Chaching.store.CustomerStore', {
 *     extend: 'Chaching.store.base.BaseStore',
       pageSize: 1000,
       model: 'Chaching.model.CustomerModel',
       proxy: {
        type: 'chachingProxy',
        actionMethods: { create: 'POST', read: 'POST', update: 'POST', destroy: 'POST' },
        api: {
            read: abp.appPath + '',
            destroy:abp.appPath+''
        },
        reader: {
            type: 'json',
            rootProperty: 'result'
        }
        
 *     });
 */
Ext.define('Chaching.data.proxy.BaseProxy', {
    extend: 'Ext.data.proxy.Ajax',
    alias: 'proxy.chachingProxy',
    timeout: 6000000,
    limitParam: 'maxResultCount',
    startParam: 'skipCount',
    sortParam: 'sortList',
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
                    DataType: filters[i].dataType,
                    IsMultiRange: filters[i].isMultiRange
                };
                filterArray.push(filterObject);
            }
            return filterArray;
        }
        return filters;
    },
    encodeSorters: function (sorters) {
        var me = this,
            model = me.getModel(),
            entityName = model.$config.values.searchEntityName;
        var sortersArray = [];
        if (sorters) {
            for (var i = 0; i < sorters.length; i++) {
                var sortObject = {
                    // override to define the sort entity column wise(if you have two entity in grid like tenants screen we have organization and tenant table data)
                    // define sorter in columnwise if needed like  sortable: true,
                    //sorter: {
                    //    property: 'organizationName',
                    //    sortOnEntity: ''
                    //},
                    Entity: (sorters[i].sortOnEntity == undefined || sorters[i].sortOnEntity == null) ? entityName : sorters[i].sortOnEntity,
                    Property: sorters[i].getProperty(),
                    Order: sorters[i].getDirection()
                };
                sortersArray.push(sortObject);
            }
            return sortersArray;
        }
        return sorters;
    },
    doRequest: function (operation) {
        var me = this,
            writer = me.getWriter(),
            request = me.buildRequest(operation),
            method = me.getMethod(request),
            jsonData, params;
        if (writer && operation.allowWrite()) {
            request = writer.write(request);
        }
        request.setConfig({
            binary: me.getBinary(),
            headers: me.getHeaders(),
            timeout: me.getTimeout(),
            scope: me,
            callback: me.createRequestCallback(request, operation),
            method: method,
            useDefaultXhrHeader: me.getUseDefaultXhrHeader(),
            disableCaching: false
        });
        // explicitly set it to false, ServerProxy handles caching
        if (method.toUpperCase() !== 'GET' && me.getParamsAsJson()) {
            params = request.getParams();
            if (params) {
                //Set users logged in organizationId
                if (!params.organizationUnitId)
                    params.organizationUnitId = Chaching.utilities.ChachingGlobals.loggedInUserInfo.userOrganizationId;

                jsonData = request.getJsonData();
                if (jsonData) {
                    jsonData = Ext.Object.merge({}, jsonData, params);
                } else {
                    jsonData = params;
                }
                request.setJsonData(jsonData);
                request.setParams(undefined);
            }
        }
        if (me.getWithCredentials()) {
            request.setWithCredentials(true);
            request.setUsername(me.getUsername());
            request.setPassword(me.getPassword());
        }
        return me.sendRequest(request);
    },
    listeners:
    {
        exception: function(proxy, request, operation) {
            if (request.responseText != undefined) {
                // responseText was returned, decode it
                try {
                    var responseObj = Ext.decode(request.responseText, true);
                    abp.notify.error(responseObj.error.message, app.localize('Error'));
                } catch (e) {
                    abp.message.warn('Unknown error: The server did not send any information about the error.', 'Error');
                    //Ext.Msg.alert('Error', 'Unknown error: The server did not send any information about the error.');
                }

            } else {
                abp.message.error('Unknown error: Unable to understand the response from the server', 'Error');
                // no responseText sent
                // Ext.Msg.alert('Error', 'Unknown error: Unable to understand the response from the server');
            }
        }
    }
});