/* global angular */
(function () {

    'use strict';

    angular.module('app.marketing')
        .factory('publishingService',
        ['$log', 'backendCompositionService',
            function ($log, backendCompositionService) {

                var _ = function () {

                    return backendCompositionService.get('home-showcase');
                };

                return {
                    homeShowcase: _
                }
            }]);

}())