'use strict';

var mod = angular.module('cartographer', ['ui.router', 'ngAnimate', 'ngResource', 'ngSanitize', 'angular-loading-bar', 'ngScrollTo'])

.config(['$stateProvider', '$urlRouterProvider', '$locationProvider',
    function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $urlRouterProvider.otherwise('/');
        $locationProvider.html5Mode(true).hashPrefix('!');

    }])
;
