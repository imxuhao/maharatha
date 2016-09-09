///**
// * This class is created as form for recurrence add/edit purpose. {@link Chaching.component.form.Panel}
// * Author: Krishna Garad
// * Date Created: 08/16/2016
// */
//Ext.define('Chaching.view.recurrence.RecurrenceForm', {
//    extend: 'Chaching.view.common.form.ChachingFormPanel',
//    alias: ['widget.recurrence.create', 'widget.recurrence.edit'],
//    requires: [
//        'Chaching.view.recurrence.RecurrenceFormController'
//    ],
//    openInPopupWindow: true,
//    hideDefaultButtons: false,
//    border: false,
//    scrollable: true,
//    controller: 'recurrence-recurrenceform',
//    modulePermissions: {
//        read: true,
//        create: true,
//        edit: true,
//        destroy: true
//    },
//    layout: {
//        type: 'fit'
//    },
//    items: [
//        //{
//        //xtype: 'hiddenfield',
//        //name: 'TransactionId'
//        //}, {
//        //xtype: 'hiddenfield',
//        //name: 'IsActive',
//        //value: false,
//        //inputValue: false
//        //}, {
//        //xtype: 'hiddenfield',
//        //name: 'RecurrenceId'
//        // }
//        //        , {
//        //        }, {
//        //            xtype: 'hiddenfield',
//        //            name: 'RowStamp'
//        //        }, 
//		//	{
//		//	    xtype: 'fieldset',
//		//	    height: 50,
//		//	    scope: this,
//		//	    disabled: false,
//		//	    frame: false,
//		//	    disabledCls: 'none;',
//		//	    items: [{
//		//	        xtype: 'textfield',
//		//	        padding: '10',
//		//	        fieldLabel: Aliaces.RecurrenceName + Aliaces.MandatoryFlag,
//		//	        name: 'Name',
//		//	        allowBlank: false,
//		//	        width: 240,
//		//	        labelWidth: 120,
//		//	        enforceMaxLength: true
//		//	    }]
//		//	},
//                {
//                    xtype: 'fieldset',
//                    height: 175,
//                    scope: this,
//                    title: app.localize('RecurrencePattern'),
//                    disabled: false,
//                    frame: false,
//                    disabledCls: 'none;',
//                    items: [
//                        {
//                            xtype: 'tabpanel',
//                            name: 'PeriodType',//'RecurranceTab',
//                            activeTab: 0,
//                            frame: false,
//                            height: 169,
//                            border: false,
//                            bodyBorder: false,
//                            minTabWidth: 114.5,
//                            bodyStyle: 'border:0;',
//                            width: '100%',
//                            tabPosition: 'top',
//                            items: [
//                                {
//                                    xtype: 'panel',
//                                    padding: '0',
//                                    width: 351,
//                                    title: app.localize('Daily'),
//                                    bodyBorder: false,
//                                    itemId: 'btnDaily',
//                                    border: false,
//                                    bodyStyle: 'border:0;',
//                                    layout: {
//                                        type: 'absolute',
//                                        width: 351
//                                    },
//                                    items: [
//                                        {
//                                            xtype: 'radiofield',
//                                            x: 0,
//                                            y: 10,
//                                            name: 'IsEverySpecificDay',
//                                            boxLabel: app.localize('Every'),
//                                            inputValue: true,
//                                            checked: true,
//                                            listeners: {
//                                                change: 'onEverySpecificDayChange',
//                                                scope: 'controller'
//                                            }
//                                        }, {
//                                            xtype: 'textfield',
//                                            x: 56,
//                                            y: 10,
//                                            name: 'EverySpecificDay',
//                                            width: 40,
//                                            maskRe: /[0-9]/,
//                                            value: 1,
//                                            enforceMaxLength: true
//                                        }, {
//                                            xtype: 'label',
//                                            x: 111,
//                                            y: 14,
//                                            text: app.localize('RecurrenceDays')
//                                        }, {
//                                            xtype: 'radiofield',
//                                            x: 0,
//                                            y: 47,
//                                            name: 'IsEveryWeekDay',
//                                            boxLabel: app.localize('EveryWeekDay'),
//                                            inputValue: true,
//                                            listeners: {
//                                                change: 'onEveryWeekDayChange',
//                                                scope: 'controller'
//                                            }
//                                        }
//                                    ]
//                                },
//                                {
//                                    xtype: 'panel',
//                                    padding: '0',
//                                    width: 350,
//                                    title: app.localize('Weekly'),
//                                    bodyBorder: false,
//                                    border: false,
//                                    itemId: 'btnWeekly',
//                                    bodyStyle: 'border:0;',
//                                    layout: {
//                                        type: 'absolute',
//                                        width: 351
//                                    },
//                                    items: [{
//                                        xtype: 'textfield',
//                                        x: 0,
//                                        y: 10,
//                                        name: 'RecurEverySpecificWeekOn',
//                                        width: 120,
//                                        fieldLabel: app.localize('RecurEvery'),
//                                        maskRe: /[0-9]/,
//                                        value: 1,
//                                        enforceMaxLength: true,
//                                        labelWidth: 80
//                                    }, {
//                                        xtype: 'label',
//                                        x: 131,
//                                        y: 14,
//                                        text: app.localize('Weekson')
//                                    }, {
//                                        xtype: 'checkboxgroup',
//                                        x: 0,
//                                        y: 40,
//                                        name: 'WeekDays',
//                                        columns: 4,
//                                        items: [
//                                            { boxLabel: app.localize('Sunday'), name: 'Sunday', inputValue: 'SUN' },
//                                            { boxLabel: app.localize('Monday'), name: 'Monday', inputValue: 'MON' },
//                                            { boxLabel: app.localize('Tuesday'), name: 'Tuesday', inputValue: 'TUE' },
//                                            { boxLabel: app.localize('Wednesday'), name: 'Wednesday', inputValue: 'WED' },
//                                            { boxLabel: app.localize('Thursday'), name: 'Thursday', inputValue: 'THU' },
//                                            { boxLabel: app.localize('Friday'), name: 'Friday', inputValue: 'FRI' },
//                                            { boxLabel: app.localize('Saturday'), name: 'Saturday', inputValue: 'SAT' }
//                                        ]
//                                    }
//                                    ]
//                                },
//                                {
//                                    xtype: 'panel',
//                                    padding: '0',
//                                    width: 350,
//                                    title: app.localize('Monthly'),
//                                    bodyBorder: false,
//                                    border: false,
//                                    itemId: 'btnMOnthly',
//                                    bodyStyle: 'border:0;',
//                                    layout: {
//                                        type: 'absolute',
//                                        width: 351
//                                    },
//                                    items: [{
//                                        xtype: 'radiofield',
//                                        x: 0,
//                                        y: 10,
//                                        name: 'IsDayOfEveryMonth',
//                                        checked: true,
//                                        inputValue: true,
//                                        boxLabel: app.localize('Day'),
//                                        listeners: {
//                                            change: 'onDayOfEveryMonthChange',
//                                            scope: 'controller'
//                                        }
//                                    }, {
//                                        xtype: 'textfield',
//                                        x: 50,
//                                        y: 10,
//                                        name: 'SpecificDayOfMonth',
//                                        width: 37,
//                                        maskRe: /[0-9]/,
//                                        maxLength: 10,
//                                        enforceMaxLength: true,
//                                        value: new Date().getDate()
//                                    }, {
//                                        xtype: 'label',
//                                        x: 106,
//                                        y: 14,
//                                        text: app.localize('OfEvery')
//                                    }, {
//                                        xtype: 'textfield',
//                                        x: 166,
//                                        y: 10,
//                                        name: 'MonthNumber',
//                                        width: 40,
//                                        maskRe: /[0-9]/,
//                                        value: 1,
//                                        enforceMaxLength: true
//                                    }, {
//                                        xtype: 'label',
//                                        x: 223,
//                                        y: 14,
//                                        text: app.localize('Months')
//                                    }, {
//                                        xtype: 'radiofield',
//                                        x: 0,
//                                        y: 50,
//                                        name: 'IsSpecificMonth',
//                                        boxLabel: app.localize('The'),
//                                        inputValue: true,
//                                        listeners: {
//                                            change: 'onSpecificMonthChange',
//                                            scope: 'controller'
//                                        }
//                                    }, {
//                                        xtype: 'combobox',
//                                        x: 50,
//                                        y: 50,
//                                        name: 'WeekNumberOfMonthly',
//                                        width: 80,
//                                        store: {
//                                            fields: [
//                                                { type: 'string', name: 'Value' },
//                                                { type: 'string', name: 'Data' }
//                                            ],
//                                            data: [{ 'Value': 1, 'Data': 'First' },
//                                                { 'Value': 2, 'Data': 'Second' },
//                                                { 'Value': 3, 'Data': 'Third' },
//                                                { 'Value': 4, 'Data': 'Fourth' }]
//                                        },
//                                        displayField: 'Data',
//                                        valueField: 'Value',
//                                        editable: false,
//                                        emptyText: app.localize('SelectOption'),
//                                        enableKeyEvents: true,
//                                        queryMode: 'local'
//                                    }, {
//                                        xtype: 'combobox',
//                                        x: 144,
//                                        y: 50,
//                                        name: 'WeekDayNameMonthly',
//                                        width: 108,
//                                        store: {
//                                            fields: [
//                                                { type: 'string', name: 'Value' },
//                                                { type: 'string', name: 'Data' }
//                                            ],
//                                            data: [{ 'Value': 'SUN', 'Data': 'Sunday' },
//                                                { 'Value': 'MON', 'Data': 'Monday' },
//                                                { 'Value': 'TUE', 'Data': 'Tuesday' },
//                                                { 'Value': 'WED', 'Data': 'Wednesday' },
//                                                { 'Value': 'THU', 'Data': 'Thursday' },
//                                                { 'Value': 'FRI', 'Data': 'Friday' },
//                                                { 'Value': 'SAT', 'Data': 'Saturday' }]
//                                        },
//                                        displayField: 'Data',
//                                        valueField: 'Value',
//                                        editable: false,
//                                        emptyText: app.localize('SelectOption'),
//                                        enableKeyEvents: true,
//                                        queryMode: 'local'
//                                    }, {
//                                        xtype: 'label',
//                                        x: 268,
//                                        y: 54,
//                                        text: app.localize('OfEvery')
//                                    }, {
//                                        xtype: 'textfield',
//                                        x: 325,
//                                        y: 50,
//                                        name: 'MonthNumberOfSpecific',
//                                        width: 46,
//                                        maskRe: /[0-9]/,
//                                        value: 1,
//                                        enforceMaxLength: true
//                                    }, {
//                                        xtype: 'label',
//                                        x: 386,
//                                        y: 54,
//                                        text: app.localize('Months')
//                                    }]
//                                },
//                                {
//                                    xtype: 'panel',
//                                    padding: '0',
//                                    width: 350,
//                                    title: app.localize('Yearly'),
//                                    bodyBorder: false,
//                                    border: false,
//                                    itemId: 'btnYearly',
//                                    bodyStyle: 'border:0;',
//                                    layout: {
//                                        type: 'absolute',
//                                        width: 351
//                                    },
//                                    items: [{
//                                        xtype: 'textfield',
//                                        x: 3,
//                                        y: 10,
//                                        name: 'RecurEveryYear',
//                                        width: 117,
//                                        fieldLabel: app.localize('RecurEvery'),
//                                        labelWidth: 76,
//                                        maskRe: /[0-9]/,
//                                        value: 1,
//                                        enforceMaxLength: true
//                                    }, {
//                                        xtype: 'label',
//                                        x: 130,
//                                        y: 14,
//                                        text: app.localize('Years')
//                                    }, {
//                                        xtype: 'radiofield',
//                                        x: 3,
//                                        y: 44,
//                                        name: 'IsMonthOfYear',
//                                        checked: true,
//                                        inputValue: true,
//                                        boxLabel: app.localize('RecurrenceOn'),
//                                        listeners: {
//                                            change: 'onMonthOfYearChange',
//                                            scope: 'controller'
//                                        }
//                                    }, {
//                                        xtype: 'combobox',
//                                        x: 70,
//                                        y: 44,
//                                        name: 'MonthNameofYear',
//                                        width: 108,
//                                        store: {
//                                            fields: [
//                                                { type: 'string', name: 'Value' },
//                                                { type: 'string', name: 'Data' }
//                                            ],
//                                            data: [{ 'Value': 1, 'Data': 'January' },
//                                                { 'Value': 2, 'Data': 'February' },
//                                                { 'Value': 3, 'Data': 'March' },
//                                                { 'Value': 4, 'Data': 'April' },
//                                                { 'Value': 5, 'Data': 'May' },
//                                                { 'Value': 6, 'Data': 'June	' },
//                                                { 'Value': 7, 'Data': 'July' },
//                                                { 'Value': 8, 'Data': 'August' },
//                                                { 'Value': 9, 'Data': 'September' },
//                                                { 'Value': 10, 'Data': 'October ' },
//                                                { 'Value': 11, 'Data': 'November' },
//                                                { 'Value': 12, 'Data': 'December' }]
//                                        },
//                                        displayField: 'Data',
//                                        valueField: 'Value',
//                                        editable: false,
//                                        emptyText: app.localize('SelectOption'),
//                                        enableKeyEvents: true,
//                                        queryMode: 'local'
//                                    }, {
//                                        xtype: 'textfield',
//                                        x: 192,
//                                        y: 44,
//                                        name: 'DayOfMonthYearly',
//                                        width: 37,
//                                        maskRe: /[0-9]/,
//                                        maxLength: 10,
//                                        enforceMaxLength: true,
//                                        value: new Date().getDate()
//                                    }, {
//                                        xtype: 'radiofield',
//                                        x: 3,
//                                        y: 84,
//                                        name: 'IsSpecficMonthofYear',
//                                        boxLabel: app.localize('Onthe'),
//                                        inputValue: true,
//                                        listeners: {
//                                            change: 'onSpecificMonthOfYearChange',
//                                            scope: 'controller'
//                                        }
//                                    }, {
//                                        xtype: 'combobox',
//                                        x: 70,
//                                        y: 84,
//                                        name: 'WeekNumberOfYearly',
//                                        width: 100,
//                                        store: {
//                                            fields: [
//                                                { type: 'string', name: 'Value' },
//                                                { type: 'string', name: 'Data' }
//                                            ],
//                                            data: [{ 'Value': 1, 'Data': 'First' },
//                                                { 'Value': 2, 'Data': 'Second' },
//                                                { 'Value': 3, 'Data': 'Third' },
//                                                { 'Value': 4, 'Data': 'Fourth' }]
//                                        },
//                                        displayField: 'Data',
//                                        valueField: 'Value',
//                                        editable: false,
//                                        emptyText: app.localize('SelectOption'),
//                                        enableKeyEvents: true,
//                                        queryMode: 'local'
//                                    }, {
//                                        xtype: 'combobox',
//                                        x: 192,
//                                        y: 84,
//                                        name: 'WeekDayNameYearly',
//                                        width: 100,
//                                        store: {
//                                            fields: [
//                                                { type: 'string', name: 'Value' },
//                                                { type: 'string', name: 'Data' }
//                                            ],
//                                            data: [{ 'Value': 'SUN', 'Data': 'Sunday' },
//                                                { 'Value': 'MON', 'Data': 'Monday' },
//                                                { 'Value': 'TUE', 'Data': 'Tuesday' },
//                                                { 'Value': 'WED', 'Data': 'Wednesday' },
//                                                { 'Value': 'THU', 'Data': 'Thursday' },
//                                                { 'Value': 'FRI', 'Data': 'Friday' },
//                                                { 'Value': 'SAT', 'Data': 'Saturday' }]
//                                        },
//                                        displayField: 'Data',
//                                        valueField: 'Value',
//                                        editable: false,
//                                        emptyText: app.localize('SelectOption'),
//                                        enableKeyEvents: true,
//                                        queryMode: 'local'
//                                    }, {
//                                        xtype: 'label',
//                                        x: 305,
//                                        y: 87,
//                                        text: app.localize('Of')
//                                    }, {
//                                        xtype: 'combobox',
//                                        x: 334,
//                                        y: 84,
//                                        name: 'MonthNameofYearSpecific',
//                                        width: 100,
//                                        store: {
//                                            fields: [
//                                                { type: 'string', name: 'Value' },
//                                                { type: 'string', name: 'Data' }
//                                            ],
//                                            data: [{ 'Value': 1, 'Data': 'January' },
//                                                { 'Value': 2, 'Data': 'February' },
//                                                { 'Value': 3, 'Data': 'March' },
//                                                { 'Value': 4, 'Data': 'April' },
//                                                { 'Value': 5, 'Data': 'May' },
//                                                { 'Value': 6, 'Data': 'June	' },
//                                                { 'Value': 7, 'Data': 'July' },
//                                                { 'Value': 8, 'Data': 'August' },
//                                                { 'Value': 9, 'Data': 'September' },
//                                                { 'Value': 10, 'Data': 'October ' },
//                                                { 'Value': 11, 'Data': 'November' },
//                                                { 'Value': 12, 'Data': 'December' }]
//                                        },
//                                        displayField: 'Data',
//                                        valueField: 'Value',
//                                        editable: false,
//                                        emptyText: app.localize('SelectOption'),
//                                        enableKeyEvents: true,
//                                        queryMode: 'local'
//                                    }
//                                    ]
//                                }
//                            ],
//                            listeners: {
//                                tabchange: 'onTabChange', scope: 'controller'
//                            }
//                        }
//                    ]
//                },
//                {
//                    xtype: 'fieldset',
//                    height: 175,
//                    scope: this,
//                    title: app.localize('RecurrenceRange'),
//                    disabled: false,
//                    frame: false,
//                    disabledCls: 'none;',
//                    layout: {
//                        type: 'absolute',
//                        width: 351
//                    },
//                    items: [
//                        {
//                            xtype: 'datefield',
//                            x: 3,
//                            y: 10,
//                            name: 'StartDate',
//                            fieldLabel: 'Start',
//                            labelWidth: 38,
//                            width: 160,
//                            msgTarget: 'side',
//                            format: ChachingGlobals.defaultExtDateFieldFormat,
//                            submitFormat: ChachingGlobals.defaultExtDateFieldFormat,
//                            enableKeyEvents: true,
//                            //plugins: [new Ext.ux.InputTextMask('99/99/99', true)],
//                            invalidText: app.localize('InValidDateText')
//                        }, {
//                            xtype: 'radiofield',
//                            x: 193,
//                            y: 10,
//                            name: 'IsNoEndDate',
//                            checked: true,
//                            inputValue: true,
//                            boxLabel: app.localize('NoEndDate'),
//                            listeners: {
//                                change: 'onNoEndDateChange',
//                                scope: 'controller'
//                            }
//                        }, {
//                            xtype: 'radiofield',
//                            x: 193,
//                            y: 50,
//                            name: 'IsEndAfter',
//                            inputValue: true,
//                            boxLabel: app.localize('EndAfter'),
//                            listeners: {
//                                change: 'onEndAfterChange',
//                                scope: 'controller'
//                            }
//                        }, {
//                            xtype: 'textfield',
//                            x: 275,
//                            y: 50,
//                            name: 'EndOfOccurences',
//                            width: 40,
//                            maskRe: /[0-9]/,
//                            value: 0,
//                            enforceMaxLength: true,
//                            saveDelay: 400,
//                            listeners: {
//                                change: 'onEndOfOccurencesChange',
//                                scope: 'controller'
//                            }
//                        }, {
//                            xtype: 'label',
//                            x: 330,
//                            y: 54,
//                            text: app.localize('Occurrences')
//                        }, {
//                            xtype: 'radiofield',
//                            x: 193,
//                            y: 90,
//                            name: 'IsRecurrenceEndDate',
//                            inputValue: true,
//                            boxLabel: app.localize('EndBy'),
//                            listeners: {
//                                change: 'onRecurrenceEndDateSelected',
//                                scope: 'controller'
//                            }
//                        }, {
//                            xtype: 'datefield',
//                            x: 275,
//                            y: 90,
//                            name: 'EndDate',
//                            width: 120,
//                            msgTarget: 'side',
//                            format: ChachingGlobals.defaultExtDateFieldFormat,
//                            submitFormat: ChachingGlobals.defaultExtDateFieldFormat,
//                            enableKeyEvents: true,
//                            invalidText: app.localize('InValidDateText'),
//                           // plugins: [new Ext.ux.InputTextMask('99/99/99', true)],
//                            listeners: {
//                                change: 'onRecurrenceEndDateChange',
//                                scope: 'controller'
//                            }
//                        }
//                    ]
//                }

