app.controller('RegisteredController', ['$scope', '$http', '$timeout', function ($scope, $http, $timeout) {
    $scope.Loading = true;
    $http.get("/api/alumni").then(function (data) {
        $scope.Loading = false;
        $scope.users = data.data;
        console.log(data);
        
    })
}]);