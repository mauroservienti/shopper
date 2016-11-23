(function () {
    angular.module('app.services')
        .provider('backendCompositionService', function backendCompositionServiceProvider() {

            var queryHandlerFactories = {};
            var queryHandlers = {};

            var viewModelVisitorFactories = {};
            var viewModelVisitors = {};

            this.registerQueryHandlerFactory = function (queryId, factory) {

                if (!queryHandlerFactories.hasOwnProperty(queryId)) {
                    queryHandlerFactories[queryId] = [];
                }

                queryHandlerFactories[queryId].push(factory);
            };

            this.registerViewModelVisitorFactory = function (queryId, factory) {

                if (!viewModelVisitorFactories.hasOwnProperty(queryId)) {
                    viewModelVisitorFactories[queryId] = [];
                }

                viewModelVisitorFactories[queryId].push(factory);
            };

            this.$get = ['$log', '$injector', '$q', 'messageBroker',
                function backendCompositionServiceFactory($log, $injector, $q, messageBroker) {

                    $log.debug('backendCompositionServiceFactory');

                    var svc = {};

                    svc.get = function (queryId, args) {

                        var handlers = queryHandlers[queryId];
                        if (!handlers) {
                            var factories = queryHandlerFactories[queryId];
                            if (!factories) {
                                throw 'Cannot find any valid queryHandler or factory for "' + queryId + '"';
                            }

                            handlers = [];
                            angular.forEach(factories, function (factory, index) {
                                var handler = $injector.invoke(factory);
                                handlers.push(handler);
                            });

                            queryHandlers[queryId] = handlers;
                        }

                        var visitors = viewModelVisitors[queryId];
                        if (!visitors) {
                            visitors = [];
                            var factories = viewModelVisitorFactories[queryId];
                            if (factories) {
                                angular.forEach(factories, function (factory, index) {
                                    var visitor = $injector.invoke(factory);
                                    visitors.push(visitor);
                                });

                                viewModelVisitors[queryId] = visitors;
                            }
                        }

                        var deferred = $q.defer();

                        var composedResult = {
                            dataType: 'root'
                        };
                        var promises = [];

                        angular.forEach(handlers, function (handler, index) {

                            var handlerPromise = handler.get(args, composedResult);
                            if (!handlerPromise) {
                                throw 'executeQuery must return a promise.';
                            }

                            handlerPromise
                                .then(function(rawData){
                                    messageBroker.broadcast(queryId + '/retrieved', this, {
                                        rawData: rawData,
                                        composedResult: composedResult
                                    });
                                })
                                .then(function(rawData){
                                    angular.forEach(visitors, function (visitor, index) {
                                        visitor.visit(args, composedResult, rawData);
                                    });
                                });

                            promises.push(handlerPromise);
                        });

                        return $q.all(promises)
                            .then(function (_) {
                                $log.debug(queryId, '-> completed -> ComposedResult: ', composedResult);
                                return composedResult;
                            });
                    };

                    return svc;

                }];

        });
}())