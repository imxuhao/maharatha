Ext.define('Chaching.utilities.ChachingGlobals', {
    singleton: true,
    loggedInUserInfo: {
        defaultOrganizationId:null,
        emailAddress: null,
        userId: null,
        name: null,
        profilePictureId: null,
        surname: null,
        userName:null,
        userOrganizationId: null,
        gotoMyAccount:false
    },
    defaultDateFormat: 'MM/DD/YYYY',
    defaultDateTimeFormat: 'MM/DD/YYYY hh:mm a',
    defaultDateTimeSecFormat: 'MM/DD/YYYY hh:mm:ss a',
    usersDefaultGridViewSettings: null,
    mandatoryFlag: '&nbsp;<font color=red>*</font>'
});