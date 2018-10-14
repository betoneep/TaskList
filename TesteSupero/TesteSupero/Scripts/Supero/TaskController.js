(function () {
    angular.module('App').controller('TaskController', ['$scope', '$http', function ($scope, $http) {
        $scope = $scope || {};
        $scope.taskList = [];
        $scope.request = {};
        $scope.mostrarMesangem = false;
        $scope.mostrarMesangemErro = false;
        $scope.mensagemSucesso = "";
        $scope.mensagemErro = "";

        $scope.salvar = function () {
            if (!validarForm())
                return;
             
            $http({
                url: '/api/task/SalvarTarefa/salvar',
                method: "POST",
                data: $scope.request
            })
                .then(function (response) {
                    //console.log(response.data);
                    init();
                },
                    function (error) {
                        mostrarMensagemErro(error.data.Message);
                    });
        };

        $scope.editar = function (selectedItem) {
            carregaInfoTask(selectedItem);
        };

        $scope.cancelar = function () {
            init();
        };

        $scope.deletar = function () {
            if ($scope.request.Id == "0") {
                mostrarMensagemErro("Selecione uma tarefa para remocao");
                
            } else {
                $http({
                    url: '/api/task/RemoverTarefa/delete',
                    method: "POST",
                    data: $scope.request.Id
                })
                    .then(function (response) {
                        console.log(response.data);
                        
                        $scope.mostrarMesangem = true;
                        $scope.mensagemSucesso = response.data;
                        setTimeout(function () {
                            $scope.mostrarMesangem = false;
                            $scope.mensagemSucesso = "";
                            $scope.$apply();
                        }, 3000);

                        init();
                    },
                        function (error) {
                            console.log(error);
                            mostrarMensagemErro(error.data.Message);
       
                        });
            }
        };

        function validarForm() {
            if ($scope.request.Titulo && $scope.request.Descricao && $scope.request.DataConclusao != "")
                return true;

            return false;
            //$scope.request.DataConclusao = "";
        };

        function mostrarMensagemErro(msg) {
            $scope.mostrarMesangemErro = true;
            $scope.mensagemErro = msg || "Erro";

            setTimeout(function () {
                $scope.mostrarMesangemErro = false;
                $scope.mensagemErro = "";
                $scope.$apply();
            }, 10000);
        };

        function obterTaskList() {
            $http({
                method: 'GET',
                url: '/api/task/GetTarefas'
            }).then(function successCallback(response) {
                $scope.taskList = response.data;
                ; 
            }, function errorCallback(error) {
                mostrarMensagemErro(error.data.Message);
            });
        };

        function carregaInfoTask(task) {
            console.log(task);
            $scope.request.Id = task.Id;
            $scope.request.Titulo = task.Titulo;
            $scope.request.Descricao = task.Descricao;
            $scope.request.Status = task.Status.toString();
            $scope.request.DescricaoStatus = task.DescricaoStatus;
            $scope.request.DataCriacao = task.DataCriacao;
            $scope.request.DataConclusao = new Date(task.DataConclusao);

            console.log('Tarefa selecionada ' + $scope.request.Id);
        }

        function init() {
            $scope.request.Id = 0;
            $scope.request.Titulo = "";
            $scope.request.Descricao = "";
            $scope.request.Status = "3";
            $scope.request.DescricaoStatus = "Novo";
            $scope.request.DataCriacao = "";
            $scope.request.DataConclusao = "";

            obterTaskList();
        };

        init();
    }]);
})();