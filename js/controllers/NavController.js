app.controller('NavController', ['$scope', '$http','$timeout','$location', function ($scope, $http,$timeout,$location) {
    $scope.isActive = function (destination) {
        return destination === $location.path();
    }
}]);