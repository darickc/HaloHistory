'use strict';

var mod = angular.module('cartographer', ['ui.router', 'ngAnimate', 'ngResource', 'ngSanitize', 'angularUtils.directives.dirPagination', 'angular-loading-bar', 'ngScrollTo'])

.config(['$stateProvider', '$urlRouterProvider', '$locationProvider',
    function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $urlRouterProvider.otherwise('/');
        $locationProvider.html5Mode(true).hashPrefix('!');

    }])
.run(function ($rootScope) {

    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        var stateList = toState.name.split('.');
        if (stateList.length)
            $rootScope.containerClass = stateList[stateList.length-1] + "State";
    });

})
;
