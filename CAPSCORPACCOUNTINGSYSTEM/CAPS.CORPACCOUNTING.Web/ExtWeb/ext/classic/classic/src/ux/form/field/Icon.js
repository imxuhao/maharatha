// vim: sw=4:ts=4:nu:nospell:fdc=4
/*global Ext:true */
/*jslint browser: true, devel:true, sloppy: true, white: true, plusplus: true */

/*
 This file is part of saki-field-icon Package

 Copyright (c) 2014, Jozef Sakalos, Saki

 Package:  saki-field-icon
 Author:   Jozef Sakalos, Saki
 Contact:  http://extjs.eu/contact
 Date:     23. April 2014

 Commercial License
 Developer, or the specified number of developers, may use this file in any number
 of projects during the license period in accordance with the license purchased.

 Uses other than including the file in a project are prohibited.
 See http://extjs.eu/licensing for details.
 */

/**
 * # Icon for form field
 *
 * This plugin adds highly configurable icon to the form field it is plugged into.
 * Icon can be made (right) clickable to trigger an action or to display a context
 * menu or a {@link Ext.tip.QuickTip QuickTip} or {@link Ext.tip.ToolTip ToolTip}
 * can be configured to show on icon hover.
 *
 * The plugin works best with icon fonts especially with
 * <a href="http://fortawesome.github.io/Font-Awesome/">`FontAwesome`</a> for which it
 * is preconfigured.
 *
 * ### How to use Field Icon with `FontAwesome`
 *
 *  1. Include `FontAwesome` stylesheet in your `index.html`. For example:
 *
 *          <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css">
 *
 *  2. Select an icon to show at
 *      <a href="http://fortawesome.github.io/Font-Awesome/icons/">FontAwesome</a> site.
 *      The default icon is `fa-info-circle`, if it is the icon you want you can skip this step.
 *
 *  3. Configure your form field similar to this:
 *
 *           xtype:'numberfield'
 *          ,name:'age'
 *          ,fieldLabel:'Your Age'
 *          ,plugins:[{
 *               ptype:'saki-ficn'
 *              ,iconCls:'fa-smile-o'
 *              ,qtip:'Integer posuere erat a ante venenatis dapibus posuere velit aliquet. '
 *                   +'Cras mattis consectetur purus sit amet fermentum. Cum sociis natoque '
 *                   +'penatibus et magnis dis parturient montes, nascetur ridiculus mus.'
 *          }]
 *
 * ### See this documentation and included documentation of {@link Ext.form.field.Field Ext.form.field.Field} for other configuration options.
 */
