(function () {
    'use strict';

    angular.module('cartographer')
        .filter('addspace', addspace)
    .directive('goTo', ['ScrollTo', '$timeout', function (ScrollTo, $timeout) {
        return {
            restrict: "AC",
            compile: function () {

                return function (scope, element, attr) {
                    element.bind("click", function (event) {
                        $timeout(function() {
                            ScrollTo.idOrName(attr.goTo, attr.offset);
                        }, 50);
                        //ScrollTo.idOrName(attr.goTo, attr.offset);
                    });
                };
            }
        };
    }])
    .directive('repeatDone', ['gameHistoryService', '$timeout', function (gameHistoryService, $timeout) {
        return function (scope, element, attrs) {
            if (scope.$last) { // all are rendered
                //scope.$eval(attrs.repeatDone);
                if (gameHistoryService.scrollPosition) {
                    $timeout(function () {
                        $('.cartographer').scrollTop(gameHistoryService.scrollPosition);
                    }, 100);
                }
            }
        }
    }])
    .directive('saveScroll', ['$rootScope', 'gameHistoryService', function ($rootScope, gameHistoryService) {
        return function (scope, element, attrs) {
            $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
                //alert($('.cartographer').scrollTop());
                if (fromState.name === attrs.saveScroll) {
                    gameHistoryService.scrollPosition = $('.cartographer').scrollTop();
                }
            });
        }
    }]);

    function addspace() {
        return function (input) {
            return input.replace(/([a-z])([A-Z])/g, '$1 $2');
        };
    }
})();