Ext.define('Chaching.utilities.Prototypes', {
    singleton: true
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
String.prototype.initCap = function () {
    return this.toLowerCase().replace(/(?:^|\s)[a-z]/g, function (m) {
        return m.toUpperCase();
    });
};