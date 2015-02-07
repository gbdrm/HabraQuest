(function () {
    'use strict';

    angular
        .module('app')
        .factory('mainQuest', mainQuestService);

    mainQuestService.$inject = ['$resource'];

    function mainQuestService($resource) {
        var service = {
            submitAnswer: submitAnswer
        };

        return service;

        function submitAnswer() {
            var requestUri = 'api/mainQuest';
            return $resource(requestUri);
        }
    }
})();