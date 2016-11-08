/* global angular */
(function () {

    'use strict';

    angular.module('app.customers', []);
    angular.module('app.finance', []);

    angular.module('app.sales', []);
    angular.module('app.home', []);
    angular.module('app.marketing', []);

    angular.module('app.controllers', []);
    angular.module('app.services', []);

    var app = angular.module('app', [
                'ngRoute',
                'ui.router',
                'app.services',
                'app.templates',
                'app.home',
                'app.marketing',
                'app.sales' //,
                // 'app.customers',
                // 'app.finance',
                // 'app.sales'
    ]);

    app.config(['$locationProvider', '$logProvider',
        function ($locationProvider, $logProvider ) {

                $logProvider.debugEnabled(true);
                $locationProvider.html5Mode(false);
                
                console.debug('app config.');
        }]);

    app.run(['$log', '$rootScope',
        function ($log, $rootScope) {
            $rootScope.$log = $log;
            
            console.debug('app run.');
        }]);

}())