app.controller('RegistrationController', ['$scope', '$http','$timeout', function ($scope, $http,$timeout) {
    $scope.firstName = "";
    $scope.lastName = "";
    $scope.emailAddress = "";
    $scope.helpPlanning = "";
    $scope.phoneNumber = "";
    $scope.jsonData = {};
    $scope.Message = "";
    $scope.MessageTitle = "";
    function capitalizeFirstLetter(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }
    // Bools for Alerts
    $scope.Loading = false;
    $scope.FormSubmitted = false;
    $scope.Error = false;



    $scope.SubmitForm = function () {
        console.log($scope.userForm.emailAddress.$pristine);
        
        if ($scope.userForm.$valid) {
            $scope.Loading = true;
            console.log("Submitting");
            $scope.jsonData = {
                firstName: $scope.firstName,
                lastName: $scope.lastName,
                emailAddress: $scope.emailAddress,
                phoneNumber: $scope.phoneNumber,
                helpPlanning: $scope.helpPlanning
            }
            $http.post('https://cvhsreunion.azurewebsites.net/api/alumni', $scope.jsonData).then(function (data) {
                $scope.Loading = false;
                console.log(data);
                if (data.data == "") {
                    $scope.firstName = $scope.firstName.toLowerCase();
                    $scope.lastName = $scope.lastName.toLowerCase();
                    $scope.Message = "Thank you " + capitalizeFirstLetter($scope.firstName) + " " + capitalizeFirstLetter($scope.lastName) + ", but " + "it looks like you have already been added to the list.";
                    $scope.MessageTitle = "Success!";
                } else {
                    $scope.Message = "Thank you " + capitalizeFirstLetter(data.data.firstName) + " " + capitalizeFirstLetter(data.data.lastName) + ". " + "You have been added to the list";
                    $scope.MessageTitle = "Success!";
                }
                
                $scope.FormSubmitted = true;
                $timeout(function () {
                    $scope.Message = "";
                    $scope.MessageTitle = "";
                    $scope.FormSubmitted = false;
                    $scope.emailAddress = "";
                    $scope.firstName = "";
                    $scope.lastName = "";
                    $scope.helpPlanning = "";
                    $scope.phoneNumber = "";
                    $scope.userForm.$setPristine();
                }, 3000);

            }, function (error) {
                $scope.Loading = false;
                console.log("Damn these errors");
                console.log(error);
                $scope.Message = "Looks like Sparky caused more damage then we previously thought, please bare with us while we fix things";
                $scope.MessageTitle="Squirrel Evoked Damages"
            })
        } else {
            $scope.Loading = false;
            console.log(userForm);
            $scope.Message = "Oops, Looks like you've missed something";
            $scope.MessageTitle = "Incomplete Information";
            $scope.userForm.firstName.$setDirty();
            $scope.userForm.lastName.$setDirty();
            $scope.userForm.emailAddress.$setDirty();
            $scope.userForm.helpPlanning.$setDirty();
            $scope.userForm.phoneNumber.$setDirty();
            $scope.Error = true;
            $timeout(function () {
                $scope.Message = "";
                $scope.MessageTitle ="";
                $scope.Error = false;
            }, 3000);
        }
    }
}]);