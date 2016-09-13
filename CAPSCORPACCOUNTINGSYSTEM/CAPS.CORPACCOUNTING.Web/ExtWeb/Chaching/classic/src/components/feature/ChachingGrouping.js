/**
 * This file contains the correction in library for grouping menu item on hiding and showing columns.
 * Author: Krishna Garad
 * Date:09/07/2016
 */
/**
 * This feature allows to display the grid rows aggregated into groups as specified by the {@link Ext.data.Store#grouper grouper}
 *
 * underneath. The groups can also be expanded and collapsed.
 *
 * ## Extra Events
 *
 * This feature adds several extra events that will be fired on the grid to interact with the groups:
 *
 *  - {@link #groupclick}
 *  - {@link #groupdblclick}
 *  - {@link #groupcontextmenu}
 *  - {@link #groupexpand}
 *  - {@link #groupcollapse}
 *
 * ## Menu Augmentation
 *
 * This feature adds extra options to the grid column menu to provide the user with functionality to modify the grouping.
 * This can be disabled by setting the {@link #enableGroupingMenu} option. The option to disallow grouping from being turned off
 * by the user is {@link #enableNoGroups}.
 *
 * ## Controlling Group Text
 *
 * The {@link #groupHeaderTpl} is used to control the rendered title for each group. It can modified to customized
 * the default display.
 *
 * ## Groupers
 *
 * By default, this feature expects that the data field that is mapped to by the 
 * {@link Ext.data.AbstractStore#groupField} config is a simple data type such as a 
 * String or a Boolean. However, if you intend to group by a data field that is a 
 * complex data type such as an Object or Array, it is necessary to define one or more 
 * {@link Ext.util.Grouper groupers} on the feature that it can then use to lookup 
 * internal group information when grouping by different fields.
 *
 *     @example
 *     var feature = Ext.create('Ext.grid.feature.ChachingGrouping', {
 *         startCollapsed: true,
 *         groupers: [{
 *             property: 'asset',
 *             groupFn: function (val) {
 *                 return val.data.name;
 *             }
 *         }]
 *     });
 *
 * ## Example Usage
 *
 *     @example
 *     var store = Ext.create('Ext.data.Store', {
 *         fields: ['name', 'seniority', 'department'],
 *         groupField: 'department',
 *         data: [
 *             { name: 'Michael Scott', seniority: 7, department: 'Management' },
 *             { name: 'Dwight Schrute', seniority: 2, department: 'Sales' },
 *             { name: 'Jim Halpert', seniority: 3, department: 'Sales' },
 *             { name: 'Kevin Malone', seniority: 4, department: 'Accounting' },
 *             { name: 'Angela Martin', seniority: 5, department: 'Accounting' }
 *         ]
 *     });
 *
 *     Ext.create('Ext.grid.Panel', {
 *         title: 'Employees',
 *         store: store,
 *         columns: [
 *             { text: 'Name', dataIndex: 'name' },
 *             { text: 'Seniority', dataIndex: 'seniority' }
 *         ],
 *         features: [{ftype:'chachinggrouping'}],
 *         width: 200,
 *         height: 275,
 *         renderTo: Ext.getBody()
 *     });
 *
 * **Note:** To use grouping with a grid that has {@link Ext.grid.column.Column#locked locked columns}, you need to supply
 * the grouping feature as a config object - so the grid can create two instances of the grouping feature.
 */
Ext.define('Chaching.components.feature.ChachingGrouping',
{
    extend: 'Ext.grid.feature.Grouping',
    alias: 'feature.chachingGrouping',
    showMenuBy: function (clickEvent, t, header) {
        var me = this,
            menu = me.getMenu(),
            groupMenuItem = menu.down('#groupMenuItem'),
            groupMenuMeth = header.groupable === false || !header.dataIndex || me.view.headerCt.getVisibleGridColumns().length < 2 ? 'disable' : 'enable',
            groupToggleMenuItem = menu.down('#groupToggleMenuItem'),
            isGrouped = me.grid.getStore().isGrouped(),
            grid = me.view && me.view.ownerGrid;

        if (!groupMenuItem && header.groupable && grid && grid.requireGrouping) {
            var groupingItem = me.getMenuItems();
            me.menu = new Ext.menu.Menu({
                hideOnParentHide: false,
                // Persists when owning ColumnHeader is hidden
                items: groupingItem,
                listeners: {
                    beforeshow: me.beforeMenuShow,
                    hide: me.onMenuHide,
                    scope: me
                }
            });
            me.fireEvent('menucreate', me, me.menu);

            grid.fireEvent('headermenucreate', grid, me.menu, me);
            groupMenuItem = me.menu.down('#groupMenuItem');
        }
        groupMenuItem[groupMenuMeth]();
            if (groupToggleMenuItem) {
                groupToggleMenuItem.setChecked(isGrouped, true);
                groupToggleMenuItem[isGrouped ? 'enable' : 'disable']();
            }
        
        Ext.grid.header.Container.prototype.showMenuBy.apply(me, arguments);
    },
    getMenuItems: function () {
        var me = this,
            groupByText = me.groupByText,
            disabled = me.disabled || !me.getGroupField(),
            showGroupsText = me.showGroupsText,
            enableNoGroups = me.enableNoGroups,
            getMenuItems = me.view.headerCt.getMenuItems;

        // runs in the scope of headerCt
        return function () {

            // We cannot use the method from HeaderContainer's prototype here
            // because other plugins or features may already have injected an implementation
            var o = getMenuItems.call(this);
            o.push('-', {
                iconCls: Ext.baseCSSPrefix + 'group-by-icon',
                itemId: 'groupMenuItem',
                text: groupByText,
                handler: me.onGroupMenuItemClick,
                scope: me
            });
            if (enableNoGroups) {
                o.push({
                    itemId: 'groupToggleMenuItem',
                    text: showGroupsText,
                    checked: !disabled,
                    checkHandler: me.onGroupToggleMenuItemClick,
                    scope: me
                });
            }
            return o;
        };
    }
});