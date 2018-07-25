var app = angular.module('Userapp', []);
app.controller('UsersController', ['$scope', '$http', '$timeout', '$window', UsersController]);

// Angularjs Controller
function UsersController($scope, $http, $timeout, $window) {
    // Declare variable
    $scope.loading = true;
    $scope.updateShow = false;
    $scope.addShow = true;
    $scope.responsemsg = "";
    // Get All Employee
    $http.get('/api/Users/').success(function (data) {
        $scope.users = data;
        $scope.responsemsg = "";
    }).error(function () {
        $scope.error = "An Error has occured while loading posts!";
    });

    //Insert Employee
    $scope.add = function () {
        $scope.loading = true;
        $scope.responsemsg = "Please wait...";
        $http.post('/api/Users/', this.newuser).success(function (data) {
            $scope.users = data.emplist;
            $scope.updateShow = false;
            $scope.addShow = true;
            $scope.newuser = '';

            $scope.responsemsg = data.responsemessage;
            $scope.startFade = true;
            $timeout(function () {
                $scope.startFade = false;
                $scope.responsemsg = "";
            }, 4500);

        }).error(function (data) {
            $scope.error = "An Error has occured while Adding employee! " + data;
        });
    }

    //Edit Employee
    $scope.edit = function () {
        console.log(this.user);
        var Id = this.user.UserId;
        $http.get('/api/Users/' + Id).success(function (data) {
            $scope.newuser = data;
            $scope.newuser.PasswordHash = "";
            $scope.updateShow = true;
            $scope.addShow = false;


        }).error(function () {
            $scope.error = "An Error has occured while loading posts!";
        });
    }

    $scope.update = function () {
        $scope.loading = true;
        $scope.responsemsg = "Please wait...";
        $http.put('/api/Users/', this.newuser).success(function (data) {
            $scope.users = data.emplist;
            $scope.updateShow = false;
            $scope.addShow = true;
            $scope.newuser = '';

            $scope.responsemsg = data.responsemessage;
            $scope.startFade = true;
            $timeout(function () {
                $scope.startFade = false;
                $scope.responsemsg = "";
            }, 4500);

        }).error(function (data) {
            $scope.error = "An Error has occured while Saving employee! " + data;
        });
    }

    ////Delete Employee
    $scope.delete = function () {
        var Id = this.user.UserId;
        $scope.loading = true;
        $scope.responsemsg = "Please wait...";
        $http.delete('/api/Users/' + Id).success(function (data) {
            $scope.users = data.emplist;

            $scope.responsemsg = data.responsemessage;
            $scope.startFade = true;
            //$window.location.reload();
            $timeout(function () {
                $scope.startFade = false;
                $scope.responsemsg = "";

            }, 1000);


        }).error(function (data) {
            $scope.error = "An Error has occured while Saving employee! " + data;
        });
    }

    //Delete Employee
    $scope.undelete = function () {
        var Id = this.user.UserId;
        $scope.responsemsg = "Please wait...";
        $scope.loading = true;
        $http.delete('/api/Users/' + Id + "?Undelete=true").success(function (data) {
            $scope.users = data.emplist;
            $scope.responsemsg = data.responsemessage;
            $scope.startFade = true;
            //$window.location.reload();
            $timeout(function () {
                $scope.startFade = false;
                $scope.responsemsg = "";
                //$window.location.reload();

            }, 1000);
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving employee! " + data;
        });
    }


    //Delete Employee
    $scope.Undelete = function () {
        var Id = this.user.UserId;
        $scope.loading = true;
        $http.UnDelete('/api/Users/' + Id).success(function (data) {
            $scope.users = data.emplist;
            $scope.responsemsg = data.responsemessage;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving employee! " + data;
        });
    }

    $scope.openpopup = function () {
        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller: ModalInstanceCtrl,
            resolve: {
                items: function () {
                    return $scope.items;
                },
                selected: function () {
                    return $scope.selected;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            $scope.selected = selectedItem;
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };


    //Cancel Employee
    $scope.cancel = function () {
        $scope.updateShow = false;
        $scope.addShow = true;
        $scope.newuser = '';
    }
}