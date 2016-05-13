
Ext.define('Chaching.view.financials.journals.JournalEntryGrid',{
    extend: 'Ext.panel.Panel',
    xtype: 'widget.financials.journals.entry',
    requires: [
        'Chaching.view.financials.journals.JournalEntryGridController',
        'Chaching.view.financials.journals.JournalEntryGridModel'
    ],
    modulePermissions: {
        read: abp.auth.isGranted('Pages.Financials.Journals.Entry'),
        create: abp.auth.isGranted('Pages.Financials.Journals.Entry.Create'),
        edit: abp.auth.isGranted('Pages.Financials.Journals.Entry.Edit'),
        destroy: abp.auth.isGranted('Pages.Financials.Journals.Entry.Delete')
    },
    controller: 'financials-journals-journalentrygrid',
    viewModel: {
        type: 'financials-journals-journalentrygrid'
    },
    gridId:18,
    html: 'Hello, World!!'
});
