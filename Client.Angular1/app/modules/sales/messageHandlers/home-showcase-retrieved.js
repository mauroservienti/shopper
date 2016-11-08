/* global angular */
(function () {

    angular.module('app.sales')
        .run(['$log', 'messageBroker', '$http', 'sales.config', function ($log, messageBroker, $http, config) {

            messageBroker.subscribe('home-showcase/retrieved', function (sender, args) {

                var showcaseIds = [];
                showcaseIds.push(args.viewModel.headlineProduct.stockItemId);
                angular.forEach(args.viewModel.showcaseProducts, function(value, key){
                    showcaseIds.push(value.stockItemId);
                });

                $log.debug('showcaseIds', showcaseIds);

                var uri = config.apiUrl + '/SellingPrices/ByStockItem?ids=' + showcaseIds;
                $log.debug('showcaseIds - URL', uri);

                $http.get(uri)
                     .then(function (response) {

                         $log.debug('HTTP response', response.data);
                         var _headline = response.data[0];
                         var _showcases = response.data.slice(1);

                        args.viewModel.headlineProduct.price = _headline;
                        angular.forEach(_showcases, function(value, key){
                            var vm = _.findWhere(args.viewModel.showcaseProducts, { 
                                stockItemId: value.stockItemId
                            }); 
                            vm.price = value;
                        });


                        $log.debug('Sales - home-showcase/retrieved - composed', args.viewModel);
                     });
            });

        }]);
}())