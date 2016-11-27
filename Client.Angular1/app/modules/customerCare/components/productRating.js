/* global angular */
(function () {

    angular.module('app.customerCare')
        .directive('productRating', ['$log',
            function($log) {
                $log.debug('productRating directive');
            
                return {
                    restrict: 'E',
                    scope: {
                        model: '=',
                    },
                    templateUrl: '/app/modules/customerCare/components/productRating.html'
                };
        }]);

}())