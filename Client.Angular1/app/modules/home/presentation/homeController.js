(function () {
    angular.module('app.home')
        .controller('homeController',
        ['$log', 'publishingService',
            function ($log, publishingService) {

                var vm = this;

                vm.isBusy = null;
                vm.showcase = null;

                vm.isBusy = publishingService
                    .homeShowcase()
                    .then(function (hs) {
                        $log.debug('showcase:', hs);
                        vm.showcase = hs;
                    });

            }]);
}())