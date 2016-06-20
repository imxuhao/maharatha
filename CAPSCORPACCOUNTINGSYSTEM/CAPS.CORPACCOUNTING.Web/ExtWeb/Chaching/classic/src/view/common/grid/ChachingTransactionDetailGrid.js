/**
 * The base class for all transaction details/distribution grids
 * Author: Krishna Garad
 * Date : 17/05/2016
 */
/**
 * Grids are an excellent way of showing large amounts of tabular data on the client side.
 * Essentially a supercharged `<table>`, GridPanel makes it easy to fetch, sort and filter
 * large amounts of data.
 *
 * Grids are composed of two main pieces - a {@link Ext.data.Store Store} full of data and
 * a set of columns to render.
 *
 * ## Basic GridPanel
 *
 *     @example
 *     Ext.create('Ext.data.Store', {
 *         storeId: 'simpsonsStore',
 *         fields:[ 'name', 'email', 'phone'],
 *         data: [
 *             { name: 'Lisa', email: 'lisa@simpsons.com', phone: '555-111-1224' },
 *             { name: 'Bart', email: 'bart@simpsons.com', phone: '555-222-1234' },
 *             { name: 'Homer', email: 'homer@simpsons.com', phone: '555-222-1244' },
 *             { name: 'Marge', email: 'marge@simpsons.com', phone: '555-222-1254' }
 *         ]
 *     });
 *
 *     Ext.create('Chaching.view.common.grid.ChachingTransactionDetailGrid', {
 *         title: 'Simpsons',
 *         store: Ext.data.StoreManager.lookup('simpsonsStore'),
 *         columns: [
 *             { text: 'Name', dataIndex: 'name' },
 *             { text: 'Email', dataIndex: 'email', flex: 1 },
 *             { text: 'Phone', dataIndex: 'phone' }
 *         ],
 *         height: 200,
 *         width: 400,
 *         renderTo: Ext.getBody()
 *     });
 *
 * The code above produces a simple grid with three columns. We specified a Store which
 * will load JSON data inline.
 * In most apps we would be placing the grid inside another container and wouldn't need to
 * use the {@link #height}, {@link #width} and {@link #renderTo} configurations but they
 * are included here to make it easy to get up and running.
 *
 * The grid we created above will contain a header bar with a title ('Simpsons'), a row of
 * column headers directly underneath and finally the grid rows under the headers.
 *
 * **Height config with bufferedRenderer: true**
 *
 * The {@link #height} config must be set when creating a grid using
 * {@link #bufferedRenderer bufferedRenderer}: true _and_ the grid's height is not managed
 * by an owning container layout.  In Ext JS 5.x bufferedRendering is true by default.
 *
 */
