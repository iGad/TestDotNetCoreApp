(function () {
    'use strict';

    angular
       .module('testdotnetcoreapp')
       .controller('addressBookController', addressBookController);

    addressBookController.$inject = ['$scope', 'student'];

    function addressBookController($scope, addressBook)
    {

        $scope.addressBook = addressBook.APIData();
    }
})();
