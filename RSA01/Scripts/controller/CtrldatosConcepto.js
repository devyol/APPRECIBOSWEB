app.controller('CtrldatosConcepto', ['$scope', 'SendBox', 'ServicioRecibo', function ($scope, SendBox, ServicioRecibo) {

    $scope.eventos = {};
    $scope.listadatosconcepto = new Array();
    $scope.mostrartabla = false;

    //Se obtiene el evento
    ServicioRecibo.ObtenerEventos().then(function (data) {
        $scope.eventos = data;
        $scope.idEvento = data.idevento;

    });

    //Se obtiene el listado de estadisticas por Concepto
    $scope.listardatosconcepto = function (eve) {

        ServicioRecibo.ObtenerDatosConcepto(eve.idevento).then(function (data) {
            $scope.listadatosconcepto = data;
            $scope.mostrartabla = true;

        })
    };
}]);