Ext.define('Chaching.view.imports.entities.ProjectImportsGridController', {
    extend: 'Chaching.view.projects.projectmaintenance.ProjectsGridController',
    alias: 'controller.imports-entities-projectimportsgrid',
    doBeforeDataImportSaveOperation: function (data) {
        for (var i = 0; i < data.length; i++) {
            data[i].rollupAccountId = data[i].accountId;
        }
        return data;
    }
    
});
