/* global angular */
(function () {

    angular.module('app.marketing')
        .config(['backendCompositionServiceProvider',
        function (backendCompositionServiceProvider) {
                
                var homeShowcaseQueryId = 'home-showcase';
                backendCompositionServiceProvider.registerViewModelVisitorFactory(homeShowcaseQueryId,
                    ['$log', '$http', 'marketing.config', function ($log, $http, config) {

                        var visitor = {
                            get: function (args, composedResults, rawData) {

                                $log.debug('Ready to visit ', homeShowcaseQueryId, ': ', args, composedResults, rawData);

                                var showcaseIds = [];
                                showcaseIds.push(args.viewModel.headlineProduct.stockItemId);
                                angular.forEach(args.viewModel.showcaseProducts, function(value, key){
                                    showcaseIds.push(value.stockItemId);
                                });

                                var uri = config.apiUrl + '/ProductDescriptions/ByStockItem?ids=' + showcaseIds;
                                return $http.get(uri)
                                     .then(function (response) {

                                         $log.debug('HTTP response', response.data);
                                         var _headline = response.data[0];
                                         var _showcases = response.data.slice(1);

                                        args.viewModel.headlineProduct.description = _headline;
                                        angular.forEach(_showcases, function(value, key){
                                            var vm = _.findWhere(args.viewModel.showcaseProducts, { 
                                                stockItemId: value.stockItemId
                                            }); 
                                            vm.description = value;
                                        });


                                        $log.debug('Marketing - home-showcase/retrieved - composed', args.viewModel);
                                     });

                            }
                        }

                        return visitor;
                    }]);
        }]);



        // .run(['$log', 'backendCompositionService', '$http', 'marketing.config', function ($log, backendCompositionService, $http, config) {

        //     messageBroker.subscribe('home-showcase/retrieved', function (sender, args) {

        //         var showcaseIds = [];
        //         showcaseIds.push(args.viewModel.headlineProduct.stockItemId);
        //         angular.forEach(args.viewModel.showcaseProducts, function(value, key){
        //             showcaseIds.push(value.stockItemId);
        //         });

        //         $log.debug('showcaseIds', showcaseIds);

        //         var uri = config.apiUrl + '/ProductDescriptions/ByStockItem?ids=' + showcaseIds;
        //         $log.debug('showcaseIds - URL', uri);

        //         $http.get(uri)
        //              .then(function (response) {

        //                  $log.debug('HTTP response', response.data);
        //                  var _headline = response.data[0];
        //                  var _showcases = response.data.slice(1);

        //                 args.viewModel.headlineProduct.description = _headline;
        //                 angular.forEach(_showcases, function(value, key){
        //                     var vm = _.findWhere(args.viewModel.showcaseProducts, { 
        //                         stockItemId: value.stockItemId
        //                     }); 
        //                     vm.description = value;
        //                 });


        //                 $log.debug('Marketing - home-showcase/retrieved - composed', args.viewModel);
        //              });
        //     });

        // }]);
}())