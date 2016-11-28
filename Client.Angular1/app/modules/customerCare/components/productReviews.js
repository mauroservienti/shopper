/* global angular */
(function () {

    angular.module('app.customerCare')
        .directive('productReviews', ['$log',
            function($log) {
                $log.debug('productReviews directive');
            
                return {
                    restrict: 'E',
                    scope: {
                        model: '=',
                    },
                    templateUrl: '/app/modules/customerCare/components/productReviews.html'
                };
        }]);

}())