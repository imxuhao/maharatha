(function () {
    appModule.controller('host.views.tenants.editModal', [
        '$scope', '$modalInstance', 'abp.services.app.tenant', 'abp.services.app.commonLookup', 'tenantId',
        function ($scope, $modalInstance, tenantService, commonLookupService, tenantId) {
            var vm = this;

            vm.saving = false;
            vm.tenant = null;
            vm.editions = [];

            vm.save = function () {
                if (vm.tenant.editionId == 0) {
                    vm.tenant.editionId = null;
                }

                vm.saving = true;
                tenantService.updateTenant(vm.tenant)
                    .success(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        $modalInstance.close();
                    }).finally(function () {
                        vm.saving = false;
                    });
            };

            vm.cancel = function () {
                $modalInstance.dismiss();
            };

            vm.getEditionValue = function (item) {
                return parseInt(item.value);
            };

            function init() {
                commonLookupService.getEditionsForCombobox({}).success(function (result) {
                    vm.editions = result.items;
                    vm.editions.unshift({ value: "0", displayText: app.localize('NotAssigned') });
                });

                tenantService.getTenantForEdit({
                    id: tenantId
                }).success(function (result) {
                    vm.tenant = result;
                    vm.tenant.editionId = vm.tenant.editionId || 0;
                });
            }

            init();
        }
    ]);
})();