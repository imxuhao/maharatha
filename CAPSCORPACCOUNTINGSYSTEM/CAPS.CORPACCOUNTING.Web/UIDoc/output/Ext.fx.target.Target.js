Ext.data.JsonP.Ext_fx_target_Target({"tagname":"class","name":"Ext.fx.target.Target","autodetected":{"aliases":true,"alternateClassNames":true,"extends":true,"mixins":true,"requires":true,"uses":true,"members":true,"code_type":true},"files":[{"filename":"Target.js","href":"Target.html#Ext-fx-target-Target"}],"abstract":true,"aliases":{},"alternateClassNames":[],"extends":"Ext.Base","mixins":[],"requires":[],"uses":[],"members":[{"name":"isAnimTarget","tagname":"property","owner":"Ext.fx.target.Target","id":"property-isAnimTarget","meta":{"private":true}},{"name":"constructor","tagname":"method","owner":"Ext.fx.target.Target","id":"method-constructor","meta":{}},{"name":"getId","tagname":"method","owner":"Ext.fx.target.Target","id":"method-getId","meta":{"private":true}},{"name":"remove","tagname":"method","owner":"Ext.fx.target.Target","id":"method-remove","meta":{"private":true}}],"code_type":"ext_define","id":"class-Ext.fx.target.Target","short_doc":"This class specifies a generic target for an animation. ...","component":false,"superclasses":["Ext.Base"],"subclasses":["Ext.fx.target.Component","Ext.fx.target.Element","Ext.fx.target.Sprite"],"mixedInto":[],"parentMixins":[],"html":"<div><pre class=\"hierarchy\"><h4>Hierarchy</h4><div class='subclass first-child'>Ext.Base<div class='subclass '><strong>Ext.fx.target.Target</strong></div></div><h4>Subclasses</h4><div class='dependency'><a href='#!/api/Ext.fx.target.Component' rel='Ext.fx.target.Component' class='docClass'>Ext.fx.target.Component</a></div><div class='dependency'><a href='#!/api/Ext.fx.target.Element' rel='Ext.fx.target.Element' class='docClass'>Ext.fx.target.Element</a></div><div class='dependency'><a href='#!/api/Ext.fx.target.Sprite' rel='Ext.fx.target.Sprite' class='docClass'>Ext.fx.target.Sprite</a></div><h4>Files</h4><div class='dependency'><a href='source/Target.html#Ext-fx-target-Target' target='_blank'>Target.js</a></div></pre><div class='doc-contents'><p>This class specifies a generic target for an animation. It provides a wrapper around a\nseries of different types of objects to allow for a generic animation API.\nA target can be a single object or a Composite object containing other objects that are\nto be animated. This class and it's subclasses are generally not created directly, the\nunderlying animation will create the appropriate <a href=\"#!/api/Ext.fx.target.Target\" rel=\"Ext.fx.target.Target\" class=\"docClass\">Ext.fx.target.Target</a> object by passing\nthe instance to be animated.</p>\n\n<p>The following types of objects can be animated:</p>\n\n<ul>\n<li><a href=\"#!/api/Ext.fx.target.Component\" rel=\"Ext.fx.target.Component\" class=\"docClass\">Components</a></li>\n<li><a href=\"#!/api/Ext.fx.target.Element\" rel=\"Ext.fx.target.Element\" class=\"docClass\">Elements</a></li>\n<li><a href=\"#!/api/Ext.fx.target.Sprite\" rel=\"Ext.fx.target.Sprite\" class=\"docClass\">Sprites</a></li>\n</ul>\n\n</div><div class='members'><div class='members-section'><div class='definedBy'>Defined By</div><h3 class='members-title icon-property'>Properties</h3><div class='subsection'><div id='property-isAnimTarget' class='member first-child not-inherited'><a href='#' class='side expandable'><span>&nbsp;</span></a><div class='title'><div class='meta'><span class='defined-in' rel='Ext.fx.target.Target'>Ext.fx.target.Target</span><br/><a href='source/Target.html#Ext-fx-target-Target-property-isAnimTarget' target='_blank' class='view-source'>view source</a></div><a href='#!/api/Ext.fx.target.Target-property-isAnimTarget' class='name expandable'>isAnimTarget</a> : Boolean<span class=\"signature\"><span class='private' >private</span></span></div><div class='description'><div class='short'> ...</div><div class='long'>\n<p>Defaults to: <code>true</code></p></div></div></div></div></div><div class='members-section'><div class='definedBy'>Defined By</div><h3 class='members-title icon-method'>Methods</h3><div class='subsection'><div id='method-constructor' class='member first-child not-inherited'><a href='#' class='side expandable'><span>&nbsp;</span></a><div class='title'><div class='meta'><span class='defined-in' rel='Ext.fx.target.Target'>Ext.fx.target.Target</span><br/><a href='source/Target.html#Ext-fx-target-Target-method-constructor' target='_blank' class='view-source'>view source</a></div><strong class='new-keyword'>new</strong><a href='#!/api/Ext.fx.target.Target-method-constructor' class='name expandable'>Ext.fx.target.Target</a>( <span class='pre'>target</span> ) : <a href=\"#!/api/Ext.fx.target.Target\" rel=\"Ext.fx.target.Target\" class=\"docClass\">Ext.fx.target.Target</a><span class=\"signature\"></span></div><div class='description'><div class='short'>Creates new Target. ...</div><div class='long'><p>Creates new Target.</p>\n<h3 class=\"pa\">Parameters</h3><ul><li><span class='pre'>target</span> : Ext.Component/Ext.dom.Element/Ext.draw.sprite.Sprite<div class='sub-desc'><p>The object to be animated</p>\n</div></li></ul><h3 class='pa'>Returns</h3><ul><li><span class='pre'><a href=\"#!/api/Ext.fx.target.Target\" rel=\"Ext.fx.target.Target\" class=\"docClass\">Ext.fx.target.Target</a></span><div class='sub-desc'>\n</div></li></ul></div></div></div><div id='method-getId' class='member  not-inherited'><a href='#' class='side expandable'><span>&nbsp;</span></a><div class='title'><div class='meta'><span class='defined-in' rel='Ext.fx.target.Target'>Ext.fx.target.Target</span><br/><a href='source/Target.html#Ext-fx-target-Target-method-getId' target='_blank' class='view-source'>view source</a></div><a href='#!/api/Ext.fx.target.Target-method-getId' class='name expandable'>getId</a>( <span class='pre'></span> )<span class=\"signature\"><span class='private' >private</span></span></div><div class='description'><div class='short'> ...</div><div class='long'>\n</div></div></div><div id='method-remove' class='member  not-inherited'><a href='#' class='side expandable'><span>&nbsp;</span></a><div class='title'><div class='meta'><span class='defined-in' rel='Ext.fx.target.Target'>Ext.fx.target.Target</span><br/><a href='source/Target.html#Ext-fx-target-Target-method-remove' target='_blank' class='view-source'>view source</a></div><a href='#!/api/Ext.fx.target.Target-method-remove' class='name expandable'>remove</a>( <span class='pre'></span> )<span class=\"signature\"><span class='private' >private</span></span></div><div class='description'><div class='short'> ...</div><div class='long'>\n</div></div></div></div></div></div></div>","meta":{"abstract":true}});