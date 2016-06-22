/**
 * This file contains the custom combobox containing grid as picker field.
 * Author: Krishna Garad
 * Date:14/06/2016
 */
/**
 * A combobox control with support for autocomplete, remote loading, and many other features.
 *
 * A ComboBox is like a combination of a traditional HTML text `<input>` field and a `<select>`
 * field; the user is able to type freely into the field, and/or pick values from a dropdown selection
 * list. The user can input any value by default, even if it does not appear in the selection list;
 * to prevent free-form values and restrict them to items in the list, set {@link #forceSelection} to `true`.
 *
 * The selection list's options are populated from any {@link Ext.data.Store}, including remote
 * stores. The data items in the store are mapped to each option's displayed text and backing value via
 * the {@link #valueField} and {@link #displayField} configurations, respectively.
 *
 * If your store is not remote, i.e. it depends only on local data and is loaded up front, you should be
 * sure to set the {@link #queryMode} to `'local'`, as this will improve responsiveness for the user.
 *
 * # Example usage:
 *
 *     @example
 *     // The data store containing the list of states
 *     var states = Ext.create('Ext.data.Store', {
 *         fields: ['abbr', 'name'],
 *         data : [
 *             {"abbr":"AL", "name":"Alabama"},
 *             {"abbr":"AK", "name":"Alaska"},
 *             {"abbr":"AZ", "name":"Arizona"}
 *         ]
 *     });
 *
 *     // Create the combo box, attached to the states data store
 *     Ext.create('Ext.form.ComboBox', {
 *         fieldLabel: 'Choose State',
 *         store: states,
 *         queryMode: 'local',
 *         displayField: 'name',
 *         valueField: 'abbr',
 *         renderTo: Ext.getBody()
 *     });
 *
 * # Events
 *
 * To do something when something in ComboBox is selected, configure the select event:
 *
 *     var cb = Ext.create('Ext.form.ComboBox', {
 *         // all of your config options
 *         listeners:{
 *              scope: yourScope,
 *              'select': yourFunction
 *         }
 *     });
 *
 *     // Alternatively, you can assign events after the object is created:
 *     var cb = new Ext.form.field.ComboBox(yourOptions);
 *     cb.on('select', yourFunction, yourScope);
 *
 * # Multiple Selection
 * The {@link #multiSelect} config is deprecated.  For multiple selection use 
 * {@link Ext.form.field.Tag} or {@link Ext.view.MultiSelector}.
 *
 * # Filtered Stores
 *
 * If you have a local store that is already filtered, you can use the {@link #lastQuery} config option
 * to prevent the store from having the filter being cleared on first expand.
 *
 * ## Customized combobox
 *
 * Both the text shown in dropdown menu and text field can be easily customized:
 *
 *     @example
 *     var states = Ext.create('Ext.data.Store', {
 *         fields: ['abbr', 'name'],
 *         data : [
 *             {"abbr":"AL", "name":"Alabama"},
 *             {"abbr":"AK", "name":"Alaska"},
 *             {"abbr":"AZ", "name":"Arizona"}
 *         ]
 *     });
 *
 *     Ext.create('Chaching.components.form.field.ComboBox', {
 *         fieldLabel: 'Choose State',
 *         store: states,
 *         queryMode: 'local',
 *         valueField: 'abbr',
 *         renderTo: Ext.getBody(),
 *         modulePermissions: {
                        read: true,
                        create:true,
                        edit: true,
                        destroy: true
                    },
                    primaryEntityCrudApi: {
                        read: abp.appPath + '...',
                        create: abp.appPath + '..',
                        update: abp.appPath + '..',
                        destroy: abp.appPath + '..'
                    },
                    createEditEntityType: 'xtype of edit/create entity',
                    createEditEntityGridController: 'grid controller',
                    entityType: 'type of entity',
 *     });
 *
 * See also the {@link #listConfig} option for additional configuration of the dropdown.
 *
 */
