(function () {
    'use strict';

    var app = angular.module('app');

    app.constant('routes', getRoutes());
    app.config(['$routeProvider', 'routes', routeConfigurator]);
    function routeConfigurator($routeProvider, routes) {
        routes.forEach(function (r) {
            $routeProvider.when(r.url, r.config);
        });
        $routeProvider.otherwise({ redirectTo: '/' });
    }
    function getRoutes() {
        return [
            {
                url: '/',
                config: {
                    templateUrl: 'app/home/home.html',
                    title: 'home',
                    settings: {
                        nav: 3
                    }
                }
            }, {
                url: '/start',
                config: {
                    title: 'start',
                    templateUrl: 'app/start/start.html',
                    settings: {
                        nav: 2,
                    }
                }
            }
        ];
    }
})();