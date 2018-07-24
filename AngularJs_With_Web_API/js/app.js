var app = angular.module('HomeApp', ['ui.bootstrap']);
app.controller('HomeController', ['$scope', '$http', HomeController]);

// Angularjs Controller
function HomeController($scope, $http) {
    // Declare variable
    // Get All Employee
    $http.get('/api/EmployeeAPI/').success(function (data) {
        $scope.employees = data;        
    }).error(function () {
        $scope.error = "An Error has occured while loading posts!";
    });


    
}