app.controller('CtrlInfoDetalle', ['$scope', 'SendBox', 'ServicioRecibo','$window', function ($scope, SendBox, ServicioRecibo,$window) {

    $scope.eventos = {};
    $scope.mostrarboton = false;

    //Se obtiene el evento
    ServicioRecibo.ObtenerEventos().then(function (data) {
        $scope.eventos = data;
        $scope.idEvento = data.idevento;

    });

    //Mostrar Boton
    $scope.MostrarBoton = function () {
        $scope.mostrarboton = true;
    }

    $scope.reporteDetallado = function (objEve) {
        $window.open("http://localhost:19190/ImpresionRecibo/reporteDetallado?idevento=" + objEve.idevento);
        //$window.open("http://www.apprecibos.com/ImpresionRecibo/reporteDetallado?idevento=" + objEve.idevento);
    };

}]);