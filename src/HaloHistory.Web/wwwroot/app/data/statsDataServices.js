(function () {
    'use strict';

    angular
        .module('cartographer')
        .factory('statsDataService', statsDataService);

    statsDataService.$inject = ['$q', '$resource'];

    function statsDataService($q, $resource) {
        var service = $resource('api/stats/matches/:id/:gameMode?start=:start', {}, {
            match: { url: 'api/stats/matches/:gamerTag/:gameMode/:id', isArray: false }
        });

        return {
            getMatchesForUser: getMatchesForUser,
            getMatch: getMatch
        };


        function getMatchesForUser(gamerTag, gameMode,start) {
            return service.get({ id: gamerTag, gameMode: gameMode,start:start }).$promise;
        }

        function getMatch(gamerTag, gameMode, id) {
            return service.match({gamerTag:gamerTag, gameMode:gameMode, id: id }).$promise;
        }


    }
})();