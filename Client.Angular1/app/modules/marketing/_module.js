(function () {

    angular.module('app.marketing')
        .constant('publishing.config', {
            apiUrl: 'http://localhost:20188/api'
        })
        .constant('marketing.config', {
            apiUrl: 'http://localhost:20188/api'
        });

    angular.module('app.marketing')
        .config(['$stateProvider',
            function ($stateProvider) {

                var homeViews = {
                    '': {
                        templateUrl: '/app/modules/marketing/presentation/homeView.html',
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
                    })
                    .state('product', {
                        url: '/products/:id',
                        views: {
                            '': {
                                templateUrl: '/app/modules/marketing/presentation/productView.html',
                                controller: 'productController',
                                controllerAs: 'viewModel'
                            }
                        }
                    });

            }]);
}())