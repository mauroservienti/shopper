/* global angular */
(function () {

    angular.module('app.customerCare')
        .config(['backendCompositionServiceProvider',
        function (backendCompositionServiceProvider) {
                
                var homeShowcaseQueryId = 'home-showcase';
                backendCompositionServiceProvider.registerViewModelVisitorFactory(homeShowcaseQueryId,
                    ['$log', '$http', 'customerCare.config', function ($log, $http, config) {

                        $log.debug('Registering CustomerCare home-showcase-visitor');

                        var visitor = {
                            visit: function (args, composedResults, rawData) {

                                $log.debug('CustomerCare - Ready to visit ', homeShowcaseQueryId, ': ', args, composedResults, rawData);

                                var showcaseIds = [];
                                showcaseIds.push(composedResults.headlineProduct.stockItemId);
                                angular.forEach(composedResults.showcaseProducts, function(value, key){
                                    showcaseIds.push(value.stockItemId);
                                });

                                var uri = config.apiUrl + '/Raitings/ByStockItem?ids=' + showcaseIds;
                                $log.debug('CustomerCare home-showcase-visitor URI', uri);
                                return $http.get(uri)
                                     .then(function (response) {

                                         $log.debug('CustomerCare home-showcase-visitor HTTP response', response.data);
                                         var _headline = response.data[0];
                                         var _showcases = response.data.slice(1);

                                        composedResults.headlineProduct.itemRating = _headline;
                                        angular.forEach(_showcases, function(value, key){
                                            var vm = _.findWhere(composedResults.showcaseProducts, { 
                                                stockItemId: value.stockItemId
                                            });
                                            vm.itemRating = value;
                                        });

                                        $log.debug('CustomerCare - home-showcase-visitor - composed', composedResults);

                                        return response.data;
                                     });
                            }
                        }

                        return visitor;
                    }]);
        }]);
}())