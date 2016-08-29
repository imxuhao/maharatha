
Ext.define('Chaching.view.pettycash.entry.PettyCashForm',{
    extend: 'Ext.panel.Panel',
    alias: ['widget.pettycash.entry.create', 'widget.pettycash.entry.edit'],
    requires: [
        'Chaching.view.pettycash.entry.PettyCashFormController'
    ],

    controller: 'pettycash-entry-pettycashform',

    html: 'Hello, World!!'
});
