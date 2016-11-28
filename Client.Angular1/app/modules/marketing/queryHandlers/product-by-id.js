(function () {

    angular.module('app.marketing')
        .config(['backendCompositionServiceProvider',
        function (backendCompositionServiceProvider) {
                
                var queryId = 'product-by-id';
                backendCompositionServiceProvider.registerQueryHandlerFactory(queryId,
                    ['$log', '$q', '$timeout', function ($log, $q, $timeout) {

                        var factory = {
                            query: function (args, composedResults) {

                                $log.debug('Ready to handle ', queryId, ' args: ', args);

                                return  $timeout(function() {
                                    composedResults.id = args.id;
                                    return {};
                                });
                            }
                        }

                        return factory;
                    }]);

        }]);
}())