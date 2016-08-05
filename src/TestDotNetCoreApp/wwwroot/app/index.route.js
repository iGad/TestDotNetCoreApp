(function() {
    'use strict';

    angular
      .module('app')
      .config(routerConfig);

    routerConfig.$inject = ['$stateProvider', '$urlRouterProvider'];

    function routerConfig($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/login');
    }
    //angular
    //    .module('app')
    //    .config(function routerConfig($stateProvider, $urlRouterProvider) {
    //        $urlRouterProvider.otherwise('/');

    //        $stateProvider
    //            .state('home', {
    //                url: '/',
                    
    //                templateUrl: 'app/login/login.html',
    //                controller: 'LoginController',
    //                controllerAs: 'loginCtrl'
    //                //,
    //                //    'content@main': {
    //                //        templateUrl: 'app/main/main.html',
    //                //        controller: 'AddressBookController',
    //                //        controllerAs: 'addressbookCtrl'
    //                //    }
    //                //        templateUrl: 'app/main/main.html',
    //                //        controller: 'AddressBookController',
    //                //        controllerAs: 'addressbookCtrl'
    //                //    }
    //            });
    //    });
})();
//    angular
//      .module('app')
//      .config(routerConfig);

//    function routerConfig($stateProvider, $urlRouterProvider) {
//        $urlRouterProvider.otherwise('/');

//        $stateProvider
//            .state('home', {
//                url: '/',
//                templateUrl: 'app/main/main.html',
//                controller: 'AddressBookController',
//                controllerAs: 'addressbookCtrl'
//            });
//    }
//})();