(function () {


    angular.module('App').controller('TaskController', ['$scope', '$http', function ($scope, $http) {
        $scope = $scope || {};
        $scope.taskList = [];
        $scope.request = {};
        
        
        $scope.salvar = function () {

            $http({
                url: '/api/task/salvar',
                method: "POST",
                data:  $scope.request 
            })
                .then(function (response) {
                    console.log(response.data);
                    obterTaskList();
                },
                function (error) { // optional
                    alert(error);
                    });
        };

        function obterTaskList() {
            $http({
                method: 'GET',
                url: '/api/task/GetTarefas'
            }).then(function successCallback(response) {
                $scope.taskList = response.data;
                }, function errorCallback(error) {
                    alert(error);
            });
        };


        function init() {
            $scope.request.Titulo = "";
            $scope.request.Descricao = "";
            $scope.request.Status = "3";
            $scope.request.DescricaoStatus = "Novo";
            $scope.request.DataCriacao = "";

            obterTaskList();

        };

        init();
    }]);
})();