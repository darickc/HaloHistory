(function () {
    'use strict';

    angular
        .module('cartographer')
        .controller('match', match);

    match.$inject = ['statsDataService', '$stateParams', 'profileDataService'];

    function match(statsDataService, $stateParams, profileDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.selectedPlayer = null;
        vm.showDetails = showDetails;

        activate();

        function activate() {
            statsDataService.getMatch($stateParams.gamerTag, $stateParams.gameMode, $stateParams.id)
               .then(function (match) {
                   vm.match = match;

                   angular.forEach(match.teams, function (team) {
                       angular.forEach(team.players, function (player) {
                           getPlayerProfile(player);
                           angular.forEach(player.enemies, function (enemy) {
                               if (enemy.isPlayer) {
                                   getEnemyProfile(enemy);
                               }
                           });
                       });
                   });
               })
               .catch(function () {
               });
        }

        function showDetails(player) {
            if (vm.selectedPlayer != null) {
                vm.selectedPlayer.showDetails = false;
                if (vm.selectedPlayer.gamerTag === player.gamerTag) {
                    vm.selectedPlayer = null;
                    return;
                } 
            }
            vm.selectedPlayer = player;
            vm.selectedPlayer.showDetails = true;
        }

        function getPlayerProfile(player) {
            profileDataService.getSpartanImageForUser(player.gamerTag)
                .then(function(profile) {
                    player.profile = profile;
                }).catch();
        }

        function getEnemyProfile(enemy) {
            profileDataService.getSpartanImageForUser(enemy.name)
                .then(function (profile) {
                    enemy.profile = profile;
                }).catch();
        }
    }
})();
