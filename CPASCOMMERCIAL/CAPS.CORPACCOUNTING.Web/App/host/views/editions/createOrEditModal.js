(function () {
    appModule.controller('host.views.editions.createOrEditModal', [
        '$scope', '$modalInstance', 'abp.services.app.edition', 'editionId',
        function ($scope, $modalInstance, editionService, editionId) {
            var vm = this;

            vm.saving = false;
            vm.edition = null;
            vm.featureEditData = null;

            vm.save = function () {
                if (!vm.featureEditData.isValid()) {
                    abp.message.warn(app.localize('InvalidFeaturesWarning'));
                    return;
                }

                vm.saving = true;
                editionService.createOrUpdateEdition({
                    edition: vm.edition,
                    featureValues: vm.featureEditData.featureValues
                }).success(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $modalInstance.close();
                }).finally(function() {
                    vm.saving = false;
                });
            };

            vm.cancel = function () {
                $modalInstance.dismiss();
            };

            function init() {
                editionService.getEditionForEdit({
                    id: editionId
                }).success(function (result) {
                    vm.edition = result.edition;
                    vm.featureEditData = {
                        features: result.features,
                        featureValues: result.featureValues
                    };
                });
            }

            init();
        }
    ]);
})();