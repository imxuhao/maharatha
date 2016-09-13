Ext.define('Chaching.view.receivables.inquiry.InquiryGridController', {
    extend: 'Chaching.view.common.grid.ChachingGridPanelController',
    alias: 'controller.receivables-inquiry-accountsreceivableinquirygrid',
    doAfterCreateAction: function (createMode, formPanel, isEdit, record) {
        var form = undefined;
        var viewModel = undefined;
        if (formPanel) {
            //form = formPanel.getForm();
            //viewModel = formPanel.getViewModel();
        }
        if (form && viewModel) {

        }

    }
});