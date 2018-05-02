(function () {
    'use strict';

    angular
        .module('cartographer')
        .controller('main', main);

    main.$inject = ['$state','$stateParams'];

    function main($state, $stateParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'main';
        vm.setUser = setUser;
        vm.$state = $state;

        activate();

        function activate() {
            vm.canRemember = typeof (Storage) !== "undefined";
            vm.error = $stateParams.error;
            if (vm.canRemember && $state.is('main')) {
                var gamerTag = localStorage.getItem("gamerTag");
                if (gamerTag) {
                    $state.go("main.player.gameHistory", { gamerTag: gamerTag,mode:"All" });
                }
            }
        }

        function setUser(form) {
            if (!form.$valid) {
                form.submitted = true;
                return;
            }

            if (vm.rememberMe) {
                localStorage.setItem("gamerTag", vm.gamerTag);
            }
            $state.go("main.player.gameHistory", { gamerTag: vm.gamerTag, mode: "All" });
        }
    }
})();
