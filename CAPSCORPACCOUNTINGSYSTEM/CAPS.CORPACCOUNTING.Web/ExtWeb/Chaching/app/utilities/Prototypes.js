Ext.define('Chaching.utilities.Prototypes', {
    singleton: true
});

// Add the additional 'advanced' VTypes
Ext.apply(Ext.form.field.VTypes, {
    daterange: function (val, field) {
        var date = field.parseDate(val);
        if (!date) {
            return false;
        }
        if (field.startDateField && (!this.dateRangeMax || (date.getTime() != this.dateRangeMax.getTime()))) {
            var start = field.up('form').down('#' + field.startDateField);
            start.setMaxValue(date);
           // start.validate();
            this.dateRangeMax = date;
        }
        else if (field.endDateField && (!this.dateRangeMin || (date.getTime() != this.dateRangeMin.getTime()))) {
            var end = field.up('form').down('#' + field.endDateField);
            end.setMinValue(date);
           // end.validate();
            this.dateRangeMin = date;
        }
        /*
         * Always return true since we're only using this vtype to set the
         * min/max allowed values (these are tested for after the vtype test)
         */
        return true;
    },

    daterangeText: 'Start date must be less than end date'
});

String.prototype.replaceAll = function(find, replace) {
    var str = this;
    return str.replace(new RegExp(find, 'g'), replace);
};
String.prototype.format = function () {
    // The string containing the format items (e.g. "{0}")
    // will and always has to be the first argument.
    var theString = arguments[0];

    // start with the second argument (i = 1)
    for (var i = 1; i < arguments.length; i++) {
        // "gm" = RegEx options for Global search (more than one instance)
        // and for Multiline search
        var regEx = new RegExp("\\{" + (i - 1) + "\\}", "gm");
        theString = theString.replace(regEx, arguments[i]);
    }

    return theString;
};
String.prototype.startsWith = function (searchString, position) {
    position = position || 0;
    return this.lastIndexOf(searchString, position) === position;
};
String.prototype.indexesOf = function (v, charTosearch) {
    //Returns the positions of a char in a string
    var str = v;
    var indices = [];
    for (var j = 0; j < str.length; j++) {
        if (str[j] === charTosearch) indices.push(j);
    }
    return indices;
};
String.prototype.insertAt = function (index, string) {
    //Insert a char at a position in a string
    return this.substr(0, index) + string + this.substr(index);
};

String.prototype.getDateString = function (monthName, year, isStartDateOfMonth) {
    //Returns numeric month
    var dateStr = "";
    var isLeapYear = false;
    var noOfDays = 0;
    var month = '';
    if (monthName == "Jan") { noOfDays = 31, month = 01 };
    if (monthName == "Feb") { noOfDays = isLeapYear.isLeapYear(year) ? 29 : 28 ,  month = 02};
    if (monthName == "Mar") { noOfDays = 31, month = 03 };
    if (monthName == "Apr") { noOfDays = 30, month = 04 };
    if (monthName == "May") { noOfDays = 31, month = 05 };
    if (monthName == "Jun") { noOfDays = 30, month = 06 };
    if (monthName == "Jul") { noOfDays = 31, month = 07 };
    if (monthName == "Aug") { noOfDays = 31, month = 08 };
    if (monthName == "Sep") { noOfDays = 30, month = 09 };
    if (monthName == "Oct") { noOfDays = 31, month = 10 };
    if (monthName == "Nov") { noOfDays = 30, month = 11 };
    if (monthName == "Dec") { noOfDays = 31, month = 12 };
   
    if (isStartDateOfMonth) {
        dateStr = month + '/' + '01/' + year;
    } else {
        dateStr = month + '/' + noOfDays + '/' + year;
    }
    return Ext.util.Format.date(dateStr, Chaching.utilities.ChachingGlobals.defaultExtDateFieldFormat);
};

Boolean.prototype.isLeapYear = function (year) {
    if ((parseInt(year) % 4) == 0) {
        if (parseInt(year) % 100 == 0) {
            if (parseInt(year) % 400 != 0) {
                return false;
            }
            if (parseInt(year) % 400 == 0) {
                return true;
            }
        }
        if (parseInt(year) % 100 != 0) {
            return true;
        }
    }
    if ((parseInt(year) % 4) != 0) {
        return false;
    }
};

String.prototype.initCap = function () {
    return this.toLowerCase().replace(/(?:^|\s)[a-z]/g, function (m) {
        return m.toUpperCase();
    });
};