Ext.define('Chaching.view.common.grid.ChachingTransactionDetailGrid',{
    extend: 'Ext.grid.Panel',

    requires: [
        'Chaching.view.common.grid.ChachingTransactionDetailGridController',
        'Chaching.view.common.grid.ChachingTransactionDetailGridModel',
        'Chaching.components.plugins.CellEditing',
        'Chaching.components.selection.ChachingSpreadsheetModel',
        'Chaching.components.plugins.Clipboard'
    ],

    controller: 'common-grid-chachingtransactiondetailgrid',
    viewModel: {
        type: 'common-grid-chachingtransactiondetailgrid'
    },
    /**
    * @cfg {object} config object for the grid.
    */
    config: {
        //Set true to use multisearch on grid columns.
        requireMultiSearch: true,
        //Set true to allow multiple column sorting
        requireMultisort: true,
        //Set true to allow data display in groups.
        requireGrouping: true,
        //Module specific columns list. Those columns will be combined with base columns.
        moduleColumns: null,
        //Array of columns names to order the columns. To hide any base column just ignore to add in this list
        columnOrder: null,
        //Set this property to true if having grouped header.
        isGroupedHeader: false,
        //Object grouped header base config
        groupedHeaderBaseConfig: null,
        //Object grouped header module columns config
        groupedHeaderModuleConfig:null,
        //Provide module specific buttons object array to add in the grid's toolbar
        moduleButtons: null,
        //Set to tru if summary is required for grid. Defaults to true
        requireSummary: true,
        //Set module permissions of parent.
        modulePermissions: {
            read: true,
            create: true,
            edit: true,
            destroy: true
        },
        cls: 'chaching-transactiongrid',
        isInViewMode:false
    },
    /**
    * @cfg {string/object} Store for the grid.
    */
    store: null,
    columnLines: true,
    padding: 5,
    frame: false,
    layout: {
        type: 'fit'
    },
    cls: 'chaching-transactiongrid',
    isTransactionDetailsGrid: true,
    viewConfig: {
        getRowClass: function(record) {
            if (record && record.get('isSplit')) {
                return record.get('SplitGroupCls');
            }
            return '';
        }
    },
    initComponent: function() {
        var me = this,
            controller = me.getController();
        var features = [],
            plugins = [],
            dockedItems = [];
        //verify grid configuration first
        if (!me.getModuleColumns()) Ext.Error.raise('Please provide module columns for the grid');
        if (!me.getColumnOrder()) Ext.Error.raise('Please provide column order.');
        if (!me.store) Ext.Error.raise('Please provide store configuration');
        if (me.getIsGroupedHeader() && !me.getGroupedHeaderBaseConfig())Ext.Error.raise('Please provide group header config to add columns in group');
        var columns = me.getColumnsForGrid();
        var modulePermissions = me.getModulePermissions();
        if (columns) {
            if (modulePermissions.destroy) {
                columns.push(me.getDeleteActionColumn());
            }
            me.columns = columns;
        }
        var gridStore = me.store;
        if (typeof (gridStore) === "string") {
            me.store = Ext.create('Chaching.store.' + gridStore);
        }

        //add grouping if required
        if (me.getRequireGrouping()) {
            var groupingFeature = {
                ftype: 'grouping',
                hideGroupedHeader: true,
                startCollapsed: true
            };
            features.push(groupingFeature);
        }
        //add summary feature
        if (me.getRequireSummary()) {
            var summaryFeature = {
                ftype: 'summary',
                dock: 'bottom'
            };
            features.push(summaryFeature);
        }
        if (me.getRequireMultiSearch()) {
            var mutisearch = {
                ptype: 'saki-gms',
                iconColumn: false,
                clearItemIconCls: 'icon-settings',
                pluginId: 'gms',
                height: 32,
                filterOnEnter: false,
                viewModel: {
                    type: 'common-grid-chachingtransactiondetailgrid'
                }
            };
            plugins.push(mutisearch);
        }
        me.selModel = {
            type: 'chachingSpreadsheetSelectionModel',
            columnSelect: true,
            checkboxSelect: false,
            pruneRemoved: false,
            rowNumbererHeaderWidth:55,
            extensible: 'y'
        };
       
        if (modulePermissions.edit || modulePermissions.create) {
            plugins.push({
                ptype: 'chachingClipboard',
                //formats: {
                //    text: {
                //        get: 'getTextData',
                //        put: 'putTextData'
                //    }
                //},
                memory: true
            });
            plugins.push({
                ptype: 'chachingselectionreplicator'
            });
            var editingModel = {
                ptype: 'chachingCellediting',
                pluginId: 'editingPlugin',
                clicksToEdit: 2,
                listeners: {
                    beforeedit: 'onBeforeGridEdit'
                }
            }
            plugins.push(editingModel);
        }
        var defaultButtons = me.getDefaultActionButtons();
        if (defaultButtons && defaultButtons.length > 0) {
            var toolBar = {
                xtype: 'toolbar',
                dock: 'bottom',
                ui: 'plainBottom',
                isActionToolBar:true,
                layout: {
                    type: 'hbox',
                    pack: 'left'
                },
                items: defaultButtons
            };
            dockedItems.push(toolBar);
        }
        me.dockedItems = dockedItems;
        me.plugins = plugins;
        me.features = features;
        me.callParent(arguments);

        ///load all required data for copy/paste if column has combo as editor
        var allColumns = me.columns,
            length = allColumns.length;
        for (var i = 0; i < length; i++) {
            var col = allColumns[i];
            if (col.valueField) { //if column has combo editor
                var dataClassName = col.dataLoadClass;
                if (!dataClassName) {
                    Ext.Error.raise('Please specify dataLoadClass for column' + col.dataIndex);
                }
                var loadClass = Ext.create(dataClassName);
                loadClass.column = col;
                loadClass.load({
                    callback: function(records, operation, success) {
                        if (this.column) {
                            var remoteData = [];
                            Ext.each(records, function(record) {
                                remoteData.push(record.data);
                            });
                            this.column.remoteData = remoteData;
                        }
                    }
                });
            }
        }
    },
    getDefaultActionButtons:function() {
        var me = this,
            modulePermissions = me.getModulePermissions(),
            buttons = [];
        buttons.push('->');
        if (modulePermissions.create || modulePermissions.edit) {
            var numberField = {
                xtype: 'numberfield',
                width: 70,
                itemId:'multiplyOf',
                value: 1,
                minValue: 1,
                maxValue:100
            };
            buttons.push(numberField);
            var addNew = {
                xtype: 'button',
                scale: 'small',
                name: 'AddNewRecord',
                itemId: 'AddNewRecord',
                iconCls: 'fa fa-plus-square',
                ui: 'actionButton',
                tooltip: app.localize('InsertRecord'),
                listeners: {
                    click:'onNewClicked'
                }
            };
            buttons.push(addNew);

            var splitBtn = {
                xtype: 'button',
                scale: 'small',
                name: 'SplitRecord',
                itemId: 'SplitRecord',
                iconCls: 'fa fa-unlink',
                ui: 'actionButton',
                tooltip: app.localize('SplitRecord'),
                listeners: {
                    click: 'onSplitClicked'
                }
            };
            buttons.push(splitBtn);
        }
        var refreshBtn = {
            xtype: 'button',
            scale: 'small',
            name: 'RefreshData',
            itemId: 'RefreshData',
            iconCls: 'fa fa-refresh',
            ui: 'actionButton',
            tooltip: app.localize('RefreshData'),
            listeners: {
                click: 'onRefreshClicked'
            }
        };
        buttons.push(refreshBtn);
        return buttons;
    },
    getDeleteActionColumn: function () {
        return {
            xtype: 'actioncolumn',
            name: 'Delete',
            dataIndex:'Delete',
            scale: 'small',
            iconCls: 'deleteCls',
            tooltip: app.localize('Delete'),
            width: 50,
            hideable: false,
            movable: false,
            resizable: false,
            sortable: false,
            groupable: false,
            menuDisabled: true,
            handler: 'onDeleteClicked'
        };
    },
    getColumnsForGrid:function() {
        var me = this,
            columns = undefined,
            columnOrder = me.getColumnOrder();
        var baseColumns = me.getBaseColumns(),
            moduleColumns = me.getModuleColumns();
        if (me.getIsGroupedHeader()) baseColumns = me.groupBaseColumns(baseColumns);
        if (columnOrder && columnOrder.length > 0 && (baseColumns || moduleColumns)) {
            columns = [];
            var length = columnOrder.length;
            for (var i = 0; i < length; i++) {
                var colName = columnOrder[i];
                //find the column in baseColumns or moduleColumns
                var resultColumn = baseColumns.filter(function (col) { return col.name === colName });
                if (!resultColumn||resultColumn.length===0) resultColumn = moduleColumns.filter(function (col) { return col.name === colName });
                if (!resultColumn||resultColumn.length===0) Ext.Error.raise(colName + ' does not belongs to baseColumns as well as in moduleColumns. Please verify column name exists.');
                else columns.push(resultColumn[0]);///TODO: check visible columns preferences and add column to columns list.
            }
        }
        return columns;
    },
    getBaseColumns: function () {
        var me = this;
        var baseColumns = [
            {
                xtype: 'gridcolumn',
                dataIndex: 'amount',
                name: 'amount',
                hideable:false,
                text: app.localize('Amount').initCap(),
                renderer: Chaching.utilities.ChachingRenderers.amountsRenderer,
                summaryRenderer: Chaching.utilities.ChachingRenderers.amountSummaryRenderer,
                isMandatory:true,
                filterField: {
                    xtype: 'numberfield',
                    hideTrigger: true,
                    emptyText: app.localize('ToolTipAmount')
                },editor: {
                    xtype: 'numberfield',
                    hideTrigger:true,
                    listeners: {
                        change: 'onDetailsAmountChange',
                        focus: 'onDetailsAmountFocus',
                        scope:'controller'
                    }
                }
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'jobNumber',
                name: 'jobNumber',
                text: app.localize('JobDivision'),
                width: '10%',
                hideable: false,
                valueField: 'jobId',///***** Important to set ValueField for column to work copy/paste functionality.
                dataLoadClass: 'Chaching.store.utilities.autofill.JobDivisionStore',
                isMandatory: true,
                filterField: {
                    xtype: 'chachingcombobox',
                    store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                    valueField: 'jobId',
                    displayField: 'jobNumber',
                    queryMode: 'remote',
                    minChars: 2,
                    useDisplayFieldToSearch: true,
                    modulePermissions: {
                        read: abp.auth.isGranted('Pages.Financials.Accounts.Divisions'),
                        create:false,//abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Create'),
                        edit: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Edit'),
                        destroy: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Delete')
                    },
                    primaryEntityCrudApi: {
                        read: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
                        create: abp.appPath + 'api/services/app/divisionUnit/CreateDivisionUnit',
                        update: abp.appPath + 'api/services/app/divisionUnit/UpdateDivisionUnit',
                        destroy: abp.appPath + 'api/services/app/divisionUnit/DeleteDivisionUnit'
                    },
                    createEditEntityType: 'financials.accounts.divisions',
                    createEditEntityGridController: 'financials-accounts-divisionsgrid',
                    entityType: 'Division',
                    isTwoEntityPicker: true,
                    secondEntityDetails: {
                        editCreateModelClass: 'Chaching.model.projects.projectmaintenance.ProjectModel',
                        identificationKey: 'isDivision',
                        entityType: 'Job',
                        createEditEntityType: 'projects.projectmaintenance.projects',
                        createEditEntityGridController: 'Chaching.view.projects.projectmaintenance.ProjectsGridController',
                        modulePermissions: {
                            read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
                            create: false,//abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
                            edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
                            destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete')
                        },
                        secondoryEntityCrudApi: {
                            read: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
                            create: abp.appPath + 'api/services/app/jobUnit/CreateJobUnit',
                            update: abp.appPath + 'api/services/app/jobUnit/UpdateJobUnit',
                            destroy: abp.appPath + 'api/services/app/jobUnit/DeleteJobUnit'
                        }
                    }
                }, editor: {
                    xtype: 'chachingcombobox',
                    store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                    valueField: 'jobId',
                    displayField: 'jobNumber',
                    queryMode: 'remote',
                    minChars: 2,
                    useDisplayFieldToSearch: true,
                    modulePermissions: {
                        read: abp.auth.isGranted('Pages.Financials.Accounts.Divisions'),
                        create: false,//abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Create'),
                        edit: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Edit'),
                        destroy: abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Delete')
                    },
                    primaryEntityCrudApi: {
                        read: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
                        create: abp.appPath + 'api/services/app/divisionUnit/CreateDivisionUnit',
                        update: abp.appPath + 'api/services/app/divisionUnit/UpdateDivisionUnit',
                        destroy: abp.appPath + 'api/services/app/divisionUnit/DeleteDivisionUnit'
                    },
                    createEditEntityType: 'financials.accounts.divisions',
                    createEditEntityGridController: 'financials-accounts-divisionsgrid',
                    entityType: 'Division',
                    isTwoEntityPicker: true,
                    secondEntityDetails: {
                        editCreateModelClass: 'Chaching.model.projects.projectmaintenance.ProjectModel',
                        identificationKey: 'isDivision',
                        entityType: 'Job',
                        createEditEntityType: 'projects.projectmaintenance.projects',
                        createEditEntityGridController: 'Chaching.view.projects.projectmaintenance.ProjectsGridController',
                        modulePermissions: {
                            read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects'),
                            create: false,//abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Create'),
                            edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Edit'),
                            destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.Projects.Delete')
                        },
                        secondoryEntityCrudApi: {
                            read: abp.appPath + 'api/services/app/list/GetJobOrDivisionList',
                            create: abp.appPath + 'api/services/app/jobUnit/CreateJobUnit',
                            update: abp.appPath + 'api/services/app/jobUnit/UpdateJobUnit',
                            destroy: abp.appPath + 'api/services/app/jobUnit/DeleteJobUnit'
                        }
                    }
                }
            },

            {
                xtype: 'gridcolumn',
                dataIndex: 'accountNumber',
                name: 'accountNumber',
                text: app.localize('LineNumber'),
                width: '10%',
                hideable: false,
                valueField: 'accountId',///***** Important to set ValueField for column to work copy/paste functionality.
                dataLoadClass: 'Chaching.store.utilities.autofill.AccountsStore',
                isMandatory: true,
                filterField: {
                    xtype: 'chachingcombobox',
                    store: new Chaching.store.utilities.autofill.AccountsStore(),
                    valueField: 'accountId',
                    displayField: 'accountNumber',
                    queryMode: 'remote',
                    minChars: 2,
                    useDisplayFieldToSearch: true,
                    listConfig: {
                        minWidth: 400,
                        minHeight: 150,
                        maxHeight: 250
                    },
                    modulePermissions: {
                        read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
                        create:false,//abp.auth.isGranted('Pages.Financials.Accounts.Divisions.Create'),
                        edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
                        destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete')
                    },
                    primaryEntityCrudApi: {
                        read: abp.appPath + 'api/services/app/list/GetAccountsList',
                        create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
                        update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
                        destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
                    },
                    createEditEntityType: 'financials.accounts.accounts',
                    createEditEntityGridController: 'financials-accounts-accountsgrid',
                    entityType: 'Account',
                    isTwoEntityPicker: true,
                    secondEntityDetails: {
                        editCreateModelClass: 'Chaching.model.financials.accounts.AccountsModel',
                        identificationKey: 'isCorporate',
                        entityType: 'Line',
                        createEditEntityType: 'projects.projectmaintenance.linenumbers',
                        createEditEntityGridController: 'Chaching.view.projects.projectmaintenance.LineNumbersGridController',
                        modulePermissions: {
                            read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs'),
                            create: false,//abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Create'),
                            edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Edit'),
                            destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Delete')
                        },
                        secondoryEntityCrudApi: {
                            read: abp.appPath + 'api/services/app/list/GetAccountsList',
                            create: abp.appPath + 'api/services/app/linesUnit/CreateLineUnit',
                            update: abp.appPath + 'api/services/app/linesUnit/UpdateLineUnit',
                            destroy: abp.appPath + 'api/services/app/linesUnit/DeleteLineUnit'
                        }
                    }
                }, editor: {
                    xtype: 'chachingcombobox',
                    store: new Chaching.store.utilities.autofill.AccountsStore(),
                    valueField: 'accountId',
                    displayField: 'accountNumber',
                    queryMode: 'remote',
                    minChars: 2,
                    useDisplayFieldToSearch: true,
                    listConfig: {
                        minWidth: 400,
                        minHeight: 150,
                        maxHeight: 250
                    },
                    modulePermissions: {
                        read: abp.auth.isGranted('Pages.Financials.Accounts.Accounts'),
                        create: false,//abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Create'),
                        edit: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Edit'),
                        destroy: abp.auth.isGranted('Pages.Financials.Accounts.Accounts.Delete')
                    },
                    primaryEntityCrudApi: {
                        read: abp.appPath + 'api/services/app/list/GetAccountsList',
                        create: abp.appPath + 'api/services/app/accountUnit/CreateAccountUnit',
                        update: abp.appPath + 'api/services/app/accountUnit/UpdateAccountUnit',
                        destroy: abp.appPath + 'api/services/app/accountUnit/DeleteAccountUnit'
                    },
                    createEditEntityType: 'financials.accounts.accounts',
                    createEditEntityGridController: 'financials-accounts-accountsgrid',
                    entityType: 'Account',
                    isTwoEntityPicker: true,
                    secondEntityDetails: {
                        editCreateModelClass: 'Chaching.model.financials.accounts.AccountsModel',
                        identificationKey: 'isCorporate',
                        entityType: 'Line',
                        createEditEntityType: 'projects.projectmaintenance.linenumbers',
                        createEditEntityGridController: 'Chaching.view.projects.projectmaintenance.LineNumbersGridController',
                        modulePermissions: {
                            read: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs'),
                            create: false,//abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Create'),
                            edit: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Edit'),
                            destroy: abp.auth.isGranted('Pages.Projects.ProjectMaintenance.ProjectCOAs.Delete')
                        },
                        secondoryEntityCrudApi: {
                            read: abp.appPath + 'api/services/app/list/GetAccountsList',
                            create: abp.appPath + 'api/services/app/linesUnit/CreateLineUnit',
                            update: abp.appPath + 'api/services/app/linesUnit/UpdateLineUnit',
                            destroy: abp.appPath + 'api/services/app/linesUnit/DeleteLineUnit'
                        }
                    }
                }
            },

            //{
            //    xtype: 'gridcolumn',
            //    dataIndex: 'accountNumber',
            //    name: 'accountNumber',
            //    hideable: false,
            //    text: app.localize('LineNumber').initCap(),
            //    width: '10%',
            //    valueField: 'accountId',
            //    dataLoadClass: 'Chaching.store.utilities.autofill.AccountsStore',
            //    isMandatory: true,
            //    filterField: {
            //        xtype: 'combobox',
            //        store: new Chaching.store.utilities.autofill.AccountsStore(),
            //        valueField: 'accountNumber',
            //        displayField: 'accountNumber',
            //        queryMode: 'remote',
            //        minChars: 2,
            //        useDisplayFieldToSearch: true,
            //        listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
            //        emptyText: app.localize('SearchText')
            //    }, editor: {
            //        xtype: 'combobox',
            //        store: new Chaching.store.utilities.autofill.AccountsStore(),
            //        valueField: 'accountId',
            //        displayField: 'accountNumber',
            //        queryMode: 'remote',
            //        minChars: 2,
            //        listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
            //        emptyText: app.localize('SearchText'),
            //        listeners: {
            //            beforequery: 'beforeAccountQuery'
            //        }
            //    }
            //},


            {
                xtype: 'gridcolumn',
                dataIndex: 'subAccountNumber1',
                name: 'subAccountNumber1',
                text: app.localize('SubAccount1').initCap(),
                width: '10%',
                valueField: 'subAccountId1',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId1', 'subAccountNumber1'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId1', 'subAccountNumber1')
            },{
                xtype: 'gridcolumn',
                dataIndex: 'subAccountNumber2',
                name: 'subAccountNumber2',
                text: app.localize('SubAccount2').initCap(),
                width: '10%',
                valueField: 'subAccountId2',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId2', 'subAccountNumber2'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId2', 'subAccountNumber2')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccountNumber3',
                name: 'subAccountNumber3',
                text: app.localize('SubAccount3').initCap(),
                width: '10%',
                valueField: 'subAccountId3',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId3', 'subAccountNumber3'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId3', 'subAccountNumber3')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccountNumber4',
                name: 'subAccountNumber4',
                text: app.localize('SubAccount4').initCap(),
                width: '10%',
                valueField: 'subAccountId4',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId4', 'subAccountNumber4'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId4', 'subAccountNumber4')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccountNumber5',
                name: 'subAccountNumber5',
                text: app.localize('SubAccount5').initCap(),
                width: '10%',
                valueField: 'subAccountId5',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId5', 'subAccountNumber5'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId5', 'subAccountNumber5')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccountNumber6',
                name: 'subAccountNumber6',
                text: app.localize('SubAccount6').initCap(),
                width: '10%',
                valueField: 'subAccountId6',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId6', 'subAccountNumber6'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId6', 'subAccountNumber6')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccountNumber7',
                name: 'subAccountNumber7',
                text: app.localize('SubAccount7').initCap(),
                width: '10%',
                valueField: 'subAccountId7',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId7', 'subAccountNumber7'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId7', 'subAccountNumber7')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccountNumber8',
                name: 'subAccountNumber8',
                text: app.localize('SubAccount8').initCap(),
                width: '10%',
                valueField: 'subAccountId8',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId8', 'subAccountNumber8'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId8', 'subAccountNumber8')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccountNumber9',
                name: 'subAccountNumber9',
                text: app.localize('SubAccount9').initCap(),
                width: '10%',
                valueField: 'subAccountId9',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId9', 'subAccountNumber9'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId9', 'subAccountNumber9')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccountNumber10',
                name: 'subAccountNumber10',
                text: app.localize('SubAccount10').initCap(),
                width: '10%',
                valueField: 'subAccountId10',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId10', 'subAccountNumber10'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId10', 'subAccountNumber10')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'typeOf1099T4',
                name: 'typeOf1099T4',
                text: app.localize('Ten99Code').initCap(),
                width: '10%',
                valueField: 'typeOf1099T4Id',
                dataLoadClass: 'Chaching.store.utilities.autofill.T41099Store',
                filterField: {
                    xtype: 'combobox',
                    store: new Chaching.store.utilities.autofill.T41099Store(),
                    valueField: 'typeOf1099T4',
                    displayField: 'typeOf1099T4',
                    queryMode: 'remote',
                    minChars: 2,
                    useDisplayFieldToSearch: true,
                    listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                    emptyText: app.localize('SearchText')
                }, editor: {
                    xtype: 'combobox',
                    store: new Chaching.store.utilities.autofill.T41099Store(),
                    valueField: 'typeOf1099T4Id',
                    displayField: 'typeOf1099T4',
                    queryMode: 'remote',
                    minChars: 2,
                    useDisplayFieldToSearch: true,
                    listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                    emptyText: app.localize('SearchText')
                }
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'itemMemo',
                name: 'itemMemo',
                text: app.localize('ItemMemo').initCap(),
                width: '10%',
                hideable: false,
                filterField:Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipItemMemo')),
                editor: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipItemMemo'))
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'accountRef1',
                name: 'accountRef1',
                text: app.localize('AccountRef1').initCap(),
                width: '9%',
                filterField: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef1')),
                editor: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef1'))
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'accountRef2',
                name: 'accountRef2',
                text: app.localize('AccountRef2').initCap(),
                width: '9%',
                filterField: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef2')),
                editor: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef2'))
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'accountRef3',
                name: 'accountRef3',
                text: app.localize('AccountRef3').initCap(),
                width: '9%',
                filterField: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef3')),
                editor: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef3'))
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'accountRef4',
                name: 'accountRef4',
                text: app.localize('AccountRef4').initCap(),
                width: '9%',
                filterField: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef4')),
                editor: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef4'))
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'accountRef5',
                name: 'accountRef5',
                text: app.localize('AccountRef5').initCap(),
                width: '9%',
                filterField: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef5')),
                editor: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef5'))
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'accountRef6',
                name: 'accountRef6',
                text: app.localize('AccountRef6').initCap(),
                width: '9%',
                filterField: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef6')),
                editor: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef6'))
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'accountRef7',
                name: 'accountRef7',
                text: app.localize('AccountRef7').initCap(),
                width: '9%',
                filterField: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef7')),
                editor: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef7'))
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'accountRef8',
                name: 'accountRef8',
                text: app.localize('AccountRef8').initCap(),
                width: '9%',
                filterField: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef8')),
                editor: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef8'))
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'accountRef9',
                name: 'accountRef9',
                text: app.localize('AccountRef9').initCap(),
                width: '9%',
                filterField: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef9')),
                editor: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef9'))
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'accountRef10',
                name: 'accountRef10',
                text: app.localize('AccountRef10').initCap(),
                width: '9%',
                filterField: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef10')),
                editor: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipAccountRef10'))
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'ledgerReference',
                name: 'ledgerReference',
                text: app.localize('InvoiceRef').initCap(),
                width: '10%',
                filterField: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipInvoiceRef')),
                editor: Chaching.utilities.ChachingGlobals.getTextField(app.localize('ToolTipInvoiceRef'))
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'isAsset',
                name: 'isAsset',
                text: app.localize('IsAsset').initCap(),
                sortable: false,
                groupable: false,
                renderer: Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer,
                width: '7%',
                editor: {
                    xtype: 'checkboxfield'
                }
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'taxRebateNumber',
                name: 'taxRebateNumber',
                text: app.localize('TaxRebate'),
                width: '10%',
                valueField: 'taxRebateId',
                dataLoadClass: 'Chaching.store.utilities.autofill.TaxRebateStore',
                filterField: {
                    xtype: 'combobox',
                    store: new Chaching.store.utilities.autofill.TaxRebateStore(),
                    valueField: 'taxRebateNumber',
                    displayField: 'taxRebateNumber',
                    queryMode: 'remote',
                    minChars: 2,
                    useDisplayFieldToSearch: true,
                    listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                    emptyText: app.localize('SearchText')
                },editor: {
                    xtype: 'combobox',
                    store: new Chaching.store.utilities.autofill.TaxRebateStore(),
                    valueField: 'taxRebateId',
                    displayField: 'taxRebateNumber',
                    queryMode: 'remote',
                    minChars: 2,
                    useDisplayFieldToSearch: true,
                    listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                    emptyText: app.localize('SearchText')
                }
            }
        ];
        return baseColumns;
    },
    groupBaseColumns:function(baseColumns) {
        var me = this,
            groupedBaseColumns = [],
            groupHeaderConfig = me.getGroupedHeaderBaseConfig();
        if (groupHeaderConfig&&groupHeaderConfig.length>0) {
            var groupLength = groupHeaderConfig.length;
            for (var i = 0; i < groupLength; i++) {
                var group = groupHeaderConfig[i],
                    groupHeaderText = group.groupHeaderText,
                    childColumns = group.childColumnNames,
                    childWidths = group.childColumnWidths,
                    childColumnItems = [];
                if (childColumns && childColumns.length > 1) {
                    if (!childWidths && childWidths.length !== childColumns.length) {
                        Ext.Error.raise('Child column name length must be equals to child columns width length');
                        return false;
                    }
                    var childLength = childColumns.length;
                    for (var j = 0; j < childLength; j++) {
                        var cColName = childColumns[j];
                        var columnInBase = baseColumns.filter(function (o) { return o.name === cColName; });
                        if (!columnInBase||columnInBase.length===0) {
                            Ext.Error.raise(cColName + ' not exists in baseColumns. Please provide a valid name');
                            return false;
                        }
                        columnInBase[0].width = childWidths[j];
                        childColumnItems.push(columnInBase[0]);
                    }
                    var groupedCol = {
                        text: groupHeaderText,
                        name: group.columnName,
                        columns: childColumnItems
                    };
                    groupedBaseColumns.push(groupedCol);
                }
            }
        }
        if (groupedBaseColumns.length > 0) {
            for (var k = 0; k < baseColumns.length; k++) {
                groupedBaseColumns.push(baseColumns[k]);
            }
             return groupedBaseColumns;
        }
        return baseColumns;
    }
});
