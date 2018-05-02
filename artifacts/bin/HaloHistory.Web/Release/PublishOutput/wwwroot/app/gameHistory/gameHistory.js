(function () {
    'use strict';

    angular
        .module('cartographer')
        .controller('gameHistory', gameHistory);

    gameHistory.$inject = ['playerData', 'statsDataService','$stateParams'];

    function gameHistory(playerData, statsDataService, $stateParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'gameHistory';
        vm.start = 0;
        vm.getMatchesForGamemode = getMatchesForGamemode;
        vm.next = next;
        vm.prev = prev;

        activate();

        function activate() {
            getMatches();
        }

        function getMatchesForGamemode(gamemode) {
            vm.start = null;
            vm.gameMode = gamemode;
            getMatches();
        }

        function next(start) {
            if (start < 0)
                start = 0;
            vm.start = start;
            getMatches();
        }

        function prev(start) {
            vm.start = start;
            getMatches();
        }

        function getMatches() {

            statsDataService.getMatchesForUser(playerData.gamertag, $stateParams.mode, $stateParams.start)
                .then(function (matches) {
                    vm.matches = matches;
                    vm.start = matches.start;
                })
                .catch(function() {});
        }
    }
})();
