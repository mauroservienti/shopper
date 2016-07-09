/* global angular */
(function(){
    
    angular.module('app.components')
        .directive('priceOrderDetails', ['$log',
            function($log) {
                $log.debug('priceOrderDetails directive');
            
                return {
                    restrict: 'E',
                    scope: {
                        price: '=',
                    },
                    templateUrl: '/app/modules/finance/components/priceOrderDetails.html'
                };
        }]);
}())