(function () {
    'use strict';

    angular
        .module('cartographer')
        .factory('gameHistoryService', gameHistoryService);

    gameHistoryService.$inject = [];

    function gameHistoryService() {
        var service = {
            //getData: getData
        };

        return service;

        //function getData() { }
    }
})();