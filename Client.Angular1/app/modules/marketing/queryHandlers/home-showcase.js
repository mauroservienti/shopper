(function () {

    angular.module('app.marketing')
        .config(['backendCompositionServiceProvider',
        function (backendCompositionServiceProvider) {
                
                var homeShowcaseQueryId = 'home-showcase';
                backendCompositionServiceProvider.registerQueryHandlerFactory(homeShowcaseQueryId,
                    ['$log', '$http', 'publishing.config', function ($log, $http, config) {

                        var factory = {
                            query: function (args, composedResults) {

                                $log.debug('Ready to handle ', homeShowcaseQueryId, ' args: ', args);
                                var uri = config.apiUrl + '/publishing/homeShowcase';
                                return $http.get(uri)
                                    .then(function (response) {

                                        $log.debug('home-showcase HTTP response', response.data);

                                        // var vm = new HomeShowcase(response.data);
                                        // composedResults.showcase = vm;
                                        composedResults.headlineProduct ={
                                            stockItemId: response.data.headlineStockItemId
                                        };
                                        composedResults.showcaseProducts = [];
                                        angular.forEach(response.data.showcaseStockItemIds, function(value, key){
                                            composedResults.showcaseProducts.push({stockItemId: value});
                                        });

                                        $log.debug('Query ', homeShowcaseQueryId, 'handled: ', composedResults);

                                        return response.data;
                                    });

                            }
                        }

                        return factory;
                    }]);

        }]);
}())