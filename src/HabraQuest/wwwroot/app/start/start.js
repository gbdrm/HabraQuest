(function () {
    'use strict';
    var controllerId = 'start';
    angular.module('app').controller(controllerId, ['mainQuest', home]);

    function home(mainQuestSvc) {
        var vm = this;

        vm.taskId = 1;
        vm.taskTitle = "Первый";
        vm.taskContent = "Какой уровень следующий?";
        vm.hints = "";
        vm.answer = "";
        vm.lastTask = false;

        function activate() {
            
        };

        activate();

        vm.submit = function () {
            mainQuestSvc.submitAnswer().get({ taskId: vm.taskId, answer: vm.answer }, function (result) {
                if (result) {
                    if (result.isAnswerRight) {
                        if (!result.task) {
                            vm.lastTask = true;
                            return;
                        }
                        vm.taskId = result.task.id;
                        vm.taskTitle = result.task.title;
                        vm.taskContent = result.task.content;
                    } else {
                        vm.hints = "неа.";
                    }
                    vm.answer = "";
                } else {
                    alert('error');
                }
            });
        }
    }
})();
