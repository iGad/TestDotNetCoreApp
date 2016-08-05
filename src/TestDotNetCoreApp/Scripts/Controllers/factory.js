(function () {
    'use strict';
    var addressService = angular.module('addressService', ['ngResource']);

    addressService.factory('addressBook',  ['$resource',  
        function($resource)  
        {    
            return $resource('/api/AddressBook/', {}, {  
  
                APIData:  
                {  
                    method: 'GET',  
                    params: {},  
                    isArray: true  
                }  
  
            });    
        }  
    ]);
})();