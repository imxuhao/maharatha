
Ext.define('Chaching.view.editions.EditionsForm',
{
    extend: 'Chaching.view.common.form.ChachingFormPanel',


    alias: ['host.edition.create', 'host.edition.edit'],
    requires: [
        'Chaching.view.editions.EditionsFormController'
    ],

    controller: 'editions-editionsform',
    name: 'Editions',
    openInPopupWindow: true,
    hideDefaultButtons: false,
    layout: 'fit',
    defaults: {
        bodyStyle: { 'background-color': 'trasparent' },
        labelAlign: 'top'
    },
    defaultFocus: 'textfield#displayName',

    items: [
        {
            xtype: 'tabpanel',
            region: 'center',
            ui: 'formTabPanels',
            items: [
                {
                    title: 'Editions',
                    padding: '0 0 0 10',
                    iconCls: 'icon-grid',
                    items: [
                        {
                            xtype: 'hiddenfield',
                            name: 'id',
                            value: 0
                        }, {
                            xtype: 'textfield',
                            name: 'displayName',
                            itemId: 'displayName',
                            fieldLabel: app.localize('Name'),
                            width: '100%',
                            ui: 'fieldLabelTop',
                            emptyText: app.localize('Edition Name')
                        }
                    ]
                },
                {
                    title: app.localize('Features'),
                    padding: '5',
                    iconCls: 'icon-home',
                    layout: 'fit',
                    xtype: 'treepanel',
                    name: 'features',
                    itemId: 'features',
                    cls: 'chaching-grid',
                    store: 'editions.EditionsTreeStore',
                    rootVisible: false,
                    width: '100%',
                    alwaysReload: true,
                    hideHeaders: false,
                    selModel: 'cellmodel',
                    viewConfig: { toggleOnDblClick: false },
                    columns: [
                        {
                            xtype: 'treecolumn',
                            dataIndex: 'displayName',
                            text: 'Feature Name',
                            flex: 1
                        }, {
                            dataIndex: 'defaultValue',
                            itemId:'featureValues',
                            width: 100,
                            text: 'Value',
                            renderer: function(value, cell, record, rowIdx, colIdx) {
                                var inputType = record.get('inputType');
                                var returnValue;
                                if (inputType) {
                                    switch (inputType.name) {
                                    case "CHECKBOX":
                                        if (value === "false") value = false;
                                        else if (value === "true") value = true;
                                        returnValue = Chaching.utilities.ChachingRenderers
                                            .rightWrongMarkRenderer(value, cell, record, rowIdx, colIdx);
                                        break;
                                    case "COMBOBOX":
                                        var dataSource = inputType.itemSource.items;
                                        if (dataSource && dataSource.length > 0) {
                                            for (var i = 0; i < dataSource.length; i++) {
                                                if (dataSource[i].value === value) {
                                                    returnValue = dataSource[i].displayText;
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                    case "SINGLE_LINE_STRING":
                                        returnValue = value;
                                        break;
                                    default:
                                        returnValue = value;
                                        break;
                                    }
                                }
                                return returnValue;
                            },
                            getEditor: function(record) {
                                var editorType = record.get('inputType');
                                var editor = undefined;
                                if (editorType) {
                                    switch (editorType.name) {
                                    case "CHECKBOX":
                                        editor = new Ext.grid.CellEditor({
                                            field: {
                                                xtype: 'checkbox'
                                            }
                                        });
                                        break;
                                    case "COMBOBOX":
                                        editor = new Ext.grid.CellEditor({
                                            field: {
                                                xtype: 'combo',
                                                valueField: 'defaultValue',
                                                displayField: 'displayText',
                                                store: {
                                                    fields:
                                                    [
                                                        { name: 'value' }, { name: 'displayText' },
                                                        { name: 'defaultValue', mapping: 'value' }
                                                    ],
                                                    data: editorType.itemSource.items
                                                }
                                            }
                                        });
                                        break;
                                    case "SINGLE_LINE_STRING":
                                        editor = new Ext.grid.CellEditor({
                                            field: {
                                                xtype: 'textfield'
                                            }
                                        });
                                        break;
                                    default:
                                        break;
                                    }
                                }
                                return editor;
                            }
                        }
                    ],
                    plugins: [
                        {
                            ptype: 'chachingCellediting',
                            pluginId: 'editingPlugin',
                            clicksToEdit: 2
                        }
                    ]
                }
            ]
        }
    ]
});
