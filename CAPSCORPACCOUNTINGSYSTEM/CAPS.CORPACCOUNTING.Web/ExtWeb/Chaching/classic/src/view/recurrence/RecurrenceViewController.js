Ext.define('Chaching.view.recurrence.RecurrenceViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.recurrence-recurrenceview',
    parentController : null,
    onSaveRecurrenceClicked: function (btn) {
        var me = this,
            wnd = me.getView(),
            form = wnd.down('form'),
            basicform = form.getForm();
        var requestObject = me.prepareRequestObject(wnd, basicform);
        me.getCronExpression(requestObject);
    },
    onCancelRecurrenceClicked: function (btn) {
        var me = this,
          wnd = me.getView();
        Ext.destroy(wnd);
    },

    prepareRequestObject: function (wnd, form) {
        var requestObject = {};
        var tabPanel = wnd.down('tabpanel'),
            activeTab = tabPanel.getActiveTab(),
            title = activeTab.title,
            activeTabIndex = tabPanel.items.findIndex('id', activeTab.id),
            recurrenceStartDate = form.findField('startDate').value,
            isNeverEndDate = form.findField('isNever').value,
            isEndAfter = form.findField('isEndAfter').value,
            endOfOccurences = parseInt(form.findField('endOfOccurences').value),
            isRecurrenceEndDate = form.findField('isRecurrenceEndDate').value,
            recurrenceEndDate = form.findField('endDate').value;
        switch (activeTabIndex) {
            case 0:
                var isEverySpecificDay = form.findField('isEverySpecificDay').value,
                    everySpecificDay = parseInt(form.findField('everySpecificDay').value),
                    isEveryWeekDay = form.findField('isEveryWeekDay').value;
                    requestObject.scheduleType = 3;
                    requestObject.timeZone = abp.setting.values["Abp.Timing.TimeZone"];
                    requestObject.startDate = recurrenceStartDate;
                    requestObject.repeat = isEverySpecificDay ? everySpecificDay : null;
                    requestObject.jobEndType = isNeverEndDate ? 1 : (isEndAfter ? 2 : (isRecurrenceEndDate ? 3 : 0));
                    requestObject.occurrence = endOfOccurences;//everySpecificDay;
                    requestObject.endDate = null;
                    if (isRecurrenceEndDate) {
                        requestObject.endDate = recurrenceEndDate;
                    }
                    if (isEveryWeekDay) {
                        requestObject.days = [1,2,3,4,5];
                    }
                break;
            case 1:
                var  weekDays = form.findField('weekDays').getValue();
                var arrayOfWeekDays = Object.keys(weekDays).map(function (key) {
                    return weekDays[key];
                });
                requestObject.scheduleType = 4;
                requestObject.days = arrayOfWeekDays;
                requestObject.timeZone = abp.setting.values["Abp.Timing.TimeZone"];
                requestObject.startDate = recurrenceStartDate;
                requestObject.repeat = null;
                requestObject.jobEndType = isNeverEndDate ? 1 : (isEndAfter ? 2 : (isRecurrenceEndDate ? 3 : 0));
                requestObject.occurrence = endOfOccurences;
                requestObject.endDate = null;
                if (isRecurrenceEndDate) {
                    requestObject.endDate = recurrenceEndDate;
                }
                break;
            case 2:
                var isDayOfEveryMonth = form.findField('IsDayOfEveryMonth').value,
                    specificDayOfMonth = parseInt(form.findField('SpecificDayOfMonth').value),
                    monthNumber = parseInt(form.findField('MonthNumber').value),
                     specificMonthNumber = parseInt(form.findField('specificMonthNumber').value),
                    isSpecificMonth = form.findField('IsSpecificMonth').value,
                    weekNumberOfMonthly = parseInt(form.findField('WeekNumberOfMonthly').value),
                    weekDayNameMonthly = form.findField('WeekDayNameMonthly').value;

                    requestObject.scheduleType = 5;
                    requestObject.timeZone = abp.setting.values["Abp.Timing.TimeZone"];
                    requestObject.startDate = recurrenceStartDate;
                    requestObject.repeat = isDayOfEveryMonth ? monthNumber : (isSpecificMonth ? specificMonthNumber : null); 
                    requestObject.jobEndType = isNeverEndDate ? 1 : (isEndAfter ? 2 : (isRecurrenceEndDate ? 3 : 0));
                    requestObject.occurrence = endOfOccurences;
                    requestObject.endDate = null;
                    requestObject.dayOfMonth = isDayOfEveryMonth ? specificDayOfMonth : null;
                    requestObject.dayOfWeek = isSpecificMonth ? weekDayNameMonthly + '#' + weekNumberOfMonthly : null;
                    if (isRecurrenceEndDate) {
                        requestObject.endDate = recurrenceEndDate;
                    }
                break;
            case 3:
                var isMonthOfYear = form.findField('IsMonthOfYear').value,
                    monthNameofYear = parseInt(form.findField('MonthNameofYear').value),
                    dayOfMonthYearly = parseInt(form.findField('DayOfMonthYearly').value),
                    isSpecficMonthofYear = form.findField('IsSpecficMonthofYear').value,
                    weekNumberOfYearly = parseInt(form.findField('WeekNumberOfYearly').value),
                    weekDayNameYearly = form.findField('WeekDayNameYearly').value,
                    monthNameofYearSpecific = parseInt(form.findField('MonthNameofYearSpecific').value);

                    requestObject.scheduleType = 6;
                    requestObject.timeZone = abp.setting.values["Abp.Timing.TimeZone"];
                    requestObject.startDate = recurrenceStartDate;
                    requestObject.repeat = null;
                    requestObject.jobEndType = isNeverEndDate ? 1 : (isEndAfter ? 2 : (isRecurrenceEndDate ? 3 : 0));
                    requestObject.occurrence = endOfOccurences;
                    requestObject.endDate = null;
                    requestObject.month = isMonthOfYear ? monthNameofYear : (isSpecficMonthofYear ? monthNameofYearSpecific : null);
                    requestObject.dayOfMonth = isMonthOfYear ? dayOfMonthYearly : null;
                    requestObject.dayOfWeek = isMonthOfYear ? null : (isSpecficMonthofYear ? (weekDayNameYearly + '#' + weekNumberOfYearly) : null);
                    if (isRecurrenceEndDate) {
                        requestObject.endDate = recurrenceEndDate;
                    }
                break;
        }

        return requestObject;
    },

    getCronExpression: function (reqParamsObject) {
        var me = this;
        Ext.Ajax.request({
            url: abp.appPath +  'api/services/app/cronExpression/GetCronExpression',
            method: 'POST',
            jsonData : reqParamsObject,
            success: function (response) {
                var dataResponse = Ext.decode(response.responseText);
                if (dataResponse.success) {
                    me.parentController.cronExpression = dataResponse.result.cronExpression;
                    var wnd = me.getView();
                    Ext.destroy(wnd);
                } else {
                    ChachingGlobals.showPageSpecificErrors(response);
                }
            },
            failure: function (response) {
                ChachingGlobals.showPageSpecificErrors(response);
            }
        });
    }


});
