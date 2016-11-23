(function () {

    angular.module('app.home')
        .config(['backendCompositionServiceProvider',
        function (backendCompositionServiceProvider) {
                
                var homeShowcaseQueryId = 'home-showcase';
                backendCompositionServiceProvider.registerQueryHandlerFactory(homeShowcaseQueryId,
                    ['$log', '$http', 'publishing.config', function ($log, $http, config) {

                        var factory = {
                            get: function (args, composedResults) {

                                $log.debug('Ready to handle ', homeShowcaseQueryId, ' args: ', args);
                                var uri = config.apiUrl + '/publishing/homeShowcase';
                                return $http.get(uri)
                                    .then(function (response) {

                                        $log.debug('home-showcase HTTP response', response.data);

                                        var vm = new HomeShowcase(response.data);
                                        composedResults.showcase = vm;

                                        $log.debug('Query ', homeShowcaseQueryId, 'handled: ', composedResults);

                                        return response.data;
                                    });

                            }
                        }

                        return factory;
                    }]);

        }]);
}())