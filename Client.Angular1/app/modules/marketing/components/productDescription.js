/* global angular */
(function () {

    angular.module('app.marketing')
        .directive('productDescription', ['$log',
            function($log) {
                $log.debug('productDescription directive');
            
                return {
                    restrict: 'E',
                    scope: {
                        model: '=',
                    },
                    templateUrl: '/app/modules/marketing/components/productDescription.html'
                };
        }]);

}())