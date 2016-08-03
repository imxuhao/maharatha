/*
 * This file is generated and updated by Sencha Cmd. You can edit this file as
 * needed for your application, but these edits will have to be merged by
 * Sencha Cmd when upgrading.
 */

//This will remove _dc cache param from requests that are getting files.
Ext.Loader.setConfig({
    disableCaching: false
});

//For disabling _dc on XHR Ext.Ajax requests use
Ext.Ajax.disableCaching = false;

Ext.application({
    name: 'Chaching',

    extend: 'Chaching.Application',

    requires: [
         'Chaching.*'
    ]

    // The name of the initial view to create. With the classic toolkit this class
    // will gain a "viewport" plugin if it does not extend Ext.Viewport. With the
    // modern toolkit, the main view will be added to the Viewport.
    //
    //mainView: 'Chaching.view.main.ChachingViewport'
	
    //-------------------------------------------------------------------------
    // Most customizations should be made to Chaching.Application. If you need to
    // customize this file, doing so below this section reduces the likelihood
    // of merge conflicts when upgrading to new versions of Sencha Cmd.
    //-------------------------------------------------------------------------
});
