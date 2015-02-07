(function () {
    'use strict';
    var controllerId = 'home';
    angular.module('app').controller(controllerId, home);

    home.$inject = ['$location'];

    function home($location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = controllerId;

        activate();

        function activate() { }
    }
})();
