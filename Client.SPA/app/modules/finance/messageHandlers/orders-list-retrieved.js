(function () {

    angular.module('app.finance')
        .run(['$log', 'messageBroker', '$http', 'finance.config', function ($log, messageBroker, $http, config) {

            messageBroker.subscribe('orders-list-retrieved', function (sender, args) {

                angular.forEach(args.rawData, function (order, index) {

                    var uri = config.apiUrl + '/prices/total/' + order.productIds;

                    $http.get(uri)
                         .then(function (response) {

                             $log.debug('Total price HTTP response', response.data);

                             var vm = new PriceViewModel(response.data);
                             args.viewModels[order.id].price = vm;

                             $log.debug('Orders composed w/ Prices', args.viewModels);
                         });

                });
            });

        }]);
}())