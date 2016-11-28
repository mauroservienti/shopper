/* global angular */
(function () {

    angular.module('app.sales')
        .directive('productPrice', ['$log',
            function($log) {
                $log.debug('productPrice directive');
            
                return {
                    restrict: 'E',
                    scope: {
                        model: '=',
                    },
                    templateUrl: '/app/modules/sales/components/productPrice.html'
                };
        }]);

}())