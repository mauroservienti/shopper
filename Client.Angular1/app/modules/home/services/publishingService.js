/* global angular */
(function () {

    'use strict';

    angular.module('app.home')
        .factory('publishingService',
        ['$log', 'backendCompositionService',
            function ($log, backendCompositionService) {

                var homeShowcase = function () {

                    var promise = backendCompositionService
                        .get('home-showcase')
                        .then(function (composedResult) {
                            $log.debug('home-showcase -> composedResult:', composedResult);
                            return composedResult.showcase;
                        });

                    return promise;
                };

                return {
                    homeShowcase: homeShowcase
                }
            }]);

}())