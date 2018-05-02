(function () {
    'use strict';

    angular
        .module('cartographer')
        .controller('gameHistory', gameHistory);

    gameHistory.$inject = ['playerData', 'statsDataService', '$stateParams', 'gameHistoryService', '$state'];

    function gameHistory(playerData, statsDataService, $stateParams, gameHistoryService, $state) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'gameHistory';
        vm.start = 0;
        vm.mode = $stateParams.mode;
        vm.getMatchesForGamemode = getMatchesForGamemode;
        vm.next = next;
        vm.prev = prev;
        vm.goToMatch = goToMatch;

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
                .catch(function () { });
        }

        function goToMatch(match) {
            //.match({gameMode:match.gameMode,id:match.matchId})    
            //gameHistoryService.matchId = match.matchId;
            $state.go(".match", { gameMode: match.gameMode, id: match.matchId });
        }
    }
})();
