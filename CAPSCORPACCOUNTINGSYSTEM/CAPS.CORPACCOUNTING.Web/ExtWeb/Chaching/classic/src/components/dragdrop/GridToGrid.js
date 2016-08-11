/**
 * This Class is created for drag and drop from one grid to another grid.
 * Author: Krishna Garad
 * Date: 26/04/2016
 */
/**
 * @class Chaching.components.dragdrop.GridToGrid
 * The Drag & Drop control from one grid to another.
 * @alias widget.chachingGridDragDrop
 *
 *     @example usage 
 *     Ext.create('Chaching.components.dragdrop.GridToGrid', {
 *      config:{
 *              columns:[{text:'Column1',dataIndex:'Column1'},{text:'Column2',dataIndex:'Column2'}],
 *              store:{
 *                      fields:[{name:'Column1'},{name:'Column2'}],
 *                      data:[{Column1:'rec1',Column2:'10'},{Column1:'rec2',Column2:'20'},{Column1:'rec3',Column2:'30'},{Column1:'rec4',Column2:'40'},{Column1:'rec5',Column2:'50'}]
 *                     }
 *              },
 * 
 *      renderTo:Ext.getBody() 
 *     });
 */

Ext.define('Chaching.components.dragdrop.GridToGrid', {
    extend: 'Ext.container.Container',
    alias: 'widget.chachingGridDragDrop',
    requires: [
        'Ext.grid.*',
        'Ext.layout.container.HBox',
        'Chaching.components.selection.CheckboxSelectionModel'
    ],
    /**
    * @cfg {object}
    * A config option for columns, store, left title and right title
    */
    config: {
       /**
       * A config option for columns      
       */
        columns: null,
        /**
        * @cfg {object/string}
        * A config option store to assign to grids
        */
        store: null,
        /**
        * @cfg {object/string}
        * A config option store to assign to left grid
        */
        leftStore: null,
        /**
        * @cfg {object}
        * A config option to load the leftStore with given params.
        * Only used when loadStoreOnCreate=true
        */
        leftStoreLoadOptions:null,
        /**
        * @cfg {object/string}
        * A config option store to assign to right grid
        */
        rightStore: null,
        /**
        * @cfg {object}
        * A config option to load the rightStore with given params.
        */
        rightStoreLoadOptions: null,
        /**
        * @cfg {boolean}
        * A config option to load the store(s) on create.
        * Only used when loadStoreOnCreate=true
        */
        loadStoreOnCreate:false,
        /**
        * @cfg {String}
        * A config option for left grid title
        * Defuaults to 'Left Group'
        */
        leftTitle: 'Left Group',
        /**
        * @cfg {String}
        * A config option for right grid title
        * Defuaults to 'Right Group'
        */
        rightTitle: 'Right Group',
        /**
        * @cfg {object}
        * A config option for selModel for grids
        * Defuaults to 'rowmodel'
        * Override selModelConfig to checkboxselection model if required.
        */
        selModelConfig: {
            selType: 'rowmodel',
            multiSelect:true
        },
        /**
        * @cfg {boolean}
        * Require multisearch option
        * Defuaults to 'false'      
        */
        requireMultiSearch: false,
        /**
        * @cfg {String}
        * Config option for dragDrop Direction
        * Defaults to 'both'
        * Possible values are lefttoright,righttoleft and both
        * Defuaults to 'false'      
        */
        dragDropDirection: 'both',
        /**
        * @cfg {Object}
        * Range selector config  
        * Defaults to Null
        */
        rangeSelectorConfig: null,
        /**
        * @cfg {Object}
        * IsSearchRequiredFilters config  
        * Defaults to false
        */
        isSearchRequiredFilters: false,
        /**
        * @cfg {Object}
        * IsSearchRequiredFilters config  
        * filtersConfig to null
        */
        filtersConfig: null
    },
    /**
    * @hide
    * @private
    * @cfg {String/object} layout
    */
    layout: {
        type: 'hbox',
        align: 'stretch',
        padding: 5
    },
    width: '100%',
    height: 220,
    /**
     * Called automatically by the framework.
     * @private     
     */
    initComponent: function() {
        var me = this;
        var leftGroup = me.id + 'LeftGroup',
            rightGroup = me.id + 'RightGroup';
        var columns = me.getColumns();
        if (!columns) {
            Ext.Error.raise('Please provide columns config');
        }
        //control can have single store for both grids or can have left and right store.
        //if store config is provided the left and right stores will be ignored
        var singleStore = me.getStore();
        var leftStore = me.getLeftStore(),
            rightStore = me.getRightStore();
        if (!singleStore && (!leftStore && !rightStore)) {
            Ext.Error.raise('Please provide store/left and right store config');
        }
        if (singleStore) {
            if (typeof(singleStore)==="string") {
                leftStore = Ext.create(singleStore);
                rightStore = Ext.create(singleStore);
            }
        } else {
            if (typeof(leftStore)==="string") {
                leftStore = Ext.create(leftStore);
            }
            if (typeof(rightStore) === "string") {
                rightStore = Ext.create(rightStore);
            }
        }
        me.setLeftStore(leftStore);
        me.setRightStore(rightStore);
        if (me.getLoadStoreOnCreate()) {
            leftStore.load(me.getLeftStoreLoadOptions());
            rightStore.load(me.getRightStoreLoadOptions());
        }
        var leftPlugins = [], rightPlugins = [];
        var buttons = [];
        switch (me.getDragDropDirection()) {
            case 'lefttoright':
                leftPlugins.push({
                    ptype: 'gridviewdragdrop',
                    containerScroll: true,
                    dragGroup: leftGroup,
                    dropGroup: rightGroup
                });
                buttons.push({
                    xtype: 'button',
                    itemId:'leftToRight',
                    scale: 'small',
                    ui: 'actionButton',
                    iconCls: 'fa fa-arrow-circle-o-right',
                    tooltip:'Move selected.',
                    width: 40,
                    handler:me.onLeftToRight
                });
                break;
            case 'righttoleft':
                rightPlugins.push({
                    ptype: 'gridviewdragdrop',
                    containerScroll: true,
                    dragGroup: rightGroup,
                    dropGroup: leftGroup
                });
                buttons.push({
                    xtype: 'button',
                    itemId: 'rightToLeft',
                    scale: 'small',
                    ui: 'actionButton',
                    iconCls: 'fa fa-arrow-circle-o-left',
                    tooltip: 'Move selected.',
                    width: 40,
                    handler: me.onRightToLeft
                });
                break;
            default://both way
                leftPlugins.push({
                    ptype: 'gridviewdragdrop',
                    containerScroll: true,
                    dragGroup: leftGroup,
                    dropGroup: rightGroup
                });
                rightPlugins.push({
                    ptype: 'gridviewdragdrop',
                    containerScroll: true,
                    dragGroup: rightGroup,
                    dropGroup: leftGroup
                });
                buttons.push({
                    xtype: 'button',
                    itemId: 'leftToRight',
                    scale: 'small',
                    ui: 'actionButton',
                    iconCls: 'fa fa-arrow-circle-o-right',
                    tooltip: 'Move selected.',
                    width: 40,
                    handler: me.onLeftToRight
                });
                buttons.push({ xtype: 'tbspacer', height:30 });
                buttons.push({
                    xtype: 'button',
                    itemId: 'rightToLeft',
                    scale: 'small',
                    ui: 'actionButton',
                    iconCls: 'fa fa-arrow-circle-o-left',
                    tooltip: 'Move selected.',
                    width: 40,
                    handler: me.onRightToLeft
                });
                break;

        }
        var plugins = [];
        if (me.getRequireMultiSearch()) {
            plugins.push({
                ptype: 'saki-gms',
                iconColumn: false,
                clearItemIconCls: 'icon-settings',
                pluginId: 'gms',
                height: 32,
                filterOnEnter: false
            });
        }
        var rangeSelector = null;
        if (me.getRangeSelectorConfig()) {
            rangeSelector = {
                xtype: 'toolbar',
                dock: 'bottom',
                ui: 'plain',
                layout: {
                    type:'hbox'
                },
                items:[
                {
                    xtype: 'textfield',
                    ui: 'fieldLabelTop',
                    width:'70%',
                    fieldLabel: app.localize('Range').initCap(),
                    emptyText: app.localize('RangeMessage'),
                    maxLength: 200,
                    selectOnFocus: true,
                    itemId:'rangeBox',
                    enforceMaxLength: true,
                    regex: /^(([0-9-]+([;]{0,1}[0-9-]+)*)*|([0-9-]+([.]{2})+[0-9-]+([;]{0,1}[0-9-]+)*)*)$/,
                    triggers: {
                        rangeSelector: {
                            cls: 'rangeSelectorTriggerCls',
                            handler:me.onRangeActionClick
                        }
                    },
                    listeners: {
                        specialkey:me.onRangeActionEnter
                    }
                },'->', {
                    xtype: 'button',
                    ui: 'actionButton',
                    text: app.localize('Reset').toUpperCase(),
                    iconCls: 'fa fa-hourglass-start',
                    handler:me.resetRangeFilters
                }]
            }
        }
        me.items = [
            {
                xtype: 'gridpanel',
                itemId: 'leftGrid',
                flex:1,
                //width: '47%',
                selModel: me.getSelModelConfig(),
                store: leftStore,
                title: me.getLeftTitle(),
                columns: columns,
                padding: 10,
                plugins: plugins,
                cls: 'chaching-grid',
                ui: 'dragDropPanel',
                border: false,
                dockedItems: rangeSelector,
                viewConfig: {
                    plugins: leftPlugins,
                    listeners: {
                        drop: function (node, data, dropRec, dropPosition) {
                            me.doSaveOperation('rightToLeft', data.records);
                        }
                    }
                }
            }, {
                xtype: 'panel',
                itemId: 'toolPanel',
                border: false,
                frame:false,
                ui: 'dragDropPanel',
                width: '6%',
                padding:10,
                layout: {
                    type: 'fit'
                },
                items:[
                {
                    xtype: 'toolbar',
                    //ui:'plain',
                    dock: 'center',
                    height: '100%',
                    baseCls:'',
                    bodyStyle: {
                        'background-color': 'transparent',
                        'border-color': 'transparent',
                        'border-style': 'transparent'
                    },
                    layout: {
                        type:'vbox',
                        pack:'center'
                    },
                    items:buttons
                }]
        }, {
                xtype: 'gridpanel',
                itemId: 'rightGrid',
                flex: 1,
                //width: '47%',
                selModel: me.getSelModelConfig(),
                store: rightStore,
                title: me.getRightTitle(),
                columns: columns,
                padding: 10,
                plugins: plugins,
                cls: 'chaching-grid',
                ui: 'dragDropPanel',
                border: false,
                dockedItems: rangeSelector,
                viewConfig: {
                    plugins: rightPlugins,
                    listeners: {
                        drop: function (node, data, dropRec, dropPosition) {
                            me.doSaveOperation('leftToRight', data.records);
                        }
                    }
                }
            }
        ];
        me.callParent(arguments);
    },
    /**
    * Records moved from left to right.
    * @param {Object} button
    */
    onLeftToRight:function(btn) {
        var me = btn.up('chachingGridDragDrop');
        var leftGrid = me.getLeftGrid(),
            rightGrid = me.getRightGrid();
        var leftSelModel = leftGrid.getSelectionModel(),
            leftSelected = leftSelModel.getSelection();

        if (leftSelected && leftSelected.length > 0) {
            var rightStore = me.getRightStore();
            var leftStore = me.getLeftStore();
            for (var i = 0; i < leftSelected.length; i++) {
                var selected = leftSelected[i];
                rightStore.insert(rightStore.getTotalCount() + 1, selected);
                leftStore.remove(selected);
            }
            me.doSaveOperation('leftToRight', leftSelected);
        }
        
    },
    /**
    * Records moved from right to left.
    * @param {Object} button
    */
    onRightToLeft: function (btn) {
        var me = btn.up('chachingGridDragDrop');
        var leftGrid = me.getLeftGrid(),
            rightGrid = me.getRightGrid();
        var rightSelModel = rightGrid.getSelectionModel(),
            rightSelected = rightSelModel.getSelection();

        if (rightSelected && rightSelected.length > 0) {
            var rightStore = me.getRightStore();
            var leftStore = me.getLeftStore();
            for (var i = 0; i < rightSelected.length; i++) {
                var selected = rightSelected[i];
                leftStore.insert(leftStore.getTotalCount() + 1, selected);
                rightStore.remove(selected);
            }
            me.doSaveOperation('rightToLeft', rightSelected);
        }
    },
    /**
    * Do save operation as you like
    * @param {String} direction values are leftToRight and rightToLeft
    * @param{Array}records moved
    */
    doSaveOperation:function(direction,records){},
    /**
    * Get leftGrid component.   
    */
    getLeftGrid:function() {
        var me = this;
        return me.down('gridpanel[itemId=leftGrid]');
    },
    /**
    * Get rightGrid component.   
    */
    getRightGrid: function () {
        var me = this;
        return me.down('gridpanel[itemId=rightGrid]');
    },
    onRangeActionClick: function(field, trigger, e) {
        var me = this,
           gridToGrid = me.up('chachingGridDragDrop');
        if (field.getValue()&&field.isValid()) {
            gridToGrid.onRangeAction(gridToGrid, field);
        } else {
            abp.notify.warn(app.localize('InvalidRange'));
        }
    },
    onRangeActionEnter: function(field, e, eOpts) {
        var me = this,
            gridToGrid = me.up('chachingGridDragDrop');
        if (e.getKey() === e.ENTER) {
            e.stopEvent();
            if (field.isValid() && field.getValue())
                gridToGrid.onRangeAction(gridToGrid, field);
            else abp.notify.warn(app.localize('InvalidRange'));
        }
    },
    onRangeAction:function(me,field) {
        var parentGrid = field.up('grid'),
            parentGridStore = parentGrid.getStore(),
            parentGridSelModel=parentGrid.getSelectionModel(),
            rangeSelectorConfig = me.getRangeSelectorConfig();
        if (parentGridStore) {
            ///TODO: Create new multi range comparator on server filters.
            parentGridStore.clearFilter(true);
            parentGridSelModel.deselectAll();
            var filters = [];
            var filter = new Ext.util.Filter({
                entity: rangeSelectorConfig.entityName,
                searchTerm: field.getValue(),
                //comparator: 6,
                //dataType: 1,
                //operator: 'in',
                comparator: 5,
                dataType: 1,
                isMultiRange: true,
                property: rangeSelectorConfig.propertyName,
                value: field.getValue()
            });
            if (parentGridStore.remoteFilter) {
                filters.push(filter);
                parentGridStore.load({
                    filters: filters,
                    callback: function(records, operation, success) {
                        if (success && records && records.length > 0) {
                            parentGridSelModel.select(records);
                        }
                        else {
                            if (operation.exception) {
                                abp.notify.warn(operation.error.statusText);
                            }
                        }
                    }
                });
            } else {
                var filterValue = me.getFilterRanges(field.getValue());
                if (filterValue) {
                    filter.searchTerm = filterValue;
                    filter.setValue(filterValue);
                    parentGridStore.isRangeFilters = true;
                    //parentGridStore.addFilter(filter, false);
                    parentGridStore.filterBy(function (record, id) {
                        var val = Ext.Array.contains(filterValue, parseInt(record.get(filter.getProperty())));
                        //parentGridSelModel.select(record);
                        return val;
                    }, this);
                    
                }
            }
        }
    },
    resetRangeFilters:function(btn) {
        var me = this,
            parentGrid = me.up('grid'),
            rangeInput=parentGrid.down('textfield[itemId=rangeBox]'),
            parentGridStore = parentGrid.getStore();
        parentGridStore.clearFilter();
        var dragDropControl = parentGrid.up('chachingGridDragDrop'),
            filters = [];
        if (dragDropControl.filtersConfig) {
            filters.push(dragDropControl.filtersConfig);
            parentGridStore.getProxy().setExtraParam('filters', filters);
            parentGridStore.load({ sorting: null });
        }
        else {
            parentGridStore.load({ sorting: null, filters: null });
        }
        rangeInput.reset();
    },
    getFilterRanges: function (stringValue) {
        var outPut = [];
        if (stringValue) {
            var ranges = stringValue.split(';');
            if (ranges && ranges.length > 0) {
                for (var i = 0; i < ranges.length; i++) {
                    var range = ranges[i].split('..');
                    if (range&&range.length>0) {
                        var startValue = parseInt(range[0]),
                            endValue = parseInt(range[1]);
                        outPut.push(startValue);
                        if (endValue < startValue) {
                            abp.notify.error(app.localize('InvalidRange'));
                            return [];
                        }
                        if (startValue > 0 && endValue > 0) {
                            while (startValue !== endValue) {
                                startValue = parseInt(startValue + 1);
                                outPut.push(startValue);
                            }
                        }
                    }
                }
            }
        }
        //outPut = outPut.replace(/,$/, "");
        return outPut;
    }
});