app.controller('UserCtrl', ['$scope', '$modal', '$state', 'dataService', 'util', 'storage', function ($scope, $modal, $state, dataService, util, storage) {

    $scope.model = {
        userName: null,
        password: null,
        errMessage: null,
        old_password: null,
        new_password: null
    };

    $scope.login = function () {
        if ($scope.model.userName && $scope.model.password) {
            dataService.login($scope.model.userName, $scope.model.password).then(function (result) {
                if (result.data && result.data.token) {
                    storage.setTokenAndUser(result.data.token, result.data.user);
                    $state.go('app.request');
                } else if (result.data) {
                    $scope.model.errMessage = result.data.message;
                    if ($scope.autologin) {
                        var ele = document.getElementsByTagName('h1')[0];
                        if (ele) {
                            ele.innerText = '自动登录失败: ' + result.data.message;
                        }
                    }
                }
            });
        }
    };

    $scope.changepwd = function () {
        var user = storage.getUser();
        dataService.changepwd(user.id, $scope.model.old_password, $scope.model.new_password).then(function (data) {
            $state.go('app.request');
        });
    }

    if (sessionStorage.autologin) {
        $scope.autologin = true;
        $scope.model.userName = sessionStorage.getItem('userName');
        $scope.model.password = sessionStorage.getItem('password');
        sessionStorage.removeItem('userName');
        sessionStorage.removeItem('password');
        sessionStorage.removeItem('autologin');
        $scope.login();
    }
}]);