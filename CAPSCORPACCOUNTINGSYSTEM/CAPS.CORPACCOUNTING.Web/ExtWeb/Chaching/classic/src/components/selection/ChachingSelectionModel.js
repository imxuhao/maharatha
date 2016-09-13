/**
 * This file contains the custom component for rendering column header text for checkbox selection model.
 * Author: Krishna Garad
 * Date:04/15/2016
 */

Ext.define('Chaching.components.selection.CheckboxSelectionModel', {
    extend: 'Ext.selection.CheckboxModel',
    alias: 'selection.chachingCheckboxSelectionModel',

    headerWidth: 40,
    headerText: '',
    getHeaderConfig: function () {
        var me = this,
            showCheck = me.showHeaderCheckbox !== false;
        var headerTextStyle = 'style="float:right; width:70%;text-align:left;"';
        if (showCheck)
            headerTextStyle = 'style = "width:70%;text-align:left;"';
        var checkBox = {
            isCheckerHd: showCheck,
            text: me.headerText,
            labelAlign: 'right',
            clickTargetName: 'el',
            width: me.headerWidth,
            sortable: false,
            draggable: false,
            resizable: false,
            hideable: false,
            menuDisabled: true,
            dataIndex: '',
            tdCls: me.tdCls,
            cls: showCheck ? Ext.baseCSSPrefix + 'column-header-checkbox' : '',
            defaultRenderer: me.renderer.bind(me),
            editRenderer: me.editRenderer || me.renderEmpty,
            locked: me.hasLockedHeader(),
            ///TODO: Once Ext fixes out headerText issue remove renderTpl
            renderTpl: [
        '<div id="{id}-titleEl" data-ref="titleEl" {tipMarkup}class="', Ext.baseCSSPrefix, 'column-header-inner<tpl if="!$comp.isContainer"> ', Ext.baseCSSPrefix, 'leaf-column-header</tpl>',
            '<tpl if="empty"> ', Ext.baseCSSPrefix, 'column-header-inner-empty</tpl>">',
            '<span id="{id}-textContainerEl" data-ref="textContainerEl" class="', Ext.baseCSSPrefix, 'column-header-text-container">',
                '<span class="', Ext.baseCSSPrefix, 'column-header-text-wrapper">',
                    '<span id="{id}-textEl" data-ref="textEl" class="', Ext.baseCSSPrefix, 'column-header-text',
                        '{childElCls}" style="float:left; width:18px !important;color:white !important;">',
                    '</span>',
                    '<span style="'+headerTextStyle+'">{text}</span>',
                '</span>',
            '</span>',
            '<tpl if="!menuDisabled">',
                '<div id="{id}-triggerEl" data-ref="triggerEl" role="presentation" class="', Ext.baseCSSPrefix, 'column-header-trigger',
                '{childElCls}" style="{triggerStyle}"></div>',
            '</tpl>',
        '</div>',
        '{%this.renderContainer(out,values)%}'
            ]

        }
        return checkBox;

    },
    renderer: function (value, metaData, record, rowIndex, colIndex, store, view) {
        var me = this;
        var baseCSSPrefix = Ext.baseCSSPrefix;
        metaData.tdCls = baseCSSPrefix + 'grid-cell-special ' + baseCSSPrefix + 'grid-cell-row-checker';
        if (record.data.IsEmpty == true) {
            return "";
        }
        return '<div class="' + baseCSSPrefix + 'grid-row-checker">&#160; </div><div style="margin: -15px 0 0 25px;">' + me.columnText + '</div>';
    }
});