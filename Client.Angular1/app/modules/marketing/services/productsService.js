/* global angular */
(function () {

    'use strict';

    angular.module('app.marketing')
        .factory('productsService',
        ['$log', 'backendCompositionService',
            function ($log, backendCompositionService) {

                var _ = function (productId) {

                    return backendCompositionService.get('product-by-id', { id: productId });
                };

                return {
                    getById: _
                }
            }]);

}())