//    ]
//});

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
    hideDefaultButtons: false,
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
                    name: 'IsEverySpecificDay',
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
                    style: {
                        marginLeft: '10px'
                    },
                    name: 'EverySpecificDay',
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
                    labelSeparator : '',
                    fieldLabel: app.localize('RecurrenceDays')
                }]
            }, {
                xtype: 'radiofield',
                name: 'IsEveryWeekDay',
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
            items: [{
                xtype: 'fieldcontainer',
                width: 600,
                layout: 'hbox',
                fieldDefaults: {
                    // margin: '0px 10px 0px 10px'
                },
                items: [{
                    xtype: 'textfield',
                    fieldLabel: app.localize('RecurEvery'),
                    name: 'RecurEverySpecificWeekOn',
                    ui: 'fieldLabelTop',
                    maskRe: /[0-9]/,
                    value: 1,
                    width : 140,
                    enforceMaxLength: true
                }, {
                    xtype: 'displayfield',
                    style: {
                        marginLeft: '10px'
                    },
                    ui: 'fieldLabelTop',
                    fieldLabel: app.localize('Weekson'),
                    labelSeparator : ''
                }]
            }, {
                xtype: 'checkboxgroup',
                fieldLabel: app.localize('RepeatOn'),
                labelStyle: 'padding-top:8px !important;',
                ui: 'fieldLabelTop',
                name: 'WeekDays',
                columns: 4,
                width : 500,
                items: [
                    { boxLabel: app.localize('Sunday'), boxLabelCls: 'checkboxLabel', name: 'Sunday', inputValue: 'SUN' },
                    { boxLabel: app.localize('Monday'), boxLabelCls: 'checkboxLabel', name: 'Monday', inputValue: 'MON' },
                    { boxLabel: app.localize('Tuesday'), boxLabelCls: 'checkboxLabel', name: 'Tuesday', inputValue: 'TUE' },
                    { boxLabel: app.localize('Wednesday'), boxLabelCls: 'checkboxLabel', name: 'Wednesday', inputValue: 'WED' },
                    { boxLabel: app.localize('Thursday'), boxLabelCls: 'checkboxLabel', name: 'Thursday', inputValue: 'THU' },
                    { boxLabel: app.localize('Friday'), boxLabelCls: 'checkboxLabel', name: 'Friday', inputValue: 'FRI' },
                    { boxLabel: app.localize('Saturday'), boxLabelCls: 'checkboxLabel', name: 'Saturday', inputValue: 'SAT' }
                ]
            }]

        }, {
            title: app.localize('Monthly'),
            layout: 'vbox',
            width: 600,
            items: [{
                xtype: 'fieldcontainer',
                layout: 'hbox',
                width: 600,
                fieldDefaults: {
                    // margin: '0px 10px 0px 10px'
                },
                items: [{
                    xtype: 'textfield',
                    fieldLabel: app.localize('RecurEvery'),
                    name: 'MonthNumber',
                    ui: 'fieldLabelTop',
                    maskRe: /[0-9]/,
                    value: 1,
                    width: 140,
                    enforceMaxLength: true
                }, {
                    xtype: 'displayfield',
                    style: {
                        marginLeft: '10px'
                    },
                    ui: 'fieldLabelTop',
                    fieldLabel: app.localize('Months'),
                    labelSeparator : ''
                }]
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: app.localize('RepeatOn'),
                labelStyle: 'padding-top:8px !important;',
                ui: 'fieldLabelTop',
                layout: 'vbox',
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
                            style: {
                                marginLeft: '10px'
                            },
                            name: 'SpecificDayOfMonth',
                            ui: 'fieldLabelTop',
                            width: 37,
                            maskRe: /[0-9]/,
                            maxLength: 10,
                            enforceMaxLength: true,
                            value: new Date().getDate()
                        }, {
                            xtype: 'displayfield',
                            style: {
                                marginLeft: '10px'
                            },
                            ui: 'fieldLabelTop',
                            fieldLabel: app.localize('OfTheMonth'),
                            labelSeparator : ''
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
                                data: [{ 'Value': 'SUN', 'Data': 'Sunday' },
                                    { 'Value': 'MON', 'Data': 'Monday' },
                                    { 'Value': 'TUE', 'Data': 'Tuesday' },
                                    { 'Value': 'WED', 'Data': 'Wednesday' },
                                    { 'Value': 'THU', 'Data': 'Thursday' },
                                    { 'Value': 'FRI', 'Data': 'Friday' },
                                    { 'Value': 'SAT', 'Data': 'Saturday' }]
                            },
                            displayField: 'Data',
                            valueField: 'Value',
                            editable: false,
                            emptyText: app.localize('SelectOption'),
                            enableKeyEvents: true,
                            queryMode: 'local'
                        }, {
                            xtype: 'displayfield',
                            style: {
                                marginLeft: '10px'
                            },
                            ui: 'fieldLabelTop',
                            fieldLabel: app.localize('OfTheMonth'),
                            labelSeparator : ''
                        }]
                    }
                ]
            }]
        }, {
            title: app.localize('Yearly'),
            layout: 'vbox',
            items: [{
                xtype: 'fieldcontainer',
                width: 600,
                anchor : '100%',
                layout: 'hbox',
                fieldDefaults: {
                    // margin: '0px 10px 0px 10px'
                },
                items: [{
                    xtype: 'textfield',
                    fieldLabel: app.localize('RecurEvery'),
                    name: 'RecurEveryYear',
                    ui: 'fieldLabelTop',
                    maskRe: /[0-9]/,
                    value: 1,
                    width: 140,
                    enforceMaxLength: true
                }, {
                    //xtype: 'label',
                    //style : {
                    //    marginLeft : '10px'
                    //},
                    //text: app.localize('Years')

                    xtype: 'displayfield',
                    ui: 'fieldLabelTop',
                    style: {
                        marginLeft: '10px'
                    },
                    labelSeparator : '',
                    fieldLabel: app.localize('Years')
                }]
            }, {
                xtype: 'fieldcontainer',
                fieldLabel: app.localize('RepeatOn'),
                labelStyle: 'padding-top:8px !important;',
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
                            name: 'DayOfMonthYearly',
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
                            style: {
                                marginLeft: '10px'
                            },
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
                                data: [{ 'Value': 'SUN', 'Data': 'Sunday' },
                                    { 'Value': 'MON', 'Data': 'Monday' },
                                    { 'Value': 'TUE', 'Data': 'Tuesday' },
                                    { 'Value': 'WED', 'Data': 'Wednesday' },
                                    { 'Value': 'THU', 'Data': 'Thursday' },
                                    { 'Value': 'FRI', 'Data': 'Friday' },
                                    { 'Value': 'SAT', 'Data': 'Saturday' }]
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
                                fieldLabel: app.localize('Of'),
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
            name: 'StartDate',
            fieldLabel: 'Start',
            ui: 'fieldLabelTop',
            labelWidth: 38,
            width: 160,
            msgTarget: 'side',
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
            labelStyle : 'padding-top:8px !important;',
            layout: 'vbox',
            labelWidth : 110,
            items: [{
                xtype: 'radiofield',
                name: 'IsNoEndDate',
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
                    name: 'IsEndAfter',
                    inputValue: true,
                    boxLabel: app.localize('EndAfter'),
                    boxLabelCls: 'checkboxLabel',
                    listeners: {
                        change: 'onEndAfterChange',
                        scope: 'controller'
                    }
                }, {
                    xtype: 'textfield',
                    style: {
                        marginLeft: '10px'
                    },
                    name: 'EndOfOccurences',
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
                    labelSeparator : '',
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
                    name: 'IsRecurrenceEndDate',
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
                    name: 'EndDate',
                    ui: 'fieldLabelTop',
                    width: 120,
                    msgTarget: 'side',
                    format: ChachingGlobals.defaultExtDateFieldFormat,
                    submitFormat: ChachingGlobals.defaultExtDateFieldFormat,
                    enableKeyEvents: true,
                    invalidText: app.localize('InValidDateText'),
                    // plugins: [new Ext.ux.InputTextMask('99/99/99', true)],
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

