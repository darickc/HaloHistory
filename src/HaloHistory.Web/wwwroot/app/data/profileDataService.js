(function () {
    'use strict';

    angular
        .module('cartographer')
        .factory('profileDataService', profileDataService);

    profileDataService.$inject = ['$q', '$resource'];

    function profileDataService($q, $resource) {
        var self = this;
        self.profiles = [];

        var service = $resource('api/profile/:id', {}, {

        });

        return {
            getSpartanImageForUser: getSpartanImageForUser,
            // getUserByInumber: getUserByInumber
        };


        function getSpartanImageForUser(gamerTag) {
            if (self.profiles[gamerTag]) {
                var deferred = $q.defer();
                deferred.resolve(self.profiles[gamerTag]);
                return deferred.promise;
            } else {
                self.profiles[gamerTag] = service.get({ id: gamerTag });
                return self.profiles[gamerTag].$promise;
            }

            //return service.get({ id: gamerTag }).$promise;
        }


    }
})();