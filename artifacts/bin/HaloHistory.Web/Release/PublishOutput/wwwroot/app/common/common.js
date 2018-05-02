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
    }]);

    function addspace() {
        return function (input) {
            return input.replace(/([a-z])([A-Z])/g, '$1 $2');
        };
    }

    //goTo.$inject = ['$timeout', 'ScrollTo'];
    //function goTo(ScrollTo) {
    //    return {
    //        restrict: 'A',
    //        link: function (scope, element, attr) {
    //            element.bind("click", function (event) {
    //                ScrollTo.idOrName(attr.scrollTo, attr.offset);
    //            });
    //        }
    //    }
    //}
})();