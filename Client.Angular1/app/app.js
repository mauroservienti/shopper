/* global angular */
(function () {

    'use strict';

    angular.module('app.sales', []);
    angular.module('app.marketing', []);
    angular.module('app.customerCare', []);

    angular.module('app.services', []);

    var app = angular.module('app', [
                'ngRoute',
                'ui.router',
                'app.services',
                'app.templates',
                'app.marketing',
                'app.sales',
                'app.customerCare'
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