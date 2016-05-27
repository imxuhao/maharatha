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
        'Ext.grid.selection.SpreadsheetModel',
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
        cls: 'chaching-transactiongrid'
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
        if (columns) {
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
            type: 'spreadsheet',
            columnSelect: true,
            checkboxSelect: false,
            pruneRemoved: false,
            extensible: 'y'
        };
        var modulePermissions = me.getModulePermissions();
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
        if (modulePermissions.destroy) {
            var deleteBtn = {
                xtype: 'button',
                scale: 'small',
                name: 'DeleteRecord',
                itemId: 'DeleteRecord',
                iconCls: 'fa fa-trash',
                ui: 'actionButton',
                tooltip: app.localize('DeleteRecord'),
                listeners: {
                    click: 'onDeleteClicked'
                }
            };
            buttons.push(deleteBtn);
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
                    emptyText: app.localize('ToolTipAmount')
                },editor: {
                    xtype:'textfield'
                }
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'job',
                name: 'job',
                text: app.localize('JobDivision'),
                width: '10%',
                hideable: false,
                valueField: 'jobId',///***** Important to set ValueField for column to work copy/paste functionality.
                dataLoadClass: 'Chaching.store.utilities.autofill.JobDivisionStore',
                isMandatory: true,
                filterField: {
                    xtype: 'combobox',
                    store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                    valueField: 'job',
                    displayField: 'job',
                    queryMode: 'remote',
                    minChars: 2,
                    useDisplayFieldToSearch: true,
                    listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                    emptyText: app.localize('SearchText')
                }, editor: {
                    xtype: 'combobox',
                    store: new Chaching.store.utilities.autofill.JobDivisionStore(),
                    valueField: 'jobId',
                    displayField: 'job',
                    queryMode: 'remote',
                    minChars: 2,
                    listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                    emptyText: app.localize('SearchText')
                }
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'account',
                name: 'account',
                hideable: false,
                text: app.localize('LineNumber').initCap(),
                width: '10%',
                valueField: 'accountId',
                dataLoadClass: 'Chaching.store.utilities.autofill.AccountsStore',
                isMandatory: true,
                filterField: {
                    xtype: 'combobox',
                    store: new Chaching.store.utilities.autofill.AccountsStore(),
                    valueField: 'account',
                    displayField: 'account',
                    queryMode: 'remote',
                    minChars: 2,
                    useDisplayFieldToSearch: true,
                    listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                    emptyText: app.localize('SearchText')
                }, editor: {
                    xtype: 'combobox',
                    store: new Chaching.store.utilities.autofill.AccountsStore(),
                    valueField: 'accountId',
                    displayField: 'account',
                    queryMode: 'remote',
                    minChars: 2,
                    listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                    emptyText: app.localize('SearchText'),
                    listeners: {
                        beforequery: 'beforeAccountQuery'
                    }
                }
            },{
                xtype: 'gridcolumn',
                dataIndex: 'subAccount1',
                name: 'subAccount1',
                text: app.localize('SubAccount1').initCap(),
                width: '10%',
                valueField: 'subAccountId1',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccount1', 'subAccount1'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId1', 'subAccount1')
            },{
                xtype: 'gridcolumn',
                dataIndex: 'subAccount2',
                name: 'subAccount2',
                text: app.localize('SubAccount2').initCap(),
                width: '10%',
                valueField: 'subAccountId2',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccount2', 'subAccount2'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId2', 'subAccount2')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccount3',
                name: 'subAccount3',
                text: app.localize('SubAccount3').initCap(),
                width: '10%',
                valueField: 'subAccountId3',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccount3', 'subAccount3'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId3', 'subAccount3')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccount4',
                name: 'subAccount4',
                text: app.localize('SubAccount4').initCap(),
                width: '10%',
                valueField: 'subAccountId4',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccount4', 'subAccount4'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId4', 'subAccount4')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccount5',
                name: 'subAccount5',
                text: app.localize('SubAccount5').initCap(),
                width: '10%',
                valueField: 'subAccountId5',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccount5', 'subAccount5'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId5', 'subAccount5')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccount6',
                name: 'subAccount6',
                text: app.localize('SubAccount6').initCap(),
                width: '10%',
                valueField: 'subAccountId6',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccount6', 'subAccount6'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId6', 'subAccount6')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccount7',
                name: 'subAccount7',
                text: app.localize('SubAccount7').initCap(),
                width: '10%',
                valueField: 'subAccountId7',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccount7', 'subAccount7'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId7', 'subAccount7')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccount8',
                name: 'subAccount8',
                text: app.localize('SubAccount8').initCap(),
                width: '10%',
                valueField: 'subAccountId8',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccount8', 'subAccount8'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId8', 'subAccount8')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccount9',
                name: 'subAccount9',
                text: app.localize('SubAccount9').initCap(),
                width: '10%',
                valueField: 'subAccountId9',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccount9', 'subAccount9'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId9', 'subAccount9')
            }, {
                xtype: 'gridcolumn',
                dataIndex: 'subAccount10',
                name: 'subAccount10',
                text: app.localize('SubAccount10').initCap(),
                width: '10%',
                valueField: 'subAccountId10',
                dataLoadClass: 'Chaching.store.utilities.autofill.SubAccountsStore',
                filterField: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccount10', 'subAccount10'),
                editor: Chaching.utilities.ChachingGlobals.getSubAccountCombo('subAccountId10', 'subAccount10')
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
                dataIndex: 'taxRebate',
                name: 'taxRebate',
                text: app.localize('TaxRebate'),
                width: '10%',
                valueField: 'taxRebateId',
                dataLoadClass: 'Chaching.store.utilities.autofill.TaxRebateStore',
                filterField: {
                    xtype: 'combobox',
                    store: new Chaching.store.utilities.autofill.TaxRebateStore(),
                    valueField: 'taxRebate',
                    displayField: 'taxRebate',
                    queryMode: 'remote',
                    minChars: 2,
                    useDisplayFieldToSearch: true,
                    listConfig: Chaching.utilities.ChachingGlobals.comboListConfig,
                    emptyText: app.localize('SearchText')
                },editor: {
                    xtype: 'combobox',
                    store: new Chaching.store.utilities.autofill.TaxRebateStore(),
                    valueField: 'taxRebateId',
                    displayField: 'taxRebate',
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