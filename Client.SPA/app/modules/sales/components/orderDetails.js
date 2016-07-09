/* global angular */
(function(){
    
    angular.module('app.sales')
        .directive('orderDetails', ['$log',
            function($log) {
                $log.debug('orderDetails directive');
            
                return {
                    restrict: 'E',
                    scope: {
                        order: '=',
                    },
                    templateUrl: '/app/modules/sales/components/orderDetails.html'
                };
        }]);
}())