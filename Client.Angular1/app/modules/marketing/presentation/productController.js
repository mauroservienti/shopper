(function () {
    angular.module('app.marketing')
        .controller('productController',
        ['$log', 'productsService', '$stateParams',
            function ($log, productsService, $stateParams) {

                $log.debug('productController - $stateParams: ', $stateParams);
                var vm = this;

                vm.isBusy = null;
                vm.product = null;

                vm.isBusy = productsService
                    .getById($stateParams.id)
                    .then(function (model) {
                        $log.debug('product getById model:', model);
                        vm.product = model;
                    });

            }]);
}())