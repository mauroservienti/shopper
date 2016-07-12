(function () {

    angular.module('app.sales')
        .constant('orders.config', {
            apiUrl: 'http://localhost:20185/api'
        });

    angular.module('app.sales')
        .config(['$stateProvider',
            function ($stateProvider) {

                $stateProvider
                    .state('orders', {
                        url: '/orders',
                        views: {
                            '': {
                                templateUrl: '/app/modules/sales/presentation/ordersView.html',
                                controller: 'ordersController',
                                controllerAs: 'orders'
                            }
                        }
                    });
            }]);
}())