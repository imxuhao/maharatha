(function () {
    appModule.controller('host.views.tenants.featuresModal', [
        '$scope', '$modalInstance', 'abp.services.app.tenant', 'tenant',
        function ($scope, $modalInstance, tenantService, tenant) {
            var vm = this;
            
            vm.saving = false;
            vm.resettingFeatures = false;
            vm.tenant = tenant;
            vm.featureEditData = null;

            vm.resetFeatures = function () {
                vm.resettingFeatures = true;
                tenantService.resetTenantSpecificFeatures({
                    id: vm.tenant.id
                }).success(function () {
                    abp.notify.info(app.localize('ResetSuccessfully'));
                    loadFeatures();
                }).finally(function () {
                    vm.resettingFeatures = false;
                });
            };

            vm.save = function () {
                if (!vm.featureEditData.isValid()) {
                    abp.message.warn(app.localize('InvalidFeaturesWarning'));
                    return;
                }

                vm.saving = true;
                tenantService.updateTenantFeatures({
                    id: vm.tenant.id,
                    featureValues: vm.featureEditData.featureValues
                }).success(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $modalInstance.close();
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.cancel = function () {
                $modalInstance.dismiss();
            };

            function loadFeatures() {
                tenantService.getTenantFeaturesForEdit({
                    id: vm.tenant.id
                }).success(function (result) {
                    vm.featureEditData = result;
                });
            }

            loadFeatures();
        }
    ]);
})();