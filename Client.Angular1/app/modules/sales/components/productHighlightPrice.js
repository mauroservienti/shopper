/* global angular */
(function () {

    angular.module('app.sales')
        .directive('productHighlightPrice', ['$log',
            function($log) {
                $log.debug('productHighlightPrice directive');
            
                return {
                    restrict: 'E',
                    scope: {
                        product: '=',
                    },
                    templateUrl: '/app/modules/sales/components/productHighlightPrice.html'
                };
        }]);

}())