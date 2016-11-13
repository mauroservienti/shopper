(function () {

    angular.module('app.home')
        .constant('publishing.config', {
            apiUrl: 'http://localhost:20188/api'
        });

    angular.module('app.home')
        .config(['$stateProvider',
            function ($stateProvider) {

                var homeViews = {
                    '': {
                        templateUrl: '/app/modules/home/presentation/homeView.html',
                        controller: 'homeController',
                        controllerAs: 'home'
                    }
                };

                $stateProvider
                    .state('root', {
                        url: '',
                        views: homeViews
                    })
                    .state('home', {
                        url: '/',
                        views: homeViews
                    });

            }]);
}())