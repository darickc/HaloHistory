(function () {
    'use strict';

    angular
        .module('cartographer')
        .config(['$stateProvider',
            function ($stateProvider) {
                $stateProvider
                    .state('main', {
                        url: '/',
                        templateUrl: 'app/main/main.html',
                        controller: 'main as vm',
                        params: {
                            error: null
                        }
                    })
                    .state('main.player', {
                        url: '{gamerTag}/',
                        templateUrl: 'app/player/player.html',
                        controller: 'player as vm',
                        resolve: {
                            playerData: function (profileDataService, $stateParams) {
                                return profileDataService.getSpartanImageForUser($stateParams.gamerTag);
                            }
                        }
                    })
                    .state('main.player.gameHistory', {
                        url: 'game-history/{mode}/?start',
                        templateUrl: 'app/gameHistory/gameHistory.html',
                        controller: 'gameHistory as vm'
                    })
                    .state('main.player.gameHistory.match', {
                        url: 'match/{gameMode}/{id}/',
                        templateUrl: 'app/match/match.html',
                        controller: 'match as vm'
                    })
                    //.state('main.player', {
                    //    url: '{gamerTag}/',
                    //    views: {
                    //        'player': {
                    //            templateUrl: 'app/player/player.html',
                    //            controller: 'player as vm'
                    //        },
                    //        'matches': {
                    //            templateUrl: 'app/matches/matches.html',
                    //            controller: 'matches as vm'
                    //        }
                    //    },
                    //    resolve: {
                    //        playerData: function (profileDataService, $stateParams) {
                    //            return profileDataService.getSpartanImageForUser($stateParams.gamerTag);
                    //        }
                    //    }
                    //})
                    //.state('main.player.match', {
                    //    url: '{gameMode}/{id}/',
                    //    templateUrl: 'app/match/match.html',
                    //    controller: 'match as vm'
                    //})
                ;
            }
    ]);


})();
