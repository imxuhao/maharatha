/**
 * This class is created as pop up window for recurrence purpose. {@link Chaching.view.recurrence.RecurrenceView}
 * Author: Kamal
 * Date Created: 09/8/2016
 */
Ext.define('Chaching.view.recurrence.RecurrenceView', {
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.recurrence'],
    requires: [
        'Chaching.view.recurrence.RecurrenceViewController',
        'Chaching.view.recurrence.RecurrenceForm'
    ],
    height: 550,
    width: 550,
    layout: 'fit',
    autoShow: true,
    modal : true,
    controller: 'recurrence-recurrenceview',
    title: app.localize('Recurrence'),
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.recurrence.RecurrenceForm', {
            //height: '100%',
           // width: '100%',
            name: 'Recurrence'
        });
        me.items = [form];
        me.callParent(arguments);
    }
});
