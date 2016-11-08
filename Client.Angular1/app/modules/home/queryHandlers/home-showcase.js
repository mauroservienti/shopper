(function () {

    angular.module('app.home')
        .config(['backendCompositionServiceProvider',
        function (backendCompositionServiceProvider) {
                
                var homeShowcaseQueryId = 'home-showcase';
                backendCompositionServiceProvider.registerQueryHandlerFactory(homeShowcaseQueryId,
                    ['$log', '$http', 'messageBroker', 'publishing.config', function ($log, $http, messageBroker, config) {

                        var handler = {
                            get: function (args, composedResults) {

                                $log.debug('Ready to handle ', homeShowcaseQueryId, ' args: ', args);
                                var uri = config.apiUrl + '/publishing/homeShowcase';
                                return $http.get(uri)
                                    .then(function (response) {

                                        $log.debug('home-showcase HTTP response', response.data);

                                        var vm = new HomeShowcase(response.data);
                                        composedResults.showcase = vm;

                                        messageBroker.broadcast('home-showcase/retrieved', this, {
                                            rawData: response.data,
                                            viewModel: vm
                                        });

                                        $log.debug('Query ', homeShowcaseQueryId, 'handled: ', composedResults);

                                        return composedResults;
                                    });

                            }
                        }

                        return handler;
                    }]);

        }]);
}())