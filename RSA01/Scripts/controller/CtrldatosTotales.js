app.controller('CtrldatosTotales', ['$scope', 'SendBox', 'ServicioRecibo', function ($scope, SendBox, ServicioRecibo) {
    
    $scope.eventos = {};
    $scope.listadatostotales = new Array();
    $scope.mostrartabla = false;

    //Se obtiene el evento
    ServicioRecibo.ObtenerEventos().then(function (data) {
        $scope.eventos = data;
        $scope.idEvento = data.idevento;

    });

    //Se obtiene el listado de estadisticas por Concepto
    $scope.listardatostotales = function (eve) {

        ServicioRecibo.ObtenerDatosTotales(eve.idevento).then(function (data) {
            $scope.listadatostotales = data;
            $scope.mostrartabla = true;

        })
    };

}]);