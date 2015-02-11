(function () {
    'use strict';
    var controllerId = 'start';
    angular.module('app').controller(controllerId, ['$cookies', 'mainQuest', start]);

    function start($cookies, mainQuestSvc) {
        var vm = this;
        vm.hints = "";
        vm.answer = "";
        vm.lastTask = false;
        vm.userToken = "";
        vm.watched = "?";
        vm.done = "?";

        // set random color to hint
        var refreshHintStyle = function () {
            var letters = '0123456789ABC'.split('');
            var color = '#';
            for (var i = 0; i < 6; i++) {
                color += letters[Math.floor(Math.random() * 13)];
            }
            vm.hintColor = color;
        };

        function activate() {
            var tokenCookie = $cookies.token;
            if (tokenCookie) {
                vm.userToken = tokenCookie;
            }

            mainQuestSvc.Statistics().get({ token: vm.userToken }, function (result) {
                if (result && result.ok === "OK") {
                    vm.watched = result.watched;
                    vm.done = result.done;
                }
            });

            mainQuestSvc.submitAnswer().get({ token: vm.userToken, taskNumberString: "-1" }, function (result) {
                if (result) {
                    if (!result.task) {
                        vm.lastTask = true;
                        return;
                    }
                    vm.taskId = result.task.number;
                    vm.taskTitle = result.task.title;
                    vm.taskContent = result.task.content;
                    vm.answer = "";
                    $cookies.token = result.token;
                    vm.userToken = result.token;
                }
            });
        };

        activate();

        vm.startAgain = function () {
            mainQuestSvc.submitAnswer().save({ token: vm.userToken, startAgaing: 'true' });
            $cookies.token = null;
            vm.userToken = null;
            vm.taskId = 1;
            vm.taskTitle = "Первый";
            vm.taskContent = "Какой уровень следующий?";
            vm.hints = "";
            vm.answer = "";
            vm.lastTask = false;
            vm.userToken = "";

            mainQuestSvc.Statistics().get({ token: vm.userToken }, function (result) {
                if (result && result.ok === "OK") {
                    vm.watched = result.watched;
                    vm.done = result.done;
                } else {
                    alert('error');
                }
            });
        }

        vm.submit = function () {
            mainQuestSvc.submitAnswer().get({ token: vm.userToken, taskNumberString: vm.taskId, answer: vm.answer }, function (result) {
                if (result) {
                    if (result.isAnswerRight) {
                        if (!result.task) {
                            vm.lastTask = true;
                            return;
                        }
                        vm.taskId = result.task.number;
                        vm.taskTitle = result.task.title;
                        vm.taskContent = result.task.content;

                        mainQuestSvc.Statistics().get({ token: vm.userToken }, function (result) {
                            if (result && result.ok === "OK") {
                                vm.watched = result.watched;
                                vm.done = result.done;
                            } else {
                                alert('error');
                            }
                        });
                    } else {
                        vm.hints = "Ответ не верный.";
                        refreshHintStyle();
                    }
                    vm.answer = "";
                }
            });
        }
    }
})();