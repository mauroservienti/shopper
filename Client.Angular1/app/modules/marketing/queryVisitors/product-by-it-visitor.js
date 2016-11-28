/* global angular */
(function () {

    angular.module('app.marketing')
        .config(['backendCompositionServiceProvider',
        function (backendCompositionServiceProvider) {
                
                var queryId = 'product-by-id';
                backendCompositionServiceProvider.registerViewModelVisitorFactory(queryId,
                    ['$log', '$http', 'marketing.config', function ($log, $http, config) {

                        $log.debug('Registering Marketing product-by-it-visitor');

                        var visitor = {
                            visit: function (args, composedResults, rawData) {

                                $log.debug('Ready to visit ', queryId, ': ', args, composedResults, rawData);

                                var uri = config.apiUrl + '/ProductDescriptions/ByStockItem?ids=' + composedResults.id;
                                $log.debug('Marketing product-by-id-visitor URI', uri);
                                return $http.get(uri)
                                     .then(function (response) {

                                         $log.debug('Marketing product-by-id-visitor HTTP response', response.data);
                                         
                                        composedResults.itemDescription = response.data[0];
                                        $log.debug('Marketing - product-by-id-visitor - composed', composedResults);

                                        return response.data;
                                     });
                            }   
                        }

                        return visitor;
                    }]);
        }]);
}())