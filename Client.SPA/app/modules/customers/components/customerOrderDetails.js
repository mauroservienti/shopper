/* global angular */
(function () {

    angular.module('app.components', [])
    .directive('customerOrderDetails', ['$log',
            function($log) {
                $log.debug('customerOrderDetails directive');
            
                return {
                    restrict: 'E',
                    scope: {
                        customer: '=',
                    },
                    templateUrl: '/app/modules/customers/components/customerOrderDetails.html'
                };
        }]);

}())