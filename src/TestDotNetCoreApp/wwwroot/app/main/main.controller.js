(function () {
    'use strict';

    angular
        .module('app')
        .controller('AddressBookController', AddressBookController);

    AddressBookController.$inject = ['$http', '$http'];

    function AddressBookController($scope, $http) {
        var vm = this;

        vm.records = [];

        $http.get("api/addressbook/getAll").then(function(response) {
            vm.records = response.data;
        });
    }
})();
