/* global angular */
(function () {

    angular.module('app.sales')
        .config(['backendCompositionServiceProvider',
            function (backendCompositionServiceProvider) {

                var queryId = 'product-by-id';
                backendCompositionServiceProvider.registerViewModelVisitorFactory(queryId,
                    ['$log', '$http', 'sales.config', function ($log, $http, config) {

                        $log.debug('Registering Sales product-by-id-visitor');

                        var visitor = {
                            visit: function (args, composedResults, rawData) {

                                $log.debug('Sales - Ready to visit ', queryId, ': ', args, composedResults, rawData);

                                var uri = config.apiUrl + '/ItemPrices/ByStockItem?ids=' + composedResults.id;
                                $log.debug('Sales product-by-id-visitor URI', uri);
                                return $http.get(uri)
                                    .then(function (response) {

                                        $log.debug('Sales product-by-id-visitor HTTP response', response.data);

                                        composedResults.itemPrice = response.data[0];
                                        
                                        $log.debug('Sales - product-by-id-visitor - composed', composedResults);

                                        return response.data;
                                    });
                            }
                        }

                        return visitor;
                    }]);
            }]);
} ())