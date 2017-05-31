var app = angular.module('ReunionApp', ['ngRoute', 'ngAnimate','ui.bootstrap']);

app.config(function ($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl: "views/home.html",
        controller: 'HomeController'
    })
    .when("/registered", {
        templateUrl: "views/registered.html",
        controller: 'RegisteredController'
    })
    .when("/register", {
        templateUrl: "views/register.html",
        controller: 'RegistrationController'
    })
    .otherwise({redirectTo:'/'})
});

app.filter('proper', function () {
    return function (input) {
        return (!!input) ? input.charAt(0).toUpperCase() + input.substr(1).toLowerCase() : '';
    }
});