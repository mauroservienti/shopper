(function () {

    angular.module('app.customers')
        .run(['$log', 'messageBroker', '$http', 'customers.config', function ($log, messageBroker, $http, config) {

            messageBroker.subscribe('orders-list-retrieved', function (sender, args) {

                var groupedByCustomerId = _.groupBy(args.rawData, 'customerId');

                var customerUniqueIds = _.chain(args.rawData)
                    .map(function (rawOrder) { return rawOrder.customerId; })
                    .uniq()
                    .reduce(function (memo, id) { return memo + '|' + id; }, '')
                    .value()
                    .substring(1);

                var uri = config.apiUrl + '/customers/byids/' + customerUniqueIds;
                $http.get(uri)
                     .then(function (response) {

                         $log.debug('HTTP response', response.data);

                         angular.forEach(response.data, function (item, index) {
                             var vm = new CustomerViewModel(item);
                             var orders = groupedByCustomerId[vm.id];

                             $log.debug('Orders for customer', vm.id, orders);

                             angular.forEach(orders, function (order, orderIdx) {
                                 args.viewModels[order.id].customer = vm;
                             });
                         });

                         $log.debug('Orders composed w/ Customers', args.viewModels);

                     });
            });

        }]);
}())