(function () {
    'use strict';

    angular
        .module('app')
        .factory('mainQuest', mainQuestService);

    mainQuestService.$inject = ['$resource'];

    function mainQuestService($resource) {
        var service = {
            submitAnswer: submitAnswer,
            Statistics: getStatistics
        };

        return service;

        function submitAnswer() {
            return $resource('api/quest');
        }

        function getStatistics() {
            return $resource('api/statistics');
        }
    }
})();