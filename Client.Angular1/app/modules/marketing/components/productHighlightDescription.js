/* global angular */
(function () {

    angular.module('app.marketing')
        .directive('productHighlightDescription', ['$log',
            function($log) {
                $log.debug('productHighlightDescription directive');
            
                return {
                    restrict: 'E',
                    scope: {
                        model: '=',
                    },
                    templateUrl: '/app/modules/marketing/components/productHighlightDescription.html'
                };
        }]);

}())