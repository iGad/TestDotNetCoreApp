(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$scope', '$http', '$state', 'auth0Service'];

    function LoginController($scope, $http, $state, auth0Service) {
        var vm = this;

        vm.hideButton = true;

        vm.loginInfo = {
            UserName: "",
            Password: ""
        };

        vm.login = function () {
            auth0Service.login(vm.loginInfo, function (response) {
                if (response.data == "OK") {
                    auth0Service.authenticate();
                    $state.go('main');
                } else {
                    $scope.validationErrors = [];
                    if (response.data && angular.isObject(response.data)) {
                        for (var key in response.data) {
                            $scope.validationErrors.push(response.data[key][0]);
                        }
                    }
                }
            });
        }

        vm.updateHideButton = function () {
            vm.hideButton = (vm.loginInfo.UserName == "" || vm.loginInfo.UserName == undefined) && (vm.loginInfo.Password == "" || vm.loginInfo.Password == undefined);
        }
    }
})();
