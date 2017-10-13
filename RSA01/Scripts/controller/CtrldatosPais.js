app.controller('CtrldatosPais', ['$scope', 'SendBox', 'ServicioRecibo', function ($scope, SendBox, ServicioRecibo) {

    $scope.eventos = {};
    $scope.listadatospais = new Array();
    $scope.mostrartabla = false;

    //Se obtiene el evento
    ServicioRecibo.ObtenerEventos().then(function (data) {
        $scope.eventos = data;
        $scope.idEvento = data.idevento;

    });

    //Se obtiene el listado de estadisticas por Pais
    $scope.listardatospais = function (eve) {

        ServicioRecibo.ObtenerDatosPais(eve.idevento).then(function (data) {
            $scope.listadatospais = data;
            $scope.mostrartabla = true;
        })
    };
    
}]);