/* global angular */
(function () {

    angular.module('app.marketing')
        .config(['backendCompositionServiceProvider',
        function (backendCompositionServiceProvider) {
                
                var homeShowcaseQueryId = 'home-showcase';
                backendCompositionServiceProvider.registerViewModelVisitorFactory(homeShowcaseQueryId,
                    ['$log', '$http', 'marketing.config', function ($log, $http, config) {

                        $log.debug('Registering Marketing home-showcase-visitor');

                        var visitor = {
                            visit: function (args, composedResults, rawData) {

                                $log.debug('Ready to visit ', homeShowcaseQueryId, ': ', args, composedResults, rawData);

                                var showcaseIds = [];
                                showcaseIds.push(composedResults.headlineProduct.stockItemId);
                                angular.forEach(composedResults.showcaseProducts, function(value, key){
                                    showcaseIds.push(value.stockItemId);
                                });

                                var uri = config.apiUrl + '/ProductDescriptions/ByStockItem?ids=' + showcaseIds;
                                $log.debug('Marketing home-showcase-visitor URI', uri);
                                return $http.get(uri)
                                     .then(function (response) {

                                         $log.debug('Marketing home-showcase-visitor HTTP response', response.data);
                                         var _headline = response.data[0];
                                         var _showcases = response.data.slice(1);

                                        composedResults.headlineProduct.itemDescription = _headline;
                                        angular.forEach(_showcases, function(value, key){
                                            var vm = _.findWhere(composedResults.showcaseProducts, { 
                                                stockItemId: value.stockItemId
                                            });
                                            vm.itemDescription = value;
                                        });

                                        $log.debug('Marketing - home-showcase-visitor - composed', composedResults);

                                        return response.data;
                                     });
                            }
                        }

                        return visitor;
                    }]);
        }]);
}())