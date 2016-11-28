/* global angular */
(function () {

    angular.module('app.customerCare')
        .config(['backendCompositionServiceProvider',
            function (backendCompositionServiceProvider) {

                var queryId = 'product-by-id';
                backendCompositionServiceProvider.registerViewModelVisitorFactory(queryId,
                    ['$log', '$http', 'customerCare.config', '$q', function ($log, $http, config, $q) {

                        $log.debug('Registering CustomerCare product-by-id-visitor');

                        var visitor = {
                            visit: function (args, composedResults, rawData) {

                                $log.debug('CustomerCare - Ready to visit ', queryId, ': ', args, composedResults, rawData);

                                var ratingsUri = config.apiUrl + '/Raitings/ByStockItem?ids=' + composedResults.id;
                                var reviewsUri = config.apiUrl + '/Reviews/ByStockItem?ids=' + composedResults.id;
                                $log.debug('CustomerCare product-by-id-visitor URI', ratingsUri, reviewsUri);

                                var ratingsPromise = $http.get(ratingsUri)
                                    .then(function (response) {

                                        $log.debug('CustomerCare product-by-id-visitor HTTP response', response.data);
                                        composedResults.itemRating = response.data[0];

                                        $log.debug('CustomerCare - product-by-id-visitor - composed', composedResults);

                                        return response.data;
                                    });

                                var reviewsPromise = $http.get(reviewsUri)
                                    .then(function (response) {

                                        $log.debug('CustomerCare product-by-id-visitor HTTP response', response.data);
                                        composedResults.itemReviews = response.data;

                                        $log.debug('CustomerCare - product-by-id-visitor - composed', composedResults);

                                        return response.data;
                                    });

                                return $q.all(ratingsPromise, reviewsPromise);
                            }
                        }

                        return visitor;
                    }]);
            }]);
} ())