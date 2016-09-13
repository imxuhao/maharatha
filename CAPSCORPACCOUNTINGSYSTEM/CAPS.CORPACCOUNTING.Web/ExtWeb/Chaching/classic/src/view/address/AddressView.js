/**
 * Popup window containing {@link Chaching.view.address.AddressForm} for address add/edit.
 * Author: Krishna Garad
 * Date Created: 08/16/2016
 */
Ext.define('Chaching.view.address.AddressView',{
    extend: 'Chaching.view.common.window.ChachingWindowPanel',
    alias: ['widget.address.createView', 'widget.address.editView'],
    requires: [
        'Chaching.view.address.AddressViewController',
        'Chaching.view.address.AddressForm'
    ],
    height: '60%',
    width: '55%',
    layout: 'fit',
    controller: 'address-addressview',
    initComponent: function (config) {
        var me = this,
            controller = me.getController();
        var form = Ext.create('Chaching.view.address.AddressForm', {
            height: '100%',
            width: '100%',
            name: 'Address'
        });
        me.items = [form];
        me.callParent(arguments);
    }
});