Ext.define('Chaching.components.form.field.ComboBox', {
    extend: 'Ext.form.field.ComboBox',
    requires: ['Ext.grid.Panel'],
    alias: ['widget.chachingcombobox', 'widget.chachingcombo'],
    emptyText: app.localize('SearchText'),
    autoSelect: false,
    selectOnFocus: false,
    enableKeyEvents: true,
    typeAhead: false,
    editable: true,
    config: {
        filters: null,

        /**
         * @cfg {Ext.data.Model} selection
         * The selected model. Typically used with {@link #bind binding}.
         */
        selection: null,

        /**
         * @cfg {String} [valueNotFoundText]
         * When using a name/value combo, if the value passed to setValue is not found in the store, valueNotFoundText will
         * be displayed as the field text if defined. If this default text is used, it means there
         * is no value set and no validation will occur on this field.
         */
        valueNotFoundText: null,

        /**
         * @cfg {String/String[]/Ext.XTemplate} [displayTpl]
         * The template to be used to display selected records inside the text field. An array of the selected records' data
         * will be passed to the template. Defaults to:
         *
         *     '<tpl for=".">' +
         *         '{[typeof values === "string" ? values : values["' + me.displayField + '"]]}' +
         *         '<tpl if="xindex < xcount">' + me.delimiter + '</tpl>' +
         *     '</tpl>'
         *
         * By default only the immediate data of the record is passed (no associated data). The {@link #getRecordDisplayData} can
         * be overridden to extend this.
         */
        displayTpl: null,

        //<locale>
        /**
         * @cfg {String} delimiter
         * The character(s) used to separate the {@link #displayField display values} of multiple selected items when
         * `{@link #multiSelect} = true`.
         * @deprecated 5.1.0 For multiple selection use {@link Ext.form.field.Tag} or 
         * {@link Ext.view.MultiSelector}
         */
        delimiter: ', ',
        //</locale>

        /**
         * @cfg {String} displayField
         * The underlying {@link Ext.data.Field#name data field name} to bind to this ComboBox.
         *
         * See also `{@link #valueField}`.
         */
        displayField: 'text',
        /**
        * @cfg {Object} modulePermissions
        * Entity permissions to access this combo data
        */
        modulePermissions: {
            read: true,
            create: true,
            edit: true,
            destroy:true
        },
        /**
       * @cfg {String} createEditEntityType
       * Entity to be opened when editing/creating new from combo.
       */
        createEditEntityType: null,
        /**
        * @cfg {String} createEditEntityGridController
        * Create/Edit entity grid controller aliase name.
        */
        createEditEntityGridController: null,
        /**
        * @cfg {Object/Array} userDefinedColumns
        * Columns for grid picker. If provided default columns will be ignored.
        */
        userDefinedColumns: null,
        /**
        * @cfg {String} entityType
        * Target entity type/data is populated from the entity type.
        */
        entityType: null,
        /**
       * @cfg {Object} crudApi
       * Primary entities crud server api.
       */
        primaryEntityCrudApi: {
            read:null,
            create: null,
            destroy: null,
            update:null
        },
        /**
       * @cfg {boolean} isTwoEntityPicker
       * Does picker contains data from two different entities.
       */
        isTwoEntityPicker: false,
        secondEntityDetails: {
            editCreateModelClass: null,
            identificationKey: null,
            entityType: null,
            createEditEntityType: null,
            createEditEntityGridController: null,
            modulePermissions: {
                read: false,
                create: false,
                edit: false,
                destroy: false
            },
            secondoryEntityCrudApi: {
                read:null,
                create: null,
                destroy: null,
                update:null
            }
        },
        /**
       * @cfg {boolean} selectOnTabPressed
       * Select item when picker has selection and tab is pressed.
       */
        selectOnTabPressed:true
    },
    minChars: 2,
    queryParam: 'query',
    queryMode: 'remote',
    forceSelection: false,
    listConfig: {
        minWidth: 350,
        maxHeight: 250
    },
    defaultListConfig: {
        loadingHeight: 210,
        minWidth: 70,
        maxHeight: 300,
        shadow: 'sides'
    },
    initComponent: function () {
        var me = this;
        if (me.getCreateEditEntityGridController() == null) {
            Ext.raise('createEditEntityGridController config is required for add/update/delete of an entity for AutoFillCombo(' + me.geEntityType() + ')');
        }
        if (me.getIsTwoEntityPicker() && me.getSecondEntityDetails()== null) {
            Ext.raise('Please spcify secondEntityDetails if picker has data from two different entities.');
        }
        //if (me.entityType == null) {
        //    Ext.raise('entityType config is required to open edit/create page  in popup window for that entity.');
        //}
        //if (me.entityPermission == null && !me.modulePermissions) {
        //    Ext.raise('entityPermission config is required to show or hide action buttons in grid(autocompletecombo).');
        //}
        if (me.selectOnFocus && !me.editable) {
            Ext.raise('If selectOnFocus is enabled the combo must be editable: true -- please change one of those settings.');
        }
        me.autoSelect = false;
        me.callParent(arguments);
        me.mon(me, {
            specialkey: me.handleFieldEvents,
            keypress:me.handleFieldEvents,
            scope: me
        });
        //key events
        //me.on('keyup', me.baseKeyUp, this);
        //me.on('specialkey', me.baseSpecialkey, this);
    },
    handleFieldEvents:function(field, e) {
        var me = this,
            selectOnTabPressed = me.getSelectOnTabPressed(),
            picker = me.picker,
            selModel = undefined,
            selectedRec = undefined,
            modulePermissions = me.getModulePermissions();

        if (!picker) picker = me.getPicker();
        if (picker) selModel = picker.getSelectionModel();
        if (selModel)selectedRec = selModel.getSelection().length > 0 ? selModel.getSelection()[0] : undefined;
        switch (e.getKey()) {
            case 9://set value of selected record from picker
                if (!e.shiftKey && !e.ctrlKey && selectOnTabPressed && selectedRec) {
                    e.stopEvent();
                    me.setValue(selectedRec);
                    me.collapse();
                    selModel.deselectAll();
                    me.focus();
                }
                break;
            case 45://add new record if has primary entity permissions
                if (!e.shiftKey && !e.ctrlKey && modulePermissions && modulePermissions.create) {
                    e.stopEvent();
                    me.doCRUDOperation('create', null);
                }
                break;
            default:
                break;
        }
    },
    onDownArrow: function (e) {
        var me = this,
            picker = me.picker,
            recordsCount = me.getStore().getCount();

        if ((e.time - me.lastDownArrow) > 150) {
            delete me.lastDownArrow;
        }

        if (!me.isExpanded) {
            // Do not let the down arrow event propagate into the picker
            e.stopEvent();

            // Don't call expand() directly as there may be additional processing involved before
            // expanding, e.g. in the case of a ComboBox query.
            me.onTriggerClick();

            me.lastDownArrow = e.time;
        }
        else if (!e.isStopped && (e.time - me.lastDownArrow) < 150) {
            delete me.lastDownArrow;
        }
        if (picker && me.isExpanded && recordsCount > 0) {
            var pickerView = picker.getView(),
                selectionModel = pickerView.getSelectionModel();
            selectionModel.deselectAll();
            pickerView.focus();
            selectionModel.select(0, false);
            pickerView.getCell(0, 0).focus();
            ///TODO: Handle scroll issue.
            //var scrollTask = new Ext.util.DelayedTask(function () {
            //    var dom = pickerView.getTargetEl().dom;
            //    if (dom) {
            //        dom.scrollTop = 0;
            //        var scroller = pickerView.getScrollable();
            //        if (scroller) scroller.scrollBy(0, 0, false);
            //    }
            //});
            //scrollTask.delay(100);
        }
    },
    createPicker: function() {
        var me = this,
            picker,
            columnList = me.createGridColumns(),
            actionToolBar = me.getActionToolBar();

        var pickerCfg = Ext.apply({
            xtype: 'boundlist',
            pickerField: me,
            selModel: {
                mode: 'SINGLE'
            },
            floating: true,
            hidden: true,
            store: me.store,
            displayField: me.displayField,
            focusOnToFront: false,
            preserveScrollOnRefresh: true,
            pageSize: me.pageSize,
            tpl: me.tpl,

            columns: columnList,
            columnLines: false,
            rowLines: false,
            headerBorders: false,
            forceFit: true,
            layout: 'fit',
            recordToSetInComboBox: null,
            viewConfig: {
                stripeRows: true,
                emptyText: 'No Records Found',
                alwaysOnTop: true,
                loadingHeight: 100,
                loadingText:'Loading data. Please wait'
            },
            dockedItems: actionToolBar,
            multiSelect: false,
            cls: 'chaching-combogrid',
            controller: me.getCreateEditEntityGridController()
        }, me.listConfig, me.defaultListConfig);


        picker = me.picker = Ext.create('Ext.grid.Panel', pickerCfg);

        // hack: pass getNode() to the view
        picker.getNode = function() {
            picker.getView().getNode(arguments);
        };

        if (me.pageSize) {
            picker.pagingToolbar.on('beforechange', me.onPageChange, me);
        }

        me.mon(picker, {
            itemdblclick: me.onItemClick,
            refresh: me.onListRefresh,
            scope: me
        });

        me.mon(picker.getView(), {
            itemkeydown: me.handlePickerEvents,
            scope: me
        });

        me.mon(picker.getSelectionModel(), {
            beforeselect: me.onBeforeSelect,
            beforedeselect: me.onBeforeDeselect,
            selectionchange: me.onListSelectionChange,
            scope: me
        });

        return picker;
    },
    onListSelectionChange: function () {
        var me = this;
        if (me.lastInfoPanel) me.lastInfoPanel.hide();
    },
    handlePickerEvents:function(view,record,tableView,rowIdx,e,eOpts) {
        var me = this,
            selModel = view.getSelectionModel(),
            modulePermissions = me.getModulePermissions(),
            columns = view.getColumnManager().columns,
            infoColumn = columns[columns.length - 1];
        switch (e.getKey()) {
            case 9://select highligheted record and collapse the picker
                if (record) {
                    e.stopEvent();
                    me.cleanUpPicker(me, view, selModel, true);
                    if (me.nextSibling() && !e.shiftKey) me.nextSibling().focus();
                    else if (me.previousSibling() && e.shiftKey) me.previousSibling().focus();
                }
                break;
            case 13://setValue of selected record when enter is pressed.
                if (record && selModel) {
                    e.stopEvent();
                    me.setValue(record);
                    me.cleanUpPicker(me, view, selModel,true);
                }
                break;
            case 27://close picker when esc is pressed
                e.stopEvent();
                me.cleanUpPicker(me, view, selModel,true);
                break;
            case 35://end pressed navigate to last record
                if (record && selModel) {
                    e.stopEvent();
                    e.preventDefault();
                    selModel.deselectAll();
                    selModel.select(me.store.getTotalCount() - 1, false);
                    view.getCell(me.store.getTotalCount() - 1, 0).focus();
                }
                break;
            case 36://home pressed navigate to first record
                if (record && selModel) {
                    e.stopEvent();
                    e.preventDefault();
                    selModel.deselectAll();
                    selModel.select(0, false);
                    view.getCell(0, 0).focus();
                }
                break;
            case 38://when first record is selected and up arrow pressed move cursor to field
                if (record && selModel && rowIdx === 0) {
                    e.stopEvent();
                    me.cleanUpPicker(me, view, selModel, false);
                }
                break;
            case 45://add new record if has primary entity permissions
                if (!e.shiftKey && !e.ctrlKey && modulePermissions && modulePermissions.create) {
                    e.stopEvent();
                    me.cleanUpPicker(me, view, selModel,true);
                    me.doCRUDOperation('create', null);
                }
                break;
            case 69://edit selected record if has permissions
                if (record && selModel && modulePermissions.edit) {
                    e.stopEvent();
                    me.cleanUpPicker(me, view, selModel,true);
                    me.doCRUDOperation('edit', record);
                }
                break;
            case 46://delete selected Record is has permissions.
                if (record && selModel && modulePermissions.destroy) {
                    e.stopEvent();
                    me.cleanUpPicker(me, view, selModel,false);
                    me.doCRUDOperation('delete', record);
                }
                break;
            case 73://info if has read permission
                if (record && selModel && modulePermissions.read) {
                    e.stopEvent();
                    //me.cleanUpPicker(me, view, selModel,false);
                    me.doCRUDOperation('info', record, false, view.getCell(record, infoColumn));
                }
                break;
            case 82://refresh grid
                e.stopEvent();
                me.cleanUpPicker(me, view, selModel,false);
                me.doRefreshList();
                break;
            default:
                break;
        }
    },
    cleanUpPicker: function (me, view, selModel, collapse) {
        var navigationModel = undefined;
        if (selModel) {
            selModel.deselectAll();
            navigationModel = selModel.navigationModel;
            if (navigationModel && navigationModel.cell) {
                navigationModel.cell.removeCls('x-grid-item-focused');
            }
        }
        view.blur();
        me.picker.blur();
        view.refresh();
        me.focus();
        if (me.lastInfoPanel) me.lastInfoPanel.hide();
        if (collapse)
            me.collapse();
    },
    getActionToolBar:function() {
        var me = this,
            secondoryEntityDetails = me.getSecondEntityDetails(),
            isTwoEntityPicker = me.getIsTwoEntityPicker(),
            actionButtons = ['->'];

        if (me.getModulePermissions().create) {
            actionButtons.push({
                xtype: 'button',
                tooltip: abp.localization.localize("Add") + " " + me.getEntityType(),
                iconAlign: 'left',
                scale: 'small',
                iconCls: 'fa fa-plus-square',
                ui: 'combogridactionButton',
                height: 20,
                width: 20,
                handler: function (btn) {
                    me.doCRUDOperation('create', null);
                }
            });
        }
        if (isTwoEntityPicker && secondoryEntityDetails.modulePermissions.create) {
            actionButtons.push({
                xtype: 'button',
                tooltip: abp.localization.localize("Add") + " " + secondoryEntityDetails.entityType,
                iconAlign: 'left',
                scale: 'small',
                iconCls: 'fa fa-plus-square',
                height: 20,
                width: 20,
                ui: 'combogridactionButton',
                handler: function (btn) {
                    me.doCRUDOperation('create', null, true);
                }
            });
        }
        actionButtons.push({
            xtype: 'button',
            scale: 'small',
            name: 'RefreshData',
            itemId: 'RefreshData',
            iconCls: 'fa fa-refresh',
            height: 20,
            width: 20,
            ui: 'combogridactionButton',
            tooltip: app.localize('RefreshData'),
            handler: function (btn) {
                me.doRefreshList();
            }
        });
        var actionToolBar = {
            xtype: 'toolbar',
            dock: 'bottom',
            ui:'combogridtoolbar',
            layout: {
                type: 'hbox',
                pack: 'center'
            },
            items: actionButtons
        };
        return actionToolBar;
    },
    onBeforeSelect: function (list, record, recordIndex) {
        return this.fireEvent('beforeselect', this, record, recordIndex);
    },

    onBeforeDeselect: function (list, record, recordIndex) {
        return this.fireEvent('beforedeselect', this, record, recordIndex);
    },
    onFocusChange: function (selModel, prevRecord, newRecord) {
        var picker = this.picker,
            el;

        if (newRecord) {
            // Ext.get is to ensure the node has an id
            el = Ext.get(picker.getView().getNodeByRecord(newRecord));

            if (el) {
                this.ariaEl.dom.setAttribute('aria-activedescendant', el.id);
            }
        }
    },
    onItemClick: function(view, record, item, index, e, eOpts) {
        var me = this;
        if (!view.ownerCt.multiSelect) {
            me.setValue(record);
            me.collapse();
        }
    },
   
    createGridColumns: function () {
        var me = this,
            //store = Ext.create('Chaching.store.' + me.store),
            store = me.store,
            model = store.getModel(),
            fields = model.getFields(),
            count = fields.length,
            columns = [],
            secondoryEntityDetails = me.getSecondEntityDetails(),
            isTwoEntityPicker = me.getIsTwoEntityPicker();

        if (me.userDefinedColumns == null) {
            for (var i = 0; i < count; i++) {
                if (fields[i].hidden === false) {
                    var field = fields[i];
                    var column = {
                        text: field.headerText == null ? field.name : app.localize(field.headerText),
                        sortable: false,
                        hideable: false,
                        menuDisabled: true,
                        filterable: true,
                        minWidth: field.minWidth?field.minWidth:50,
                        width: field.width,
                        hidden: field.hidden,
                        dataIndex: field.name,
                        flex: field.width > 0 ? null : field.flex
                    }
                    if (field.type === "boolean") {
                        column.renderer = Chaching.utilities.ChachingRenderers.rightWrongMarkRenderer;
                        column.align = 'center';
                    }
                    columns.push(column);
                }
            }
        } else {
            count = me.userDefinedColumns.length;
            for (var i = 0; i < count; i++) {
                if (userDefinedColumns[i].hidden === false) {
                    var column = {
                        text: userDefinedColumns[i].headerText == null ? userDefinedColumns[i].name : app.localize(userDefinedColumns[i].headerText),
                        sortable: false,
                        hideable: false,
                        menuDisabled: true,
                        filterable: true,
                        minWidth: 50,
                        width: userDefinedColumns[i].width,
                        hidden: userDefinedColumns[i].hidden,
                        dataIndex: userDefinedColumns[i].name,
                        flex: userDefinedColumns[i].width > 0 ? null : userDefinedColumns[i].flex
                    }
                    columns.push(column);
                }
            }
        }

        var editActionItem = {
            scale: 'small',
            iconCls: 'editCls',
            tooltip: app.localize('Edit'),
            handler: function (grid, rowIndex, colIndex) {
                var rec = grid.getStore().getAt(rowIndex);
                me.doCRUDOperation('edit', rec);
            }
        };
        var deleteActionItem = {
            scale: 'small',
            iconCls: 'deleteCls',
            tooltip: app.localize('Delete'),
            handler: function (grid, rowIndex, colIndex) {
                var rec = grid.getStore().getAt(rowIndex);
                me.doCRUDOperation('delete', rec);
            }
        };
        var infoActionItem = {
            xtype:'button',
            scale: 'small',
            iconCls: 'infoCls',
            tooltip: app.localize('Info'),
            handler: function (grid, rowIndex, colIndex,item,target,record) {
                var rec = grid.getStore().getAt(rowIndex);
                me.doCRUDOperation('info', rec, false, grid.getCell(record, this));
            }
        };
        if (me.getModulePermissions().edit || (isTwoEntityPicker&&secondoryEntityDetails.modulePermissions.edit)) {
            var editActionColumn = {
                xtype: 'actioncolumn',
                // width: '5%',
                width : 20,
                minWidth: 20,
                sortable: false,
                hideable: false,
                menuDisabled: true,
                filterable: true,
                items: [editActionItem]
            }
            columns.push(editActionColumn);
        }

        if (me.getModulePermissions().destroy || (isTwoEntityPicker&&secondoryEntityDetails.modulePermissions.destroy)) {
            var deleteActionColumn = {
                xtype: 'actioncolumn',
                //width: '5%',
                width: 20,
                minWidth: 20,
                sortable: false,
                hideable: false,
                menuDisabled: true,
                filterable: true,
                items: [deleteActionItem]
            }
            columns.push(deleteActionColumn);
        }

        if (me.getModulePermissions().read || (isTwoEntityPicker && secondoryEntityDetails.modulePermissions.read)) {
            var infoActionColumn = {
                xtype: 'actioncolumn',
                // width: '5%',
                width: 20,
                minWidth: 20,
                sortable: false,
                hideable: false,
                menuDisabled: true,
                filterable: true,
                items: [infoActionItem]
            }
            columns.push(infoActionColumn);
        }
        return columns;
    },
    doQuery: function (queryString, forceAll, rawQuery) {
        var me = this,
            store = me.getStore(),
            filters = store.getFilters(),
            // if we have a queryString and the queryFilter is not filtering the store, we should do a localQuery
            refreshFilters = !!queryString && me.queryFilter && (filters.indexOf(me.queryFilter) < 0),
            // Decide if, and how we are going to query the store
            queryPlan = me.beforeQuery({
                lastQuery: me.lastQuery || '',
                query: queryString || '',
                rawQuery: rawQuery,
                forceAll: forceAll,
                combo: me,
                cancel: false
            });
        // Allow veto.
        if (queryPlan !== false && !queryPlan.cancel) {
            // If they're using the same value as last time (and not being asked to query all), 
            // and the filters don't need to be refreshed, just show the dropdown
            if (me.queryCaching && !refreshFilters && queryPlan.query === me.lastQuery) {
                // The filter changing was done with events suppressed, so
                // refresh the picker DOM while hidden and it will layout on show.
                me.getPicker().getView().refresh();
                me.expand();
                me.afterQuery(queryPlan);
            } else // Otherwise filter or load the store
            {
                me.lastQuery = queryPlan.query;
                if (me.queryMode === 'local') {
                    me.doLocalQuery(queryPlan);
                } else {
                    me.doRemoteQuery(queryPlan);
                }
            }
            return true;
        } else // If the query was vetoed we still need to check the change
            // in case custom validators are used
        {
            me.startCheckChangeTask();
        }
        return false;
    },
    afterQuery: function (queryPlan) {
        var me = this;

        if (me.store.getCount()) {
            if (me.typeAhead) {
                me.doTypeAhead(queryPlan);
            }

            if (queryPlan.rawQuery) {
                if (me.picker && !me.picker.getSelectionModel().hasSelection()) {
                    me.doAutoSelect();
                }
            } else {
                me.doAutoSelect();
            }
        }

        // doQuery is called upon field mutation, so check for change after the query has done its thing
        me.startCheckChangeTask();
    },
    doCRUDOperation: function (operation, record, isSecondEntityAdd, infoItem) {
        var me = this,
            picker = me.picker,
            xtype = me.getCreateEditEntityType(),
            secondoryEntityDetails,
            isTwoEntityPicker = me.getIsTwoEntityPicker(),
            identificationKey,
            secondEntityType,
            secondCreateEditEntityType,
            secondCreateEditEntityGridController,
            secondEntityModelClass,
            entityType = me.getEntityType();

        var recordToLoad = me.getStore().model.create();
        var entityTypeController = me.picker.getController();
        
        if ((record && isTwoEntityPicker) || isSecondEntityAdd) {
            secondoryEntityDetails = me.getSecondEntityDetails();
            identificationKey = secondoryEntityDetails.identificationKey;
            secondCreateEditEntityType = secondoryEntityDetails.createEditEntityType;
            secondEntityType = secondoryEntityDetails.entityType;
            secondCreateEditEntityGridController = secondoryEntityDetails.createEditEntityGridController;
            secondEntityModelClass = secondoryEntityDetails.editCreateModelClass;
            var storeApi = me.getPrimaryEntityCrudApi();
            if ((record && !record.get(identificationKey)) || isSecondEntityAdd) {
                xtype = secondCreateEditEntityType;
                recordToLoad = Ext.create(secondEntityModelClass);
                entityTypeController = Ext.create(secondCreateEditEntityGridController);
                entityType = secondEntityType;
                storeApi = secondoryEntityDetails.secondoryEntityCrudApi;
            }
            if (storeApi) me.store.getProxy().setApi(storeApi);
        } else {
            var storeApi = me.getPrimaryEntityCrudApi();
            if (storeApi) me.store.getProxy().setApi(storeApi);
            if (me.identificationKey != undefined) {
                identificationKey = me.identificationKey;
            }
        }
        if (!picker) me.picker = me.getPicker();
        var xtypeOfView = "";
        if (operation === 'create') {
            xtypeOfView = xtype + ".create";
        } else if (operation === 'edit') {
            xtypeOfView = xtype + ".edit";
        }

        if (me.store && me.store.proxy && (me.store.proxy.urlToGetRecordById === "" || me.store.proxy.urlToGetRecordById == undefined)) {
            Ext.raise('urlToGetRecordById config is required in proxy config of ' + me.store.$className + ' to edit record in AutoFillCombo(' + me.nameOfEntity + ')');
        }
        var recordByIdUrl = me.store.proxy.urlToGetRecordById;
        if (operation === 'edit' || operation === "info") {
            Ext.Ajax.request({
                url: recordByIdUrl,
                jsonData: Ext.encode({ id: record.get(me.valueField), value: record.get(identificationKey) }),
                success: function(response, opts) {
                    var res = Ext.decode(response.responseText);
                    if (res.success) {
                        if (operation === "edit") {
                            var popupWindow = me.createPopupWindow(xtypeOfView, me, operation, entityType);
                            var formView = popupWindow.down('form');
                            Ext.apply(recordToLoad.data, res.result);
                            entityTypeController.doAfterCreateAction('popup', formView, true, recordToLoad);
                            formView.loadRecord(recordToLoad);
                        } else {
                            var floatingPanel = me.createDynamicPanel(res.result,entityType);
                            floatingPanel.showBy(infoItem,'tr');
                        }
                    } else {
                        abp.message.error(res.error.message, 'Error');
                    }
                },
                failure: function(response, opts) {
                    var res = Ext.decode(response.responseText);
                    Ext.toast(res.exceptionMessage);
                    console.log(response);
                }
            });
        } else if (operation === 'delete') {
            abp.message.confirm(app.localize('ConfirmMessage') + " " + entityType, app.localize('Confirm'), function(actionResult) {
                if (actionResult) {
                    me.deleteEntity(me, record);
                }
            });
        } else if (operation === 'create') {
            var popupWindow = me.createPopupWindow(xtypeOfView, me, operation, entityType);
            var formView = popupWindow.down('form');
            entityTypeController.doAfterCreateAction('popup', formView, false, null);
        }
    },
    createDynamicPanel: function (response,title) {
        var me = this,
            entityTitle = '<h3>' + title + '</h3>',
            html = '<div class="leftarrowdiv" style="width:97% !important;left:11px !important;height:100% !important"><table style="height:100%; width:100%;">',
            height = 40;
        if (response) {
            for (var key in response) {
                if (response.hasOwnProperty(key)) {
                    var value = response[key];
                    if (value && typeof(value) === "string") {
                        html += '<tr><td style="height:25px; width:35%;">' + key.initCap() + ':</td><td style="height:25px; width:65%; padding-left:10px;">' + value.initCap() + '</td></tr>';
                        height += 25;
                    }
                }
            }
        }
        html += "</table></div>";
        var panel = undefined;
        if (me.lastInfoPanel) {
            panel = me.lastInfoPanel;
            panel.update(html);
            panel.setHeight(height);
            me.lastInfoPanel = panel;
            return panel;
        }
        panel = Ext.widget({
            xtype: 'panel',
            floating: true,
            shadow: false,
            width: 350,
            baseCls: '',
            styleHtmlContent:true,
            html: html,
            border: false,
            frame: false,
            bodyStyle: {
                'background-color': 'transparent',
                'box-shadow':'transparent'
            }
        });
        me.lastInfoPanel = panel;
        panel.setHeight(height);
        return panel;
    },
    getTextValue: function () {
        var me = this;
        return me.rawValue;
    },
    deleteEntity: function (me, record) {
        var picker = me.getPicker();
        if (record.get(me.displayField) === me.getTextValue()) {
            me.setValue('');
        }
        Ext.Ajax.request({
            url: me.store.proxy.api.destroy,
            jsonData: Ext.encode({ id: record.get(me.valueField) }),
            success: function (response, opts) {
                var res = Ext.decode(response.responseText);
                if (res.success) {
                    abp.notify.success('Operation completed successfully.', 'Success');
                    picker.getSelectionModel().deselectAll();
                    me.store.load({ params: me.store.params });
                } else {
                    abp.message.error(res.error.message, 'Error');
                }
            },
            failure: function (response, opts) {
                var res = Ext.decode(response.responseText);
                Ext.toast(res.exceptionMessage);
                console.log(response);
            }
        });
    },
    createPopupWindow: function (xtypeOfView, me, operation, entityType) {
        var windowTitle = operation + ' ' + entityType;
        var createEditView = Ext.widget({
            xtype: xtypeOfView,
            openInPopupWindow: true,
            parentGrid: me.picker,
            showFormTitle: false
        });
        var popupWndSize = createEditView.popupWndSize;
        var height = '90%', width = '90%';
        if (popupWndSize) {
            height = popupWndSize.height;
            width = popupWndSize.width;
        }
        var window = Ext.create('Chaching.view.common.window.ChachingWindowPanel', {
            layout: 'fit',
            title: windowTitle.toUpperCase(),
            autoShow: true,
            modal: true,
            height: height,
            width: width,
            items: [createEditView],
            listeners: {
                beforedestroy: function (cmp, eOpts) {

                    //set record in auto fill combo
                    var record = me.picker.recordToSetInComboBox;
                    if (!record) record = cmp.down('form').getRecord();
                    if (!record) record = cmp.down('form').getValues();
                    if (record)
                        me.onSelect(record);
                }
            }
        });

        return window;
    },
    onSelect: function (record) {
        if (record && record.data) {
            var me = this;
            var idVal = record.data[me.valueField];
            var nameVal = record.data[me.displayField];
            me.setValue(record);
            me.fireEvent('change', me, idVal, record);
            me.collapse();
            me.fireEvent('select', me, record);
            me.doRefreshList();
        }
    },
    doRefreshList:function() {
        var me = this,
            store = me.getStore();
        store.reload();
    },
    onExpand: function () {
        var keyNav = this.getPicker().getNavigationModel();
        if (keyNav) {
            keyNav.enable();
        }
        this.doAutoSelect();
        this.focus();
    },
    /**
     * @private
     * Disables the key navs for the BoundList when it is collapsed.
     */
    onCollapse: function () {
        var picker = this.getPicker(),
            keyNav = picker.getNavigationModel(),
            selModel = picker.getSelectionModel();
        if (keyNav) {
            keyNav.disable();
        }
        if (this.updatingValue) {
            this.doQueryTask.cancel();
        }
        if (selModel)selModel.deselectAll();
        picker.blur();
        this.focus();
        if (this.lastInfoPanel) this.lastInfoPanel.hide();
    },
    onListRefresh: function () {
        this.alignPicker();
        this.syncSelection();
        if (this.lastInfoPanel) this.lastInfoPanel.hide();
    },
    alignPicker: function () {
        var me = this,
            picker = me.getPicker(),
            heightAbove = me.getPosition()[1] - Ext.getBody().getScroll().top,
            heightBelow = Ext.Element.getViewportHeight() - heightAbove - me.getHeight(),
            space = Math.max(heightAbove, heightBelow);

        if (picker.height) {
            delete picker.height;
            picker.updateLayout();
        }

        if (picker && picker.getHeight() > space - 5) {
            picker.setHeight(space - 5);
        }
        else if (me.store.isLoading()) {
            //set loading height
            picker.setHeight(me.defaultListConfig.loadingHeight);
            me.store.pickerField = picker;
            me.store.on('load', function (storeObj) {
                if (storeObj && storeObj.pickerField) {
                    //refresh to autoHeight based on records available in store
                    storeObj.pickerField.refresh(1);
                    storeObj.pickerField.updateLayout();
                }
            });
        }
        me.callParent();
    },

    syncSelection: function () {
        var me = this,
            picker = me.picker,
            selection, selModel,
            values = me.valueModels || [],
            vLen = values.length, v, value;

        if (picker) {

            selection = [];
            for (v = 0; v < vLen; v++) {
                value = values[v];

                if (value && value.isModel && me.store.indexOf(value) >= 0) {
                    selection.push(value);
                }
            }


            me.ignoreSelection++;
            selModel = picker.getSelectionModel();
            selModel.deselectAll();
            if (selection.length) {
                selModel.select(selection, undefined, true);
            }
            me.ignoreSelection--;
        }
    }
});