Ext.define('Ext.saki.form.field.Icon', {
     extend:'Ext.AbstractPlugin'
    ,alternateClassName:'Ext.ux.form.field.Icon'
    ,alias:['plugin.saki-ficn', 'plugin.ux-ficn']

    /**
     * @cfg {string} iconBaseCls This CSS class is
     * prepended to {@link #iconCls iconCls}
     */
    ,iconBaseCls:'fa'

    /**
     * @cfg {string} iconCls class of the icon
     */
    ,iconCls:'fa-info-circle'

    /**
     * @cfg {string} iconPath Path to the image of icon. Used only
     * if {@link #iconMode iconMode} is '**img**'
     *
     * Defaults: undefined
     */

    /**
     * @cfg {string} iconMode Mode of the icon. Valid values are
     * '**font**', '**img**' and '**css**'
     */
    ,iconMode:'font'

    /**
     * @cfg {string} iconColor CSS color specification for the icon.
     * It is used only for {@link #iconMode iconMode}  '**font**'
     */
    ,iconColor:'#5278FF'

    /**
     * @cfg {number} iconWidth Width of the icon in pixels
     */
    ,iconWidth:16

    /**
     * @cfg {number} iconHeight Height of the icon in pixels
     */
    ,iconHeight:16

    /**
     * @cfg {number} cellWidthAdjust Width in pixels to add to
     * iconWidth to calculate wrapping cell width.
     */
    ,cellWidthAdjust:6

    /**
     * @cfg {string} iconCursor Cursor style specification that is
     * added to the icon style. You can try '`help`' if you use info
     * icons.
     */
    ,iconCursor:'pointer'

    /**
     * @cfg {string} iconMargin CSS specification of margin to add
     * around the icon in order: top, right, bottom, left
     */
    ,iconMargin:'0 3px 0 3px'

    /**
     * @cfg {string} position Where you want to insert your icon. Valid
     * positions are: '**beforeLabel**', '**afterLabel**', '**beforeInput**',
     * '**afterInput**'.
     *
     * Note: Some field type/position combinations do not look good. For
     * example, checkbox with '**afterInput**' is not very usable.
     */
    ,position:'afterInput'

    /**
     * @cfg {string[]} clickEvents These click events on the icon are
     * listened to and fired as if the were originated from the field.
     * Names are prepended with `icon`, so the form field fires
     * `iconclick` and `iconcontextmenu`
     */
    ,clickEvents:['click', 'contextmenu']

    /**
     * @cfg {string/undefined} qtipTitle Title to show on quick tip.
     * Used only in {@link #tip tip} is not defined.
     *
     * Defaults to: `undefined`
     */

    /**
     * @cfg {string/undefined} qtip Text to show on quick tip.
     * Used only in {@link #tip tip} is not defined.
     *
     * Defaults to: `undefined`
     */

    /**
     * @cfg {Object/Ext.tip.ToolTip/undefined} tip Configuration object or instance
     * of {@link Ext.tip.ToolTip Ext.tip.ToolTip} to display on icon hover.
     *
     * Defaults to: undefined
     */

    /**
     * @cfg {string} iconPath Path to icon image. Only used when
     * {@link #iconMode iconMode} is '**img**'
     *
     * Defaults to: undefined
     */

    /**
     * @readonly
     * @member Ext.form.field.Field
     * @property {Ext.dom.Element} iconEl Icon element
     */

    /**
     * @event iconclick
     * @member Ext.form.field.Field
     * @param {Ext.form.field.Field} this
     * @param {Ext.EventObject} e
     */

    /**
     * @event iconcontextmenu
     * @member Ext.form.field.Field
     * @param {Ext.form.field.Field} this
     * @param {Ext.EventObject} e
     */

    /**
     * The plugin initialization. Called by the framework
     * @private
     * @param {Ext.form.Field} cmp Form field this plugin is in
     */
    ,init:function(cmp) {
        var  me = this;

        me.setCmp(cmp);

        // Insert the icon after the field (cmp) is rendered
        cmp.on({
            afterrender:{
                 scope:me
                ,single:true
                ,fn:me.afterCmpRender
            }
        });

        // add some useful methods to the field
        Ext.apply(cmp, {
            /**
             * Returns instance of this plugin
             * @member Ext.form.field.Field
             * @returns {Ext.saki.form.field.Icon}
             */
            getIcon:function() {
               return me;
            }

            /**
             * Sets the new icon by setting the new {@link Ext.saki.form.field.Icon#iconCls iconCls}
             * @member Ext.form.field.Field
             * @param {string} cls The new icon css class
             */
            ,setIconCls:function(cls) {
                this.iconEl.replaceCls(me.iconCls, cls);
                me.iconCls = cls;
            }

            /**
             * Sets the style of the icon
             * @member Ext.form.field.Field
             * @param {string} style Style for the icon
             */
            ,setIconStyle:function(style) {
                this.iconEl.applyStyles(style);
            }

            /**
             * Sets the icon color. Only used if
             * {@link Ext.saki.form.field.Icon#iconMode iconMode} is '**font**'
             * @member Ext.form.field.Field
             * @param {string} color Color of the icon
             */
            ,setIconColor:function(color) {
                this.setIconStyle({color:color});
            }

            /**
             * Sets the quick tip text. Only used for icons with quick tips.
             * It is ignored if a rich tooltip is used.
             * @member Ext.form.field.Field
             * @param {string} text Text for the quicktip
             */
            ,setIconTip:function(text) {
                this.iconEl.set({'data-qtip':text})
            }

            /**
             * Sets the quick tip title. Only used for icons with quick tips.
             * It is ignored if a rich tooltip is used.
             * @member Ext.form.field.Field
             * @param {string} text Text for the quicktip title
             */
            ,setIconTipTitle:function(text) {
                this.iconEl.set({'data-qtitle':text})
            }

        });

    } // eo function init

    /**
     * Runs once after the field is rendered. Creates the icon
     * and initializes the events.
     * @private
     */
    ,afterCmpRender:function() {
        var  me = this
            ,cmp = me.getCmp()
            ,cfg = me.getIconConfig()
            ,isCheckbox = false
            ,isTextArea = false
            ,iconEl
            ,wrap = {
                 tag:'div'
                ,style:{
                     display:'table-cell'
                    ,width:(me.iconWidth + me.cellWidthAdjust) + 'px'
                }
                ,cn:[cfg]
            }
        ;

        // special handling for checkbox and textarea
        try {
            isCheckbox = cmp instanceof Ext.form.field.Checkbox;
            isTextArea = cmp instanceof Ext.form.field.TextArea;
        } catch(e){};

        // put icon near top of textarea
        if(isTextArea) {
            Ext.apply(wrap.style, {
                 'vertical-align':'top'
                ,'padding-top':'3px'
            });
        }

        // icon position switch
        switch(me.position) {
            case 'afterInput':
                if(isCheckbox) {
                    iconEl = cmp.inputEl.insertSibling(cfg, 'after');
                } else {
                    iconEl = cmp.bodyEl.insertSibling(wrap, 'after');
                    iconEl = iconEl.down('i');
                }
            break;

            case 'beforeInput':
                iconEl = cmp.labelEl.next().insertSibling(wrap, 'before');
                iconEl = iconEl.down('i');
            break;

            case 'afterLabel':
                if(isCheckbox && cmp.boxLabelEl) {
                    cfg.style['vertical-align'] = 'middle';
                    iconEl = cmp.boxLabelEl.insertSibling(cfg, 'after');
                } else {
                    iconEl = cmp.labelEl.insertSibling(wrap, 'after');
                }
            break;

            case 'beforeLabel':
                if(isCheckbox && cmp.boxLabelEl) {
                    cfg.style['margin-left'] = (me.iconWidth + me.cellWidthAdjust/2) + 'px';
                    cmp.boxLabelEl.setStyle({'padding-left':0});
                    iconEl = cmp.boxLabelEl.insertSibling(cfg, 'before');
                } else {
                    iconEl = cmp.labelEl.insertSibling(wrap, 'before');
                    iconEl = iconEl.down('i');
                }
            break;
        }

        cmp.iconEl = iconEl;

        // set the target of tooltip if we have one
        if(me.tip) {
            if(!(me.tip instanceof Ext.Base)) {
                me.tip = Ext.widget('tooltip', me.tip);
            }
            me.tip.setTarget(iconEl);
        }

        me.initEvents();

    } // eo afterCmpRender

    /**
     * Installs listeners on the icon that then fire events as if they
     * would be originated by the field
     * @private
     */
    ,initEvents:function() {
        var  me = this
            ,cmp = me.getCmp()
            ,iconEl = cmp.iconEl
            ,listeners
        ;
        Ext.Array.each(me.clickEvents, function(ev) {
            iconEl.on(ev, function(e){
                e.stopEvent();
                cmp.fireEvent('icon' + ev, cmp, e);
                return false;
            });
        });

    } // eo function initEvents

    /**
     * Override this method if you need a special icon
     * @returns {Object} Icon configuration for Ext.DomHelper
     * @template
     */
    ,getIconConfig:function() {
        var  me = this
            ,cmp = me.getCmp()

            // this cfg will do for iconMode font and css
            ,cfg = {
                 tag:'i'
                ,cls:Ext.String.format('{0} {1}', me.iconBaseCls, me.iconCls)
                ,style:{
                     'width':me.iconWidth + 'px'
                    ,'height':me.iconHeight + 'px'
                    ,'font-size':me.iconHeight + 'px'
                    ,'color':me.iconColor
                    ,'cursor':me.iconCursor
                    ,'margin':me.iconMargin
//                    ,'vertical-align':'middle'
                }

            }
        ;

        // handling of iconMode img
        if('img' === me.iconMode && me.iconPath) {
            cfg.cls = me.iconBaseCls;
            cfg.cn = [{
                 tag:'img'
                ,src:me.iconPath
                ,style:{
                    'vertical-align':'middle'
                }
            }];

        }

        // use qtip config options only if we don't have tip
        if(!me.tip) {
            if(me.qtipTitle) {
                cfg['data-qtitle'] = me.qtipTitle;
            }
            if(me.qtip) {
                cfg['data-qtip'] = me.qtip
            }
        }
        return cfg;

    } // eo function getIconConfig

    /**
     * Destroys the plugin
     * @private
     */
    ,destroy:function() {
        var iconEl = this.getCmp().iconEl;
        iconEl.removeAllListeners();
        Ext.destroy(iconEl);
        iconEl = null;
        console.log('destroyed');
    } // eo function destroy

});

// eof