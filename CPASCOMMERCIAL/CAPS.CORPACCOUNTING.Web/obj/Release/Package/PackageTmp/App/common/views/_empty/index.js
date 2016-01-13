(function () {
    appModule.controller('common.views.EMPTY.index', [
        '$scope', '$modal',
        function ($scope, $modal) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            //start from here...
        }
    ]);
})();