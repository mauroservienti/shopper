(function () {
    angular.module('app.marketing')
        .controller('homeController',
        ['$log', 'publishingService',
            function ($log, publishingService) {

                var vm = this;

                vm.isBusy = null;
                vm.model = null;

                vm.isBusy = publishingService
                    .homeShowcase()
                    .then(function (model) {
                        $log.debug('homeShowcase model:', model);
                        vm.model = model;
                    });

            }]);
}())