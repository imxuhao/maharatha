
/**
 * This class is created as form for recurrence add/edit purpose. {@link Chaching.view.recurrence.RecurrenceForm}
 * Author: Kamal
 * Date Created: 08/16/2016
 */
Ext.define('Chaching.view.recurrence.RecurrenceForm', {
    extend: 'Chaching.view.common.form.ChachingFormPanel',
    alias: ['widget.recurrence.create', 'widget.recurrence.edit'],
    requires: [
        'Chaching.view.recurrence.RecurrenceFormController'
    ],
    openInPopupWindow: true,
    hideDefaultButtons: true,
    displayDefaultButtonsCenter : true,
    border: false,
    scrollable: true,
    controller: 'recurrence-recurrenceform',
    modulePermissions: {
        read: true,
        create: true,
        edit: true,
        destroy: true
    },
    //layout: {
    //    type: 'fit'
    //},
    items: [
    {
    xtype : 'fieldset',
    title: app.localize('RecurrencePattern'),
    ui: 'transparentFieldSet',
    collapsible: true,
    //height : 300,
    layout : 'fit',
    items : [
        {
            xtype: 'tabpanel',
            ui: 'dashboard',
        items: [
            {
            title: app.localize('Daily'),
            layout : 'vbox',
            items: [{
                xtype: 'fieldcontainer',
                layout: 'hbox',
                fieldDefaults: {
                    // margin: '0px 10px 0px 10px'
                },
                items: [{
                    xtype: 'radiofield',
                    name: 'isEverySpecificDay',
                    boxLabel: app.localize('Every'),
                    boxLabelCls: 'checkboxLabel',
                    inputValue: true,
                    checked: true,
                    listeners: {
                        change: 'onEverySpecificDayChange',
                        scope: 'controller'
                    }
                }, {
                    xtype: 'textfield',
                    inputCls: 'inputTextCls',
                    style: {
                        marginLeft: '10px'
                    },
                    name: 'everySpecificDay',
                    ui: 'fieldLabelTop',
                    maskRe: /[0-9]/,
                    value: 1,
                    enforceMaxLength: true,
                    width: 50
                }, {
                    xtype: 'displayfield',
                    style: {
                        marginLeft: '10px'
                    },
                    ui: 'fieldLabelTop',
                    labelSeparator: '',
                    labelStyle: 'padding-top:8px !important;',
                    fieldLabel: app.localize('RecurrenceDays')
                }]
            }, {
                xtype: 'radiofield',
                name: 'isEveryWeekDay',
                boxLabelCls: 'checkboxLabel',
                boxLabel: app.localize('EveryWeekDay'),
                inputValue: true,
                listeners: {
                    change: 'onEveryWeekDayChange',
                    scope: 'controller'
                }

            }]
        }, {
            title: app.localize('Weekly'),
            layout: 'vbox',
            width: 600,
            items: [ {
                xtype: 'checkboxgroup',
                fieldLabel: app.localize('RepeatOn'),
                labelWidth : 80,
                labelStyle: 'padding-top:8px !important;',
                ui: 'fieldLabelTop',
                name: 'weekDays',
                columns: 4,
                width : 500,
                items: [
                    { boxLabel: app.localize('Sunday'), boxLabelCls: 'checkboxLabel', name: 'Sunday', inputValue: '0' },
                    { boxLabel: app.localize('Monday'), boxLabelCls: 'checkboxLabel', name: 'Monday', inputValue: '1' },
                    { boxLabel: app.localize('Tuesday'), boxLabelCls: 'checkboxLabel', name: 'Tuesday', inputValue: '2' },
                    { boxLabel: app.localize('Wednesday'), boxLabelCls: 'checkboxLabel', name: 'Wednesday', inputValue: '3' },
                    { boxLabel: app.localize('Thursday'), boxLabelCls: 'checkboxLabel', name: 'Thursday', inputValue: '4' },
                    { boxLabel: app.localize('Friday'), boxLabelCls: 'checkboxLabel', name: 'Friday', inputValue: '5' },
                    { boxLabel: app.localize('Saturday'), boxLabelCls: 'checkboxLabel', name: 'Saturday', inputValue: '6' }
                ]
            }]

        }, {
            title: app.localize('Monthly'),
            layout: 'vbox',
            width: 600,
            items: [{
                xtype: 'fieldcontainer',
                fieldLabel: app.localize('RepeatOn'),
                labelStyle: 'padding-top:8px !important;',
                ui: 'fieldLabelTop',
                layout: 'vbox',
                labelWidth: 80,
                width: 600,
                items: [
                    {
                        xtype: 'fieldcontainer',
                        width: 600,
                        layout : 'hbox',
                        items : [{ xtype: 'radiofield',
                                    name: 'IsDayOfEveryMonth',
                                    checked: true,
                                    inputValue: true,
                                    boxLabel: app.localize('Day'),
                                    boxLabelCls: 'checkboxLabel',
                                    listeners: {
                                        change: 'onDayOfEveryMonthChange',
                                        scope: 'controller'
                                    }
                        }, {
                            xtype: 'textfield',
                            inputCls: 'inputTextCls',
                            style: {
                                marginLeft: '10px'
                            },
                            name: 'SpecificDayOfMonth',
                            ui: 'fieldLabelTop',
                            width: 37,
                            minValue: 1,
                            maxValue : 31,
                            maskRe: /[0-9]/,
                            maxLength: 10,
                            enforceMaxLength: true,
                            value: new Date().getDate()
                        },
                        {
                            xtype: 'textfield',
                            inputCls: 'inputTextCls',
                            labelStyle: 'padding-top:8px !important;',
                            style: {
                                marginLeft: '10px'
                            },
                            labelSeparator: '',
                            labelWidth: 60,
                            width: 100,
                            minValue: 1,
                            maxValue: 12,
                            fieldLabel: app.localize('OfEvery'),
                            name: 'MonthNumber',
                            ui: 'fieldLabelTop',
                            maskRe: /[0-9]/,
                            value: 1,
                            enforceMaxLength: true
                        }, {
                            xtype: 'displayfield',
                            style: {
                                marginLeft: '10px'
                            },
                            ui: 'fieldLabelTop',
                            fieldLabel: app.localize('Months'),
                            labelStyle: 'padding-top:8px !important;',
                            labelSeparator: ''
                        }]
                    },
                    {
                        xtype : 'fieldcontainer',
                        layout: 'hbox',
                        width: 600,
                        items : [{ xtype: 'radiofield',
                                    name: 'IsSpecificMonth',
                                    boxLabel: app.localize('The'),
                                    boxLabelCls: 'checkboxLabel',
                                    inputValue: true,
                                    listeners: {
                                        change: 'onSpecificMonthChange',
                                        scope: 'controller'
                                    }
                        }, {
                            xtype: 'combobox',
                            name: 'WeekNumberOfMonthly',
                            inputCls: 'inputTextCls',
                            forceSelection: true,
                            ui: 'fieldLabelTop',
                            width: 80,
                            style: {
                                marginLeft: '10px'
                            },
                            store: {
                                fields: [
                                    { type: 'string', name: 'Value' },
                                    { type: 'string', name: 'Data' }
                                ],
                                data: [{ 'Value': 1, 'Data': 'First' },
                                    { 'Value': 2, 'Data': 'Second' },
                                    { 'Value': 3, 'Data': 'Third' },
                                    { 'Value': 4, 'Data': 'Fourth' }]
                            },
                            displayField: 'Data',
                            valueField: 'Value',
                            editable: false,
                            emptyText: app.localize('SelectOption'),
                            enableKeyEvents: true,
                            queryMode: 'local'
                        }, {
                            xtype: 'combobox',
                            name: 'WeekDayNameMonthly',
                            inputCls: 'inputTextCls',
                            ui: 'fieldLabelTop',
                            forceSelection: true,
                            width: 108,
                            style: {
                                marginLeft: '10px'
                            },
                            store: {
                                fields: [
                                    { type: 'string', name: 'Value' },
                                    { type: 'string', name: 'Data' }
                                ],
                                data: [{ 'Value': '0', 'Data': 'Sunday' },
                                    { 'Value': '1', 'Data': 'Monday' },
                                    { 'Value': '2', 'Data': 'Tuesday' },
                                    { 'Value': '3', 'Data': 'Wednesday' },
                                    { 'Value': '4', 'Data': 'Thursday' },
                                    { 'Value': '5', 'Data': 'Friday' },
                                    { 'Value': '6', 'Data': 'Saturday' }]
                            },
                            displayField: 'Data',
                            valueField: 'Value',
                            editable: false,
                            emptyText: app.localize('SelectOption'),
                            enableKeyEvents: true,
                            queryMode: 'local'
                        },  {
                            xtype: 'textfield',
                            inputCls: 'inputTextCls',
                            labelStyle: 'padding-top:8px !important;',
                            style: {
                                marginLeft: '10px'
                            },
                            labelSeparator: '',
                            labelWidth: 60,
                            width : 100,
                            fieldLabel: app.localize('OfEvery'),
                            name: 'specificMonthNumber',
                            ui: 'fieldLabelTop',
                            maskRe: /[0-9]/,
                            minValue: 1,
                            maxValue : 12,
                            enforceMaxLength: true
                        }, {
                            xtype: 'displayfield',
                            style: {
                                marginLeft: '10px'
                            },
                            ui: 'fieldLabelTop',
                            fieldLabel: app.localize('Months'),
                            labelStyle: 'padding-top:8px !important;',
                            labelSeparator: ''
                        }]
                    }
                ]
            }]
        }, {
            title: app.localize('Yearly'),
            layout: 'vbox',
            items: [ {
                xtype: 'fieldcontainer',
                fieldLabel: app.localize('RepeatOn'),
                labelStyle: 'padding-top:8px !important;',
                labelWidth : 80,
                ui: 'fieldLabelTop',
                layout: 'vbox',
                anchor: '100%',
                width: 600,
                items: [
                    {
                        xtype: 'fieldcontainer',
                        layout: 'hbox',
                        width: 600,
                        items: [{
                            xtype: 'radiofield',
                            name: 'IsMonthOfYear',
                            checked: true,
                            inputValue: true,
                            boxLabel: app.localize('Every'),
                            boxLabelCls: 'checkboxLabel',
                            listeners: {
                                change: 'onMonthOfYearChange',
                                scope: 'controller'
                            }
                        }, {
                            xtype: 'combobox',
                            name: 'MonthNameofYear',
                            ui: 'fieldLabelTop',
                            forceSelection: true,
                            inputCls: 'inputTextCls',
                            style: {
                                marginLeft: '10px'
                            },
                            width: 120,
                            store: {
                                fields: [
                                    { type: 'string', name: 'Value' },
                                    { type: 'string', name: 'Data' }
                                ],
                                data: [{ 'Value': 1, 'Data': 'January' },
                                    { 'Value': 2, 'Data': 'February' },
                                    { 'Value': 3, 'Data': 'March' },
                                    { 'Value': 4, 'Data': 'April' },
                                    { 'Value': 5, 'Data': 'May' },
                                    { 'Value': 6, 'Data': 'June	' },
                                    { 'Value': 7, 'Data': 'July' },
                                    { 'Value': 8, 'Data': 'August' },
                                    { 'Value': 9, 'Data': 'September' },
                                    { 'Value': 10, 'Data': 'October ' },
                                    { 'Value': 11, 'Data': 'November' },
                                    { 'Value': 12, 'Data': 'December' }]
                            },
                            displayField: 'Data',
                            valueField: 'Value',
                            editable: false,
                            emptyText: app.localize('SelectOption'),
                            enableKeyEvents: true,
                            queryMode: 'local'
                        }, {
                            xtype: 'textfield',
                            inputCls: 'inputTextCls',
                            name: 'DayOfMonthYearly',
                            minValue: 1,
                            maxValue: 31,
                            ui: 'fieldLabelTop',
                            style: {
                                marginLeft: '10px'
                            },
                            width: 40,
                            maskRe: /[0-9]/,
                            maxLength: 10,
                            enforceMaxLength: true,
                            value: new Date().getDate()
                        }]
                    },
                    {
                        xtype: 'fieldcontainer',
                        layout: 'hbox',
                        width: 600,
                        items: [{
                            xtype: 'radiofield',
                            name: 'IsSpecficMonthofYear',
                            boxLabel: app.localize('The'),
                            boxLabelCls: 'checkboxLabel',
                            inputValue: true,
                            listeners: {
                                change: 'onSpecificMonthOfYearChange',
                                scope: 'controller'
                            }
                        }, {
                            xtype: 'combobox',
                            name: 'WeekNumberOfYearly',
                            ui: 'fieldLabelTop',
                            inputCls: 'inputTextCls',
                            style: {
                                marginLeft: '10px'
                            },
                            forceSelection : true,
                            width: 100,
                            store: {
                                fields: [
                                    { type: 'string', name: 'Value' },
                                    { type: 'string', name: 'Data' }
                                ],
                                data: [{ 'Value': 1, 'Data': 'First' },
                                    { 'Value': 2, 'Data': 'Second' },
                                    { 'Value': 3, 'Data': 'Third' },
                                    { 'Value': 4, 'Data': 'Fourth' }]
                            },
                            displayField: 'Data',
                            valueField: 'Value',
                            editable: false,
                            emptyText: app.localize('SelectOption'),
                            enableKeyEvents: true,
                            queryMode: 'local'
                        }, {
                            xtype: 'combobox',
                            name: 'WeekDayNameYearly',
                            inputCls: 'inputTextCls',
                            forceSelection: true,
                            ui: 'fieldLabelTop',
                            width: 108,
                            style: {
                                marginLeft: '10px'
                            },
                            store: {
                                fields: [
                                    { type: 'string', name: 'Value' },
                                    { type: 'string', name: 'Data' }
                                ],
                                data: [{ 'Value': '0', 'Data': 'Sunday' },
                                    { 'Value': '1', 'Data': 'Monday' },
                                    { 'Value': '2', 'Data': 'Tuesday' },
                                    { 'Value': '3', 'Data': 'Wednesday' },
                                    { 'Value': '4', 'Data': 'Thursday' },
                                    { 'Value': '5', 'Data': 'Friday' },
                                    { 'Value': '6', 'Data': 'Saturday' }]
                            },
                            displayField: 'Data',
                            valueField: 'Value',
                            editable: false,
                            emptyText: app.localize('SelectOption'),
                            enableKeyEvents: true,
                            queryMode: 'local'
                        }, {
                                xtype: 'combobox',
                                name: 'MonthNameofYearSpecific',
                                inputCls: 'inputTextCls',
                                forceSelection: true,
                                fieldLabel: app.localize('Of'),
                                labelStyle: 'padding-top:8px !important;',
                                ui: 'fieldLabelTop',
                                labelWidth: 20,
                                width : 140,
                                labelSeparator: '',
                                style: {
                                    marginLeft: '10px'
                                },
                                store: {
                                    fields: [
                                        { type: 'string', name: 'Value' },
                                        { type: 'string', name: 'Data' }
                                    ],
                                    data: [{ 'Value': 1, 'Data': 'January' },
                                        { 'Value': 2, 'Data': 'February' },
                                        { 'Value': 3, 'Data': 'March' },
                                        { 'Value': 4, 'Data': 'April' },
                                        { 'Value': 5, 'Data': 'May' },
                                        { 'Value': 6, 'Data': 'June	' },
                                        { 'Value': 7, 'Data': 'July' },
                                        { 'Value': 8, 'Data': 'August' },
                                        { 'Value': 9, 'Data': 'September' },
                                        { 'Value': 10, 'Data': 'October ' },
                                        { 'Value': 11, 'Data': 'November' },
                                        { 'Value': 12, 'Data': 'December' }]
                                },
                                displayField: 'Data',
                                valueField: 'Value',
                                editable: false,
                                emptyText: app.localize('SelectOption'),
                                enableKeyEvents: true,
                                queryMode: 'local'
                        }]
                    }
                ]
            }]
        }]
    }]

},
    {
        xtype : 'fieldset',
        title: app.localize('RecurrenceRange'),
        ui: 'transparentFieldSet',
        collapsible: true,
        layout: 'column',
        items : [{
            columnWidth: 0.3,
            padding: '10 10 0 0',
            xtype: 'datefield',
            name: 'startDate',
            fieldLabel: app.localize('RecurrenceStart'),
            ui: 'fieldLabelTop',
            labelWidth: 38,
            width: 160,
            msgTarget: 'side',
            value : new Date(),
            format: ChachingGlobals.defaultExtDateFieldFormat,
            submitFormat: ChachingGlobals.defaultExtDateFieldFormat,
            enableKeyEvents: true,
            invalidText: app.localize('InValidDateText')},
        {
            columnWidth: 0.7,
            padding: '10 10 0 20',
            xtype: 'fieldcontainer',
            ui: 'fieldLabelTop',
            fieldLabel: app.localize('EndRecurrence'),
            labelStyle: 'padding-top:8px !important;',
            layout: 'vbox',
            labelWidth : 110,
            items: [{
                xtype: 'radiofield',
                name: 'isNever',
                boxLabelCls: 'checkboxLabel',
                checked: true,
                inputValue: true,
                boxLabel: app.localize('NoEndDate'),
                listeners: {
                    change: 'onNoEndDateChange',
                    scope: 'controller'
                }
            },
            {
                xtype: 'fieldcontainer',
                layout: 'hbox',
                fieldDefaults: {
                   // margin: '0px 10px 0px 10px'
                },
                items: [{
                    xtype: 'radiofield',
                    name: 'isEndAfter',
                    inputValue: true,
                    boxLabel: app.localize('EndAfter'),
                    boxLabelCls: 'checkboxLabel',
                    listeners: {
                        change: 'onEndAfterChange',
                        scope: 'controller'
                    }
                }, {
                    xtype: 'textfield',
                    inputCls: 'inputTextCls',
                    style: {
                        marginLeft: '10px'
                    },
                    name: 'endOfOccurences',
                    ui: 'fieldLabelTop',
                    maskRe: /[0-9]/,
                    value: 0,
                    enforceMaxLength: true,
                    width : 50,
                    saveDelay: 400,
                    listeners: {
                        change: 'onEndOfOccurencesChange',
                        scope: 'controller'
                    }
                }, {
                    xtype: 'displayfield',
                    style : {
                        marginLeft : '10px'
                    },
                    ui: 'fieldLabelTop',
                    labelSeparator: '',
                    labelStyle: 'padding-top:8px !important;',
                    fieldLabel: app.localize('Occurrences')
                }]
            },
            {
                xtype: 'fieldcontainer',
                layout: 'hbox',
                fieldDefaults: {
                    //margin: '0px 10px 0px 10px'
                },
                items: [{
                    xtype: 'radiofield',
                    name: 'isRecurrenceEndDate',
                    inputValue: true,
                    boxLabel: app.localize('EndBy'),
                    boxLabelCls: 'checkboxLabel',
                    listeners: {
                        change: 'onRecurrenceEndDateSelected',
                        scope: 'controller'
                    }
                }, {
                    xtype: 'datefield',
                    style: {
                        marginLeft: '10px'
                    },
                    name: 'endDate',
                    ui: 'fieldLabelTop',
                    width: 120,
                    msgTarget: 'side',
                    format: ChachingGlobals.defaultExtDateFieldFormat,
                    submitFormat: ChachingGlobals.defaultExtDateFieldFormat,
                    enableKeyEvents: true,
                    invalidText: app.localize('InValidDateText'),
                    listeners: {
                        change: 'onRecurrenceEndDateChange',
                        scope: 'controller'
                    }
                }]
            }
            ]

        }
        ]
    }]
});

