(function () {
    'use strict';

    angular
        .module('cartographer')
        .controller('player', player);

    player.$inject = ['playerData','$state'];

    function player(playerData, $state) {
        /* jshint validthis:true */
        var vm = this;
        vm.change = change;
        vm.player = playerData;

        activate();

        function activate() {
            if (!playerData.gamertag) {
                localStorage.removeItem("gamerTag");
                $state.go('main',{error:true});
            }
        }

        function change() {
            localStorage.removeItem("gamerTag");
            $state.go('main');
        }
    }
})();
