/**
 * This file contains the custom component for speadsheet selection model.
 * Author: Krishna Garad
 * Date:06/01/2016
 */

/**
 * A selection model for {@link Ext.grid.Panel grids} which allows you to select data in
 * a spreadsheet-like manner.
 *
 * Supported features:
 *
 *  - Single / Range / Multiple individual row selection.
 *  - Single / Range cell selection.
 *  - Column selection by click selecting column headers.
 *  - Select / deselect all by clicking in the top-left, header.
 *  - Adds row number column to enable row selection.
 *  - Optionally you can enable row selection using checkboxes
 *
 * # Example usage
 *
 *     @example
 *     var store = Ext.create('Ext.data.Store', {
 *         fields: ['name', 'email', 'phone'],
 *         data: [
 *             { name: 'Lisa', email: 'lisa@simpsons.com', phone: '555-111-1224' },
 *             { name: 'Bart', email: 'bart@simpsons.com', phone: '555-222-1234' },
 *             { name: 'Homer', email: 'homer@simpsons.com', phone: '555-222-1244' },
 *             { name: 'Marge', email: 'marge@simpsons.com', phone: '555-222-1254' }
 *         ]
 *     });
 *
 *     Ext.create('Ext.grid.Panel', {
 *         title: 'Simpsons',
 *         store: store,
 *         width: 400,
 *         renderTo: Ext.getBody(),
 *         columns: [
 *             { text: 'Name', dataIndex: 'name' },
 *             { text: 'Email', dataIndex: 'email', flex: 1 },
 *             { text: 'Phone', dataIndex: 'phone' }
 *         ],
 *         selModel: {
 *            type: 'chachingSpreadsheetSelectionModel'
 *         }
 *     });
 *
 * @since 5.1.0
 */
Ext.define('Chaching.components.selection.ChachingSpreadsheetModel', {
    extend: 'Ext.grid.selection.SpreadsheetModel',
    alias: 'selection.chachingSpreadsheetSelectionModel',
    privates: {
        getNumbererColumnConfig: function () {
            var me = this;

            return {
                xtype: 'rownumberer',
                width: me.rowNumbererHeaderWidth,
                editRenderer: '&#160;',
                tdCls: me.rowNumbererTdCls,
                cls: me.rowNumbererHeaderCls,
                locked: me.hasLockedHeader,
                summaryRenderer: Chaching.utilities.ChachingRenderers.summaryTotalTextRenderer
            };
        }
    }
});