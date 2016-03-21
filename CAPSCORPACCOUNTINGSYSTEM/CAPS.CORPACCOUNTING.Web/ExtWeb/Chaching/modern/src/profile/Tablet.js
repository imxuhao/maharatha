Ext.define('Chaching.profile.Tablet', {
    extend: 'Ext.app.Profile',

    //requires: [
    //    'Chaching.view.tablet.*'
    //],

    isActive: function () {
        return !Ext.platformTags.phone;
    }
});
