Ext.define('Chaching.view.imports.ImportsErrorViewController', {
    extend: 'Chaching.view.common.window.ChachingWindowPanelController',
    alias: 'controller.window-importsView',
    ErrorList:null,
    onFormShow: function (form, eOpts) {
        var me = this, panelStore, panelTpl, panelItems, 
            win= me.getView(),
            formView = win.down('form').getController().getView(),
            pnl = formView.down('panel[itemId=errorPanelItemId]');
        
            if (panelStore==undefined) {
                panelStore = Ext.create('Chaching.store.imports.ErrorStore');
            }
            panelStore.loadData(me.ErrorList);
            //{#}
            panelTpl = new Ext.XTemplate(
            '<div style="width:100%; height:450px; overflow:auto; display:block; padding-right:5px">',
            '<tpl for=".">',
                '<div style="width:45%; float:left; border: 1px solid #ccc; color: #333; line-heiht:1.5; height:75px; overflow:auto; display:block; padding:10px; margin:0px 10px 10px 0px; border-radius:4px;">',
                    '<div style="width:100%">',
                        '<span style="font-weight:bold; width:30%"> ' + app.localize("Rownumber") + ': </span> <span style="width:70%"> {rowNumber} </span>',
                    '</div>',
                    '<div style="width:100%">',
                        '<span style="font-weight:bold; width:30%"> ' + app.localize("Description") + ': </span> <span style="width:70%"> {errorMessage} </span>',
                    '</div>',
                '</div>',
            '</tpl>',
            '</div>'
        );


        //var pnlTpl = new Ext.XTemplate(
        //    "<table style='width:100%; border: 1px solid #ccc; line-height: 1.4; color: #333; height:200px; overflow:auto; display:block'>",
        //    '<tr>',
        //        '<td style="width:30%; padding: 10px; line-height: 1.4; background-color: #f5f5f5; border: 1px solid #ccc; font-weight:bold"> Row Number </td>',
        //        '<td style="width:70%; padding: 10px; line-height: 1.4; background-color: #f5f5f5; border: 1px solid #ccc; font-weight:bold"> Description </td>',
        //    '</tr>',
        //    '<tpl for=".">',
        //        "<tr>",
        //            '<td style="padding: 10px; border: 1px solid #ccc; line-height: 1.4; word-break: break-all; word-wrap: break-word;">{rowNumber}</td>',
        //            '<td style="padding: 10px; border: 1px solid #ccc; line-height: 1.4; word-break: break-all; word-wrap: break-word;">{errorMessage}</td>',
        //        '</tr>',
        //    '</tpl>',
        //    '</table>'
        //);

        //var pnlTpl = new Ext.XTemplate(
        //    '<tpl for=".">',
        //        '<div style=" width: 50%; display: block; padding: 9.5px; line-height: 1.4;word-break: break-all; word-wrap: break-word; color: #333; background-color: #f5f5f5; border: 1px solid #ccc; border-radius: 4px;"> ROWNUMBER </div> \
        //        <div style=" width: 50%; display: block; padding: 9.5px; line-height: 1.4;word-break: break-all; word-wrap: break-word; color: #333; background-color: #f5f5f5; border: 1px solid #ccc; border-radius: 4px;"> DESCRIPTION </div> \
        //        <div class="error-template"> \
        //            <div style=" width: 50%; "> {rowNumber} </div> \
        //            <div style=" width: 50%; ">{errorMessage}</div> <br/> \
        //        </div>',
        //    '</tpl>'
        //);

            panelItems = Ext.create('Ext.view.View', {
                store: panelStore,
                tpl: panelTpl,
                itemSelector: 'div.error-template'
            });

            pnl.add(panelItems);
        
    }
});